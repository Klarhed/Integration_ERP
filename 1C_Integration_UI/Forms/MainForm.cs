using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _1C_Integration_UI.Forms
{
    public partial class MainForm : Form
    {
        private Panel _menuPanel;
        private Panel _contentPanel;

        private ucInvoicesList _ucInvoices;
        private ucWarehouseList _ucWarehouses;

        public MainForm()
        {
            InitializeComponent();

            this.Text = "Интеграция 1С — Учет Склада (ERP)";
            this.Size = new System.Drawing.Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeLayout();
        }

        private void InitializeLayout()
        {
            _menuPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 200,
                BackColor = System.Drawing.Color.FromArgb(45, 45, 48)
            };
            _contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.White
            };

            this.Controls.Add(_contentPanel);
            this.Controls.Add(_menuPanel);

            Button btnInvoices = CreateMenuButton("🧾 Инвойсы / Накладные", 0);
            Button btnWarehouses = CreateMenuButton("🏢 Склады компании", 45);
            Button btnCounterparties = CreateMenuButton("🤝 Контрагенты", 90);

            btnInvoices.Click += async (s, e) => await ShowInvoicesScreenAsync();
            btnWarehouses.Click += async (s, e) => await ShowWarehousesScreenAsync();
            btnCounterparties.Click += (s, e) => ShowPlaceholderScreen("Экран управления контрагентами в разработке");

            _menuPanel.Controls.Add(btnInvoices);
            _menuPanel.Controls.Add(btnWarehouses);
            _menuPanel.Controls.Add(btnCounterparties);

            _ucInvoices = new ucInvoicesList();
            _contentPanel.Controls.Add(_ucInvoices);

            this.Load += async (s, e) => await ShowInvoicesScreenAsync();
        }


        private Button CreateMenuButton(string text, int topPosition)
        {
            return new Button
            {
                Text = text,
                Location = new System.Drawing.Point(0, topPosition),
                Size = new System.Drawing.Size(200, 45),
                FlatStyle = FlatStyle.Flat,
                ForeColor = System.Drawing.Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                FlatAppearance = { BorderSize = 0 }
            };
        }

        private async Task ShowPlaceholderScreen(string message)
        {
            foreach (Control ctrl in _contentPanel.Controls)
            {
                ctrl.Visible = false;
            }

            Label lbl = new Label
            {
                Text = message,
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Italic),
                ForeColor = System.Drawing.Color.Gray,
                Location = new System.Drawing.Point(50, 50)
            };

            // Очищаем старые временные лейблы, чтобы не плодились
            var oldLabel = _contentPanel.Controls.OfType<Label>().FirstOrDefault();
            if (oldLabel != null) _contentPanel.Controls.Remove(oldLabel);

            _contentPanel.Controls.Add(lbl);
            lbl.Visible = true;
        }

        private async Task ShowInvoicesScreenAsync()
        {
            foreach (Control ctrl in _contentPanel.Controls)
            {
                ctrl.Visible = false;
            }

            _ucInvoices.Visible = true;
            await _ucInvoices.LoadInvoicesListAsync();
        }

        public async Task ShowWarehousesScreenAsync()
        {
            foreach (Control ctrl in _contentPanel.Controls)
            {
                ctrl.Visible = false;
            }

            if (_ucWarehouses != null)
            {
                _ucWarehouses.Visible = true;
                await _ucWarehouses.LoadWarehousesListAsync();
            }
            else
            {
                MessageBox.Show("Экран управления складами в разработке", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
