using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Collections;
using System.Linq;
using DataGridViewAutoFilter;

namespace GulliverII
{
    public partial class flcsThirdpartyHotelRequests : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private AccommodationHandler.ThirdPartyHotelHandler thirdPartyHandler;
        private string source; 

        public flcsThirdpartyHotelRequests()
        {
            InitializeComponent();           
            FillSource();
            FillOccupancy();
        }

        private void FillSource()
        {
            List<string> hotelSources = new List<string>() { "Multicom", "Expedia" };

            foreach (string source in hotelSources)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = source;
                item.Value = source;

                cbSources.Items.Add(item);
            }
        }

        private void cbSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            thirdPartyHandler = new AccommodationHandler.ThirdPartyHotelHandler(((ComboBoxItem)cbSources.SelectedItem).Value.ToString());
            source = ((ComboBoxItem)cbSources.SelectedItem).Value.ToString();
            FillRequests();
        }

        private void FillOccupancy()
        {
            Hashtable occupancys = new Hashtable();
            occupancys.Add("2A 0C 0I", "2,0,0");
            occupancys.Add("2A 1C 0I", "2,1,0");
            occupancys.Add("2A 2C 0I", "2,2,0");
            occupancys.Add("2A 2C 1I", "2,2,1");
            occupancys.Add("2A 0C 1I", "2,0,1");
            occupancys.Add("3A 0C 0I", "3,0,0");

            foreach (string key in occupancys.Keys)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = key;
                item.Value = occupancys[key].ToString();
                cbOcupancy.Items.Add(item);
            }
        }

        private void FillRequests()
        {
            List<AccommodationHandler.ThirdpartyRequest> thirdPartyCacheRequets = thirdPartyHandler.GetRequests();
            thirdPartyCacheRequets = FilterRequest(thirdPartyCacheRequets);
            Hashtable destinations = thirdPartyHandler.GetDestinationBySource();
            thirdPartyHandler = new AccommodationHandler.ThirdPartyHotelHandler(((ComboBoxItem)cbSources.SelectedItem).Value.ToString());
            
            this.thirdPartyDS.Requets.Rows.Clear();
            foreach (AccommodationHandler.ThirdpartyRequest r in thirdPartyCacheRequets)
                this.thirdPartyDS.Requets.AddRequetsRow(r.id, "Delete", "View", ((source.ToUpper() == "EXPEDIA")? r.city.Trim() +" - ("+r.destination.Trim()+")" :(destinations.ContainsKey(r.destination.Trim()) ? destinations[r.destination].ToString() + " - (" + r.destination.Trim() + ")" : r.destination.Trim())), r.durations.Trim().Replace("#", ", "), ProcessOccupnacy(r.occupancys.Trim()), r.stars.Trim().Replace("#", ", "), r.boardBasis.Trim().Replace("#", ", "), r.searchPeriod, r.enabled);

            //dataGVRequests.Sort(dataGVRequests.Columns[3], ListSortDirection.Ascending);            
        }

        private string ProcessOccupnacy(string occupnacy)
        {
            List<string> proceedTexts = new List<string>();

            foreach (string occ in occupnacy.Split('#'))
            {
               string[] occupnacys = occ.Split(',');
               proceedTexts.Add(occupnacys[0] + "A " + occupnacys[1] + "C " + occupnacys[2] + "I");
            }

            return string.Join(", " , proceedTexts);
        }

        private List<AccommodationHandler.ThirdpartyRequest> FilterRequest(List<AccommodationHandler.ThirdpartyRequest> requets)
        {
            if (cbDurations.CheckedItems.Count > 0)
            {
                List<string> checkedDurations = cbDurations.CheckedItems.Cast<string>().ToList();
                requets = requets.Where(r => checkedDurations.Any(d => r.durations.Split('#').Contains(d))).ToList();
            }

            if (cbOcupancy.CheckedItems.Count > 0)
            {
                List<string> checkedOccupnacy = cbOcupancy.CheckedItems.Cast<ComboBoxItem>().Select(c => c.Value.ToString()).ToList();
                requets = requets.Where(o => checkedOccupnacy.Any(c => o.occupancys.Contains(c))).ToList();
            }

            if (cbStars.CheckedItems.Count > 0)
            {
                List<string> checkedStars = cbStars.CheckedItems.Cast<string>().ToList();
                requets = requets.Where(s => checkedStars.Any(c => s.stars.Contains(c))).ToList();
            }

            if (cbBoards.CheckedItems.Count > 0)
            {
                List<string> checkedBoards = cbBoards.CheckedItems.Cast<string>().ToList();
                requets = requets.Where(b => checkedBoards.Any(c => b.boardBasis.Contains(c))).ToList();
            }
            
            return requets;
        }

        private void cbDurations_SelectedIndexChanged(object sender, EventArgs e)
        {
           FillRequests();
        }

        private void cbOcupancy_SelectedIndexChanged(object sender, EventArgs e)
        {
           FillRequests();
        }

        private void cbStars_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRequests();
        }

        private void ClearFilter()
        {
          ChangeAllDurations(false);
          ChangeAllOccupancy(false);
          ChangeAllStars(false);
          ChangeAllBoards(false);
        }

        private void ChangeAllDurations(bool check)
        {
           for (int i = 0; i <= cbDurations.Items.Count-1; i++)
              cbDurations.SetItemChecked(i, check);
        }

        private void ChangeAllOccupancy(bool check)
        {
            for (int i = 0; i <= cbOcupancy.Items.Count-1; i++)
                cbOcupancy.SetItemChecked(i, check);
        }

        private void ChangeAllStars(bool check)
        {
            for (int i = 0; i <= cbStars.Items.Count-1; i++)
                cbStars.SetItemChecked(i, check);
        }

        private void ChangeAllBoards(bool check)
        {
            for (int i = 0; i <= cbBoards.Items.Count - 1; i++)
                cbBoards.SetItemChecked(i, check);
        }

        private void lblShowAll_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dataGVRequests);
            ClearFilter();
        }

        private void dataGVRequests_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell
             .GetFilterStatus(dataGVRequests);
            if (String.IsNullOrEmpty(filterStatus))
            {
                lblShowAll.Visible = false;
                lblFilterStatus.Visible = false;
            }
            else
            {
                lblShowAll.Visible = true;
                lblFilterStatus.Visible = true;
                lblFilterStatus.Text = filterStatus;
            }
        }

        private void cbBoards_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRequests();
        }

        private void dataGVRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGVRequests.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVRequests.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected request - continue?", "Delete Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        {
                            DeleteRequest(Convert.ToInt32(dataGVRequests.Rows[e.RowIndex].Cells[0].Value));
                            dataGVRequests.Rows.Remove((DataGridViewRow)dataGVRequests.Rows[e.RowIndex]);                           
                            break;
                        }

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
            else if (e.RowIndex != -1 && dataGVRequests.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVRequests.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "View")
            {                
                flcsThirdPartyHotelManager thirdPartyManager = new flcsThirdPartyHotelManager(((ComboBoxItem)cbSources.SelectedItem).Value.ToString(), Convert.ToInt32(dataGVRequests.Rows[e.RowIndex].Cells[0].Value));
                thirdPartyManager.ShowDialog();
                FillRequests();
            }
        }

        private void DeleteRequest(int id)
        {
            thirdPartyHandler.DeleteRequest(id);
        }
             
        private void newOfferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            source = "Multicom";
            thirdPartyHandler = new AccommodationHandler.ThirdPartyHotelHandler(source);
            flcsThirdPartyHotelManager thirdPartyManager = new flcsThirdPartyHotelManager(source, 0);
            thirdPartyManager.ShowDialog();
            FillRequests();
        }

        private void newExpediaRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            source = "Expedia";
            thirdPartyHandler = new AccommodationHandler.ThirdPartyHotelHandler(source);
            flcsThirdPartyHotelManager thirdPartyManager = new flcsThirdPartyHotelManager(source, 0);
            thirdPartyManager.ShowDialog();
            FillRequests();
        }
                
    }
}