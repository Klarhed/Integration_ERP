namespace _1C_Integration_UI.Forms
{
    partial class AddWarehouseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtName = new TextBox();
            labelName = new Label();
            labelNewWarehouse = new Label();
            btnAddWarehouse = new Button();
            txtCode = new TextBox();
            lblCode = new Label();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(120, 90);
            txtName.Name = "txtName";
            txtName.Size = new Size(133, 23);
            txtName.TabIndex = 6;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(24, 93);
            labelName.Name = "labelName";
            labelName.Size = new Size(90, 15);
            labelName.TabIndex = 5;
            labelName.Text = "Наименование";
            // 
            // labelNewWarehouse
            // 
            labelNewWarehouse.AutoSize = true;
            labelNewWarehouse.Font = new Font("Segoe UI", 15F);
            labelNewWarehouse.Location = new Point(24, 9);
            labelNewWarehouse.Name = "labelNewWarehouse";
            labelNewWarehouse.Size = new Size(131, 28);
            labelNewWarehouse.TabIndex = 7;
            labelNewWarehouse.Text = "Новый склад";
            // 
            // btnAddWarehouse
            // 
            btnAddWarehouse.Location = new Point(198, 238);
            btnAddWarehouse.Name = "btnAddWarehouse";
            btnAddWarehouse.Size = new Size(75, 23);
            btnAddWarehouse.TabIndex = 12;
            btnAddWarehouse.Text = "Добавить";
            btnAddWarehouse.UseVisualStyleBackColor = true;
            btnAddWarehouse.Click += btnAddWarehouse_Click;
            // 
            // txtCode
            // 
            txtCode.Location = new Point(120, 61);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(133, 23);
            txtCode.TabIndex = 14;
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Location = new Point(24, 64);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(67, 15);
            lblCode.TabIndex = 13;
            lblCode.Text = "Код склада";
            // 
            // AddWarehouseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(285, 273);
            Controls.Add(txtCode);
            Controls.Add(lblCode);
            Controls.Add(btnAddWarehouse);
            Controls.Add(labelNewWarehouse);
            Controls.Add(txtName);
            Controls.Add(labelName);
            Name = "AddWarehouseForm";
            Text = "AddWarehouseForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private Label labelName;
        private Label labelNewWarehouse;
        private Button btnAddWarehouse;
        private TextBox txtCode;
        private Label lblCode;
    }
}