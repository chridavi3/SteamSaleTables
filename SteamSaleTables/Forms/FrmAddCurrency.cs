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
    public partial class FrmAddCurrency : Form
    {
        public FrmAddCurrency()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SteamTableManager.AddCurrency(txtAbbreviation.Text, txtCountryCode.Text, txtItad.Text, txtPriceFormat.Text,
                txtPriceDecimal.Text);

            txtAbbreviation.Clear();
            txtCountryCode.Clear();
            txtItad.Clear();
            txtPriceFormat.Clear();
            txtPriceDecimal.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
