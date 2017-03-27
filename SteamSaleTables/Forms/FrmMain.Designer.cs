namespace SteamSaleTables.Forms
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            this.lvwApps = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bestDiscount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastDiscount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dev = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pub = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ratingCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSubgroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redditApiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subredditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itadApiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currenciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCurrencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCurrencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cboGroups = new System.Windows.Forms.ComboBox();
            this.cboSubgroups = new System.Windows.Forms.ComboBox();
            this.btnUpdatePrices = new System.Windows.Forms.Button();
            this.btnAddApp = new System.Windows.Forms.Button();
            this.btnUpdateAppData = new System.Windows.Forms.Button();
            this.btnUpdateReviews = new System.Windows.Forms.Button();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.rtfConsole = new System.Windows.Forms.RichTextBox();
            this.btnAddAppSearch = new System.Windows.Forms.Button();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnUpdateReddit = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // lvwApps
            // 
            this.lvwApps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.bestDiscount,
            this.lastDiscount,
            this.title,
            this.price,
            this.dev,
            this.pub,
            this.rating,
            this.ratingCount,
            this.desc});
            this.lvwApps.FullRowSelect = true;
            this.lvwApps.Location = new System.Drawing.Point(21, 53);
            this.lvwApps.Name = "lvwApps";
            this.lvwApps.Size = new System.Drawing.Size(1016, 505);
            this.lvwApps.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwApps.TabIndex = 0;
            this.lvwApps.UseCompatibleStateImageBehavior = false;
            this.lvwApps.View = System.Windows.Forms.View.Details;
            this.lvwApps.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwApps_ColumnClick);
            // 
            // id
            // 
            this.id.Text = "Id";
            this.id.Width = 50;
            // 
            // bestDiscount
            // 
            this.bestDiscount.Text = "Best";
            this.bestDiscount.Width = 35;
            // 
            // lastDiscount
            // 
            this.lastDiscount.Text = "Last";
            this.lastDiscount.Width = 35;
            // 
            // title
            // 
            this.title.Text = "Title";
            this.title.Width = 200;
            // 
            // price
            // 
            this.price.Text = "Price";
            // 
            // dev
            // 
            this.dev.Text = "Developer";
            this.dev.Width = 150;
            // 
            // pub
            // 
            this.pub.Text = "Publisher";
            this.pub.Width = 150;
            // 
            // rating
            // 
            this.rating.Text = "Rating";
            this.rating.Width = 45;
            // 
            // ratingCount
            // 
            this.ratingCount.Text = "Count";
            // 
            // desc
            // 
            this.desc.Text = "Description";
            this.desc.Width = 214;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem,
            this.dToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1049, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bToolStripMenuItem});
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.aToolStripMenuItem.Text = "File";
            // 
            // bToolStripMenuItem
            // 
            this.bToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGroupToolStripMenuItem,
            this.addSubgroupToolStripMenuItem});
            this.bToolStripMenuItem.Name = "bToolStripMenuItem";
            this.bToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.bToolStripMenuItem.Text = "Groups";
            // 
            // addGroupToolStripMenuItem
            // 
            this.addGroupToolStripMenuItem.Name = "addGroupToolStripMenuItem";
            this.addGroupToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addGroupToolStripMenuItem.Text = "Add Group";
            this.addGroupToolStripMenuItem.Click += new System.EventHandler(this.addGroupToolStripMenuItem_Click);
            // 
            // addSubgroupToolStripMenuItem
            // 
            this.addSubgroupToolStripMenuItem.Name = "addSubgroupToolStripMenuItem";
            this.addSubgroupToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addSubgroupToolStripMenuItem.Text = "Add Subgroup";
            this.addSubgroupToolStripMenuItem.Click += new System.EventHandler(this.addSubgroupToolStripMenuItem_Click);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.redditApiToolStripMenuItem,
            this.itadApiToolStripMenuItem,
            this.currenciesToolStripMenuItem});
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.dToolStripMenuItem.Text = "Edit";
            // 
            // redditApiToolStripMenuItem
            // 
            this.redditApiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.subredditToolStripMenuItem});
            this.redditApiToolStripMenuItem.Name = "redditApiToolStripMenuItem";
            this.redditApiToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.redditApiToolStripMenuItem.Text = "Reddit API";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // subredditToolStripMenuItem
            // 
            this.subredditToolStripMenuItem.Name = "subredditToolStripMenuItem";
            this.subredditToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.subredditToolStripMenuItem.Text = "Subreddit";
            this.subredditToolStripMenuItem.Click += new System.EventHandler(this.subredditToolStripMenuItem_Click);
            // 
            // itadApiToolStripMenuItem
            // 
            this.itadApiToolStripMenuItem.Name = "itadApiToolStripMenuItem";
            this.itadApiToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.itadApiToolStripMenuItem.Text = "ITAD API";
            this.itadApiToolStripMenuItem.Click += new System.EventHandler(this.itadApiToolStripMenuItem_Click);
            // 
            // currenciesToolStripMenuItem
            // 
            this.currenciesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setCurrencyToolStripMenuItem,
            this.addCurrencyToolStripMenuItem});
            this.currenciesToolStripMenuItem.Name = "currenciesToolStripMenuItem";
            this.currenciesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.currenciesToolStripMenuItem.Text = "Currencies";
            // 
            // setCurrencyToolStripMenuItem
            // 
            this.setCurrencyToolStripMenuItem.Name = "setCurrencyToolStripMenuItem";
            this.setCurrencyToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.setCurrencyToolStripMenuItem.Text = "Set Default";
            this.setCurrencyToolStripMenuItem.Click += new System.EventHandler(this.setCurrencyToolStripMenuItem_Click);
            // 
            // addCurrencyToolStripMenuItem
            // 
            this.addCurrencyToolStripMenuItem.Name = "addCurrencyToolStripMenuItem";
            this.addCurrencyToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addCurrencyToolStripMenuItem.Text = "Add Currency";
            this.addCurrencyToolStripMenuItem.Click += new System.EventHandler(this.addCurrencyToolStripMenuItem_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(751, 564);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Apps";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cboGroups
            // 
            this.cboGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroups.FormattingEnabled = true;
            this.cboGroups.Location = new System.Drawing.Point(21, 27);
            this.cboGroups.Name = "cboGroups";
            this.cboGroups.Size = new System.Drawing.Size(165, 21);
            this.cboGroups.TabIndex = 4;
            this.cboGroups.SelectedIndexChanged += new System.EventHandler(this.cboGroups_SelectedIndexChanged);
            // 
            // cboSubgroups
            // 
            this.cboSubgroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubgroups.FormattingEnabled = true;
            this.cboSubgroups.Location = new System.Drawing.Point(192, 26);
            this.cboSubgroups.Name = "cboSubgroups";
            this.cboSubgroups.Size = new System.Drawing.Size(165, 21);
            this.cboSubgroups.TabIndex = 5;
            this.cboSubgroups.SelectedIndexChanged += new System.EventHandler(this.cboSubgroups_SelectedIndexChanged);
            // 
            // btnUpdatePrices
            // 
            this.btnUpdatePrices.Location = new System.Drawing.Point(313, 564);
            this.btnUpdatePrices.Name = "btnUpdatePrices";
            this.btnUpdatePrices.Size = new System.Drawing.Size(140, 23);
            this.btnUpdatePrices.TabIndex = 6;
            this.btnUpdatePrices.Text = "Update Prices";
            this.btnUpdatePrices.UseVisualStyleBackColor = true;
            this.btnUpdatePrices.Click += new System.EventHandler(this.btnUpdatePrices_Click);
            // 
            // btnAddApp
            // 
            this.btnAddApp.Location = new System.Drawing.Point(21, 564);
            this.btnAddApp.Name = "btnAddApp";
            this.btnAddApp.Size = new System.Drawing.Size(140, 23);
            this.btnAddApp.TabIndex = 8;
            this.btnAddApp.Text = "Add App by Id";
            this.btnAddApp.UseVisualStyleBackColor = true;
            this.btnAddApp.Click += new System.EventHandler(this.btnAddApp_Click);
            // 
            // btnUpdateAppData
            // 
            this.btnUpdateAppData.Location = new System.Drawing.Point(605, 564);
            this.btnUpdateAppData.Name = "btnUpdateAppData";
            this.btnUpdateAppData.Size = new System.Drawing.Size(140, 23);
            this.btnUpdateAppData.TabIndex = 12;
            this.btnUpdateAppData.Text = "Update Details";
            this.btnUpdateAppData.UseVisualStyleBackColor = true;
            this.btnUpdateAppData.Click += new System.EventHandler(this.btnUpdateAppData_Click);
            // 
            // btnUpdateReviews
            // 
            this.btnUpdateReviews.Location = new System.Drawing.Point(459, 564);
            this.btnUpdateReviews.Name = "btnUpdateReviews";
            this.btnUpdateReviews.Size = new System.Drawing.Size(140, 23);
            this.btnUpdateReviews.TabIndex = 11;
            this.btnUpdateReviews.Text = "Update Reviews";
            this.btnUpdateReviews.UseVisualStyleBackColor = true;
            this.btnUpdateReviews.Click += new System.EventHandler(this.btnUpdateReviews_Click);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // rtfConsole
            // 
            this.rtfConsole.BackColor = System.Drawing.SystemColors.Control;
            this.rtfConsole.Location = new System.Drawing.Point(21, 593);
            this.rtfConsole.Name = "rtfConsole";
            this.rtfConsole.ReadOnly = true;
            this.rtfConsole.Size = new System.Drawing.Size(1016, 248);
            this.rtfConsole.TabIndex = 14;
            this.rtfConsole.Text = "";
            // 
            // btnAddAppSearch
            // 
            this.btnAddAppSearch.Location = new System.Drawing.Point(167, 564);
            this.btnAddAppSearch.Name = "btnAddAppSearch";
            this.btnAddAppSearch.Size = new System.Drawing.Size(140, 23);
            this.btnAddAppSearch.TabIndex = 15;
            this.btnAddAppSearch.Text = "Add App by Search";
            this.btnAddAppSearch.UseVisualStyleBackColor = true;
            this.btnAddAppSearch.Click += new System.EventHandler(this.btnAddAppSearch_Click);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // btnUpdateReddit
            // 
            this.btnUpdateReddit.Location = new System.Drawing.Point(897, 564);
            this.btnUpdateReddit.Name = "btnUpdateReddit";
            this.btnUpdateReddit.Size = new System.Drawing.Size(140, 23);
            this.btnUpdateReddit.TabIndex = 16;
            this.btnUpdateReddit.Text = "Update Reddit";
            this.btnUpdateReddit.UseVisualStyleBackColor = true;
            this.btnUpdateReddit.Click += new System.EventHandler(this.btnUpdateReddit_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 853);
            this.Controls.Add(this.btnUpdateReddit);
            this.Controls.Add(this.btnAddAppSearch);
            this.Controls.Add(this.rtfConsole);
            this.Controls.Add(this.btnUpdateAppData);
            this.Controls.Add(this.btnUpdateReviews);
            this.Controls.Add(this.btnAddApp);
            this.Controls.Add(this.btnUpdatePrices);
            this.Controls.Add(this.cboSubgroups);
            this.Controls.Add(this.cboGroups);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lvwApps);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.Text = "Steam Sales Tables";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwApps;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cboGroups;
        private System.Windows.Forms.ComboBox cboSubgroups;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader bestDiscount;
        private System.Windows.Forms.ColumnHeader lastDiscount;
        private System.Windows.Forms.ColumnHeader title;
        private System.Windows.Forms.ColumnHeader price;
        private System.Windows.Forms.ColumnHeader dev;
        private System.Windows.Forms.ColumnHeader pub;
        private System.Windows.Forms.ColumnHeader desc;
        private System.Windows.Forms.ColumnHeader rating;
        private System.Windows.Forms.ColumnHeader ratingCount;
        private System.Windows.Forms.Button btnUpdatePrices;
        private System.Windows.Forms.Button btnAddApp;
        private System.Windows.Forms.Button btnUpdateAppData;
        private System.Windows.Forms.Button btnUpdateReviews;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.RichTextBox rtfConsole;
        private System.Windows.Forms.ToolStripMenuItem addGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSubgroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redditApiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itadApiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currenciesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCurrencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCurrencyToolStripMenuItem;
        private System.Windows.Forms.Button btnAddAppSearch;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Button btnUpdateReddit;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subredditToolStripMenuItem;


    }
}

