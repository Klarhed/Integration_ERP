using _1C_Integration_UI.Data;
using _1C_Integration_UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using _1C_Integration_UI.Services;

namespace _1C_Integration_UI.Forms
{
    public partial class ucWarehouseList : UserControl
    {
        private WarehouseService _warehouseService;
        private DataGridView _gridWarehouse;
        private BindingSource _bindingSource;
        private Button _btnCreateWarehouse;


        public ucWarehouseList()
        {
            this.Dock = DockStyle.Fill;

            _warehouseService = new WarehouseService();
            _bindingSource = new BindingSource();

            InitializeControlComponents();
        }

        public void InitializeControlComponents()
        {
            Panel bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60, // Высота подвала, где будет жить кнопка
                BackColor = System.Drawing.Color.White
            };

            // 2. Создаем кнопку "Создать"
            _btnCreateWarehouse = new Button
            {
                Text = "➕ Создать новый склад",
                Size = new System.Drawing.Size(200, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular),
                Cursor = Cursors.Hand,
                // Магия позиционирования: привязываем кнопку к ПРАВОМУ НИЖНЕМУ углу подвала
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            // Считаем координаты кнопки внутри bottomPanel, чтобы она была справа с отступом в 15px
            _btnCreateWarehouse.Location = new System.Drawing.Point(
                bottomPanel.Width - _btnCreateWarehouse.Width - 15,
                (bottomPanel.Height - _btnCreateWarehouse.Height) / 2
            );

            _btnCreateWarehouse.FlatAppearance.BorderSize = 0;
            bottomPanel.Controls.Add(_btnCreateWarehouse); // Кладем кнопку в подвал

            // 3. Создаем таблицу списка инвойсов (занимает всё ОСТАВШЕЕСЯ место сверху)
            _gridWarehouse = new DataGridView
            {
                Dock = DockStyle.Fill, // Заполняет всё до границы bottomPanel
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = System.Drawing.Color.White,
                DataSource = _bindingSource
            };

            _gridWarehouse.CellDoubleClick += GridWarehouse_CellDoubleClick;

            // 4. Добавляем элементы на UserControl в ПРАВИЛЬНОМ порядке
            this.Controls.Add(_gridWarehouse); // Сначала заполняющую таблицу
            this.Controls.Add(bottomPanel);   // Потом подвал

            // Принудительно говорим подвалу встать вниз, а таблице - занять всё остальное
            bottomPanel.SendToBack();

            // Настройка колонок и стиля
            _gridWarehouse.AutoGenerateColumns = false;
            _gridWarehouse.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 60 });
            _gridWarehouse.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Код склада", DataPropertyName = "Code", Width = 180 });
            _gridWarehouse.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Название склада", DataPropertyName = "Name", Width = 180 });

            StyleDataGridView(_gridWarehouse);
        }

        public async Task LoadWarehousesListAsync()
        {
            var warehouses = await _warehouseService.GetWarehousesAsync();
            _bindingSource.DataSource = new BindingList<WarehouseService.WarehouseDto>(warehouses);
        }

        private async void BtnCreateWarehous_Click(object sender, EventArgs e)
        {
            using(var AddWarehouseForm = new AddWarehouseForm())
            {
                if(AddWarehouseForm.ShowDialog() == DialogResult.OK)
                {
                    var dto = AddWarehouseForm.EditedWarehouse;

                    try
                    {
                        await _warehouseService.AddNewWarehouseAsync(dto.Code, dto.Name);

                        await LoadWarehousesListAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void GridWarehouse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (_bindingSource[e.RowIndex] is WarehouseService.WarehouseDto selectedWarehouseDto) 
                {
                    using (var editForm = new AddWarehouseForm())
                    {
                        editForm.LoadWarehouseData(selectedWarehouseDto);

                        if (editForm.ShowDialog() == DialogResult.OK) 
                        {
                            try 
                            { 
                                using(var context = new WarehouseContext())
                                {
                                    var warehouse = await context.Warehouses.FindAsync(selectedWarehouseDto.Id);
                                    if (warehouse != null)
                                    {
                                        warehouse.Code = editForm.EditedWarehouse.Code;
                                        warehouse.Name = editForm.EditedWarehouse.Name;
                                        await context.SaveChangesAsync();

                                        selectedWarehouseDto.Code = warehouse.Code;
                                        selectedWarehouseDto.Name = warehouse.Name;
                                    }
                                }
                                _bindingSource.ResetItem(e.RowIndex);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            // Отключаем старые системные стили Windows
            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = System.Drawing.Color.FromArgb(230, 230, 235); // Очень бледная сетка вместо серой
            dgv.BackgroundColor = System.Drawing.Color.White;

            // Настройка шрифтов для строк
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            dgv.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(45, 45, 48);
            dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(235, 243, 250); // Мягкий светло-синий цвет выделения
            dgv.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(0, 102, 204);     // Синий текст при выделении
            dgv.RowTemplate.Height = 35; // Делаем строки просторнее

            // Настройка шапки таблицы (Делаем в цвет бокового меню!)
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(45, 45, 48); // Тёмная шапка под стиль меню
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Сглаживание углов и лишних элементов
            dgv.RowHeadersVisible = false; // Прячем пустую колонку слева
            dgv.AllowUserToResizeRows = false;
        }
    }
}
