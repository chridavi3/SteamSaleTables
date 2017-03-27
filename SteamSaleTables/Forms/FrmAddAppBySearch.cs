using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamSaleTables.Forms
{
    public partial class FrmAddAppBySearch : Form
    {
        private readonly string _group, _subgroup;

        public FrmAddAppBySearch(string group, string subgroup)
        {
            _group = group;
            _subgroup = subgroup;
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var apps = SteamTableManager.GetSearchApps(txtSearch.Text);
            lvwApps.Items.Clear();

            foreach (var app in apps)
                lvwApps.Items.Add(new ListViewItem(app));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lvwApps.SelectedItems.Count == 0)
                return;

            SteamTableManager.AddApp(_group, _subgroup, lvwApps.SelectedItems[0].Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
