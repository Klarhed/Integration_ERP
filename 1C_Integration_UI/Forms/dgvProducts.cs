using _1C_Integration_UI.Data;
using _1C_Integration_UI.Models;
using _1C_Integration_UI.Services;
using _1C_Integration_UI.Services.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace _1C_Integration_UI
{
    public partial class dgvProducts : Form
    {

        private WarehouseService _service;
        private WarehouseContext _context;

        private BindingList<WarehouseService.InvoiceItemsDto> _invoiceItems;
        private BindingSource _bindingSource;

        private int? _currentInvoiceId;
        bool hasUnsavedChanges = false;

        public dgvProducts() : this(null)
        {

        }

        public dgvProducts(int? invoiceId)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeGridColumns();

            dataGridView.AutoGenerateColumns = false;

            _currentInvoiceId = invoiceId;

            _context = new WarehouseContext();
            _service = new WarehouseService(_context);

            _invoiceItems = new BindingList<WarehouseService.InvoiceItemsDto>();
            _bindingSource = new BindingSource();

            _bindingSource.DataSource = _invoiceItems;
            dataGridView.DataSource = _bindingSource;

            toolStripButtonLoadSql.Click += async (sender, e) => await RefreshCurrentInvoice_Click(sender, e);
            btnLoadToSql.Click += async (s, e) => await btnLoadToSql_Click(s, e);

            SetupMapping();

            this.Load += async (s, e) => await OnFormLoadAsync();
        }

        

        private void InitializeGridColumns()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Number", HeaderText = "№", Width = 40, ReadOnly = true });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Article", HeaderText = "Артикул", Width = 100 });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "Товар", Width = 200 });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Кол-во", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Price", HeaderText = "Цена", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "C2" } });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Sum", HeaderText = "Сумма", Width = 100, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "C2" } });
        }

        private void SetupMapping()
        {
            dataGridView.Columns["Article"].DataPropertyName = nameof(WarehouseService.InvoiceItemsDto.Article);
            dataGridView.Columns["ProductName"].DataPropertyName = nameof(WarehouseService.InvoiceItemsDto.Name);
            dataGridView.Columns["Quantity"].DataPropertyName = nameof(WarehouseService.InvoiceItemsDto.Quantity);
            dataGridView.Columns["Price"].DataPropertyName = nameof(WarehouseService.InvoiceItemsDto.Price);
        }

        private void SetDataAsActual()
        {
            hasUnsavedChanges = false;
            toolStripStatusLabel.Text = "Готов";
        }

        private void MarkAsUnsaved()
        {
            hasUnsavedChanges = true;
            toolStripStatusLabel.Text = "Есть несохраненные изменения";
        }







        
        

        private async Task btnLoadToSql_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
            {
                MessageBox.Show("Введите номер накладной!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCounterparty.SelectedValue == null || cmbWarehouse.SelectedValue == null)
            {
                MessageBox.Show("Введите склад и контрагента!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            toolStripStatusLabel.Text = "Проведение документа...";
            toolStripButtonLoadSql.Enabled = false;

            try
            {
                var itemsList = new List<WarehouseService.InvoiceItemsDto>(_invoiceItems);
                int warehouseId = (int)cmbWarehouse.SelectedValue;
                int counterpartyId = (int)cmbCounterparty.SelectedValue;

                await _service.SaveInvoiceWithRelationsAsync(_currentInvoiceId, txtInvoiceNumber.Text, dtInvoiceDate.Value, warehouseId, counterpartyId, itemsList);
                MessageBox.Show("Данные успешно сохранены в базу данных.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SetDataAsActual();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка проведения: {ex.Message}", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                toolStripButtonLoadSql.Enabled = true;
                UpdateTotalSum();
            }
        }

        private async Task RefreshCurrentInvoice_Click(object sender, EventArgs e)
        {
            if (_currentInvoiceId.HasValue) 
            { 
                await LoadInvoiceFromDatabaseByIdAsync(_currentInvoiceId.Value);
                toolStripStatusLabel.Text = "Данные обновлены из базы";
            }
            else
            {
                MessageBox.Show("Этот инвойс еще не сохранен в базе данных.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButtonLoadJson_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string jsonString = File.ReadAllText(openFileDialog.FileName);

                    var importedData = JsonSerializer.Deserialize<List<WarehouseService.InvoiceItemsDto>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        NumberHandling = JsonNumberHandling.AllowReadingFromString,
                        Converters = { new EmptyStringToDecimalConverter() }
                    });

                    if (importedData != null)
                    {
                        _invoiceItems.Clear();
                        foreach (var item in importedData)
                        {
                            _invoiceItems.Add(item);
                        }

                        txtInvoiceNumber.Text = Path.GetFileNameWithoutExtension(openFileDialog.FileName) + " " + DateTime.Now.ToString("HHmmss");
                        dtInvoiceDate.Value = DateTime.Now;

                        MarkAsUnsaved();
                        UpdateTotalSum();
                        toolStripStatusLabel.Text = $"Загружено {importedData.Count} позиций из JSON";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении JSON: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddProductToInvoice_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddProductForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    _invoiceItems.Add(addForm.EditedItem);
                    MarkAsUnsaved();
                    UpdateTotalSum();
                }
            }
        }

        private void toolStripButtonToggleMode_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = txtSearch.Text;

            if (string.IsNullOrEmpty(searchText))
            {
                _bindingSource.DataSource = _invoiceItems;
            }
            else
            {
                var filteredItems = _invoiceItems.Where(i =>
                    i.Article.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    i.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    i.Price.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                _bindingSource.DataSource = new BindingList<WarehouseService.InvoiceItemsDto>(filteredItems);
            }
            UpdateTotalSum();
        }

        private void dataGridView_KeyDown(object? sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) && (dataGridView.SelectedRows.Count > 0))
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить выбранные позиции ({dataGridView.SelectedRows.Count}) шт.?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;

                var itemsToRemove = new List<WarehouseService.InvoiceItemsDto>();

                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    if (row.DataBoundItem is WarehouseService.InvoiceItemsDto item)
                    {
                        itemsToRemove.Add(item);
                    }
                }

                foreach (var item in itemsToRemove)
                {
                    _invoiceItems.Remove(item);
                }

                string searchText = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(searchText))
                {
                    var filteredList = _invoiceItems.Where(i =>
                        i.Article.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        i.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        i.Price.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    _bindingSource.DataSource = new BindingList<WarehouseService.InvoiceItemsDto>(filteredList);
                }
                else
                {
                    _bindingSource.ResetBindings(true);
                }

                MarkAsUnsaved();
                UpdateTotalSum();
            }
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MarkAsUnsaved();
                UpdateTotalSum();
            }
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView.Columns[e.ColumnIndex].Name == "Number")
            {
                e.Value = e.RowIndex + 1;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Sum")
            {
                var item = dataGridView.Rows[e.RowIndex].DataBoundItem as WarehouseService.InvoiceItemsDto;
                if (item != null)
                {
                    e.Value = item.Quantity * item.Price;
                }
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (_bindingSource[e.RowIndex] is WarehouseService.InvoiceItemsDto selectedItem)
            {
                using (var editForm = new AddProductForm())
                {
                    editForm.LoadProductData(selectedItem);

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        selectedItem.Article = editForm.EditedItem.Article;
                        selectedItem.Name = editForm.EditedItem.Name;
                        selectedItem.Price = editForm.EditedItem.Price;
                        selectedItem.Quantity = editForm.EditedItem.Quantity;
                        selectedItem.VatRate = editForm.EditedItem.VatRate;

                        _bindingSource.ResetItem(e.RowIndex);

                        dataGridView.InvalidateRow(e.RowIndex);

                        MarkAsUnsaved();
                        UpdateTotalSum();
                    }
                }
            }
        }

        private void toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }










        private void UpdateTotalSum()
        {
            decimal total = 0;
            foreach (var item in _bindingSource)
            {
                if (item is WarehouseService.InvoiceItemsDto itemDto)
                {
                    total += itemDto.Quantity * itemDto.Price;
                }
            }
            toolStripStatusLabel.Text = $"Всего позиций: {_bindingSource.Count}";
            toolStripStatusLabelTotal.Text = $"Общая сумма: {total:C2}";
        }

        private async Task OnFormLoadAsync()
        {
            await LoadDictionaryAsync();

            if (_currentInvoiceId.HasValue)
                await LoadInvoiceFromDatabaseByIdAsync(_currentInvoiceId.Value);
            else
            {
                txtInvoiceNumber.Text = $"Новая накладная {DateTime.Now:yyyyMMdd_HHmmss}";
                dtInvoiceDate.Value = DateTime.Now;
                SetDataAsActual();
            }
        }

        private async Task LoadDictionaryAsync()
        {
            var warehouses = await _context.Warehouses.ToListAsync();
            cmbWarehouse.DataSource = warehouses;
            cmbWarehouse.DisplayMember = "Name";
            cmbWarehouse.ValueMember = "Id";
            cmbWarehouse.SelectedIndex = -1;

            var counterparties = await _context.Counterparties.ToListAsync();
            cmbCounterparty.DataSource = counterparties;
            cmbCounterparty.DisplayMember = "Name";
            cmbCounterparty.ValueMember = "Id";
            cmbCounterparty.SelectedIndex = -1;
        }

        private async Task LoadInvoiceFromDatabaseByIdAsync(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                MessageBox.Show($"Накладная с ID {id} не найдена в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtInvoiceNumber.Text = invoice.Number;
            dtInvoiceDate.Value = invoice.Date;

            if(invoice.WarehouseId.HasValue)
                cmbWarehouse.SelectedValue = invoice.WarehouseId.Value;
            if(invoice.CounterpartyId.HasValue)
                cmbCounterparty.SelectedValue = invoice.CounterpartyId.Value;

            var items = await _service.GetInvoiceByIDAsync(id);

            _invoiceItems.Clear();
            foreach (var item in items)
            {
                _invoiceItems.Add(item);
            }

            UpdateTotalSum();
            SetDataAsActual();
        }

    }
}
