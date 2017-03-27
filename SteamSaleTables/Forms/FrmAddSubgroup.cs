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
    public partial class FrmAddSubgroup : Form
    {
        private readonly string _group;

        public FrmAddSubgroup(string group)
        {
            _group = group;

            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SteamTableManager.AddSubgroup(_group, txtSubgroup.Text);
            txtSubgroup.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
