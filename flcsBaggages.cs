using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Linq;

namespace GulliverII
{
    public partial class flcsBaggages : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private GulliverLibrary.QueryHandler gulliverQueryHandler;
        private List<GulliverLibrary.BaggagePrice> baggages;

        public flcsBaggages()
        {
            gulliverQueryHandler = new GulliverLibrary.QueryHandler();
            InitializeComponent();
            FillBaggages(txtSearchbox.Text.Trim());
            SetText(txtSearchbox, "Search airline here ...");
        }

        private void FillBaggages(string searchText)
        {
            libraryDS.Baggage.Rows.Clear();
            baggages = gulliverQueryHandler.GetAllBaggagePrices();

            if (searchText.Trim() != string.Empty && searchText != "Search airline here ...")
                baggages = baggages.Where(m => m.airline.Trim().ToUpper().Contains(searchText.ToUpper().Trim())).ToList();

            foreach (GulliverLibrary.BaggagePrice baggage in baggages.OrderBy(m => m.airline.Trim()))
                libraryDS.Baggage.AddBaggageRow("Delete", baggage.airline.Trim(), baggage.price);            
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
                SetText(txtSearchbox, "Search airline here ...");
        }

        private void dataGridViewPackageBackup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGridViewBaggages.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewBaggages.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected baggage - continue?", "Delete Baggage", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridViewBaggages.Rows.Remove((DataGridViewRow)dataGridViewBaggages.Rows[e.RowIndex]);
                        gulliverQueryHandler.DeleteBaggageById(Convert.ToString(dataGridViewBaggages.Rows[e.RowIndex].Cells[1].Value));
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void txtSearchbox_TextChanged(object sender, EventArgs e)
        {
            dataGridViewBaggages.Rows.Cast<DataGridViewRow>().ToList().ForEach(r => r.Visible = true);
            dataGridViewBaggages.CurrentCell = (dataGridViewBaggages.Rows.Count != 0) ? dataGridViewBaggages.Rows[dataGridViewBaggages.Rows.Count - 1].Cells[0] : null;
          
            if (txtSearchbox.Text.Trim() != string.Empty && txtSearchbox.Text != "Search airline here ...")
            {
                 List<DataGridViewRow> rows = dataGridViewBaggages.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[1].Value != null && !r.Cells[1].Value.ToString().Trim().ToUpper().Contains(txtSearchbox.Text.Trim().ToUpper())).ToList();

                foreach (DataGridViewRow row in rows)
                {
                    try
                    {
                        row.Visible = false;
                    }
                    catch { }
                }
            }

            //FillMedias(txtSearchbox.Text.Trim());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           List<GulliverLibrary.BaggagePrice> baggages = GetBaggages();
           gulliverQueryHandler.SaveBaggages(baggages);
           this.Close();
        }

        private List<GulliverLibrary.BaggagePrice> GetBaggages()
        {
            List<GulliverLibrary.BaggagePrice> baggages = new List<GulliverLibrary.BaggagePrice>();

            foreach (LibraryDS.BaggageRow row in this.libraryDS.Baggage.Where(b => b.airline != null))
            {
                GulliverLibrary.BaggagePrice baggage = new GulliverLibrary.BaggagePrice();
                baggage.airline = row.airline.Trim();
                baggage.price = row.price;
                baggages.Add(baggage);
            }

            return baggages;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("If you have made any changes, this wont save - continue?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    this.Close();
                    break;

                case System.Windows.Forms.DialogResult.No:
                    return;
            }
        }

        private void toolAddBaggage_Click(object sender, EventArgs e)
        {
           this.libraryDS.Baggage.Rows.Add("Delete", string.Empty, 0);
           dataGridViewBaggages.Sort(dataGridViewBaggages.Columns[1], ListSortDirection.Ascending);
        }        
       
    }
}