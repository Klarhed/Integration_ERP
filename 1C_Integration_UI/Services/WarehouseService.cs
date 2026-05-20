using _1C_Integration_UI.Data;
using _1C_Integration_UI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _1C_Integration_UI.Services
{
    public class WarehouseService
    {
        private readonly WarehouseContext _context;
        public WarehouseService(WarehouseContext context)
        {
            _context = context;
        }

        public async Task SaveInvoiceWithRelationsAsync(int? invoiceId, string invoiceNumber, DateTime invoiceDate, int warehouseId, int counterpartyId, List<InvoiceItemsDto> invoiceItems)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Invoice? invoice;
                if (invoiceId.HasValue && invoiceId.Value > 0)
                {
                    invoice = await _context.Invoices
                        .Include(i => i.Items)
                        .FirstOrDefaultAsync(i => i.Id == invoiceId.Value);
                    if (invoice == null)
                    {
                        throw new Exception($"Инвойс с ID {invoiceId.Value} не найден для обновления.");
                    }
                    invoice.Number = invoiceNumber;
                    invoice.Date = invoiceDate;
                    invoice.WarehouseId = warehouseId;
                    invoice.CounterpartyId = counterpartyId;

                    _context.InvoiceItems.RemoveRange(invoice.Items);
                    invoice.Items.Clear();
                }
                else
                {
                    invoice = new Invoice
                    {
                        Number = invoiceNumber,
                        Date = invoiceDate,
                        WarehouseId = warehouseId,
                        CounterpartyId = counterpartyId,
                        Items = new List<InvoiceItem>()
                    };
                    _context.Invoices.Add(invoice);
                }
                ;

                var uniqueArticles = invoiceItems.Select(i => i.Article).Distinct().ToList();
                var existingProducts = await _context.Products
                    .Where(p => uniqueArticles.Contains(p.Article))
                    .ToDictionaryAsync(p => p.Article);

                foreach (var itemDto in invoiceItems)
                {
                    if (!existingProducts.TryGetValue(itemDto.Article, out var product))
                    {
                        product = new Product
                        {
                            Article = itemDto.Article,
                            Name = itemDto.Name,
                            BasePrice = itemDto.Price
                        };
                        _context.Products.Add(product);
                        existingProducts.Add(itemDto.Article, product);
                    }

                    var invoiceItem = new InvoiceItem
                    {
                        Invoice = invoice,
                        Product = product,
                        Quantity = itemDto.Quantity,
                        UnitPrice = itemDto.Price,
                        VatRate = itemDto.VatRate
                    };
                    _context.InvoiceItems.Add(invoiceItem);
                }
                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Invoices
                .OrderByDescending(i => i.Date)
                .ToListAsync();
        }

        public async Task<List<InvoiceItemsDto>> GetInvoiceByIDAsync(int invoiceId)
        {
            return await _context.InvoiceItems
                .Where(ii => ii.InvoiceId == invoiceId)
                .Select(ii => new InvoiceItemsDto
                {
                    Article = ii.Product.Article,
                    Name = ii.Product.Name,
                    Quantity = ii.Quantity,
                    Price = ii.UnitPrice,
                    VatRate = ii.VatRate
                })
                .ToListAsync();
        }

        public class InvoiceItemsDto
        {
            [JsonPropertyName("ref")]
            public string Article { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("qty")]
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            [JsonPropertyName("vat_rate")]
            public decimal VatRate { get; set; }
        }

        public async Task<List<Warehouse>> GetWarehousAsync()
        {
            return await _context.Warehouses
                .OrderBy(w => w.Name)
                .ToListAsync();
        }

        public async Task<List<WarehouseDto>> GetWarehousByIdAsync(int warehouseId) 
        {
            return await _context.Warehouses
                .Where(w => w.Id == warehouseId)
                .Select(w => new WarehouseDto
                {
                    Code = w.Code,
                    Name = w.Name
                })
                .ToListAsync();
        }

        public class WarehouseDto
        {
            public string Code { get; set; }
            public string Name { get; set; }

        }
    }
}
