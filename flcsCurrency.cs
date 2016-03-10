using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Linq;

namespace Gulliver
{
    public partial class flcsCurrency : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private GulliverLibrary.QueryHandler gulliverQueryHandler;
        private List<GulliverLibrary.Currency> currencys;

        public flcsCurrency()
        {
            gulliverQueryHandler = new GulliverLibrary.QueryHandler();
            InitializeComponent();
            FillCurrency(txtSearchbox.Text.Trim());
            SetText(txtSearchbox, "Search currency here ...");
        }

        private void FillCurrency(string searchText)
        {
            libraryDS.Currency.Rows.Clear();
            currencys = gulliverQueryHandler.GetAllCurrencys();

            if (searchText.Trim() != string.Empty && searchText != "Search currency here ...")
                currencys = currencys.Where(m => m.currency.Trim().ToUpper().Contains(searchText.ToUpper().Trim())).ToList();

            foreach (GulliverLibrary.Currency currency in currencys.OrderBy(m => m.currency.Trim()))
                libraryDS.Currency.AddCurrencyRow("Delete", currency.currency.Trim(), currency.exchangeRate,currency.sellAtRate);            
        }

        private void SetText(TextBox txtSearchbox, string text)
        {
            txtSearchbox.Text = text;
            txtSearchbox.ForeColor = Color.Gray;
        }

        private void txtSearchbox_Enter(object sender, EventArgs e)
        {
            if (txtSearchbox.ForeColor != Color.Black)
            {
               txtSearchbox.Text = string.Empty;
               txtSearchbox.ForeColor = Color.Black;
            }
        }

        private void txtSearchbox_Leave(object sender, EventArgs e)
        {
            if (txtSearchbox.Text.Trim() == string.Empty)
                SetText(txtSearchbox, "Search currency here ...");
        }

        private void dataGridViewPackageBackup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGridViewCurrencys.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewCurrencys.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected currency - continue?", "Delete Currency", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridViewCurrencys.Rows.Remove((DataGridViewRow)dataGridViewCurrencys.Rows[e.RowIndex]);
                        gulliverQueryHandler.DeleteCurrencyByCurrency(Convert.ToString(dataGridViewCurrencys.Rows[e.RowIndex].Cells[1].Value));
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void txtSearchbox_TextChanged(object sender, EventArgs e)
        {
            dataGridViewCurrencys.Rows.Cast<DataGridViewRow>().ToList().ForEach(r => r.Visible = true);
            dataGridViewCurrencys.CurrentCell = (dataGridViewCurrencys.Rows.Count != 0) ? dataGridViewCurrencys.Rows[dataGridViewCurrencys.Rows.Count - 1].Cells[0] : null;

            if (txtSearchbox.Text.Trim() != string.Empty && txtSearchbox.Text != "Search currency here ...")
            {
                List<DataGridViewRow> rows = dataGridViewCurrencys.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[1].Value != null && !r.Cells[1].Value.ToString().Trim().ToUpper().Contains(txtSearchbox.Text.Trim().ToUpper())).ToList();

                foreach (DataGridViewRow row in rows)
                {
                    try
                    {
                        row.Visible = false;
                    }
                    catch { }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           List<GulliverLibrary.Currency> currencys = GetCurrencys();
           gulliverQueryHandler.SaveCurrency(currencys);
           this.Close();
        }

        private List<GulliverLibrary.Currency> GetCurrencys()
        {
            List<GulliverLibrary.Currency> currencys = new List<GulliverLibrary.Currency>();

            foreach (LibraryDS.CurrencyRow row in this.libraryDS.Currency.Where(c => !c.IsCurrencyNull()))
            {
                GulliverLibrary.Currency currency = new GulliverLibrary.Currency();
                currency.currency = row.Currency.Trim();
                currency.exchangeRate = row.ExchangeRate;
                currency.sellAtRate = row.SellAtRate;
                currencys.Add(currency);
            }

            return currencys;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            switch (KryptonMessageBox.Show("If you have made any changes, this wont save - continue?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    this.Close();
                    break;

                case System.Windows.Forms.DialogResult.No:
                    return;
            }
        }

        private void toolAddCurrency_Click(object sender, EventArgs e)
        {
            this.libraryDS.Currency.Rows.Add("Delete", string.Empty, 0, 0);
            dataGridViewCurrencys.Sort(dataGridViewCurrencys.Columns[1], ListSortDirection.Ascending);
        }      
       
    }
}