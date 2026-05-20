namespace _1C_Integration_UI
{
    partial class AddProductForm
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
            txtArticle = new TextBox();
            labelNewProduct = new Label();
            labelArticle = new Label();
            labelName = new Label();
            txtName = new TextBox();
            labelBasePrice = new Label();
            numBasePrice = new TextBox();
            numQuantity = new TextBox();
            labelQuantity = new Label();
            label1 = new Label();
            cmbVat = new ComboBox();
            btnAddProduct = new Button();
            SuspendLayout();
            // 
            // txtArticle
            // 
            txtArticle.Location = new Point(132, 50);
            txtArticle.Name = "txtArticle";
            txtArticle.Size = new Size(133, 23);
            txtArticle.TabIndex = 0;
            // 
            // labelNewProduct
            // 
            labelNewProduct.AutoSize = true;
            labelNewProduct.Font = new Font("Segoe UI", 15F);
            labelNewProduct.Location = new Point(30, 9);
            labelNewProduct.Name = "labelNewProduct";
            labelNewProduct.Size = new Size(133, 28);
            labelNewProduct.TabIndex = 1;
            labelNewProduct.Text = "Новый товар";
            // 
            // labelArticle
            // 
            labelArticle.AutoSize = true;
            labelArticle.Location = new Point(36, 53);
            labelArticle.Name = "labelArticle";
            labelArticle.Size = new Size(53, 15);
            labelArticle.TabIndex = 2;
            labelArticle.Text = "Артикул";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(36, 82);
            labelName.Name = "labelName";
            labelName.Size = new Size(90, 15);
            labelName.TabIndex = 3;
            labelName.Text = "Наименование";
            // 
            // txtName
            // 
            txtName.Location = new Point(132, 79);
            txtName.Name = "txtName";
            txtName.Size = new Size(133, 23);
            txtName.TabIndex = 4;
            // 
            // labelBasePrice
            // 
            labelBasePrice.AutoSize = true;
            labelBasePrice.Location = new Point(36, 111);
            labelBasePrice.Name = "labelBasePrice";
            labelBasePrice.Size = new Size(79, 15);
            labelBasePrice.TabIndex = 5;
            labelBasePrice.Text = "Базовая цена";
            // 
            // numBasePrice
            // 
            numBasePrice.Location = new Point(132, 108);
            numBasePrice.Name = "numBasePrice";
            numBasePrice.Size = new Size(133, 23);
            numBasePrice.TabIndex = 6;
            // 
            // numQuantity
            // 
            numQuantity.Location = new Point(132, 137);
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(133, 23);
            numQuantity.TabIndex = 8;
            // 
            // labelQuantity
            // 
            labelQuantity.AutoSize = true;
            labelQuantity.Location = new Point(36, 140);
            labelQuantity.Name = "labelQuantity";
            labelQuantity.Size = new Size(72, 15);
            labelQuantity.TabIndex = 7;
            labelQuantity.Text = "Количество";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 169);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 9;
            label1.Text = "Ставка НДС";
            // 
            // cmbVat
            // 
            cmbVat.FormattingEnabled = true;
            cmbVat.Items.AddRange(new object[] { "20%", "7%", "0%" });
            cmbVat.Location = new Point(132, 166);
            cmbVat.Name = "cmbVat";
            cmbVat.Size = new Size(49, 23);
            cmbVat.TabIndex = 10;
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(214, 277);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(75, 23);
            btnAddProduct.TabIndex = 11;
            btnAddProduct.Text = "Добавить";
            btnAddProduct.UseVisualStyleBackColor = true;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // AddProductForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(301, 312);
            Controls.Add(btnAddProduct);
            Controls.Add(cmbVat);
            Controls.Add(label1);
            Controls.Add(numQuantity);
            Controls.Add(labelQuantity);
            Controls.Add(numBasePrice);
            Controls.Add(labelBasePrice);
            Controls.Add(txtName);
            Controls.Add(labelName);
            Controls.Add(labelArticle);
            Controls.Add(labelNewProduct);
            Controls.Add(txtArticle);
            Name = "AddProductForm";
            Text = "AddProductForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtArticle;
        private Label labelNewProduct;
        private Label labelArticle;
        private Label labelName;
        private TextBox txtName;
        private Label labelBasePrice;
        private TextBox numBasePrice;
        private TextBox numQuantity;
        private Label labelQuantity;
        private Label label1;
        private ComboBox cmbVat;
        private Button btnAddProduct;
    }
}