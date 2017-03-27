using System;
using System.Windows.Forms;

namespace SteamSaleTables.Forms
{
    public partial class FrmMain : Form
    {
        private readonly ListViewColumnSorter _lvwColumnSorter;
        public FrmMain()
        {
            InitializeComponent();
            
            SteamTableManager.InitializeSteamTableManager(rtfConsole);
            cboGroups.Items.AddRange(SteamTableManager.GetGroupNames());
            _lvwColumnSorter = new ListViewColumnSorter();
            lvwApps.ListViewItemSorter = _lvwColumnSorter;
        }

        private void cboGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubgroups.Items.Clear();
            cboSubgroups.Items.AddRange(SteamTableManager.GetSubgroupNames(cboGroups.SelectedItem.ToString()));
        }

        private void cboSubgroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            var apps = SteamTableManager.GetApps(cboGroups.SelectedItem.ToString(), cboSubgroups.SelectedItem.ToString());
            lvwApps.Items.Clear();

            foreach (var app in apps)
                lvwApps.Items.Add(new ListViewItem(app.ToArray(SteamTableManager.GetDefaultCurrency())));
        }

        private void lvwApps_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                _lvwColumnSorter.Order = _lvwColumnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            lvwApps.Sort();
				
        }

        private void btnAddApp_Click(object sender, EventArgs e)
        {
            // No subgroup open
            if (cboGroups.SelectedItem == null || cboSubgroups.SelectedItem == null)
                return;

            var form = new FrmAddAppById(cboGroups.SelectedItem.ToString(), cboSubgroups.SelectedItem.ToString());
            form.ShowDialog();

            var apps = SteamTableManager.GetApps(cboGroups.SelectedItem.ToString(), cboSubgroups.SelectedItem.ToString());

            if (apps.Count == lvwApps.Items.Count) 
                return;

            lvwApps.Items.Clear();

            foreach (var app in apps)
                lvwApps.Items.Add(new ListViewItem(app.ToArray(SteamTableManager.GetDefaultCurrency())));
        }

        private void btnAddAppSearch_Click(object sender, EventArgs e)
        {
            // No subgroup open
            if (cboGroups.SelectedItem == null || cboSubgroups.SelectedItem == null)
                return;

            var form = new FrmAddAppBySearch(cboGroups.SelectedItem.ToString(), cboSubgroups.SelectedItem.ToString());
            form.ShowDialog();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // No subgroup open
            if (cboGroups.SelectedItem == null || cboSubgroups.SelectedItem == null)
                return;

            foreach (ListViewItem item in lvwApps.SelectedItems)
                SteamTableManager.RemoveApp(cboGroups.SelectedItem.ToString(), cboSubgroups.SelectedItem.ToString(),
                    item.Text);

            var apps = SteamTableManager.GetApps(cboGroups.SelectedItem.ToString(), cboSubgroups.SelectedItem.ToString());

            if (apps.Count == lvwApps.Items.Count)
                return;

            lvwApps.Items.Clear();

            foreach (var app in apps)
                lvwApps.Items.Add(new ListViewItem(app.ToArray(SteamTableManager.GetDefaultCurrency())));
        }

        private void addGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmAddGroup();
            form.ShowDialog();

            var groups = SteamTableManager.GetGroupNames();

            if (groups.Length == cboGroups.Items.Count) 
                return;

            cboGroups.Items.Clear();
            cboGroups.Items.AddRange(groups);

            cboSubgroups.Items.Clear();
            lvwApps.Items.Clear();
        }

        private void addSubgroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmAddSubgroup(cboGroups.SelectedItem.ToString());
            form.ShowDialog();

            var subgroups = SteamTableManager.GetGroupNames();

            if (subgroups.Length == cboSubgroups.Items.Count) 
                return;

            cboSubgroups.Items.Clear();
            cboSubgroups.Items.AddRange(SteamTableManager.GetSubgroupNames(cboGroups.SelectedItem.ToString()));

            lvwApps.Items.Clear();
        }

        private void itadApiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmItadApi();
            form.ShowDialog();
        }

        private void setCurrencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmSelectCurrency();
            form.ShowDialog();

            var apps = SteamTableManager.GetApps(cboGroups.SelectedItem.ToString(), cboSubgroups.SelectedItem.ToString());

            lvwApps.Items.Clear();

            foreach (var app in apps)
                lvwApps.Items.Add(new ListViewItem(app.ToArray(SteamTableManager.GetDefaultCurrency())));
        }

        private void addCurrencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmAddCurrency();
            form.ShowDialog();
        }

        private void btnUpdatePrices_Click(object sender, EventArgs e)
        {
            SteamTableManager.UpdatePrices();
        }

        private void btnUpdateReviews_Click(object sender, EventArgs e)
        {
            SteamTableManager.UpdateReviewData();
        }

        private void btnUpdateAppData_Click(object sender, EventArgs e)
        {
            SteamTableManager.UpdateAppData();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmRedditLogin();
            form.ShowDialog();
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            SteamTableManager.ShortUpdate();
        }

        private void btnUpdateReddit_Click(object sender, EventArgs e)
        {

            SteamTableManager.UpdateReddit();
        }

        private void subredditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmSetSubreddit();
            form.ShowDialog();
        }
    }
}
