using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Linq;
using System.Collections;
using DataGridViewAutoFilter;

namespace GulliverII
{
    public partial class flcsThirdPartyHotelSetting : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private string source;
        private AccommodationHandler.ThirdPartyHotelHandler thirdPartyHandler;
        private int dealId;
        private PackageGenerator.PackageHandler packageHandler;

        public flcsThirdPartyHotelSetting(int dealId)
        {
            InitializeComponent();
            this.dealId = dealId;
            this.packageHandler = new PackageGenerator.PackageHandler(false);
            FillSource();
            FillThirdPartyHotels(packageHandler.GetThirdPartyHotelsByDealId(dealId));
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

            List<GulliverLibrary.ThirdPartyHotel> exsistingThirdPartyHotels = packageHandler.GetThirdPartyHotelsByDealId(dealId);
            List<string> hotelIds = exsistingThirdPartyHotels.Select(h => h.accomId.ToString()).ToList();

            List<AccommodationHandler.ThirdPartyHotel> thirdPartyHotels = thirdPartyHandler.GetLiveHotels(search);

            thirdPartyDS.Hotels.Rows.Clear();
            thirdPartyHotels = FilterHotels(thirdPartyHotels);

            foreach (AccommodationHandler.ThirdPartyHotel hotel in thirdPartyHotels)
                this.thirdPartyDS.Hotels.AddHotelsRow(hotel.hotelId, source
                    , hotel.countryCode, hotel.airportCode.Replace("#", ", "), hotel.resort, hotel.hotelname,hotel.supplier, hotel.stars, (hotelIds.Contains(hotel.hotelId) ? true : false), hotel.markup, hotel.availablePrices);
        }

        private void FillThirdPartyHotels(List<GulliverLibrary.ThirdPartyHotel> thirdPartyHotels)
        {
            gulliverIIDS.ThirdPartyHotels.Rows.Clear();
            thirdPartyLiveId.Visible = false;

            foreach (GulliverLibrary.ThirdPartyHotel thirdPartyHotel in thirdPartyHotels)
                gulliverIIDS.ThirdPartyHotels.AddThirdPartyHotelsRow(thirdPartyHotel.id, thirdPartyHotel.accomId, "Delete", thirdPartyHotel.source.Trim(), string.Join(", ", thirdPartyHotel.destination.Split('#')), thirdPartyHotel.hotelname.Trim(), thirdPartyHotel.resort.Trim(), thirdPartyHotel.supplier.Trim(), thirdPartyHotel.stars, thirdPartyHotel.markup);

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
            }

            this.thirdPartyDS.Hotels.Rows.Clear();
        }

        private void FillCountries()
        {
            cbCountries.Items.Clear();
            cbCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cbCountries.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbCountries.AutoCompleteSource = AutoCompleteSource.ListItems;
            Hashtable countries = thirdPartyHandler.GetLiveCountriesBySource(source);
            
            foreach (DictionaryEntry coun in countries.Cast<DictionaryEntry>().OrderBy(c => c.Key))
            {
               ComboBoxItem item = new ComboBoxItem();
               item.Value = coun.Value;
               item.Text = coun.Key.ToString();
               cbCountries.Items.Add(item);                
            }
        }

        private void cbSources_SelectedIndexChanged(object sender, EventArgs e)
        {
           source = ((ComboBoxItem)cbSources.SelectedItem).Value.ToString();
           thirdPartyHandler = new AccommodationHandler.ThirdPartyHotelHandler(source);
           FillCountries();            
        }

        private List<AccommodationHandler.ThirdPartyHotel> FilterHotels(List<AccommodationHandler.ThirdPartyHotel> thirdPartyHotels)
        {
            if (cbStars.CheckedItems.Count > 0)
            {
               List<string> checkedStars = cbStars.CheckedItems.Cast<string>().ToList();
               thirdPartyHotels = thirdPartyHotels.Where(s => checkedStars.Any(c => s.stars.ToString() == c)).ToList();
            }

            if (txtHotelname.Text != string.Empty)
                thirdPartyHotels = thirdPartyHotels.Where(t => t.hotelname.ToUpper().Trim().Contains(txtHotelname.Text.Trim().ToUpper())).ToList();

            return thirdPartyHotels;
        }

        private void cbDestinations_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillHotels();
        }

        private void cbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
           FillDestinations();
        }

        private void cbStars_SelectedIndexChanged(object sender, EventArgs e)
        {
          FillHotels();
        }

        private void dataGVHotels_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

        private void cmbEnable_KeyPress(object sender, KeyPressEventArgs e)
        {
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
                        
                    }

                    dataGVHotels.ContextMenuStrip = null;
                    cmbEnable.Text = "Enable ?";
                    cmsEnabled.Visible = false;

                }
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
                }

                dataGVHotels.ContextMenuStrip = null;
                txtMarkup.Text = string.Empty;
                cmsMarkup.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {                
                progressBar.Visible = true;
                progressBar.Maximum = 4;
                progressBar.Value = 1;
                GulliverLibrary.Deal deal = packageHandler.GetDealById(dealId);

                List<GulliverLibrary.ThirdPartyHotel> thirdPartyHotels = (from h in this.thirdPartyDS.Hotels
                                                                          where h.enabled
                                                                               select new GulliverLibrary.ThirdPartyHotel
                                                                               {
                                                                                   accomId = h.hotelId,
                                                                                   Deal = deal,
                                                                                   destination = h.airportCode.Replace(", ", "#"),
                                                                                   hotelname = h.hotelname,
                                                                                   resort = h.resort,
                                                                                   source = h.source,
                                                                                   stars = h.stars,
                                                                                   supplier = (!h.IssupplierNull())? h.supplier:string.Empty,
                                                                                   markup = h.markup
                                                                               }).ToList();
                progressBar.Value = 3;
                packageHandler.SaveThirdPartyHotels(thirdPartyHotels, dealId, false);
                MessageBox.Show("Hotels are added successfully!", "Update Hotels!", MessageBoxButtons.OK);
                progressBar.Value = 4;
                progressBar.Visible = false;
                FillThirdPartyHotels(packageHandler.GetThirdPartyHotelsByDealId(dealId));
            }
            catch(Exception ex) 
            {
               MessageBox.Show("Error, while saving data. Please check and submit again!", "Save Changes!",MessageBoxButtons.OK,MessageBoxIcon.Error);            
            }
        }

        private void dataGVLiveHotels_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                int selectedCellCount = dataGVLiveHotels.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 1)
                {
                    dataGVLiveHotels.ContextMenuStrip = cmsMarkupII;
                    cmsMarkupII.Visible = true;
                }
                else
                {
                    dataGVLiveHotels.ContextMenuStrip = null;
                    txtMarkupII.Text = string.Empty;
                    cmsMarkupII.Visible = false;
                }
            }
        }

        private void txtMarkupII_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool found = true;

            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                List<string> ids = new List<string>();

                try
                {
                    for (int i = 0; i < dataGVLiveHotels.Rows.Count; i++)
                    {
                        if (dataGVLiveHotels.Rows[i].Cells[9].Selected && found)
                            ids.Add(dataGVLiveHotels.Rows[i].Cells[0].Value.ToString());
                    }
                }
                catch { }

                if (ids.Count > 0)
                {
                    foreach (GulliverIIDS.ThirdPartyHotelsRow r in this.gulliverIIDS.ThirdPartyHotels.Where(h => ids.Any(i => i == h.id.ToString())).ToList())
                        r.Markup = txtMarkupII.Text.Trim();
                }

                dataGVLiveHotels.ContextMenuStrip = null;
                txtMarkupII.Text = string.Empty;
                cmsMarkupII.Visible = false;
            }
        }

        private void lblShowAllII_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dataGVLiveHotels);
        }

        private void dataGVLiveHotels_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell
             .GetFilterStatus(dataGVLiveHotels);
            if (String.IsNullOrEmpty(filterStatus))
            {
                lblShowAllII.Visible = false;
                lblFilterStatusII.Visible = false;
            }
            else
            {
                lblShowAllII.Visible = true;
                lblFilterStatusII.Visible = true;
                lblFilterStatusII.Text = filterStatus;
            }
        }

        private void btnCancelLiveHotels_Click(object sender, EventArgs e)
        {
            FillThirdPartyHotels(packageHandler.GetThirdPartyHotelsByDealId(dealId));
        }

        private void btnSaveLiveHotels_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar.Visible = true;
                progressBar.Maximum = 4;
                progressBar.Value = 1;
                GulliverLibrary.Deal deal = packageHandler.GetDealById(dealId);

                List<GulliverLibrary.ThirdPartyHotel> thirdPartyHotels = (from h in this.gulliverIIDS.ThirdPartyHotels
                                                                          select new GulliverLibrary.ThirdPartyHotel
                                                                          {
                                                                              accomId = h.accomId,
                                                                              Deal = deal,
                                                                              destination = h.Destination .Replace(", ", "#"),
                                                                              hotelname = h.Hotel,
                                                                              resort = h.Resort,
                                                                              source = h.Source,
                                                                              stars = h.Stars,
                                                                              supplier = (!h.IsSupplierNull()) ? h.Supplier : string.Empty,
                                                                              markup = h.Markup
                                                                          }).ToList();
                progressBar.Value = 3;
                packageHandler.SaveThirdPartyHotels(thirdPartyHotels, dealId,true);
                MessageBox.Show("Hotels are saved successfully!", "Update Hotels!", MessageBoxButtons.OK);
                progressBar.Value = 4;
                progressBar.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, while saving data. Please check and submit again!", "Save Changes!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FillHotels();
        }

        private void dataGVLiveHotels_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && dataGVLiveHotels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVLiveHotels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGVLiveHotels.CurrentCell = (dataGVLiveHotels.Rows.Count == 0) ? dataGVLiveHotels.Rows[0].Cells[0] : null;

                switch (MessageBox.Show("This will delete selected hotels - continue?", "Delete Third party hotels", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGVLiveHotels.Rows.Remove((DataGridViewRow)dataGVLiveHotels.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        
        }

        private void txtHotelname_TextChanged(object sender, EventArgs e)
        {
            FillHotels();
        }    
        

       
    }
}