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
    public partial class flcsThirdPartyHotelManager : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private AccommodationHandler.ThirdPartyHotelHandler thirdPartyHandler;
        private bool changed = false;
        private string source;
        private int requestId;
        private string destination;

        public flcsThirdPartyHotelManager(string source, int requestId)
        {
            thirdPartyHandler = new AccommodationHandler.ThirdPartyHotelHandler(source);
            InitializeComponent();
            this.source = source;
            this.requestId = requestId;
            FillData();
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

        private void FillData()
        {
            AccommodationHandler.ThirdpartyRequest request = thirdPartyHandler.GetRequestById(requestId);

            if (request != null)
                this.destination = (source.ToUpper() == "EXPEDIA")?request.destination.Trim() + "," + request.city.Trim():request.destination.Trim();
            else
                cbShowAll.Checked = true;

            FillCountries();
            FillOccupancy();

            if (request != null)
            {
                DisableControls();
                SetDurations(request.durations.Split('#').ToList());
                SetBoards(request.boardBasis.Split('#').ToList());
                SetStars(request.stars.Split('#').ToList());
                SetOccupancys(request.occupancys.Split('#').ToList());
                cbEnabled.Checked = request.enabled;
                cbPeriods.SelectedItem = request.searchPeriod.ToString();
                //FillDestinations();
                //FillHotels();
            }
        }

        private void FillDataByDestination()
        {
            AccommodationHandler.ThirdpartyRequest request = thirdPartyHandler.GetRequestByDestination(((ComboBoxItem)cbDestinations.SelectedItem).Value.ToString()) ;

            if (request != null)
            {
                this.destination = (source.ToUpper() == "EXPEDIA") ? request.destination.Trim() + "," + request.city.Trim() : request.destination.Trim();
                SetDurations(request.durations.Split('#').ToList());
                SetBoards(request.boardBasis.Split('#').ToList());
                SetStars(request.stars.Split('#').ToList());
                SetOccupancys(request.occupancys.Split('#').ToList());
                cbEnabled.Checked = request.enabled;
                cbPeriods.SelectedItem = request.searchPeriod.ToString();
            }
            else
            {
                SetDurations(new List<string>());
                SetBoards(new List<string>());
                SetStars(new List<string>());
                SetOccupancys(new List<string>());
                cbEnabled.Checked = false;
                cbPeriods.SelectedItem = null;
            }
        }
            
        private void SetDurations(List<string> durations)
        {
            for (int i = 0; i < cbDurations.Items.Count; i++)
            {
                if (durations.Contains(cbDurations.Items[i].ToString()))
                    cbDurations.SetItemChecked(i, true);
                else
                    cbDurations.SetItemChecked(i, false);
            }
        }

        private void SetOccupancys(List<string> occupancys)
        {
            for (int i = 0; i < cbOcupancy.Items.Count; i++)
            {
                if (occupancys.Contains(((ComboBoxItem)cbOcupancy.Items[i]).Value.ToString()))
                    cbOcupancy.SetItemChecked(i, true);
                else
                    cbOcupancy.SetItemChecked(i, false);
            }
        }

        private void SetStars(List<string> stars)
        {
            for (int i = 0; i < cbStars.Items.Count; i++)
            {
                if (stars.Contains(cbStars.Items[i].ToString()))
                    cbStars.SetItemChecked(i, true);
                else
                    cbStars.SetItemChecked(i, false);
            }
        }

        private void SetBoards(List<string> boards)
        {
            for (int i = 0; i < cbBoards.Items.Count; i++)
            {
                if (boards.Contains(cbBoards.Items[i].ToString()))
                    cbBoards.SetItemChecked(i, true);
                else
                    cbBoards.SetItemChecked(i, false);
            }
        }

        private void FillHotels()
        {
            AccommodationHandler.Search search = new AccommodationHandler.Search();
            search.source = source;
            search.destinations = new List<string>();

            if (cbDestinations.SelectedItem != null && ((ComboBoxItem)cbDestinations.SelectedItem).Value.ToString() != string.Empty)
                search.destinations.Add(((ComboBoxItem)cbDestinations.SelectedItem).Value.ToString());
            else if (cbCountries.SelectedItem != null && ((ComboBoxItem)cbCountries.SelectedItem).Value.ToString() != string.Empty)
                search.destinations = ((ComboBoxItem)cbCountries.SelectedItem).Value.ToString().Split('#').ToList();
            else
                search.destinations = new List<string>();

            List<AccommodationHandler.ThirdPartyHotel> thirdPartyHotels = thirdPartyHandler.GetHotels(search, cbShowAll.Checked);

            thirdPartyDS.Hotels.Rows.Clear();
            thirdPartyHotels = FilterHotels(thirdPartyHotels);

            foreach (AccommodationHandler.ThirdPartyHotel hotel in thirdPartyHotels)
                this.thirdPartyDS.Hotels.AddHotelsRow(hotel.hotelId, source, hotel.countryCode, hotel.airportCode, hotel.resort, hotel.hotelname, hotel.supplier, hotel.stars, hotel.enabled, (hotel.markup != null && hotel.markup != string.Empty)?hotel.markup:"0", hotel.availablePrices);
        }

        private void FillDestinations()
        {
            cbDestinations.Items.Clear();
            cbDestinations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cbDestinations.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbDestinations.AutoCompleteSource = AutoCompleteSource.ListItems;

            Hashtable destinations = thirdPartyHandler.GetDestinationBySource();
            List<DictionaryEntry> dictionaryEntries = destinations.Cast<DictionaryEntry>().ToList();

            if (cbCountries.SelectedItem != null)
            {
              List<string> selectedDestinations = ((ComboBoxItem)cbCountries.SelectedItem).Value.ToString().Split('#').ToList();
              dictionaryEntries = destinations.Cast<DictionaryEntry>().Where(d => selectedDestinations.Contains(d.Key.ToString().Split(',').First())).ToList();
            }

            ComboBoxItem itemI = new ComboBoxItem();
            itemI.Value = string.Empty;
            itemI.Text = "---";
            cbDestinations.Items.Add(itemI);

            foreach (DictionaryEntry des in dictionaryEntries.OrderBy(d => d.Value))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Value = des.Key.ToString();
                item.Text = des.Value.ToString();
                cbDestinations.Items.Add(item);

                if (destination != null && des.Key.ToString().Trim() == destination.Trim().ToUpper())
                    cbDestinations.SelectedItem = item;
            }
        }

        private void FillCountries()
        {
            cbCountries.Items.Clear();
            cbCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cbCountries.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbCountries.AutoCompleteSource = AutoCompleteSource.ListItems;
            Hashtable countries = thirdPartyHandler.GetCountriesBySource(source);


            foreach (DictionaryEntry coun in countries.Cast<DictionaryEntry>().OrderBy(c => c.Key))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Value = coun.Value;
                item.Text = coun.Key.ToString();
                cbCountries.Items.Add(item);

                if (destination != null && coun.Value.ToString().Split('#').Contains(destination.Trim().Split(',').First()))
                    cbCountries.SelectedItem = item;
            }
        }

        private void DisableControls()
        {
            cbDestinations.Enabled = false;
            cbCountries.Enabled = false;
        }

        private List<AccommodationHandler.ThirdPartyHotel> FilterHotels(List<AccommodationHandler.ThirdPartyHotel> thirdPartyHotels)
        {
            //if (cbCountries.SelectedItem != null)
            //    thirdPartyHotels = thirdPartyHotels.Where(h => ((ComboBoxItem)cbCountries.SelectedItem).Value.ToString().Split('#').Contains(h.airportCode.Trim())).ToList();

            if (cbStars.CheckedItems.Count > 0)
            {
                List<string> checkedStars = cbStars.CheckedItems.Cast<string>().ToList();
                thirdPartyHotels = thirdPartyHotels.Where(s => checkedStars.Contains(s.stars.ToString())).ToList();
            }

            if (txtHotelname.Text != string.Empty)
                thirdPartyHotels = thirdPartyHotels.Where(t => t.hotelname.ToUpper().Trim().Contains(txtHotelname.Text.Trim().ToUpper())).ToList();

            return thirdPartyHotels;
        }

        private void SaveHotels()
        {
            List<AccommodationHandler.ThirdPartyHotel> thirdPartyHotels = (from h in this.thirdPartyDS.Hotels
                                                                           select new AccommodationHandler.ThirdPartyHotel
                                                                           {
                                                                               airportCode = h.airportCode,
                                                                               countryCode = h.countryCode,
                                                                               enabled = h.enabled,
                                                                               hotelId = h.hotelId,
                                                                               hotelname = h.hotelname,
                                                                               markup = h.markup,
                                                                               resort = h.resort,
                                                                               source = h.source,
                                                                               stars = h.stars,
                                                                               supplier = h.supplier
                                                                           }).ToList();

            thirdPartyHandler.UpdateHotels(thirdPartyHotels);
            changed = false;
        }

        private void txtHotelname_TextChanged(object sender, EventArgs e)
        {
            FillHotels();
        }

        private void dataGVMarkups_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                int selectedCellCount = dataGVHotels.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 1)
                {
                    dataGVHotels.ContextMenuStrip = cmsMarkup;
                    cmsMarkup.Visible = true;
                }
                else
                {
                    dataGVHotels.ContextMenuStrip = null;
                    txtMarkup.Text = string.Empty;
                    cmsMarkup.Visible = false;
                }
            }
            else if (e.ColumnIndex == 7)
            {
                int selectedCellCount = dataGVHotels.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 1)
                {
                    dataGVHotels.ContextMenuStrip = cmsEnabled;
                    cmsEnabled.Visible = true;
                }
                else
                {
                    dataGVHotels.ContextMenuStrip = null;
                    txtMarkup.Text = string.Empty;
                    cmsEnabled.Visible = false;
                }
            }
            else
            {
                dataGVHotels.ContextMenuStrip = null;
                txtMarkup.Text = string.Empty;
                cmsMarkup.Visible = false;
            }
        }

        private void txtMarkup_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool found = true;

            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                List<string> ids = new List<string>();

                try
                {
                    for (int i = 0; i < dataGVHotels.Rows.Count; i++)
                    {
                        if (dataGVHotels.Rows[i].Cells[8].Selected && found)
                            ids.Add(dataGVHotels.Rows[i].Cells[0].Value.ToString());
                    }
                }
                catch { }

                if (ids.Count > 0)
                {
                    foreach (ThirdPartyDS.HotelsRow r in this.thirdPartyDS.Hotels.Where(h => ids.Any(i => i == h.hotelId)).ToList())
                        r.markup = txtMarkup.Text.Trim();
                    changed = true;
                }

                dataGVHotels.ContextMenuStrip = null;
                txtMarkup.Text = string.Empty;
                cmsMarkup.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FillHotels();
            changed = false;
        }

        private void lblShowAll_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dataGVHotels);
        }

        private void dataGVHotels_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell
             .GetFilterStatus(dataGVHotels);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar.Visible = true;
                progressBar.Maximum = 4;
                progressBar.Value = 1;
                AccommodationHandler.ThirdpartyRequest thirdPartyRequest = thirdPartyHandler.GetRequestByDestination(((ComboBoxItem)cbDestinations.SelectedItem).Value.ToString());

                if (thirdPartyRequest == null)
                thirdPartyRequest = new AccommodationHandler.ThirdpartyRequest();
                 
                thirdPartyRequest.boardBasis = string.Join("#", cbBoards.CheckedItems.Cast<string>().ToList());
                thirdPartyRequest.city = ((ComboBoxItem)cbDestinations.SelectedItem).Value.ToString().Split(',').Last();
                thirdPartyRequest.destination = ((ComboBoxItem)cbDestinations.SelectedItem).Value.ToString().Split(',').First();
                thirdPartyRequest.durations = string.Join("#", cbDurations.CheckedItems.Cast<string>().ToList());
                thirdPartyRequest.enabled = cbEnabled.Checked;
                thirdPartyRequest.occupancys = string.Join("#", cbOcupancy.CheckedItems.Cast<ComboBoxItem>().Select(c => c.Value).ToList());
                thirdPartyRequest.searchPeriod = Convert.ToInt32(cbPeriods.SelectedItem);
                thirdPartyRequest.stars = string.Join("#", cbStars.CheckedItems.Cast<string>().ToList());
                progressBar.Value = 2;
                thirdPartyHandler.UpdateRequest(thirdPartyRequest);
                progressBar.Value = 3;
                SaveHotels();
                MessageBox.Show("Changes are saved successfully!", "Save Changes!", MessageBoxButtons.OK);
                progressBar.Value = 4;
                progressBar.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, while saving data. Please check and submit again!", "Save Changes!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void cbShowAll_CheckedChanged(object sender, EventArgs e)
        {
            FillHotels();
        }

        private void cbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDestinations();         
        }

        private void cbDestinations_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillHotels();
            if (cbDestinations.SelectedItem != null)
                FillDataByDestination();
        }        

        private void cmbEnable_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool found = true;

            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                List<string> ids = new List<string>();

                try
                {
                    for (int i = 0; i < dataGVHotels.Rows.Count; i++)
                    {
                        if (dataGVHotels.Rows[i].Cells[7].Selected && found)
                            ids.Add(dataGVHotels.Rows[i].Cells[0].Value.ToString());
                    }
                }
                catch { }

                if (ids.Count > 0)
                {
                    foreach (ThirdPartyDS.HotelsRow r in this.thirdPartyDS.Hotels.Where(h => ids.Any(i => i == h.hotelId)).ToList())
                        r.enabled = (cmbEnable.SelectedItem.ToString() == "YES") ? true : false;
                    changed = true;
                }

                dataGVHotels.ContextMenuStrip = null;
                cmbEnable.Text = "Enable ?";
                cmsEnabled.Visible = false;
           
            }      
        }

        private void cbStars_SelectedIndexChanged(object sender, EventArgs e)
        {
           FillHotels();
        }
    }
}