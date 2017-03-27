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
    public partial class FrmAddAppById : Form
    {
        private readonly string _group, _subgroup;

        public FrmAddAppById(string group, string subgroup)
        {
            _group = group;
            _subgroup = subgroup;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SteamTableManager.AddApp(_group, _subgroup, txtAppId.Text);
            txtAppId.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtAppId_TextChanged(object sender, EventArgs e)
        {
            txtAppId.Text = txtAppId.Text.Where(char.IsDigit).Aggregate(string.Empty, (current, ch) => current + ch);
        }
    }
}
