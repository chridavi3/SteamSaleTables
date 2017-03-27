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
    public partial class FrmSelectCurrency : Form
    {
        private readonly ListViewColumnSorter _lvwColumnSorter;
        public FrmSelectCurrency()
        {
            InitializeComponent();

            _lvwColumnSorter = new ListViewColumnSorter();
            lvwCurrencies.ListViewItemSorter = _lvwColumnSorter;

            var currencies = SteamTableManager.GetCurrencies();

            foreach (var currArr in currencies.Select(currency => new []
            {
                currency.Abbreviation, 
                currency.CountryCode,
                currency.ItadCc,
                currency.PriceFormat,
                currency.PriceDecimal
            }))
                lvwCurrencies.Items.Add(new ListViewItem(currArr));
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (lvwCurrencies.SelectedItems.Count == 0)
                return;

            SteamTableManager.SetDefaultCurrency(lvwCurrencies.SelectedItems[0].Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new FrmAddCurrency();

            addForm.ShowDialog();

            var currencies = SteamTableManager.GetCurrencies();

            if (currencies.Count == lvwCurrencies.Items.Count)
                return;

            lvwCurrencies.Items.Clear();

            foreach (var currArr in currencies.Select(currency => new[]
            {
                currency.Abbreviation, 
                currency.CountryCode,
                currency.ItadCc,
                currency.PriceFormat,
                currency.PriceDecimal
            }))
                lvwCurrencies.Items.Add(new ListViewItem(currArr));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lvwCurrencies_ColumnClick(object sender, ColumnClickEventArgs e)
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
            lvwCurrencies.Sort();
        }
    }
}
