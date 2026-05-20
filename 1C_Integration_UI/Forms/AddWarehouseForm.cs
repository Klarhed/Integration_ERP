using _1C_Integration_UI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _1C_Integration_UI.Forms
{
    public partial class AddWarehouseForm : Form
    {
        public WarehouseService.WarehouseDto EditedWarehouse { get; private set; }

        public AddWarehouseForm()
        {
            InitializeComponent();
        }


        private void btnAddWarehouse_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim() == "" || txtName.Text.Trim() == "")
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.EditedWarehouse = new WarehouseService.WarehouseDto
            {
                Code = txtCode.Text.Trim(),
                Name = txtName.Text.Trim(),
            };

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        public void LoadWarehouseData(WarehouseService.WarehouseDto warehouse)
        {
            labelNewWarehouse.Text = "Редактирование склада";
            btnAddWarehouse.Text = "Сохранить";

            txtCode.Text = warehouse.Code;
            txtName.Text = warehouse.Name;
        }
    }
}
