namespace SteamSaleTables.Forms
{
    partial class FrmSelectCurrency
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
            this.lvwCurrencies = new System.Windows.Forms.ListView();
            this.abbreviation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.countryCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itadCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceFormat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceDecimal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwCurrencies
            // 
            this.lvwCurrencies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwCurrencies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.abbreviation,
            this.countryCode,
            this.itadCode,
            this.priceFormat,
            this.priceDecimal});
            this.lvwCurrencies.FullRowSelect = true;
            this.lvwCurrencies.Location = new System.Drawing.Point(12, 12);
            this.lvwCurrencies.Name = "lvwCurrencies";
            this.lvwCurrencies.Size = new System.Drawing.Size(255, 205);
            this.lvwCurrencies.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwCurrencies.TabIndex = 1;
            this.lvwCurrencies.UseCompatibleStateImageBehavior = false;
            this.lvwCurrencies.View = System.Windows.Forms.View.Details;
            this.lvwCurrencies.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwCurrencies_ColumnClick);
            // 
            // abbreviation
            // 
            this.abbreviation.Text = "Abbrev";
            this.abbreviation.Width = 50;
            // 
            // countryCode
            // 
            this.countryCode.Text = "CC";
            this.countryCode.Width = 40;
            // 
            // itadCode
            // 
            this.itadCode.Text = "ITAD";
            this.itadCode.Width = 40;
            // 
            // priceFormat
            // 
            this.priceFormat.Text = "Format";
            this.priceFormat.Width = 100;
            // 
            // priceDecimal
            // 
            this.priceDecimal.Text = ".";
            this.priceDecimal.Width = 20;
            // 
            // btnDefault
            // 
            this.btnDefault.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDefault.Location = new System.Drawing.Point(3, 3);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(79, 22);
            this.btnDefault.TabIndex = 2;
            this.btnDefault.Text = "Set Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(88, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(79, 22);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add Currency";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(173, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 22);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.btnDefault, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 223);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(255, 28);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // FrmSelectCurrency
            // 
            this.AcceptButton = this.btnDefault;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(279, 258);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lvwCurrencies);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmSelectCurrency";
            this.Text = "Select Currency";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwCurrencies;
        private System.Windows.Forms.ColumnHeader abbreviation;
        private System.Windows.Forms.ColumnHeader countryCode;
        private System.Windows.Forms.ColumnHeader priceFormat;
        private System.Windows.Forms.ColumnHeader itadCode;
        private System.Windows.Forms.ColumnHeader priceDecimal;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}