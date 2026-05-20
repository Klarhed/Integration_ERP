namespace _1C_Integration_UI
{
    partial class dgvProducts
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            toolStrip = new ToolStrip();
            toolStripButtonLoadJson = new ToolStripButton();
            toolStripButtonLoadSql = new ToolStripButton();
            toolStripComboBox = new ToolStripComboBox();
            headerPanel = new Panel();
            lblCounterparty = new Label();
            cmbCounterparty = new ComboBox();
            btnAddProductToInvoice = new Button();
            btnLoadToSql = new Button();
            lblSearch = new Label();
            txtSearch = new TextBox();
            lblWarehouse = new Label();
            cmbWarehouse = new ComboBox();
            lblInvoiceDate = new Label();
            dtInvoiceDate = new DateTimePicker();
            lblInvoiceNumber = new Label();
            txtInvoiceNumber = new TextBox();
            dataGridView = new DataGridView();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            toolStripStatusLabelTotal = new ToolStripStatusLabel();
            openFileDialog = new OpenFileDialog();
            toolStrip.SuspendLayout();
            headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { toolStripButtonLoadJson, toolStripButtonLoadSql, toolStripComboBox });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(900, 25);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip";
            // 
            // toolStripButtonLoadJson
            // 
            toolStripButtonLoadJson.ImageTransparentColor = Color.Magenta;
            toolStripButtonLoadJson.Name = "toolStripButtonLoadJson";
            toolStripButtonLoadJson.Size = new Size(136, 22);
            toolStripButtonLoadJson.Text = "Загрузить из 1С (JSON)";
            toolStripButtonLoadJson.Click += toolStripButtonLoadJson_Click;
            // 
            // toolStripButtonLoadSql
            // 
            toolStripButtonLoadSql.ImageTransparentColor = Color.Magenta;
            toolStripButtonLoadSql.Name = "toolStripButtonLoadSql";
            toolStripButtonLoadSql.Size = new Size(104, 22);
            toolStripButtonLoadSql.Text = "Загрузить из SQL";
            // 
            // toolStripComboBox
            // 
            toolStripComboBox.BackColor = SystemColors.Window;
            toolStripComboBox.DropDownWidth = 152;
            toolStripComboBox.Name = "toolStripComboBox";
            toolStripComboBox.Size = new Size(142, 25);
            toolStripComboBox.Text = "Переключить режим";
            toolStripComboBox.SelectedIndexChanged += toolStripComboBox_SelectedIndexChanged;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = SystemColors.Control;
            headerPanel.BorderStyle = BorderStyle.FixedSingle;
            headerPanel.Controls.Add(lblCounterparty);
            headerPanel.Controls.Add(cmbCounterparty);
            headerPanel.Controls.Add(btnAddProductToInvoice);
            headerPanel.Controls.Add(btnLoadToSql);
            headerPanel.Controls.Add(lblSearch);
            headerPanel.Controls.Add(txtSearch);
            headerPanel.Controls.Add(lblWarehouse);
            headerPanel.Controls.Add(cmbWarehouse);
            headerPanel.Controls.Add(lblInvoiceDate);
            headerPanel.Controls.Add(dtInvoiceDate);
            headerPanel.Controls.Add(lblInvoiceNumber);
            headerPanel.Controls.Add(txtInvoiceNumber);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 25);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(900, 80);
            headerPanel.TabIndex = 1;
            // 
            // lblCounterparty
            // 
            lblCounterparty.AutoSize = true;
            lblCounterparty.Location = new Point(440, 49);
            lblCounterparty.Name = "lblCounterparty";
            lblCounterparty.Size = new Size(73, 15);
            lblCounterparty.TabIndex = 10;
            lblCounterparty.Text = "Поставщик:";
            // 
            // cmbCounterparty
            // 
            cmbCounterparty.FormattingEnabled = true;
            cmbCounterparty.Location = new Point(530, 45);
            cmbCounterparty.Name = "cmbCounterparty";
            cmbCounterparty.Size = new Size(150, 23);
            cmbCounterparty.TabIndex = 11;
            // 
            // btnAddProductToInvoice
            // 
            btnAddProductToInvoice.Location = new Point(696, 44);
            btnAddProductToInvoice.Name = "btnAddProductToInvoice";
            btnAddProductToInvoice.Size = new Size(110, 23);
            btnAddProductToInvoice.TabIndex = 9;
            btnAddProductToInvoice.Text = "Добавить Товар";
            btnAddProductToInvoice.UseVisualStyleBackColor = true;
            btnAddProductToInvoice.Click += btnAddProductToInvoice_Click;
            // 
            // btnLoadToSql
            // 
            btnLoadToSql.Location = new Point(812, 45);
            btnLoadToSql.Name = "btnLoadToSql";
            btnLoadToSql.Size = new Size(75, 23);
            btnLoadToSql.TabIndex = 8;
            btnLoadToSql.Text = "Провести";
            btnLoadToSql.UseVisualStyleBackColor = true;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(10, 48);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 6;
            lblSearch.Text = "Поиск:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(70, 45);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 7;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblWarehouse
            // 
            lblWarehouse.AutoSize = true;
            lblWarehouse.Location = new Point(470, 12);
            lblWarehouse.Name = "lblWarehouse";
            lblWarehouse.Size = new Size(43, 15);
            lblWarehouse.TabIndex = 4;
            lblWarehouse.Text = "Склад:";
            // 
            // cmbWarehouse
            // 
            cmbWarehouse.FormattingEnabled = true;
            cmbWarehouse.Location = new Point(530, 9);
            cmbWarehouse.Name = "cmbWarehouse";
            cmbWarehouse.Size = new Size(150, 23);
            cmbWarehouse.TabIndex = 5;
            // 
            // lblInvoiceDate
            // 
            lblInvoiceDate.AutoSize = true;
            lblInvoiceDate.Location = new Point(240, 12);
            lblInvoiceDate.Name = "lblInvoiceDate";
            lblInvoiceDate.Size = new Size(35, 15);
            lblInvoiceDate.TabIndex = 2;
            lblInvoiceDate.Text = "Дата:";
            // 
            // dtInvoiceDate
            // 
            dtInvoiceDate.Location = new Point(290, 9);
            dtInvoiceDate.Name = "dtInvoiceDate";
            dtInvoiceDate.Size = new Size(150, 23);
            dtInvoiceDate.TabIndex = 3;
            // 
            // lblInvoiceNumber
            // 
            lblInvoiceNumber.AutoSize = true;
            lblInvoiceNumber.Location = new Point(10, 12);
            lblInvoiceNumber.Name = "lblInvoiceNumber";
            lblInvoiceNumber.Size = new Size(48, 15);
            lblInvoiceNumber.TabIndex = 0;
            lblInvoiceNumber.Text = "Номер:";
            // 
            // txtInvoiceNumber
            // 
            txtInvoiceNumber.Location = new Point(70, 9);
            txtInvoiceNumber.Name = "txtInvoiceNumber";
            txtInvoiceNumber.Size = new Size(150, 23);
            txtInvoiceNumber.TabIndex = 1;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(0, 105);
            dataGridView.Name = "dataGridView";
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(900, 337);
            dataGridView.TabIndex = 2;
            dataGridView.CellDoubleClick += dataGridView_CellDoubleClick;
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridView.CellValidating += dataGridView_CellValidating;
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;
            dataGridView.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
            dataGridView.KeyDown += dataGridView_KeyDown;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel, toolStripStatusLabelTotal });
            statusStrip.Location = new Point(0, 442);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(900, 22);
            statusStrip.TabIndex = 3;
            statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(38, 17);
            toolStripStatusLabel.Text = "Готов";
            // 
            // toolStripStatusLabelTotal
            // 
            toolStripStatusLabelTotal.Alignment = ToolStripItemAlignment.Right;
            toolStripStatusLabelTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripStatusLabelTotal.Name = "toolStripStatusLabelTotal";
            toolStripStatusLabelTotal.Size = new Size(103, 17);
            toolStripStatusLabelTotal.Text = "ИТОГО: 0.00 руб.";
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // dgvProducts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(900, 464);
            Controls.Add(dataGridView);
            Controls.Add(headerPanel);
            Controls.Add(toolStrip);
            Controls.Add(statusStrip);
            ForeColor = SystemColors.ControlText;
            Name = "dgvProducts";
            Text = "Поступление товаров";
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStrip toolStrip;
        private ToolStripButton toolStripButtonLoadJson;
        private ToolStripButton toolStripButtonLoadSql;
        private Panel headerPanel;
        private Label lblInvoiceNumber;
        private TextBox txtInvoiceNumber;
        private Label lblInvoiceDate;
        private DateTimePicker dtInvoiceDate;
        private Label lblWarehouse;
        private ComboBox cmbWarehouse;
        private Label lblSearch;
        private TextBox txtSearch;
        private DataGridView dataGridView;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripStatusLabel toolStripStatusLabelTotal;
        private OpenFileDialog openFileDialog;
        private Button btnLoadToSql;
        private Button btnAddProductToInvoice;
        private ToolStripComboBox toolStripComboBox;
        private Label lblCounterparty;
        private ComboBox cmbCounterparty;
    }
}
