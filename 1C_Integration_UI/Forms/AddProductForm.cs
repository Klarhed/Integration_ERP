using _1C_Integration_UI.Models;
using _1C_Integration_UI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _1C_Integration_UI
{
    public partial class AddProductForm : Form
    {
        public WarehouseService.InvoiceItemsDto EditedItem { get; private set; }

        public AddProductForm()
        {
            InitializeComponent();
        }

        

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if( txtArticle.Text.Trim() == "" || txtName.Text.Trim() == "" || numBasePrice.Text.Trim() == "")
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!decimal.TryParse(numBasePrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Пожалуйста, введите корректную цену.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!int.TryParse(numQuantity.Text.Trim(), out int quantity) || quantity < 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное количество.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal priceValue = decimal.Parse(numBasePrice.Text.Trim());
            int quantityValue = int.Parse(numQuantity.Text.Trim());

            decimal vatRate = 0m;
            if (cmbVat.SelectedItem != null)
            {
                string vatText = cmbVat.SelectedItem.ToString();
                if (vatText == "20%") vatRate = 0.2m;
                else if (vatText == "7%") vatRate = 0.07m;
            }

            this.EditedItem = new WarehouseService.InvoiceItemsDto
            {
                Article = txtArticle.Text.Trim(),
                Name = txtName.Text.Trim(),
                Price = priceValue,
                Quantity = quantityValue,
                VatRate = vatRate,
            };

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        public void LoadProductData(WarehouseService.InvoiceItemsDto invoiceItem)
        {
            labelNewProduct.Text = "Редактирование товара";
            btnAddProduct.Text = "Сохранить";

            txtArticle.Text = invoiceItem.Article;
            txtName.Text = invoiceItem.Name;
            numBasePrice.Text = invoiceItem.Price.ToString();
            numQuantity.Text = invoiceItem.Quantity.ToString();


            if (invoiceItem.VatRate == 0.2m) cmbVat.SelectedItem = "20%";
            else if (invoiceItem.VatRate == 0.07m) cmbVat.SelectedItem = "7%";
            else cmbVat.SelectedItem = "0%";

        }
    }
}
