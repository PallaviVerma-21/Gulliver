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
using System.Configuration;

namespace Gulliver
{
    public partial class flcsMain : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private MySqlDataHandler.QueryHandler queryHandler;
        private PackageGenerator.Email email;
        private GulliverLibrary.QueryHandler gulliverQueryHandler;
        private GulliverLibrary.Deal deal;
        private PackageGenerator.PackageHandler packageHandler;
        private LandingPageHandler.DataProcessor dataProcessor;
        private PackageGenerator.ICosting icosting;
        private List<int> selectedTripperExtras;
        List<GulliverLibrary.Package> packages;
        int supplierId = 0;
        int dealId = 0;
        private List<string> visibleColumns;
        private List<GulliverLibrary.DealOptionalExtra> optionalCostings;
        private bool cancel = false;
        private bool save = false;
        
        public flcsMain()
        {
           InitializeComponent();
           email = new PackageGenerator.Email();
           queryHandler = new MySqlDataHandler.QueryHandler();
           gulliverQueryHandler = new GulliverLibrary.QueryHandler();
           packageHandler = new PackageGenerator.PackageHandler();
           dataProcessor = new LandingPageHandler.DataProcessor();
           visibleColumns = new List<string>();
           SetupDefaultWindow(true);          
           FillMedias(0);
           FillDealType(0);
        }        

        public flcsMain(int id)
        {
            InitializeComponent();
            queryHandler = new MySqlDataHandler.QueryHandler();
            gulliverQueryHandler = new GulliverLibrary.QueryHandler();
            packageHandler = new PackageGenerator.PackageHandler();
            dataProcessor = new LandingPageHandler.DataProcessor();
            dealId = id;
            visibleColumns = new List<string>();
            SetupDefaultWindow(false);            
            FillDeal(id);
            DisplayDefaultColumns();
            
        }


        #region set methods

        private void SetupDefaultWindow(bool newDeal)
        {
            FillAllAirports();
            FillDepAirports();
            FillOccupancy();
            FillBoardBasis();
            FillCurrecncys();
            FillFlightSuppliers();
            FillCurrencyComboBox();
            
            selectedTripperExtras = new List<int>();
            deal = new GulliverLibrary.Deal();
            optionalCostings = new List<GulliverLibrary.DealOptionalExtra>();
            SetText(txtSearchbox, "Search Transfers here ...");
            
            if (newDeal)
            {
               FillTripperExtras(new List<int>(), string.Empty);
               btnUpdateDeal.Visible = false;
               List<GulliverLibrary.Extra> extras = new List<GulliverLibrary.Extra>();
               GulliverLibrary.Extra extra = new GulliverLibrary.Extra();
               extra.description = "transfers";
               extra.isIncluded = false;
               extra.adultPrice = 0;
               extra.childPrice = 0;
               extras.Add(extra); 
               FillExtras(extras);
            }

            this.tabMain.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabMain.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);

            this.tabControl3.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl3.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl3_DrawItem);

            this.tabControl2.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl2_DrawItem);

            tabMain.TabPages.Remove(tabPage14);
        }

        private void SetText(TextBox txtSearchbox, string text)
        {
            txtSearchbox.Text = text;
            txtSearchbox.ForeColor = Color.Gray;
        }

        private void VisibleAndInvisiblePackageColumns()
        {
            if (cbPackageColumns.SelectedItem.ToString() == "ALL")
            {
                for (int i = 0; i <= cbPackageColumns.Items.Count - 1; i++)
                    cbPackageColumns.SetItemCheckState(i, (cbPackageColumns.GetItemChecked(cbPackageColumns.SelectedIndex) ? CheckState.Checked : CheckState.Unchecked));

                if (cbPackageColumns.CheckedItems.Count == 0)
                    dataGridViewHolidays.Columns.Cast<DataGridViewColumn>().ToList().ForEach(r => r.Visible = false);

                foreach (string column in cbPackageColumns.CheckedItems)
                {
                    DataGridViewColumn dataGridViewColumn = dataGridViewHolidays.Columns.Cast<DataGridViewColumn>().SingleOrDefault(c => c.HeaderText.ToString() == column);
                    if (dataGridViewColumn != null)
                        dataGridViewColumn.Visible = true;
                }
            }
            else
            {
                if (cbPackageColumns.GetItemChecked(cbPackageColumns.SelectedIndex))
                    dataGridViewHolidays.Columns.Cast<DataGridViewColumn>().SingleOrDefault(c => c.HeaderText.ToString() == cbPackageColumns.SelectedItem.ToString()).Visible = true;
                else
                    dataGridViewHolidays.Columns.Cast<DataGridViewColumn>().SingleOrDefault(c => c.HeaderText.ToString() == cbPackageColumns.SelectedItem.ToString()).Visible = false;
            }
        }

        private void EnableDisableCommission(bool visibile)
        {
            lblCommision.Visible = visibile;
            txtCommission.Visible = visibile;
            lblPercentage.Visible = visibile;
        }

        private void DisplaySECostings(bool visible)
        {
            if (visible)
            {
                if (!tabCostings.TabPages.Contains(secretEscapeTP))
                    tabCostings.TabPages.Add(secretEscapeTP);
            }
            else
                tabCostings.TabPages.Remove(secretEscapeTP);
        }

        private void DisplayDurationCostings(bool visible)
        {
            if (visible)
            {
                if (!tabCostings.TabPages.Contains(durationCostTP))
                    tabCostings.TabPages.Add(durationCostTP);
            }
            else
                tabCostings.TabPages.Remove(durationCostTP);

            if (deal != null && deal.DurationCostings != null && deal.DurationCostings.Count > 0)
                FillDurationCostings(deal.DurationCostings.ToList());
            else
                FillDefaultDurationCostings();
        }

        private void EnableDisableChildPrices(List<string> selectedOcupancys)
        {
            bool enableChildrenAges = false;
            bool enableInfantAges = false;

            foreach (string occupancy in selectedOcupancys)
            {
                string[] occupncyArray = occupancy.Split(',').ToArray();

                if (occupncyArray[1].ToString() != "0")
                    enableChildrenAges = true;


                if (occupncyArray[2].ToString() != "0")
                    enableInfantAges = true;
            }

            EnableChildrenAges(enableChildrenAges);
            EnableInfantAges(enableInfantAges);
        }

        private void EnableChildrenAges(bool enable)
        {
            txtChildAgeTo.Enabled = enable;
            txtChildAgeFrom.Enabled = enable;
        }

        private void EnableInfantAges(bool enable)
        {
            txtInfantAgeFrom.Enabled = enable;
            txtInfantAgeTo.Enabled = enable;
        }

        private void VisibleColumns()
        {
            foreach (DataGridViewColumn column in dataGridViewHolidays.Columns.Cast<DataGridViewColumn>())
            {
                if (!visibleColumns.Contains(column.DataPropertyName))
                    column.Visible = false;
                else
                    column.Visible = true;
            }
        }
        
        private void SetStepProgressBar(ProgressBar progressBar)
        {
            progressBar.Value++;
            System.Threading.Thread.Sleep(500);
        }

        private void VisibleProgressBar(ProgressBar progressBar, bool visible)
        {
            progressBar.Visible = visible;

            if (visible)
            {
                progressBar.Value = 10;
                progressBar.Step = 10;
                SetStepProgressBar(progressBar);
                System.Threading.Thread.Sleep(1000);
            }
        }

        #endregion
        
        #region events

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font tabFont;
            Brush backBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080")); //Set background color
            Brush foreBrush = new SolidBrush(Color.White);//Set foreground color

            if (e.Index == this.tabMain.SelectedIndex)
            {
                tabFont = new Font("Calibri", 14, FontStyle.Bold, GraphicsUnit.Pixel);
                backBrush = new SolidBrush(Color.White);
                foreBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080"));
            }
            else
                tabFont = new Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            
            string tabName = this.tabMain.TabPages[e.Index].Text;
            this.tabMain.TabPages[e.Index].Width = 200;
            this.tabMain.TabPages[e.Index].Height = 100;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            Rectangle r = e.Bounds;
            r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height);
            e.Graphics.DrawString(tabName, tabFont, foreBrush, r, sf);
            //Dispose objects
            sf.Dispose();
           
            if (e.Index == this.tabMain.SelectedIndex)
            {
               tabFont.Dispose();
               backBrush.Dispose();
            }
            else
            {
              backBrush.Dispose();
              foreBrush.Dispose();
            }
        }

        private void tabControl3_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font tabFont;
            Brush backBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#73A2DB"));
            Brush foreBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080")); 

            if (e.Index == this.tabControl3.SelectedIndex)
            {
                tabFont = new Font("Segoe UI", 11, FontStyle.Bold, GraphicsUnit.Pixel);
                backBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#73A2DB"));
                foreBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080"));
            }
            else
                tabFont = new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Pixel);

            string tabName = this.tabControl3.TabPages[e.Index].Text;
           
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            Rectangle r = e.Bounds;
            r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height);
            e.Graphics.DrawString(tabName, tabFont, foreBrush, r, sf);
            //Dispose objects
            sf.Dispose();
           
            if (e.Index == this.tabControl3.SelectedIndex)
            {
               tabFont.Dispose();
               backBrush.Dispose();
            }
            else
            {
               backBrush.Dispose();
               foreBrush.Dispose();
            }
        }

        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font tabFont;
            Brush backBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#73A2DB"));
            Brush foreBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080"));

            if (e.Index == this.tabControl2.SelectedIndex)
            {
                tabFont = new Font("Segoe UI", 11, FontStyle.Bold, GraphicsUnit.Pixel);
                backBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#73A2DB"));
                foreBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080"));
            }
            else
                tabFont = new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Pixel);

            string tabName = this.tabControl2.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            Rectangle r = e.Bounds;
            r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height);
            e.Graphics.DrawString(tabName, tabFont, foreBrush, r, sf);
            //Dispose objects
            sf.Dispose();

            if (e.Index == this.tabControl2.SelectedIndex)
            {
              tabFont.Dispose();
              backBrush.Dispose();
            }
            else
            {
              backBrush.Dispose();
              foreBrush.Dispose();
            }
        }               

        private void ddlResorts_SelectedIndexChanged(object sender, EventArgs e)
        {
          FillHotels(((ComboBoxItem)ddlResorts.SelectedItem).Value.ToString(), string.Empty);
        }

        private void ddlAirports_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           FillResorts();           
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            List<int> ids = new List<int>();

            if(dataGridviewContracts.SelectedRows != null && dataGridviewContracts.SelectedRows.Count > 0)
            {
               foreach( DataGridViewRow r in dataGridviewContracts.SelectedRows)
                ids.Add(Convert.ToInt32(r.Cells[1].Value.ToString()));

               List<MySqlDataHandler.AcCGuiD> contracts = packageHandler.GetAccomGuidByRecNos(ids);
               FillOfferContracts(contracts);
            }            
        }
        
        private void btnNext_Click(object sender, EventArgs e)
        {
           SaveOfferDetails();
            if(cbCruiseDeal.Checked)
                tabMain.SelectedTab = tabPage3;
            else
               tabMain.SelectedTab = tabPage2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnFlightBack_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabPage1;
        }

        private void bntFlightNext_Click(object sender, EventArgs e)
        {
            VisibleProgressBar(progressBarTP2, true);
            SaveFlights();
            VisibleProgressBar(progressBarTP2, false);
            tabMain.SelectedTab = tabPage3;
        }

        private void btnUpdateDeal_Click(object sender, EventArgs e)
        {
            VisibleProgressBar(progressBarTP4, true);
            SetStepProgressBar(progressBarTP4);
            SaveOfferDetails();
            SetStepProgressBar(progressBarTP4);
            SaveExtra();
            SetStepProgressBar(progressBarTP4);
            SaveFlights();
            SetStepProgressBar(progressBarTP4);
            SaveCostings();
            if(PackageGenerator.Tool.GetSuppliersBySuppliertype("setypesuppliers").Contains(deal.Media.id))
                SaveSecretEscapeCostings();

            SetStepProgressBar(progressBarTP4);
            CompareHolidays(deal);
            VisibleProgressBar(progressBarTP4, false);
        }

        private void cbAllAirlines_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbAirlines.Items.Count - 1; i++)
                cbAirlines.SetItemCheckState(i, (cbAllAirlines.Checked ? CheckState.Checked : CheckState.Unchecked));
        }

        private void btnExtraBack_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabPage2;
        }

        private void btnExtraNext_Click(object sender, EventArgs e)
        {
            VisibleProgressBar(progressBarTP3, true);
            SaveExtras();
            VisibleProgressBar(progressBarTP3, false);
            tabMain.SelectedTab = tabPage4;
        }

        private void btnCostingNext_Click(object sender, EventArgs e)
        {
            VisibleProgressBar(progressBarTP4, true);
            SetStepProgressBar(progressBarTP4);
            SaveOfferDetails();
            SetStepProgressBar(progressBarTP4);
            SaveExtra();
            SetStepProgressBar(progressBarTP4);
            SaveFlights();
            SetStepProgressBar(progressBarTP4);
            SaveCostings();
            if (PackageGenerator.Tool.GetSuppliersBySuppliertype("setypesuppliers").Contains(deal.Media.id))
                SaveSecretEscapeCostings();
            SetStepProgressBar(progressBarTP4);
            SearchedHolidays(deal);
            VisibleProgressBar(progressBarTP4, false);
            btnUpdateDeal.Visible = true;
            //tabMain.TabPages.Add(tabPage5);
        }

        private void btnCostingBack_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabPage3;
        }
        
        private void ddlMedias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMedias.SelectedItem != null)
            {
                int mediaId = Convert.ToInt32(((ComboBoxItem)ddlMedias.SelectedItem).Value);
                GulliverLibrary.Media media = packageHandler.GetMediaById(mediaId);
                if(media != null)
                txtCommission.Text = (media.commission != 0) ? media.commission.ToString() : string.Empty;

                if (PackageGenerator.Tool.GetSuppliersBySuppliertype("setypesuppliers").Contains(mediaId) || PackageGenerator.Tool.GetSuppliersBySuppliertype("sagasuppliers").Contains(mediaId))
                {
                    DisplaySECostings(true);
                    DisplayDurationCostings(false);
                    EnableDisableCommission(true);
                }
                else if (PackageGenerator.Tool.GetSuppliersBySuppliertype("traveltypesuppliers").Contains(mediaId))
                {
                    DisplaySECostings(false);
                    DisplayDurationCostings(true);
                    EnableDisableCommission(false);
                }
                else if (PackageGenerator.Tool.GetSuppliersBySuppliertype("timestypesuppliers").Contains(mediaId) || PackageGenerator.Tool.GetSuppliersBySuppliertype("widjectsuppliers").Contains(mediaId))
                {
                    DisplaySECostings(false);
                    DisplayDurationCostings(true);
                    EnableDisableCommission(true);
                }
                else
                {
                    DisplaySECostings(false);
                    DisplayDurationCostings(true);
                    EnableDisableCommission(false);
                }
            }
        }

        private void cbOcupancy_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableChildPrices(cbOcupancy.CheckedItems.Cast<ComboBoxItem>().Select(i => i.Value.ToString()).ToList());
            FillOccupancyComboBox(cbOcupancy.CheckedItems.Cast<ComboBoxItem>().Select(i => i.Value.ToString()).ToList());
        }

        private void txtLongitude_TextChanged(object sender, EventArgs e)
        {
            if (txtLongitude.Text.Trim() != string.Empty && txtLatitude.Text.Trim() != string.Empty)
            {
                GulliverLibrary.HotelInformation hotelInformation = packageHandler.GetHotelInformationByGeoCodes(txtLongitude.Text, txtLatitude.Text);

                if (hotelInformation != null)
                    FillHotelInformation(hotelInformation);
            }
        }

        private void txtLatitude_TextChanged(object sender, EventArgs e)
        {
            GulliverLibrary.HotelInformation hotelInformation = packageHandler.GetHotelInformationByGeoCodes(txtLongitude.Text, txtLatitude.Text);

            if (hotelInformation != null)
            {
                if (deal.DealInformation == null)
                    deal.DealInformation = new GulliverLibrary.DealInformation();

                deal.DealInformation.HotelInformation = hotelInformation;
                FillHotelInformation(hotelInformation);
            }
        } 

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                string id = dataGridviewOfferContracts.SelectedCells[1].Value.ToString();
                flcsRoomRequestSetting roomRequestSettingForm = new flcsRoomRequestSetting(id, dealId);
                roomRequestSettingForm.ShowDialog();
            }
            catch { }
        }

        private void entryRoomOnly_CheckedChanged(object sender, EventArgs e)
        {
            List<GulliverLibrary.Package> packages = deal.Packages.ToList();

            if (entryRoomOnly.Checked)
            {
                List<GulliverLibrary.HotelContract> entryRoomContracts = packageHandler.GetEntryRoomContract(dealId);

                if (entryRoomContracts != null && entryRoomContracts.Count > 0)
                {
                    List<string> hotelKeys = entryRoomContracts.Select(h => h.apt.Trim() + ":" + h.resort.Trim() + ":" + h.accomcode.Trim() + ":" + h.codename.Trim()).ToList();
                    packages = deal.Packages.Where(p => hotelKeys.Contains(p.hotelKey)).ToList();
                }
            }

            FillPackages(packages);
        }

        private void showAllLabelH_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dataGridViewHolidays);
        }

        private void dataGridViewHolidays_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            String filterStatus = DataGridViewAutoFilterColumnHeaderCell
              .GetFilterStatus(dataGridViewHolidays);
            if (String.IsNullOrEmpty(filterStatus))
            {
                showAllLabelH.Visible = false;
                fiterStatusLabelH.Visible = false;
            }
            else
            {
                showAllLabelH.Visible = true;
                fiterStatusLabelH.Visible = true;
                fiterStatusLabelH.Text = filterStatus;
            }
        }

        private void btnSavePage_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblMessage.Visible = false;

            if (cmbCurrency.SelectedItem == null || cmbLanuages.SelectedItem == null)
            {
                MessageBox.Show("Please select deal currency and lanuage before save!");
                return;
            }

            try
            {
                if (deal.DealInformation == null)
                {
                    deal.DealInformation = new GulliverLibrary.DealInformation();
                    if (txtLongitude.Text.Trim() == string.Empty || txtLatitude.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Please enter GEO codes before save!");
                        return;
                    }
                    deal.DealInformation.HotelInformation = packageHandler.GetHotelInformationByGeoCodes(txtLongitude.Text, txtLatitude.Text);                    
                }

                deal.DealInformation.mainHeader = txtMainHeader.Text.Trim();
                deal.DealInformation.Deal = deal;
                deal.DealInformation.subHeader = txtSubHeader.Text.Trim();
                deal.DealInformation.longitude = txtLongitude.Text.Trim();
                deal.DealInformation.latitude = txtLatitude.Text.Trim();
                deal.DealInformation.HotelInformation.hotelHeader = txtHotelTitle.Text.Trim();
                deal.DealInformation.youTubeLink = txtYouTubeLink.Text.Trim();
                deal.DealInformation.introduction = txtDealIntro.Text.Trim();
                deal.DealInformation.childPrices = txtChildPrice.Text.Trim();
                deal.DealInformation.optionalExtras = txtOptionalExtras.Text.Trim();
                deal.DealInformation.pleaseNote = txtPleasenote.Text.Trim();
                deal.DealInformation.HotelInformation.hotelBodyText = txtHotelText.Text.Trim();
                deal.DealInformation.HotelInformation.destinationText = txtDestinationText.Text.Trim();
                deal.DealInformation.HotelInformation.countryText = txtCountryText.Text.Trim();
                deal.DealInformation.tripAdvisorLink = txtTripAdvisorLink.Text.Trim();
                deal.DealInformation.howToBook = txtHowToBook.Text.Trim();
                //deal.DealInformation.dealActive = cbActiveOnLuxuryWebsite.Checked;

                deal.DealInformation.HotelInformation.accessibility = txtAccessibilityText.Text;
                deal.DealInformation.HotelInformation.keyInformation = txtKeyInformationText.Text;
                deal.DealInformation.dealCurrency = cmbCurrency.SelectedItem.ToString();
                deal.DealInformation.language = cmbLanuages.SelectedItem.ToString();
                deal.DealInformation.pageName = txtPageName.Text.Trim();
                deal.DealInformation.leadPrice = txtLeadPrice.Text.Trim();
                deal.DealInformation.bestDealHeader = txtBestDealHeader.Text.Trim();
                deal.DealInformation.bestDealDescription = txtBestDealDescription.Text.Trim();
                deal.DealInformation.HotelInformation.destinationHeader = txtDestinationTitle.Text.Trim();
                deal.DealInformation.HotelInformation.countryHeader = txtCountryTitle.Text.Trim();
                deal.DealInformation.brand = (ddlBrand.SelectedItem != null) ? ddlBrand.SelectedItem.ToString() : string.Empty;
                deal.DealInformation.topHeader = txtTopHeader.Text.Trim();
                deal.DealInformation.defaultDuration = (ddlDurations.SelectedItem != null && ddlDurations.SelectedItem != string.Empty) ? Convert.ToInt32(ddlDurations.SelectedItem) : 0;
                deal.DealInformation.diplayNightsOrDays = (rbDays.Checked) ? "Days" : "Nights";
                deal.DealInformation.priority = Convert.ToInt32(ddlPriorities.SelectedItem);
                deal.DealInformation.goLiveOnBestDealPage = cbGoLiveOnBestDealPage.Checked;

                List<GulliverLibrary.Image> dealImages = new List<GulliverLibrary.Image>();
                foreach (GulliverDS.ImageRow imageRow in this.gulliverDS.Image)
                {
                    if ((imageRow.Reference != null) && (imageRow.Title != null))
                    {
                        try
                        {
                            GulliverLibrary.Image dealImage = new GulliverLibrary.Image();
                            dealImage.id = imageRow.id;
                            dealImage.Deal = deal;
                            dealImage.reference = (imageRow.Reference != null) ? imageRow.Reference : string.Empty;
                            dealImage.altText = (imageRow.Alt_Text != null) ? imageRow.Alt_Text : string.Empty;
                            dealImage.description = (imageRow.Description != null) ? imageRow.Description : string.Empty;
                            dealImage.title = (imageRow.Title != null) ? imageRow.Title : string.Empty;
                            dealImages.Add(dealImage);
                        }
                        catch { }
                    }
                }


                List<GulliverLibrary.Review> dealReviews = new List<GulliverLibrary.Review>();

                foreach (GulliverDS.ReviewRow reviewRow in this.gulliverDS.Review)
                {
                    try
                    {
                        GulliverLibrary.Review review = new GulliverLibrary.Review();
                        review.id = reviewRow.id;
                        review.Deal = deal;
                        review.date = reviewRow.Date;
                        review.source = (reviewRow.Source != null) ? reviewRow.Source : string.Empty;
                        review.stars = (reviewRow.Stars != null) ? reviewRow.Stars : 0;
                        review.link = (reviewRow.Link != null) ? reviewRow.Link : string.Empty;
                        review.text = (reviewRow.Text != null) ? reviewRow.Text : string.Empty;
                        review.title = (reviewRow.Title != null) ? reviewRow.Title : string.Empty;
                        dealReviews.Add(review);
                    }
                    catch { }
                }

                deal.DealImages = dealImages;
                deal.DealReviews = dealReviews;
        
                if (deal.id != 0)
                packageHandler.SaveDealInformation(deal);                   
                else
                lblError.Text = "Please save the deal before add any deal information for page!";

                lblMessage.Visible = true;
            }
            catch
            {
                lblError.Visible = true;
            }
        }

        private void btnCancelPage_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("This will not save all changes - continue?", "Save Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    this.Close();
                    break;

                case System.Windows.Forms.DialogResult.No:
                    break;
            }
        }

        private void btnOptionalcostings_Click(object sender, EventArgs e)
        {
            flcsOptionalExtra objOptionalExtras = new flcsOptionalExtra(optionalCostings, deal.id, packageHandler);
            objOptionalExtras.ShowDialog();
        }

        private void includesZoomout_Click(object sender, EventArgs e)
        {
            flcsZoomOut zoomout = new flcsZoomOut(txtDealIntro.Text.Trim(), "What is included");
            zoomout.ShowDialog();
            txtDealIntro.Text = zoomout.text.Trim();
        }

        private void toolChildPrices_Click(object sender, EventArgs e)
        {
            flcsZoomOut zoomout = new flcsZoomOut(txtChildPrice.Text.Trim(), "Child Price");
            zoomout.ShowDialog();
            txtChildPrice.Text = zoomout.text.Trim();
        }

        private void toolOptionalExtras_Click(object sender, EventArgs e)
        {
            flcsZoomOut zoomout = new flcsZoomOut(txtOptionalExtras.Text.Trim(), "Optional Extras");
            zoomout.ShowDialog();
            txtOptionalExtras.Text = zoomout.text.Trim();
        }

        private void toolPleasenote_Click(object sender, EventArgs e)
        {
            flcsZoomOut zoomout = new flcsZoomOut(txtPleasenote.Text.Trim(), "Please Note");
            zoomout.ShowDialog();
            txtPleasenote.Text = zoomout.text.Trim();
        }

        private void toolHotelText_Click(object sender, EventArgs e)
        {
            flcsZoomOut zoomout = new flcsZoomOut(txtHotelText.Text.Trim(), "Hotel Text");
            zoomout.ShowDialog();
            txtHotelText.Text = zoomout.text.Trim();
        }

        private void toolDestinationText_Click(object sender, EventArgs e)
        {
            flcsZoomOut zoomout = new flcsZoomOut(txtDestinationText.Text.Trim(), "Destination Text");
            zoomout.ShowDialog();
            txtDestinationText.Text = zoomout.text.Trim();
        }

        private void toolCountryText_Click(object sender, EventArgs e)
        {
            flcsZoomOut zoomout = new flcsZoomOut(txtCountryText.Text.Trim(), "Country Text");
            zoomout.ShowDialog();
            txtCountryText.Text = zoomout.text.Trim();
        }

        private void toolKeyInformationText_Click(object sender, EventArgs e)
        {
            flcsZoomOut zoomout = new flcsZoomOut(txtKeyInformationText.Text.Trim(), "Key Information");
            zoomout.ShowDialog();
            txtKeyInformationText.Text = zoomout.text.Trim();
        }

        private void toolAccessibilityText_Click(object sender, EventArgs e)
        {
            flcsZoomOut zoomout = new flcsZoomOut(txtAccessibilityText.Text.Trim(), "Accessibility");
            zoomout.ShowDialog();
            txtAccessibilityText.Text = zoomout.text.Trim();
        }

        private void btnUpdateFleetwayPage_Click(object sender, EventArgs e)
        {
            if (deal.id != 0)
            {
                string message = dataProcessor.UpdateFleetwayPage(deal, cbAirportByAvailability.Checked, false);
                if (message != string.Empty)
                {
                    lblError.Visible = true;
                    lblError.Text = message;
                }
                else
                {
                    string url = ConfigurationManager.AppSettings["fleetwaydraftPageURL"].ToString() + deal.DealInformation.pageName.Trim() + ".php";
                    System.Diagnostics.Process.Start(url);
                    this.Focus();
                }
            }
            else
                MessageBox.Show("Please save the offer before you genarate any page for Fleetway website!");
        }

        private void toCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBarMenu.Visible = true;
            Application.DoEvents();
            progressBarMenu.Maximum = 4;
            progressBarMenu.Value = 1;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV Documents (*.CSV)|*.csv";
            sfd.FileName = txtDealName.Text.Trim() + ".csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                progressBarMenu.Value++;
                icosting = PackageGenerator.FactoryCosting.GetCostingOBj(supplierId, dealId);
                List<GulliverLibrary.Package> updatedPackages = packageHandler.GetPackagesByDeal(dealId);
                icosting.WriteToCSV(updatedPackages, sfd.FileName);
                progressBarMenu.Value++;
                KryptonMessageBox.Show("Export to " + sfd.FileName, "Export Files");
            }

            progressBarMenu.Value++;
            progressBarMenu.Visible = false;
            Application.DoEvents();
        }

        private void toExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBarMenu.Visible = true;
            Application.DoEvents();
            progressBarMenu.Maximum = 10;
            progressBarMenu.Step = 1;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xlsx)|*.XLSx";
            sfd.FileName = txtDealName.Text.Trim() + ".xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                progressBarMenu.PerformStep();
                icosting = PackageGenerator.FactoryCosting.GetCostingOBj(supplierId, dealId);
                List<GulliverLibrary.Package> updatedPackages = packageHandler.GetPackagesByDeal(dealId);
                icosting.WriteToExcel(updatedPackages, sfd.FileName, progressBarMenu);
                progressBarMenu.PerformStep();
                KryptonMessageBox.Show("Export to " + sfd.FileName, "Export Files");
            }

            progressBarMenu.Step = progressBar.Maximum;
            progressBarMenu.Visible = false;
            Application.DoEvents();
        }

        private void restoreFlightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsBackupPackage objBackupPackage = new flcsBackupPackage(dealId, deal.PackageBackups.ToList(), packageHandler);
            objBackupPackage.ShowDialog();
        }

        private void manipulateDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsFilterColumns objFilterColumn = new flcsFilterColumns(this.packagesDS.PackageBackup.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList(), visibleColumns);
            objFilterColumn.ShowDialog();
            visibleColumns = objFilterColumn.visibleColumns;
            VisibleColumns();
        }

        private void setLeadingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsSetLeading setLeadings = new flcsSetLeading(dealId, packageHandler);
            setLeadings.ShowDialog();
        }

        private void cbAllUS_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbUSAirports.Items.Count - 1; i++)
                cbUSAirports.SetItemCheckState(i, (cbAllUS.Checked ? CheckState.Checked : CheckState.Unchecked));
        }

        private void cbAllGerman_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbGermanAirports.Items.Count - 1; i++)
                cbGermanAirports.SetItemCheckState(i, (cbAllGerman.Checked ? CheckState.Checked : CheckState.Unchecked));
        }

        private void cbAllAirports_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbDepartureAirports.Items.Count - 1; i++)
                cbDepartureAirports.SetItemCheckState(i, (cbAllAirports.Checked ? CheckState.Checked : CheckState.Unchecked));
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
                SetText(txtSearchbox, "Search Transfers here ...");
        }    

        private void txtSearchbox_TextChanged(object sender, EventArgs e)
        {
            FillTripperExtras(selectedTripperExtras, txtSearchbox.Text.ToUpper().Trim());
        }

        private void txtSearhHotelContract_Enter(object sender, EventArgs e)
        {
            if (txtSearhHotelContract.ForeColor != Color.Black)
            {
                txtSearhHotelContract.Text = string.Empty;
                txtSearhHotelContract.ForeColor = Color.Black;
            }
        }

        private void txtSearhHotelContract_Leave(object sender, EventArgs e)
        {
            if (txtSearhHotelContract.Text.Trim() == string.Empty && ddlResorts.Items != null && ddlResorts.Items.Count > 0)
                SetText(txtSearhHotelContract, "Search Contracts here ...");
        }

        private void txtSearhHotelContract_TextChanged(object sender, EventArgs e)
        {
            if (ddlResorts.SelectedItem != null)
                FillHotels(((ComboBoxItem)ddlResorts.SelectedItem).Value.ToString(), txtSearhHotelContract.Text.ToUpper().Trim());
        }

        private void cbCruiseDeal_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCruiseDeal.Checked)
            {
                tabMain.TabPages.Remove(tabPage2);
                tabMain.TabPages.Insert(1, tabPage14);
            }
            else
            {
                tabMain.TabPages.Insert(1, tabPage2);
                tabMain.TabPages.Remove(tabPage14);
            }
        }

        private void autoUpdateSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsAutoUpdateSetting autoUpdateSetting = new flcsAutoUpdateSetting(dealId);
            autoUpdateSetting.Show();
        }

        private void cbAllDurations_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbDurations.Items.Count - 1; i++)
                cbDurations.SetItemCheckState(i, (cbAllDurations.Checked ? CheckState.Checked : CheckState.Unchecked));
        }

        private void cbAllWeekDays_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbWeekDays.Items.Count - 1; i++)
                cbWeekDays.SetItemCheckState(i, (cbAllWeekDays.Checked ? CheckState.Checked : CheckState.Unchecked));
        }

        private void cbDurations_SelectedValueChanged(object sender, EventArgs e)
        {
            List<string> durations = cbDurations.CheckedItems.Cast<string>().ToList();
            FillDurationComboBox(durations);
        }

        private void btnPackageColumnShow_MouseClick(object sender, MouseEventArgs e)
        {
            cbPackageColumns.Visible = true;
        }

        private void dataGridViewHolidays_Click(object sender, EventArgs e)
        {
            cbPackageColumns.Visible = false;
        }

        private void cbPackageColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            VisibleAndInvisiblePackageColumns();
        }               

        #endregion                     

        #region FillData

        private void FillDeal(int id)
        {
            deal = packageHandler.GetDealById(id);

            if (deal != null)
            {
                //tab 1
                txtDealName.Text = deal.name.Trim();
                FillMedias(deal.Media.id);
                FillDealType(deal.DealType.id);
                supplierId = deal.Media.id;
                cmbProducttypes.SelectedItem = (deal.productType != null && deal.productType != string.Empty) ? deal.productType : string.Empty;
                dtpSalesOn.Value = deal.dateOfPromotion;
                dtpBookBy.Value = deal.endDateOfPromotion;
                txtCommission.Text = (deal.commission != null) ? deal.commission.ToString() : "0";
                lblOfferCreatedBy.Text = (deal.createdBy != null) ? "Created by " + deal.createdBy : string.Empty;
                lblOfferCreatedBy.Visible = true;
                cbCruiseDeal.Checked = deal.cruiseDeal;

                if (deal.lastUpdatedBy != null && deal.lastUpdatedBy != string.Empty)
                {
                    lblOfferLastCreatedBy.Text = "Last updated by " + deal.lastUpdatedBy;
                    lblOfferLastCreatedBy.Visible = true;
                }

                numericStars.Value = deal.starRating;
                txtDealCode.Text = (deal.dealCode != null) ? deal.dealCode.Trim() : string.Empty;
                FillSelectedBoardBasis(deal.boards.Trim());
                FillSelectedOccupancy(deal.occupancy);
                FillOccupancyComboBox(deal.occupancy.Split('#').ToList());
                FillDurationComboBox(deal.durations.Split('#').Select(i => i).ToList());

                if (deal.ChildrenAge != null)
                {
                    txtChildAgeFrom.Text = deal.ChildrenAge.childAgeFrom.ToString();
                    txtChildAgeTo.Text = deal.ChildrenAge.childAgeTo.ToString();
                    txtInfantAgeFrom.Text = deal.ChildrenAge.infantAgeFrom.ToString();
                    txtInfantAgeTo.Text = deal.ChildrenAge.infantAgeTo.ToString();
                }

                FillHotelContracts(deal.HotelContracts.ToList());

                //tab 2
                txtArrivalAirports.Text = deal.arrivalAirports.Trim();
                dtpStartDate.Value = deal.startDate.Date;
                dtpEndDate.Value = deal.endDate.Date;
                FillSelectedDepartureAirports(deal.departureAirports.Split('#').ToList());
                FillSelectedDurations(deal.durations.Split('#').ToList());
                FillSelectedWeekDays(deal.filteredWeekdays.Split('#').ToList());
                FillSelectdAirlines(deal.filteredAirlines.Split('#').ToList());
                FillDepartureAirportComboBox(deal.departureAirports.Split('#').ToList().Distinct().ToList());
                FillDestinationAirportComboBox(deal.arrivalAirports.Split('#').ToList());

                cbSelectCheapestFromAllFlighttypes.Checked = deal.selectCheapestFromFlightTypes;
                cbFlightTypes.SetItemChecked(0, deal.isFAB);
                cbFlightTypes.SetItemChecked(1, deal.isFlightSheet);
                cbFlightTypes.SetItemChecked(2, deal.isAmadeus);
                rbnBGPP.Checked = (deal.baggageType.Trim() == "1");
                rbnBGTwo.Checked = (deal.baggageType.Trim() == "2");
                rbnNoBaggage.Checked = (deal.baggageType.Trim() == "0");
                ftOBDepartureFrom.Text = deal.filteredFlightTimesOBDeparture.Split('#').First().Trim();
                ftOBDepartureTo.Text = deal.filteredFlightTimesOBDeparture.Split('#').Last().Trim();
                ftOBArrivalFrom.Text = deal.filteredFlightTimesOBArrival.Split('#').First().Trim();
                ftOBArrivalTo.Text = deal.filteredFlightTimesOBArrival.Split('#').Last().Trim();
                ftIBDepartureFrom.Text = deal.filteredFlightTimesIBDeparture.Split('#').First().Trim();
                ftIBDepartureTo.Text = deal.filteredFlightTimesIBDeparture.Split('#').Last().Trim();
                ftIBArrivalFrom.Text = deal.filteredFlightTimesIBArrival.Split('#').First().Trim();
                ftIBArrivalTo.Text = deal.filteredFlightTimesIBArrival.Split('#').Last().Trim();

                //tab 3
                selectedTripperExtras = new List<int>();
                if (deal.TripperExtras != null && deal.TripperExtras.Count > 0)
                    selectedTripperExtras = deal.TripperExtras.Select(i => i.recno).ToList();

                FillTripperExtras(selectedTripperExtras, string.Empty);
                FillExtras(deal.Extras.ToList());
                FillCarParking(deal.CarParkings.ToList());
                FillCarHire(deal.CarHires.ToList());

                //tab 4

                txtBaseMarkup.Text = deal.baseMarkup.ToString();
                if (PackageGenerator.Tool.GetSuppliersBySuppliertype("traveltypesuppliers").Contains(deal.Media.id) || PackageGenerator.Tool.GetSuppliersBySuppliertype("timestypesuppliers").Contains(deal.Media.id) || PackageGenerator.Tool.GetSuppliersBySuppliertype("widjectsuppliers").Contains(deal.Media.id))
                    FillDurationCostings(deal.DurationCostings.ToList());
                else if (PackageGenerator.Tool.GetSuppliersBySuppliertype("setypesuppliers").Contains(deal.Media.id) || PackageGenerator.Tool.GetSuppliersBySuppliertype("sagasuppliers").Contains(deal.Media.id))
                    FillSecretEscapeCostings(deal.SecretEscapeMarkup);
                FillDurationMarkup(deal.DurationMarkups.ToList());
                FillWeekDayMarkups(deal.WeekDayMarkups.ToList());
                FillRoomTypeMarkups(deal.RoomTypeMarkups.ToList());
                FillSupplierMarkups(deal.SupplierMarkups.ToList());
                FillDepartureAirportMarkups(deal.DepartureAirportMarkups.ToList());
                FillArrivalAirportMArkups(deal.DestinationAirportMarkups.ToList());
                FillDateRangeMarkups(deal.DateRangeMarkups.ToList());
                FillLowAvailabilityMarkups(deal.LowAvailabilityMarkups.ToList());

                if (deal.Packages != null && deal.Packages.Count > 0)
                    FillPackages(deal.Packages.ToList());

                if (deal.ManualHotelContracts != null && deal.ManualHotelContracts.Count > 0)
                    FillHotelContracts(deal.ManualHotelContracts.ToList());

                // tab 5
                if (deal.DealInformation != null)
                    FillDealInformation();
            }
        }

        private void FillCurrencyComboBox()
        {
            List<string> currencys = packageHandler.GetAllCurrencys();
            carParkingCurrency.DataSource = currencys;
            carHireCurrency.DataSource = currencys;
        }

        private void DisplayDefaultColumns()
        {
            List<string> visibleColumns = packageHandler.GetMiscSettingByKey("defaultVisibleColomns").value.Split('#').ToList();

            foreach (DataGridViewColumn column in dataGridViewHolidays.Columns.Cast<DataGridViewColumn>())
                if (visibleColumns.Contains(column.HeaderText))
                    column.Visible = true;
                else
                    column.Visible = false;

            int count = 0;
            List<int> selectedItems = new List<int>();
            foreach (string item in cbPackageColumns.Items)
            {
                if (visibleColumns.Contains(item))
                    selectedItems.Add(count);
                count++;
            }

            for (int i = 0; i < cbPackageColumns.Items.Count; i++)
                if (selectedItems.Contains(i))
                    cbPackageColumns.SetItemChecked(i, true);
        }

        private void FillOccupancyComboBox(List<string> occupancys)
        {
            OccupancyComboBox.DataSource = occupancys;
            FillNewDurationCostingForNewOccupnacy(occupancys);
        }

        private void FillDealType(int id)
        {
            foreach (GulliverLibrary.DealType dealType in packageHandler.GetAllDealTypes())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = dealType.type.Trim();
                item.Value = dealType.id;               
                ddlDealTypes.Items.Add(item);

                if (id == dealType.id)
                    ddlDealTypes.SelectedItem = item;
            }            
        }

        private void FillDurationComboBox(List<string> durations)
        {
            DurationComboBox.DataSource = durations;
            FillNewDurationCostingForNewDuration(durations);
        }

        private void FillOfferContracts(List<MySqlDataHandler.AcCGuiD> contracts)
        {
            foreach (MySqlDataHandler.AcCGuiD accomGuid in contracts.OrderByDescending(a => a.ValidFrom))
            {
                MySqlDataHandler.Expand building = packageHandler.GetBuildingByCodes(accomGuid.AcComCode.Trim());
                gulliverDS.HotelContracts.AddHotelContractsRow("Delete", 0, (int)accomGuid.SrRecNo, accomGuid.FullName.Trim(), building.Building.Trim(), accomGuid.BoardBasis.Trim(), accomGuid.ValidFrom.Value, accomGuid.ValidTo.Value, false);
            }
        }

        private void FillResorts()
        {
            ddlResorts.Items.Clear();
            ddlResorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            ddlResorts.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ddlResorts.AutoCompleteSource = AutoCompleteSource.ListItems;

            List<MySqlDataHandler.Resort> resorts = packageHandler.GetResortsByAirport(((ComboBoxItem)ddlAirports.SelectedItem).Value.ToString());
            resorts = resorts.Where(r => r.ResortCode != null).ToList();

            foreach (MySqlDataHandler.Resort resort in resorts)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = resort.ResortName.Trim() + " - (" + resort.ResortCode.Trim() + ")" + ((resort.Country != null && resort.Country != string.Empty) ? " Country: " + resort.Country : string.Empty) + ((resort.Supplier != null && resort.Supplier != string.Empty) ? " Supplier: " + resort.Supplier : string.Empty);
                item.Value = resort.ResortCode.Trim();
                ddlResorts.Items.Add(item);
            }

            if (resorts.Count > 1 && txtSearhHotelContract.Text == string.Empty)
                SetText(txtSearhHotelContract, "Search Contracts here ...");
        }

        private void FillHotels(string resortCode, string searchText)
        {
            tripper.Contracts.Rows.Clear();
            List<MySqlDataHandler.AcCGuiD> hotels = packageHandler.GetHotelsByAirportAndResort(((ComboBoxItem)ddlAirports.SelectedItem).Value.ToString(), resortCode);
            if (searchText != string.Empty && searchText != "Search Contracts here ...".ToUpper())
                hotels = hotels.Where(h => h.FullName.ToUpper().Contains(searchText.ToUpper())).ToList();
            //hotels = hotels.Where(h => h.ValidFrom > DateTime.Today.AddDays(-15)).ToList();

            foreach (MySqlDataHandler.AcCGuiD accomGuid in hotels.OrderByDescending(a => a.ValidFrom))
            {
                MySqlDataHandler.Expand building = packageHandler.GetBuildingByCodes(accomGuid.AcComCode.Trim());
                tripper.Contracts.AddContractsRow("Select", (int)accomGuid.SrRecNo, accomGuid.FullName.Trim(), building.Building.Trim(), accomGuid.BoardBasis.Trim(), accomGuid.ValidFrom.Value, accomGuid.ValidTo.Value);
            }
        }

        private void FillHotelContracts(List<GulliverLibrary.HotelContract> hotelContracts)
        {
            gulliverDS.HotelContracts.Rows.Clear();

            foreach (GulliverLibrary.HotelContract hotelContract in hotelContracts)
            {
                MySqlDataHandler.Expand building = packageHandler.GetBuildingByCodes(hotelContract.accomcode.Trim());
                MySqlDataHandler.AcCGuiD accomGuid = packageHandler.GetAccomGuidByRecNo(hotelContract.recno);
                gulliverDS.HotelContracts.AddHotelContractsRow("Delete", hotelContract.id, (int)hotelContract.recno, hotelContract.fullname.Trim(), building.Building.Trim() + " " + building.PricedOCc + " Occupancy", accomGuid.BoardBasis.Trim(), accomGuid.ValidFrom.Value, accomGuid.ValidTo.Value, hotelContract.isEntryRoom);

            }

            FillRoomTypeComboBox(hotelContracts);
        }

        private void FillOccupancy()
        {
            Hashtable occupancys = new Hashtable();
            occupancys.Add("2A 0C 0I", "2,0,0");
            occupancys.Add("2A 1C 0I", "2,1,0");
            occupancys.Add("2A 2C 0I", "2,2,0");
            occupancys.Add("2A 2C 1I", "2,2,1");
            occupancys.Add("2A 0C 1I", "2,0,1");


            foreach (string key in occupancys.Keys)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = key;
                item.Value = occupancys[key].ToString();
                cbOcupancy.Items.Add(item);
            }
        }

        private void FillMedias(int id)
        {
            ddlMedias.Items.Clear();

            ddlMedias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            ddlMedias.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ddlMedias.AutoCompleteSource = AutoCompleteSource.ListItems;

            List<GulliverLibrary.Media> suppliers = gulliverQueryHandler.GetAllMedias();

            ComboBoxItem itemI = new ComboBoxItem();
            itemI.Text = "Please select";
            itemI.Value = 0;
            ddlMedias.Items.Add(itemI);

            if (id == 0)
                ddlMedias.SelectedItem = itemI;

            foreach (GulliverLibrary.Media supplier in suppliers.OrderBy(s => s.supplier))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = supplier.supplier.Trim();
                item.Value = supplier.id;
                ddlMedias.Items.Add(item);

                if (id == supplier.id)
                    ddlMedias.SelectedItem = item;
            }

        }

        private void FillBoardBasis()
        {
            List<MySqlDataHandler.Board> boards = queryHandler.GetBoardInfo();

            foreach (MySqlDataHandler.Board board in boards.OrderBy(b => b.BoardType.Trim()))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = board.BoardType.Trim() + " (" + board.BoardCode.Trim() + ")";
                item.Value = board.BoardCode.Trim();
                cbBoards.Items.Add(item);
            }

        }

        private void FillDepAirports()
        {
            List<string> specialAirports = new List<string>() { "BHX", "BRS", "EDI", "EMA", "GLA", "LGW", "LPL", "LTN", "MAN", "NCL", "SEN", "STN" };

            List<TripperLibrary.UKAirport> ukAirports = packageHandler.GetAllUKAirports();

            foreach (TripperLibrary.UKAirport ukAirport in ukAirports)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = ukAirport.airportCode.Trim();
                item.Value = ukAirport.airportCode.Trim();
                cbDepartureAirports.Items.Add(item);
            }

            List<TripperLibrary.GermanAirport> germanAirports = packageHandler.GetAllGermanAirports();

            foreach (TripperLibrary.GermanAirport germanAirport in germanAirports)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = germanAirport.airportCode.Trim();
                item.Value = germanAirport.airportCode.Trim();
                cbGermanAirports.Items.Add(item);
            }

            List<TripperLibrary.USAirport> usaAirports = packageHandler.GetAllUSAAirports();

            foreach (TripperLibrary.USAirport germanAirport in usaAirports)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = germanAirport.airportCode.Trim();
                item.Value = germanAirport.airportCode.Trim();
                cbUSAirports.Items.Add(item);
            }
        }        
 
        private void FillNewDurationCostingForNewOccupnacy(List<string> occupancys)
        {
            if (costingsDS.DurationCosting != null)
            {
                List<string> durationCostingOccupancys = costingsDS.DurationCosting.Select(d => d.Occupancy).Distinct().ToList();
                List<string> newOccupanys = occupancys.Where(o => !durationCostingOccupancys.Any(d => d == o)).ToList();

                foreach (string occupancy in newOccupanys)
                {
                    foreach (string duration in deal.durations.Split('#'))
                    {
                        GulliverLibrary.DurationCosting durationCosting = deal.DurationCostings.SingleOrDefault(d => d.occupancy.Trim() == occupancy.Trim() && d.duration == Convert.ToInt32(duration));
                        if (durationCosting != null)
                            costingsDS.DurationCosting.AddDurationCostingRow("Delete", durationCosting.id, durationCosting.duration.ToString(), durationCosting.occupancy, durationCosting.minSellAt, durationCosting.maxSellAt, durationCosting.minChildSellAt, durationCosting.maxChildSellAt, durationCosting.minMarkupFirstRange, durationCosting.minMarkupOtherRange, durationCosting.minMarkupOtherRangeType, durationCosting.increasedBy);
                        else
                            costingsDS.DurationCosting.AddDurationCostingRow("Delete", 0, duration, occupancy, 0, 0, 0, 0, 0, 0, "£", 0);
                    }
                }

                List<string> removedOccupanys = durationCostingOccupancys.Where(o => !occupancys.Any(d => d == o)).ToList();
                List<CostingsDS.DurationCostingRow> durationCostingRows = costingsDS.DurationCosting.Where(d => removedOccupanys.Contains(d.Occupancy)).ToList();
                foreach (CostingsDS.DurationCostingRow occupancy in durationCostingRows)
                    costingsDS.DurationCosting.RemoveDurationCostingRow(occupancy);
            }
        }

        private void FillNewDurationCostingForNewDuration(List<string> durations)
        {
            if (costingsDS.DurationCosting != null)
            {
                List<string> durationCostingOccupancys = costingsDS.DurationCosting.Select(d => d.Duration.ToString()).Distinct().ToList();
                List<string> newDurations = durations.Where(o => !durationCostingOccupancys.Any(d => d == o.ToString())).Select(o => o.ToString()).ToList();

                foreach (string duration in newDurations)
                {
                    foreach (string occupancy in deal.occupancy.Split('#'))
                    {
                        GulliverLibrary.DurationCosting durationCosting = deal.DurationCostings.SingleOrDefault(d => d.occupancy.Trim() == occupancy.Trim() && d.duration == Convert.ToInt32(duration));
                        if (durationCosting != null)
                            costingsDS.DurationCosting.AddDurationCostingRow("Delete", durationCosting.id, durationCosting.duration.ToString(), durationCosting.occupancy, durationCosting.minSellAt, durationCosting.maxSellAt, durationCosting.minChildSellAt, durationCosting.maxChildSellAt, durationCosting.minMarkupFirstRange, durationCosting.minMarkupOtherRange, durationCosting.minMarkupOtherRangeType, durationCosting.increasedBy);
                        else
                            costingsDS.DurationCosting.AddDurationCostingRow("Delete", 0, duration, occupancy, 0, 0, 0, 0, 0, 0, "£", 0);
                    }
                }

                List<string> removedDurations = durationCostingOccupancys.Where(o => !durations.Any(d => d.ToString() == o)).ToList();
                List<CostingsDS.DurationCostingRow> durationCostingRows = costingsDS.DurationCosting.Where(d => removedDurations.Contains(d.Duration.ToString())).ToList();
                foreach (CostingsDS.DurationCostingRow row in durationCostingRows)
                    costingsDS.DurationCosting.RemoveDurationCostingRow(row);
            }
        }

        private void FillRoomTypeComboBox(List<GulliverLibrary.HotelContract> hotelContracts)
        {
            List<string> roomTypes = hotelContracts.Select(h => h.accomcode).Distinct().ToList();
            List<string> roomNames = new List<string>();

            foreach (string roomType in roomTypes)
            {
                MySqlDataHandler.Expand expand = packageHandler.GetBuildingByCodes(roomType);
                roomNames.Add(expand.Building.Trim() + "-" + roomType);
            }

            roomTypeCombobox.DataSource = roomNames;
        }

        private void FillDepartureAirportComboBox(List<string> departureAirports)
        {
            departureAirportComboBox.DataSource = departureAirports;
        }

        private void FillDestinationAirportComboBox(List<string> destinationAirports)
        {
            destiantionAirportComboBox.DataSource = destinationAirports;
        }       

        //private void FillHolidays(List<GulliverLibrary.Package> packages)
        //{
        //    this.packagesDS.Package.Clear();
        //    DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dataGridViewHolidays);
        //    int count = 0;

        //    foreach (GulliverLibrary.Package h in packages)
        //    {
        //        this.packagesDS.Package.AddPackageRow("Delete", h.id, count, (h.leading) ? 1 : 0, h.date.ToString("MMMM"), h.date.DayOfWeek.ToString(), h.date, string.Empty, h.hotelKey, h.departureAirport, h.destinationAirport, h.duration, h.obDepartureTime.Trim(), h.obArrivalTime.Trim(), h.ibDepartureTime.Trim(), h.ibArrivalTime.Trim(), h.board.Trim(), Math.Round(h.flightPrice, 2), h.airline, h.obFlightNo, h.ibFlightNo, h.roomType, h.occupancy, h.adults, h.children, h.infants, Math.Round(h.hotelPrice, 2), Math.Round(h.childHotelPrice), h.caa, h.baggagePrice, h.transfers, h.extras, h.childExtras, h.baseMarkup, h.totalMarkup, h.totalChildMarkup, h.carhireCosting, Math.Round(h.commission, 2), Math.Round(h.profit, 2), Math.Round(h.nett, 2), Convert.ToInt32(h.sellAt), h.childNett, h.childSellat, ((h.searchType == 1) ? "FAB" : "Flightsheet"), h.status, ((h.oldSellAt != null) ? Convert.ToInt32(h.oldSellAt) : 0));
        //        count++;
        //    }

        //    lblTotal.Text = (packages.Count > 0) ? "Total: " + packages.Count + " holidays" : "Total: " + packages.Count + " holiday";
        //}

        private void FillPackages(List<GulliverLibrary.Package> packages)
        {
            packagesDS.Package.Clear();
            int count = 0;
            foreach (GulliverLibrary.Package p in packages)
                packagesDS.Package.AddPackageRow("Delete", p.id, count, (p.leading) ? 1 : 0, p.date.ToString("MMMM"), p.date.DayOfWeek.ToString(), p.date, "", p.hotelKey, p.departureAirport.Trim(), p.destinationAirport.Trim(), p.duration, p.obDepartureTime, p.obArrivalTime, p.ibDepartureTime, p.ibArrivalTime, p.board, p.flightPrice, p.airline, p.obFlightNo, p.ibFlightNo, p.roomType, p.occupancy,
                    p.adults, p.children, p.infants, Math.Round(p.hotelPrice,2), Math.Round(p.childHotelPrice), p.caa, p.baggagePrice, p.transfers, p.extras, p.childExtras, p.baseMarkup, p.totalMarkup, p.totalChildMarkup, p.carhireCosting,p.carParkingCosting, p.commission, p.profit, p.nett, p.sellAt, p.childNett, p.childSellat, ((p.searchType == 1) ? "FAB" : "Flightsheet"), p.status, p.oldSellAt);

            lblTotal.Text = (packages.Count > 0) ? "Total: " + packages.Count + " holidays" : "Total: " + packages.Count + " holiday";

            visibleColumns = dataGridViewHolidays.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => c.DataPropertyName.Trim()).ToList();
            string invisibleColumns = packageHandler.GetMiscSettingByKey("invisibleColumns").value;

            visibleColumns = visibleColumns.Where(v => !invisibleColumns.Split('#').Contains(v)).ToList();
            VisibleColumns();
        }

        private void FillAllAirports()
        {
            ddlAirports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            ddlAirports.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ddlAirports.AutoCompleteSource = AutoCompleteSource.ListItems;

            ddlAirports.Items.Clear();
            Hashtable airports = packageHandler.GetAllAirports();

            foreach (DictionaryEntry entry in airports.Cast<DictionaryEntry>().OrderBy(s => s.Value))
            {
                if (entry.Value.ToString() != string.Empty)
                {
                   ComboBoxItem item = new ComboBoxItem();
                   item.Text = entry.Value.ToString() + "- (" + entry.Key + ")";
                   item.Value = entry.Key;
                   ddlAirports.Items.Add(item);
                }
            }           
        }

        private void FillFlightSuppliers()
        {
           List<string> flightAirlines = packageHandler.GetFlightSuppliers();

           foreach (string flightAirline in flightAirlines)
                cbAirlines.Items.Add(flightAirline);
        }

        private void FillCurrecncys()
        {
            List<MySqlDataHandler.Currency> currecncys = queryHandler.GetCurrency();

            foreach (MySqlDataHandler.Currency currecncy in currecncys.OrderBy(b => b.Currency1.Trim()))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = currecncy.Currency1;
                item.Value = currecncy.Currency1;
                ddlCurrency.Items.Add(item);
            }
        }   

        private void FillTripperExtras(List<int> tripperExtraRecnos, string searchText)
        {
            List<MySqlDataHandler.AcCGuiD> accomGuids = queryHandler.GetAccomGuidByApt("EXR");
            accomGuids = accomGuids.Where(a => a.ValidTo >= DateTime.Today).ToList();
            
            if (searchText != string.Empty && searchText != "Search Transfers here ...".ToUpper())
                accomGuids = accomGuids.Where(a => a.FullName.ToUpper().Contains(searchText.ToUpper())).ToList();

            gulliverDS.TripperExtra.Rows.Clear();
           
            foreach (MySqlDataHandler.AcCGuiD accomGuid in accomGuids.OrderByDescending(a => a.ValidFrom))
              gulliverDS.TripperExtra.AddTripperExtraRow("Delete", (int)accomGuid.SrRecNo, 0, accomGuid.FullName.Trim(), accomGuid.ValidFrom.Value, accomGuid.ValidTo.Value, tripperExtraRecnos.Contains((int)accomGuid.SrRecNo));
        }                     

        private void FillSelectedOccupancy(string selectedOccupancys)
        {
            for (int i = 0; i < cbOcupancy.Items.Count; i++)
            {
               if (selectedOccupancys.Contains( ((ComboBoxItem)cbOcupancy.Items[i]).Value.ToString()))
                 cbOcupancy.SetItemChecked(i, true);
            }

            EnableDisableChildPrices(selectedOccupancys.Split('#').ToList());
        }

        private void FillSelectedBoardBasis(string selectedBoardBasis)
        {
            for (int i = 0; i < cbBoards.Items.Count; i++)
            {
               if (selectedBoardBasis.Contains(((ComboBoxItem)cbBoards.Items[i]).Value.ToString()))
                  cbBoards.SetItemChecked(i, true);
            }
        }

        private void FillDateRangeMarkups(List<GulliverLibrary.DateRangeMarkup> dateRangeMarkups)
        {
            costingsDS.DateRangeMarkup.Rows.Clear();
            foreach (GulliverLibrary.DateRangeMarkup dateRangeMarkup in dateRangeMarkups)
                costingsDS.DateRangeMarkup.AddDateRangeMarkupRow("Delete", dateRangeMarkup.id, dateRangeMarkup.startDate, dateRangeMarkup.endDate, dateRangeMarkup.adultMarkup, dateRangeMarkup.childMarkup);
        }

        private void FillLowAvailabilityMarkups(List<GulliverLibrary.LowAvailabilityMarkup> lowAvailabilityMarkups)
        {
            costingsDS.LowAvailabilityMarkup.Rows.Clear();
            foreach (GulliverLibrary.LowAvailabilityMarkup lowAvailabilityMarkup in lowAvailabilityMarkups)
                costingsDS.LowAvailabilityMarkup.AddLowAvailabilityMarkupRow("Delete", lowAvailabilityMarkup.id, lowAvailabilityMarkup.noOfRooms, lowAvailabilityMarkup.startDate, lowAvailabilityMarkup.endDate, lowAvailabilityMarkup.adultMarkup, lowAvailabilityMarkup.childMarkup);
        }

        private void FillArrivalAirportMArkups(List<GulliverLibrary.DestinationAirportMarkup> arrivalAirportMarkups)
        {
            costingsDS.DestinationAirportMarkup.Rows.Clear();
            foreach (GulliverLibrary.DestinationAirportMarkup destinationAirportMarkup in arrivalAirportMarkups)
                costingsDS.DestinationAirportMarkup.AddDestinationAirportMarkupRow("Delete", destinationAirportMarkup.id, destinationAirportMarkup.airport.Trim(), destinationAirportMarkup.adultMarkup, destinationAirportMarkup.childMarkup);
        }

        private void FillDepartureAirportMarkups(List<GulliverLibrary.DepartureAirportMarkup> departureAirportMarkups)
        {
            costingsDS.DepartureAirportMarkup.Rows.Clear();
            foreach (GulliverLibrary.DepartureAirportMarkup departureAirportMarkup in departureAirportMarkups)
                costingsDS.DepartureAirportMarkup.AddDepartureAirportMarkupRow("Delete", departureAirportMarkup.id, departureAirportMarkup.airport.Trim(), departureAirportMarkup.adultMarkup, departureAirportMarkup.childMarkup);
        }

        private void FillSupplierMarkups(List<GulliverLibrary.SupplierMarkup> supplierMarkups)
        {
            costingsDS.SupplierMarkup.Rows.Clear();
            foreach (GulliverLibrary.SupplierMarkup supplierMarkup in supplierMarkups)
                costingsDS.SupplierMarkup.AddSupplierMarkupRow("Delete", supplierMarkup.id, supplierMarkup.supplier.Trim(), supplierMarkup.adultMarkup, supplierMarkup.childMarkup);
        }

        private void FillRoomTypeMarkups(List<GulliverLibrary.RoomTypeMarkup> roomTypeMarkups)
        {
            List<ComboBoxItem> items = new List<ComboBoxItem>();

            costingsDS.RoomTypeMarkup.Rows.Clear();
            
            foreach (GulliverLibrary.RoomTypeMarkup roomTypeMarkup in roomTypeMarkups)
            costingsDS.RoomTypeMarkup.AddRoomTypeMarkupRow("Delete", roomTypeMarkup.id, roomTypeMarkup.code.Trim(), roomTypeMarkup.roomType.Trim(), roomTypeMarkup.adultPrice, roomTypeMarkup.childPrice);
        }
       
        private void FillWeekDayMarkups(List<GulliverLibrary.WeekDayMarkup> weekDayMarkups)
        {
            costingsDS.WeekdayMarkup.Rows.Clear();

            foreach (GulliverLibrary.WeekDayMarkup weekDayMarkup in weekDayMarkups)
                costingsDS.WeekdayMarkup.AddWeekdayMarkupRow("Delete", weekDayMarkup.id, weekDayMarkup.weekday.Trim(), weekDayMarkup.adultMarkup, weekDayMarkup.childMarkup);
        }

        private void FillDurationMarkup(List<GulliverLibrary.DurationMarkup> durationMarkups)
        {
            costingsDS.DurationMarkup.Rows.Clear();
            foreach (GulliverLibrary.DurationMarkup durationMarkup in durationMarkups)
                costingsDS.DurationMarkup.AddDurationMarkupRow("Delete", durationMarkup.id, durationMarkup.duration, durationMarkup.adultMarkup, durationMarkup.childMarkup);
        }

        private void FillHotelContracts(List<GulliverLibrary.ManualHotelContract> manualHotelContracts)
        {
            gulliverDS.ManualContract.Rows.Clear();

            foreach (GulliverLibrary.ManualHotelContract manualContract in manualHotelContracts)
                gulliverDS.ManualContract.AddManualContractRow("Delete", manualContract.id, manualContract.fromDate, manualContract.toDate, manualContract.price, manualContract.allotment);
        }

        private void FillSecretEscapeCostings(GulliverLibrary.SecretEscapeMarkup secretEscapeMarkup)
        {
            if (secretEscapeMarkup != null)
            {
              txtMinimumMarkup.Text = secretEscapeMarkup.leadingMarkup.ToString();
              txtStandardMarkup.Text = secretEscapeMarkup.standardMarkup.ToString();
            }
        }

        private void FillCarHire(List<GulliverLibrary.CarHire> carHires)
        {
            gulliverDS.Carhire.Rows.Clear();

            foreach (GulliverLibrary.CarHire carHire in carHires)
                gulliverDS.Carhire.AddCarhireRow("Delete", carHire.id, carHire.startDate, carHire.endDate, carHire.currency.Trim(), carHire.amount);
        }

        private void FillCarParking(List<GulliverLibrary.CarParking> carParkings)
        {
            gulliverDS.CarParking.Rows.Clear();
           
            foreach (GulliverLibrary.CarParking carParking in carParkings)
                gulliverDS.CarParking.AddCarParkingRow("Delete", carParking.id, carParking.startDate, carParking.endDate, carParking.currency, carParking.amount);
        }

        private void FillExtras(List<GulliverLibrary.Extra> extras)
        {
            gulliverDS.Extra.Rows.Clear();
            
            foreach (GulliverLibrary.Extra extra in extras)
              gulliverDS.Extra.AddExtraRow("Delete", extra.id, extra.description.Trim(), extra.isIncluded, extra.adultPrice, extra.childPrice);            
        }

        private void FillDurationCostings(List<GulliverLibrary.DurationCosting> durationCostings)
        {
            costingsDS.DurationCosting.Rows.Clear();

            if (durationCostings != null && durationCostings.Count > 0)
            {
                foreach (GulliverLibrary.DurationCosting durationCosting in durationCostings)
                 costingsDS.DurationCosting.AddDurationCostingRow("Delete", durationCosting.id, durationCosting.duration.ToString(), durationCosting.occupancy, durationCosting.minSellAt, durationCosting.maxSellAt, durationCosting.minChildSellAt, durationCosting.maxChildSellAt, durationCosting.minMarkupFirstRange, durationCosting.minMarkupOtherRange, durationCosting.minMarkupOtherRangeType, durationCosting.increasedBy);                
            }
        }

        private void FillDefaultDurationCostings()
        {
            costingsDS.DurationCosting.Rows.Clear();

            if (deal != null && deal.durations != null && deal.durations != string.Empty && deal.occupancy != null && deal.occupancy != string.Empty)
            {
                foreach (string duration in deal.durations.Split('#'))
                {
                    foreach (string occupancy in deal.occupancy.Split('#'))
                      costingsDS.DurationCosting.AddDurationCostingRow("Delete", 0, duration, occupancy, 0, 0, 0, 0, 0, 0, "£", 0);                    
                }
            }
        }

        private void FillSelectedDurations(List<string> durations)
        {
            for (int i = 0; i < cbDurations.Items.Count; i++)
            {
               if (durations.Contains(cbDurations.Items[i].ToString()))
                  cbDurations.SetItemChecked(i, true);
            }
        }

        private void FillSelectedWeekDays(List<string> weekdays)
        {
            for (int i = 0; i < cbWeekDays.Items.Count; i++)
            {
                if (weekdays.Contains(cbWeekDays.Items[i].ToString()))
                    cbWeekDays.SetItemChecked(i, true);
            }
        }

        private void FillSelectdAirlines(List<string> airlines)
        {
            for (int i = 0; i < cbAirlines.Items.Count; i++)
            {
                if (airlines.Contains(cbAirlines.Items[i].ToString()))
                    cbAirlines.SetItemChecked(i, true);
            }
        }

        private void FillSelectedDepartureAirports(List<string> departureAirports)
        {
            for (int i = 0; i < cbDepartureAirports.Items.Count; i++)
            {
                if (departureAirports.Contains(cbDepartureAirports.Items[i].ToString()))
                    cbDepartureAirports.SetItemChecked(i, true);
            }
        }

        private void FillDealInformation()
        {
            txtMainHeader.Text = (deal.DealInformation.mainHeader != null) ? deal.DealInformation.mainHeader : string.Empty;
            txtSubHeader.Text = (deal.DealInformation.subHeader != null) ? deal.DealInformation.subHeader : string.Empty;
            txtLongitude.Text = (deal.DealInformation.longitude != null) ? deal.DealInformation.longitude : string.Empty;
            txtLatitude.Text = (deal.DealInformation.latitude != null) ? deal.DealInformation.latitude : string.Empty;            
            txtYouTubeLink.Text = (deal.DealInformation.youTubeLink != null) ? deal.DealInformation.youTubeLink : string.Empty;
            txtDealIntro.Text = (deal.DealInformation.introduction != null) ? deal.DealInformation.introduction : string.Empty;
            txtChildPrice.Text = (deal.DealInformation.childPrices != null) ? deal.DealInformation.childPrices : string.Empty;
            txtOptionalExtras.Text = (deal.DealInformation.optionalExtras != null) ? deal.DealInformation.optionalExtras : string.Empty;
            txtPleasenote.Text = (deal.DealInformation.pleaseNote != null) ? deal.DealInformation.pleaseNote : string.Empty;
            
            txtHotelText.Text = (deal.DealInformation.HotelInformation.hotelBodyText != null) ? deal.DealInformation.HotelInformation.hotelBodyText : string.Empty;            
            txtDestinationText.Text = (deal.DealInformation.HotelInformation.destinationText != null) ? deal.DealInformation.HotelInformation.destinationText : string.Empty;
            txtCountryText.Text = (deal.DealInformation.HotelInformation.countryText != null) ? deal.DealInformation.HotelInformation.countryText : string.Empty;
            txtKeyInformationText.Text = (deal.DealInformation.HotelInformation.keyInformation != null) ? deal.DealInformation.HotelInformation.keyInformation.Trim() : string.Empty;
            txtAccessibilityText.Text = (deal.DealInformation.HotelInformation.accessibility != null) ? deal.DealInformation.HotelInformation.accessibility.Trim() : string.Empty;
            txtDestinationTitle.Text = (deal.DealInformation.HotelInformation.destinationHeader != null) ? deal.DealInformation.HotelInformation.destinationHeader.Trim() : string.Empty;
            txtCountryTitle.Text = (deal.DealInformation.HotelInformation.countryHeader != null) ? deal.DealInformation.HotelInformation.countryHeader.Trim() : string.Empty;
            txtHotelTitle.Text = (deal.DealInformation.HotelInformation.hotelHeader != null) ? deal.DealInformation.HotelInformation.hotelHeader : string.Empty;
            txtHowToBook.Text = (deal.DealInformation.howToBook != null) ? deal.DealInformation.howToBook.Trim() : string.Empty;
            txtTripAdvisorLink.Text = (deal.DealInformation.tripAdvisorLink != null) ? deal.DealInformation.tripAdvisorLink : string.Empty;     
            //cbActiveOnLuxuryWebsite.Checked = deal.DealInformation.dealActive;
            cmbCurrency.SelectedItem = (deal.DealInformation.dealCurrency == null || deal.DealInformation.dealCurrency == string.Empty) ? "GBP" : deal.DealInformation.dealCurrency;
            cmbLanuages.SelectedItem = (deal.DealInformation.language == null || deal.DealInformation.language == string.Empty) ? "English" : deal.DealInformation.language;
            ddlPriorities.SelectedItem = (deal.DealInformation.priority != null) ? deal.DealInformation.priority.ToString() : "0";
            cbGoLiveOnBestDealPage.Checked = (deal.DealInformation.goLiveOnBestDealPage != null) ? deal.DealInformation.goLiveOnBestDealPage : false;
            txtPageName.Text = (deal.DealInformation.pageName != null) ? deal.DealInformation.pageName : string.Empty;
            txtLeadPrice.Text = (deal.DealInformation.leadPrice != null) ? deal.DealInformation.leadPrice : string.Empty;
            txtBestDealHeader.Text = (deal.DealInformation.bestDealHeader != null) ? deal.DealInformation.bestDealHeader : string.Empty;
            txtBestDealDescription.Text = (deal.DealInformation.bestDealDescription != null) ? deal.DealInformation.bestDealDescription : string.Empty;
            ddlBrand.SelectedItem = (deal.DealInformation.brand != null) ? deal.DealInformation.brand : string.Empty;
            txtTopHeader.Text = (deal.DealInformation.topHeader != null) ? deal.DealInformation.topHeader : string.Empty;
            ddlDurations.Items.AddRange((deal.durations != null && deal.durations != string.Empty) ? deal.durations.Split('#').ToArray() : new List<string>().ToArray());
            ddlDurations.SelectedItem = (deal.DealInformation.defaultDuration != 0) ? deal.DealInformation.defaultDuration.ToString() : "0";
            rbDays.Checked = (deal.DealInformation.diplayNightsOrDays != null && deal.DealInformation.diplayNightsOrDays.Trim() == "Days") ? true : false;
            rbNights.Checked = (deal.DealInformation.diplayNightsOrDays == null || (deal.DealInformation.diplayNightsOrDays != null && deal.DealInformation.diplayNightsOrDays.Trim() == "Nights") || deal.DealInformation.diplayNightsOrDays == string.Empty) ? true : false;
            optionalCostings = (deal.DealInformation.optionalExtras != null) ? deal.DealExtras.ToList() : new List<GulliverLibrary.DealOptionalExtra>();
            FillImages();
            FillReviews();

        }

        private void FillImages()
        {
            if (deal.DealImages == null || deal.DealImages.Count == 0)
            {
                deal.DealImages = new List<GulliverLibrary.Image>();
                gulliverDS.Image.AddImageRow(0, "Delete", string.Empty, "best deal page image", string.Empty, string.Empty);
                gulliverDS.Image.AddImageRow(0, "Delete", string.Empty, "top left image", string.Empty, string.Empty);
                gulliverDS.Image.AddImageRow(0, "Delete", string.Empty, "top right image", string.Empty, string.Empty);
                gulliverDS.Image.AddImageRow(0, "Delete", string.Empty, "bottom left image", string.Empty, string.Empty);
                gulliverDS.Image.AddImageRow(0, "Delete", string.Empty, "bottom right image", string.Empty, string.Empty);
                gulliverDS.Image.AddImageRow(0, "Delete", string.Empty, "body image", string.Empty, string.Empty);
                gulliverDS.Image.AddImageRow(0, "Delete", string.Empty, "slider image", string.Empty, string.Empty);
            }
            else
            {
                foreach (GulliverLibrary.Image image in deal.DealImages)
                    gulliverDS.Image.AddImageRow(image.id, "Delete", image.reference, image.title, image.altText, image.description);
            }

        }

        private void FillReviews()
        {
            if (deal.DealReviews != null)
            {
                foreach (GulliverLibrary.Review review in deal.DealReviews)
                    gulliverDS.Review.AddReviewRow(review.id, "Delete", review.date, review.source, review.stars, review.title, review.text, review.link);
            }
        }

        private void FillHotelInformation(GulliverLibrary.HotelInformation hotelInformation)
        {
            txtHotelText.Text = hotelInformation.hotelBodyText;
            txtHotelTitle.Text = hotelInformation.hotelHeader;
            txtDestinationTitle.Text = hotelInformation.destinationHeader;
            txtDestinationText.Text = hotelInformation.destinationText;
            txtCountryTitle.Text = hotelInformation.countryHeader;
            txtCountryText.Text = hotelInformation.countryText;
            txtKeyInformationText.Text = hotelInformation.keyInformation;
            txtAccessibilityText.Text = hotelInformation.accessibility;
        }
        
        #endregion

        #region DataGridviewCostings
       
        //extras
        private void dataGridViewExtras_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewExtras.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGridViewExtras.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGridViewExtras.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGridViewExtras.Rows[e.RowIndex].Cells[4].Value = "0";
                dataGridViewExtras.Rows[e.RowIndex].Cells[5].Value = "0";
            }
        }

        private void dataGridViewExtras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGridViewExtras.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewExtras.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected extras - continue?", "Delete Extras", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridViewExtras.Rows.Remove((DataGridViewRow)dataGridViewExtras.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGridViewExtras_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected extras - continue?", "Delete Extra", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGridViewExtras.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }
                              
        // car parking

        private void dataGridviewCarparking_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGridviewCarparking.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewCarparking.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected car parking - continue?", "Delete Car parking", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridviewCarparking.Rows.Remove((DataGridViewRow)dataGridviewCarparking.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGridviewCarparking_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridviewCarparking.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGridviewCarparking.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGridviewCarparking.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGridviewCarparking.Rows[e.RowIndex].Cells[4].Value = "GBP";
                dataGridviewCarparking.Rows[e.RowIndex].Cells[5].Value = "0";
            }
        }

        private void dataGridviewCarparking_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected carparking - continue?", "Delete Carparking", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGridviewCarparking.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }
                

        //car hire

        private void dataGridviewCarhire_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGridviewCarhire.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewCarhire.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected carhire - continue?", "Delete Carhire", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridviewCarhire.Rows.Remove((DataGridViewRow)dataGridviewCarhire.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGridviewCarhire_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected carhire - continue?", "Delete Carhire", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGridviewCarhire.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        private void dataGridviewCarhire_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridviewCarhire.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGridviewCarhire.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGridviewCarhire.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGridviewCarhire.Rows[e.RowIndex].Cells[4].Value = "GBP";
                dataGridviewCarhire.Rows[e.RowIndex].Cells[5].Value = "0";
            }
        }    


        // duration costing   

        private void dataGVDurationCosting_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGVDurationCosting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVDurationCosting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected duration costing - continue?", "Delete Duration Costings", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGVDurationCosting.Rows.Remove((DataGridViewRow)dataGVDurationCosting.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGVDurationCosting_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVDurationCosting.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGVDurationCosting.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[4].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[5].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[6].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[7].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[8].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[9].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[10].Value = "£";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[11].Value = "0";
            }
        }

        private void dataGVDurationCosting_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected duration costing - continue?", "Delete Duration Costing", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGVDurationCosting.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }
        
        
        //markup

        private void dataGVMarkups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGVMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected duration markup - continue?", "Duration Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGVMarkups.Rows.Remove((DataGridViewRow)dataGVMarkups.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGVMarkups_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVMarkups.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGVMarkups.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGVMarkups.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGVMarkups.Rows[e.RowIndex].Cells[2].Value = "0";
                dataGVMarkups.Rows[e.RowIndex].Cells[3].Value = "0";
            }
        }

        private void dataGVMarkups_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected duration markup - continue?", "Delete Duration Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGVMarkups.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

       
       //weekday markup
        private void dataGVWeekdayM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGVWeekdayM.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVWeekdayM.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected weekday markup - continue?", "Weekday Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGVWeekdayM.Rows.Remove((DataGridViewRow)dataGVMarkups.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGVWeekdayM_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVWeekdayM.Rows[e.RowIndex].Cells[1].Value == null)
            {
               dataGVWeekdayM.Rows[e.RowIndex].Cells[0].Value = "Delete";
               dataGVWeekdayM.Rows[e.RowIndex].Cells[1].Value = "0";
               dataGVWeekdayM.Rows[e.RowIndex].Cells[2].Value = "";
               dataGVWeekdayM.Rows[e.RowIndex].Cells[3].Value = "0";
            }
        }

        private void dataGVWeekdayM_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected weekday markup - continue?", "Delete Weekday Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGVWeekdayM.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        // roomtype
        private void dataGVRoomTypeMarkups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected roomtype markup - continue?", "Roomtype Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGVRoomTypeMarkups.Rows.Remove((DataGridViewRow)dataGVRoomTypeMarkups.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGVRoomTypeMarkups_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[2].Value = "";
                dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[3].Value = "";
                dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[4].Value = "0";
                dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[5].Value = "0";
            }
        }

        private void dataGVRoomTypeMarkups_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected room type markup - continue?", "Delete Roomtype Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGVRoomTypeMarkups.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

       
        //suppler markups
        private void dataGVSupplierMarkups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGVSupplierMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVSupplierMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected supplier markup - continue?", "Supplier Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGVSupplierMarkups.Rows.Remove((DataGridViewRow)dataGVSupplierMarkups.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGVSupplierMarkups_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected supplier markup - continue?", "Delete Supplier Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGVWeekdayM.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        private void dataGVSupplierMarkups_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVSupplierMarkups.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGVSupplierMarkups.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGVSupplierMarkups.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGVSupplierMarkups.Rows[e.RowIndex].Cells[2].Value = "";
                dataGVSupplierMarkups.Rows[e.RowIndex].Cells[3].Value = "0";
                dataGVSupplierMarkups.Rows[e.RowIndex].Cells[4].Value = "0";   
            }
        }
              
        
        //departure markups
        private void dataGVDepartureApMarkups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGVDepartureApMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVDepartureApMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected departure airport markup - continue?", "Departure Airport Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGVDepartureApMarkups.Rows.Remove((DataGridViewRow)dataGVDepartureApMarkups.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGVDepartureApMarkups_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected departure airport markup - continue?", "Delete Departure Airport Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGVDepartureApMarkups.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        private void dataGVDepartureApMarkups_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVDepartureApMarkups.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGVDepartureApMarkups.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGVDepartureApMarkups.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGVDepartureApMarkups.Rows[e.RowIndex].Cells[2].Value = "";
                dataGVDepartureApMarkups.Rows[e.RowIndex].Cells[3].Value = "0";
            }
        }
        
       
        //airport markups
        private void dataGVArrivalAirportMarkups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGVArrivalAirportMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVArrivalAirportMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected arrival airport markup - continue?", "Arrival Airport Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGVArrivalAirportMarkups.Rows.Remove((DataGridViewRow)dataGVArrivalAirportMarkups.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }

        }

        private void dataGVArrivalAirportMarkups_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected arrival airport markup - continue?", "Delete Arrival Airport Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGVArrivalAirportMarkups.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        private void dataGVArrivalAirportMarkups_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVArrivalAirportMarkups.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGVArrivalAirportMarkups.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGVArrivalAirportMarkups.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGVArrivalAirportMarkups.Rows[e.RowIndex].Cells[2].Value = "";
                dataGVArrivalAirportMarkups.Rows[e.RowIndex].Cells[3].Value = "0";
            }
        }
       
       
        // daternage markup
        private void dataGVDateRangeMarkup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGVDateRangeMarkup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVDateRangeMarkup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected date range markup - continue?", "Date Range Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGVDateRangeMarkup.Rows.Remove((DataGridViewRow)dataGVDateRangeMarkup.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGVDateRangeMarkup_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVDateRangeMarkup.Rows[e.RowIndex].Cells[1].Value == null)
            {
               dataGVDateRangeMarkup.Rows[e.RowIndex].Cells[0].Value = "Delete";
               dataGVDateRangeMarkup.Rows[e.RowIndex].Cells[1].Value = "0";          
               dataGVDateRangeMarkup.Rows[e.RowIndex].Cells[4].Value = "0";
            }
        }

        private void dataGVDateRangeMarkup_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected date range markup - continue?", "Delete Date Range Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGVDateRangeMarkup.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        // low availability markup
        private void dataGVLowAvailabilityMarkup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGVLowAvailabilityMarkup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVLowAvailabilityMarkup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected low availability markup - continue?", "Low availability Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGVLowAvailabilityMarkup.Rows.Remove((DataGridViewRow)dataGVLowAvailabilityMarkup.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGVLowAvailabilityMarkup_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected low availability markup - continue?", "Delete Low Availability Markup", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGVLowAvailabilityMarkup.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        private void dataGVLowAvailabilityMarkup_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVLowAvailabilityMarkup.Rows[e.RowIndex].Cells[1].Value == null)
            {
               dataGVLowAvailabilityMarkup.Rows[e.RowIndex].Cells[0].Value = "Delete";
               dataGVLowAvailabilityMarkup.Rows[e.RowIndex].Cells[1].Value = "0";
               dataGVLowAvailabilityMarkup.Rows[e.RowIndex].Cells[2].Value = "0";
               dataGVLowAvailabilityMarkup.Rows[e.RowIndex].Cells[5].Value = "0";
            }
        }


        //manaul Contracts 

        private void dataGridViewHotelContracts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewHotelContracts.Rows[e.RowIndex].Cells[1].Value == null)
            {
               dataGridViewHotelContracts.Rows[e.RowIndex].Cells[0].Value = "Delete";
               dataGridViewHotelContracts.Rows[e.RowIndex].Cells[1].Value = "0";
               dataGridViewHotelContracts.Rows[e.RowIndex].Cells[4].Value = "0";
               dataGridViewHotelContracts.Rows[e.RowIndex].Cells[5].Value = "0";
            }
        }

        private void dataGridViewHotelContracts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGridViewHotelContracts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewHotelContracts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected hotel contract - continue?", "Manual Hotel Contract", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridViewHotelContracts.Rows.Remove((DataGridViewRow)dataGridViewHotelContracts.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGridViewHotelContracts_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected manual hotel contrct - continue?", "Delete Hotel Contract", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGridViewHotelContracts.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

       
        //tripper extras       

        private void dataGridviewTripperExtras_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && dataGridviewTripperExtras.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && ((Type)dataGridviewTripperExtras.Rows[e.RowIndex].Cells[e.ColumnIndex].ValueType).FullName == "System.Boolean")
            {
                if (Convert.ToBoolean(((DataGridViewCheckBoxCell)dataGridviewTripperExtras.Rows[e.RowIndex].Cells[e.ColumnIndex]).Value))
                    selectedTripperExtras.Add(Convert.ToInt32(dataGridviewTripperExtras.Rows[e.RowIndex].Cells[1].Value));
                else
                {
                    if (selectedTripperExtras.Contains(Convert.ToInt32(dataGridviewTripperExtras.Rows[e.RowIndex].Cells[1].Value)))
                        selectedTripperExtras.Remove(Convert.ToInt32(dataGridviewTripperExtras.Rows[e.RowIndex].Cells[1].Value));
                }
            }
        }

        //images

        private void dataGridviewImages_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridviewImages.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewImages.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected image - continue?", "Delete Images", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridviewImages.Rows.Remove((DataGridViewRow)dataGridviewImages.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGridviewImages_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridviewImages.Rows[e.RowIndex].Cells[0].Value == null)
            {
                dataGridviewImages.Rows[e.RowIndex].Cells[0].Value = "0";
                dataGridviewImages.Rows[e.RowIndex].Cells[1].Value = "Delete";
                dataGridviewImages.Rows[e.RowIndex].Cells[2].Value = "";
                dataGridviewImages.Rows[e.RowIndex].Cells[3].Value = "";
                dataGridviewImages.Rows[e.RowIndex].Cells[4].Value = "";
                dataGridviewImages.Rows[e.RowIndex].Cells[5].Value = "";
            }
        }

        private void dataGridviewImages_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected Image - continue?", "Delete Images", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGridviewImages.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        //reviews
        private void dGVReviews_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (dGVReviews.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dGVReviews.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
                {
                    switch (MessageBox.Show("This will delete selected review - continue?", "Delete Reviews", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            dGVReviews.Rows.Remove((DataGridViewRow)dGVReviews.Rows[e.RowIndex]);
                            break;

                        case System.Windows.Forms.DialogResult.No:
                            return;
                    }
                }
            }
        }

        private void dGVReviews_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dGVReviews.Rows[e.RowIndex].Cells[0].Value == null)
            {
                dGVReviews.Rows[e.RowIndex].Cells[0].Value = "0";
                dGVReviews.Rows[e.RowIndex].Cells[1].Value = "Delete";
                dGVReviews.Rows[e.RowIndex].Cells[2].Value = "";
                dGVReviews.Rows[e.RowIndex].Cells[3].Value = "";
                dGVReviews.Rows[e.RowIndex].Cells[4].Value = "0";
                dGVReviews.Rows[e.RowIndex].Cells[5].Value = "";
                dGVReviews.Rows[e.RowIndex].Cells[6].Value = "";
                dGVReviews.Rows[e.RowIndex].Cells[7].Value = "";
            }
        }

        private void dGVReviews_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected Review - continue?", "Delete Review", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dGVReviews.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        #endregion

        #region SaveMethods

        //tab 1
        public void SaveHotelContracts()
        {
            List<GulliverLibrary.HotelContract> hotelContracts = new List<GulliverLibrary.HotelContract>();

            if (dataGridviewOfferContracts.Rows.Count > 0)
            {
                foreach (GulliverDS.HotelContractsRow contract in gulliverDS.HotelContracts.Rows)
                {
                    GulliverLibrary.HotelContract hotelContract = new GulliverLibrary.HotelContract();
                    MySqlDataHandler.AcCGuiD tripperAccomGuid = packageHandler.GetAccomGuidByRecNo(contract.Recno);

                    if (tripperAccomGuid != null)
                    {
                        hotelContract.id = contract.id;
                        hotelContract.apt = tripperAccomGuid.Apt.Trim();
                        hotelContract.recno = (int)tripperAccomGuid.SrRecNo;
                        hotelContract.accomcode = tripperAccomGuid.AcComCode.Trim();
                        hotelContract.codename = tripperAccomGuid.CodenAme.Trim();
                        hotelContract.Deal = deal;
                        hotelContract.fullname = tripperAccomGuid.FullName.Trim();
                        hotelContract.resort = tripperAccomGuid.Resort.Trim();
                        hotelContract.isEntryRoom = contract.EntryRoom;
                        hotelContracts.Add(hotelContract);
                    }
                }
            }

            packageHandler.UpdateHotelContracts(hotelContracts, deal.id);
        }

        public void SaveManulContracts()
        {
            List<GulliverLibrary.ManualHotelContract> manualContracts = new List<GulliverLibrary.ManualHotelContract>();

            if (dataGridViewHotelContracts.Rows.Count > 0)
            {
                foreach (GulliverDS.ManualContractRow contract in gulliverDS.ManualContract.Rows)
                {
                    GulliverLibrary.ManualHotelContract manualContract = new GulliverLibrary.ManualHotelContract();
                    manualContract.id = contract.id;
                    manualContract.Deal = deal;
                    manualContract.allotment = contract.allotment;
                    manualContract.currency = ((ComboBoxItem)ddlCurrency.SelectedItem).Value.ToString();
                    manualContract.fromDate = contract.fromDate;
                    manualContract.toDate = contract.toDate;
                    manualContract.price = contract.price;
                    manualContracts.Add(manualContract);
                }
            }

            packageHandler.UpdateManaulHotelContract(manualContracts, deal.id);
        }

        public void SaveChildAge()
        {
            GulliverLibrary.ChildrenAge childAge = new GulliverLibrary.ChildrenAge();
            childAge.Deal = deal;

            if ((txtChildAgeFrom.Enabled && txtChildAgeTo.Enabled) || (txtInfantAgeFrom.Enabled && txtInfantAgeTo.Enabled))
            {
                childAge.childAgeFrom = Convert.ToDecimal(txtChildAgeFrom.Text);
                childAge.childAgeTo = Convert.ToDecimal(txtChildAgeTo.Text);
                childAge.infantAgeFrom = Convert.ToDecimal(txtInfantAgeFrom.Text);
                childAge.infantAgeTo = Convert.ToDecimal(txtInfantAgeTo.Text);
                packageHandler.UpdateChildAges(childAge, deal.id);
            }        
        }

        //tab 2
        private void SaveFlights()
        {
            SetStepProgressBar(progressBarTP2);
            deal.arrivalAirports = txtArrivalAirports.Text.Trim();
            deal.startDate = dtpStartDate.Value;
            deal.endDate = dtpEndDate.Value;
            deal.durations = string.Join("#", cbDurations.CheckedItems.Cast<string>().ToArray());
            List<string> departureAirports = new List<string>();
            departureAirports.AddRange(cbDepartureAirports.CheckedItems.Cast<ComboBoxItem>().Select(i => i.Value.ToString()));
            deal.departureAirports = string.Join("#", departureAirports.Distinct().ToArray());
            deal.isFAB = cbFlightTypes.GetItemChecked(0);
            deal.isFlightSheet = cbFlightTypes.GetItemChecked(1);
            deal.isAmadeus = cbFlightTypes.GetItemChecked(2);
            deal.isFABMM = cbFlightTypes.GetItemChecked(3);
            deal.filteredAirlines = string.Join("#", cbAirlines.CheckedItems.Cast<string>().ToArray());
            deal.baggageType = (rbnBGPP.Checked) ? "1" : ((rbnBGTwo.Checked) ? "2" : "0");
            deal.filteredWeekdays = string.Join("#", cbWeekDays.CheckedItems.Cast<string>().ToArray());
            deal.filteredFlightTimesOBDeparture = ftOBDepartureFrom.Text.Trim() + "#" + ftOBDepartureTo.Text.Trim();
            deal.filteredFlightTimesOBArrival = ftOBArrivalFrom.Text.Trim() + "#" + ftOBArrivalTo.Text.Trim();
            deal.filteredFlightTimesIBDeparture = ftIBDepartureFrom.Text.Trim() + "#" + ftIBDepartureTo.Text.Trim();
            deal.filteredFlightTimesIBArrival = ftIBArrivalFrom.Text.Trim() + "#" + ftIBArrivalTo.Text.Trim();
            deal.lastupdatedTime = DateTime.Now;
            dealId = packageHandler.UpdateDeal(deal);
            SetStepProgressBar(progressBarTP2);
            SaveHotelContracts();

            SetStepProgressBar(progressBarTP2);
            SaveChildAge();
            SetStepProgressBar(progressBarTP2);
        }

        //tab 3
        private void SaveTripperExtars()
        {
            List<GulliverLibrary.TripperExtra> tripperExtras = new List<GulliverLibrary.TripperExtra>();

            foreach (GulliverDS.TripperExtraRow e in this.gulliverDS.TripperExtra)
            {
                if (selectedTripperExtras.Contains(e.recno))
                {
                    GulliverLibrary.TripperExtra tripperExtra = new GulliverLibrary.TripperExtra();
                    int recno = e.recno;
                    MySqlDataHandler.AcCGuiD accomGuid = queryHandler.GetAccomGuidByRecNo(recno);

                    tripperExtra.id = e.id;
                    tripperExtra.accomcode = accomGuid.AcComCode;
                    tripperExtra.apt = accomGuid.Apt;
                    tripperExtra.codename = accomGuid.CodenAme;
                    tripperExtra.Deal = deal;
                    tripperExtra.fullname = accomGuid.FullName;
                    tripperExtra.recno = e.recno;
                    tripperExtra.resort = accomGuid.Resort;
                    tripperExtras.Add(tripperExtra);
                }
            }

            packageHandler.UpdateTripperExtras(tripperExtras, deal.id);
        }

        private void SaveCarHire()
        {
            List<GulliverLibrary.CarHire> carHires = new List<GulliverLibrary.CarHire>();

            foreach (GulliverDS.CarhireRow carhire in this.gulliverDS.Carhire)
            {
               GulliverLibrary.CarHire objCarHire = new GulliverLibrary.CarHire();
               objCarHire.amount = carhire.Amount;
               objCarHire.currency = carhire.Currency;
               objCarHire.Deal = deal;
               objCarHire.endDate = carhire.EndDate;
               objCarHire.startDate = carhire.StartDate;
               objCarHire.id = carhire.id;
               carHires.Add(objCarHire);
            }

            packageHandler.UpdateCarHire(carHires, deal.id);
        } 

        private void SaveCarParking()
        {
            List<GulliverLibrary.CarParking> carParkings = new List<GulliverLibrary.CarParking>();

            foreach (GulliverDS.CarParkingRow carParking in this.gulliverDS.CarParking)
            {
              GulliverLibrary.CarParking objCarparking = new GulliverLibrary.CarParking();
              objCarparking.amount = carParking.Amount;
              objCarparking.currency = carParking.Currency;
              objCarparking.Deal = deal;
              objCarparking.endDate = carParking.EndDate;
              objCarparking.startDate = carParking.StartDate;
              objCarparking.id = carParking.id;
              carParkings.Add(objCarparking);
            }

            packageHandler.UpdateCarParking(carParkings, deal.id);
        }

        private void SaveExtra()
        {
            List<GulliverLibrary.Extra> extras = new List<GulliverLibrary.Extra>();

            foreach (GulliverDS.ExtraRow extraRow in this.gulliverDS.Extra)
            {
                GulliverLibrary.Extra objExtra = new GulliverLibrary.Extra();
                objExtra.adultPrice = extraRow.AdultPrice;
                objExtra.childPrice = extraRow.ChildPrice;
                objExtra.Deal = deal;
                objExtra.description = extraRow.Description;
                objExtra.isIncluded = extraRow.Include;
                objExtra.id = extraRow.id;
                extras.Add(objExtra);
            }            

            packageHandler.UpdateExtras(extras, deal.id);
        }

        private void SaveExtras()
        {
            SetStepProgressBar(progressBarTP3);
            SaveTripperExtars();
            SetStepProgressBar(progressBarTP3);
            SaveExtra();
            SetStepProgressBar(progressBarTP3);
            SaveCarHire();
            SetStepProgressBar(progressBarTP3);
            SaveCarParking();
            SetStepProgressBar(progressBarTP3);
        }

        //tab 4
        private void SaveDurationCostings()
        {
            List<GulliverLibrary.DurationCosting> durationCostings = new List<GulliverLibrary.DurationCosting>();

            foreach (CostingsDS.DurationCostingRow durationCosting in this.costingsDS.DurationCosting)
            {
                GulliverLibrary.DurationCosting objDurCostings = new GulliverLibrary.DurationCosting();
                objDurCostings.Deal = deal;
                objDurCostings.id = durationCosting.id;
                objDurCostings.duration = Convert.ToInt32(durationCosting.Duration);
                objDurCostings.occupancy = durationCosting.Occupancy;
                objDurCostings.minSellAt = durationCosting.MinSellAt;
                objDurCostings.maxSellAt = durationCosting.MaxSellAt;
                objDurCostings.minChildSellAt = durationCosting.ChildMinSellAt;
                objDurCostings.maxChildSellAt = durationCosting.ChildMaxSellAt;
                objDurCostings.minMarkupFirstRange = durationCosting.MinMLeading;
                objDurCostings.minMarkupOtherRange = durationCosting.MinMStandard;
                objDurCostings.minMarkupOtherRangeType = durationCosting.MimMStandardType;
                objDurCostings.increasedBy = durationCosting.IncreasedBy;
                durationCostings.Add(objDurCostings);
            }

            packageHandler.UpdateDurationCostings(durationCostings, deal.id);
        }

        private void SaveSecretEscapeCostings()
        {
            GulliverLibrary.SecretEscapeMarkup secretEscapeMarkups = new GulliverLibrary.SecretEscapeMarkup();
            secretEscapeMarkups.Deal = deal;
            secretEscapeMarkups.leadingMarkup = (txtMinimumMarkup.Text != string.Empty)?Convert.ToDecimal(txtMinimumMarkup.Text):0;
            secretEscapeMarkups.standardMarkup = (txtStandardMarkup.Text != string.Empty)?Convert.ToDecimal(txtStandardMarkup.Text):0;

            packageHandler.UpdateSecretEscapeMarkup(secretEscapeMarkups, deal.id);
        }

        private void SaveDurationMarkup()
        {
            List<GulliverLibrary.DurationMarkup> durationMarkups = new List<GulliverLibrary.DurationMarkup>();

            foreach (CostingsDS.DurationMarkupRow durationMarkup in this.costingsDS.DurationMarkup)
            {
               GulliverLibrary.DurationMarkup objDurMarkup = new GulliverLibrary.DurationMarkup();
               objDurMarkup.Deal = deal;
               objDurMarkup.duration = durationMarkup.Duration;
               objDurMarkup.id = durationMarkup.id;
               objDurMarkup.adultMarkup = durationMarkup.AdultMarkup;
               objDurMarkup.childMarkup = durationMarkup.ChildMarkup;
               durationMarkups.Add(objDurMarkup);
            }
            packageHandler.UpdateDurationMarkup(durationMarkups, deal.id);
        }

        private void SaveWeekDayMarkup()
        {
            List<GulliverLibrary.WeekDayMarkup> weekDayMarkups = new List<GulliverLibrary.WeekDayMarkup>();

            foreach (CostingsDS.WeekdayMarkupRow weekDayMarkup in this.costingsDS.WeekdayMarkup)
            {
               GulliverLibrary.WeekDayMarkup objWeekdayMarkup = new GulliverLibrary.WeekDayMarkup();
               objWeekdayMarkup.Deal = deal;
               objWeekdayMarkup.weekday = weekDayMarkup.Weekday;
               objWeekdayMarkup.Deal = deal;
               objWeekdayMarkup.id = weekDayMarkup.id;
               objWeekdayMarkup.adultMarkup = weekDayMarkup.AdultMarkup;
               objWeekdayMarkup.childMarkup = weekDayMarkup.ChildMarkup;
               weekDayMarkups.Add(objWeekdayMarkup);
            }

            packageHandler.UpdateWeekdayMarkup(weekDayMarkups, deal.id);
        }

        private void SaveRoomtypeMarkup()
        {
            List<GulliverLibrary.RoomTypeMarkup> roomTypeMarkups = new List<GulliverLibrary.RoomTypeMarkup>();

            foreach (CostingsDS.RoomTypeMarkupRow roomTypeMarkup in this.costingsDS.RoomTypeMarkup)
            {
               GulliverLibrary.RoomTypeMarkup objRoomtypeMarkup = new GulliverLibrary.RoomTypeMarkup();
               objRoomtypeMarkup.Deal = deal;
               objRoomtypeMarkup.roomType = roomTypeMarkup.Name;
               objRoomtypeMarkup.code = roomTypeMarkup.code;
               objRoomtypeMarkup.id = roomTypeMarkup.id;
               objRoomtypeMarkup.adultPrice = roomTypeMarkup.Adult;
               objRoomtypeMarkup.childPrice = roomTypeMarkup.Child;
               roomTypeMarkups.Add(objRoomtypeMarkup);
            }

            packageHandler.UpdateRoomTypeMarkup(roomTypeMarkups, deal.id);
        }

        private void SaveSupplierMarkup()
        {
            List<GulliverLibrary.SupplierMarkup> supplierMarkups = new List<GulliverLibrary.SupplierMarkup>();

            foreach (CostingsDS.SupplierMarkupRow supplierMarkup in this.costingsDS.SupplierMarkup)
            {
               GulliverLibrary.SupplierMarkup objSupplierMarkup = new GulliverLibrary.SupplierMarkup();
               objSupplierMarkup.Deal = deal;
               objSupplierMarkup.supplier = supplierMarkup.Supplier;
               objSupplierMarkup.id = supplierMarkup.id;
               objSupplierMarkup.adultMarkup = supplierMarkup.AdultMarkup;
               supplierMarkups.Add(objSupplierMarkup);
            }

            packageHandler.UpdateSupplierMarkup(supplierMarkups, deal.id);
        }

        private void SaveDepartureAirportMarkup()
        {
            List<GulliverLibrary.DepartureAirportMarkup> departureAirportMarkups = new List<GulliverLibrary.DepartureAirportMarkup>();

            foreach (CostingsDS.DepartureAirportMarkupRow departureAPMarkup in this.costingsDS.DepartureAirportMarkup)
            {
               GulliverLibrary.DepartureAirportMarkup objDepartureAPMarkup = new GulliverLibrary.DepartureAirportMarkup();
               objDepartureAPMarkup.Deal = deal;
               objDepartureAPMarkup.airport = departureAPMarkup.DepartureAirport;
               objDepartureAPMarkup.id = departureAPMarkup.id;
               objDepartureAPMarkup.adultMarkup = departureAPMarkup.AdultMarkup;
               objDepartureAPMarkup.childMarkup = departureAPMarkup.ChildMarkup;
               departureAirportMarkups.Add(objDepartureAPMarkup);
            }

            packageHandler.UpdateDepartureAirportMarkup(departureAirportMarkups, deal.id);
        }

        private void SaveDestinationAirportMarkup()
        {
            List<GulliverLibrary.DestinationAirportMarkup> destinationAirportMarkups = new List<GulliverLibrary.DestinationAirportMarkup>();

            foreach (CostingsDS.DestinationAirportMarkupRow destinationAPMarkup in this.costingsDS.DestinationAirportMarkup)
            {
                GulliverLibrary.DestinationAirportMarkup objDestinationAPMarkup = new GulliverLibrary.DestinationAirportMarkup();
               objDestinationAPMarkup.Deal = deal;
               objDestinationAPMarkup.airport = destinationAPMarkup.Destination;
               objDestinationAPMarkup.id = destinationAPMarkup.id;
               objDestinationAPMarkup.adultMarkup = destinationAPMarkup.AdultMarkup;
               objDestinationAPMarkup.childMarkup = destinationAPMarkup.ChildMarkup;
               destinationAirportMarkups.Add(objDestinationAPMarkup);
            }

            packageHandler.UpdateDestinationAirportMarkup(destinationAirportMarkups, deal.id);
        }

        private void SaveDateRangeMarkup()
        {
            List<GulliverLibrary.DateRangeMarkup> dateRangeMarkups = new List<GulliverLibrary.DateRangeMarkup>();

            foreach (CostingsDS.DateRangeMarkupRow destinationAPMarkup in this.costingsDS.DateRangeMarkup)
            {
               GulliverLibrary.DateRangeMarkup objDateRangeMarkup = new GulliverLibrary.DateRangeMarkup();
               objDateRangeMarkup.Deal = deal;
               objDateRangeMarkup.startDate = destinationAPMarkup.StartDate;
               objDateRangeMarkup.id = destinationAPMarkup.id;
               objDateRangeMarkup.endDate = destinationAPMarkup.EndDate;
               objDateRangeMarkup.adultMarkup = destinationAPMarkup.AdultMarkup;
               objDateRangeMarkup.childMarkup = destinationAPMarkup.ChildMarkup;
               dateRangeMarkups.Add(objDateRangeMarkup);
            }

            packageHandler.UpdateDateRangeMarkup(dateRangeMarkups, deal.id);
        }

        private void SaveLowAvailabilityMarkup()
        {
            List<GulliverLibrary.LowAvailabilityMarkup> lowAvailabilityMarkups = new List<GulliverLibrary.LowAvailabilityMarkup>();

            foreach (CostingsDS.LowAvailabilityMarkupRow lowAvailabilityMarkup in this.costingsDS.LowAvailabilityMarkup)
            {
               GulliverLibrary.LowAvailabilityMarkup objLowAvailabilityMarkup = new GulliverLibrary.LowAvailabilityMarkup();
               objLowAvailabilityMarkup.Deal = deal;
               objLowAvailabilityMarkup.startDate = lowAvailabilityMarkup.StartDate;
               objLowAvailabilityMarkup.noOfRooms = lowAvailabilityMarkup.NoOfRooms;
               objLowAvailabilityMarkup.id = lowAvailabilityMarkup.id;
               objLowAvailabilityMarkup.endDate = lowAvailabilityMarkup.EndDate;
               objLowAvailabilityMarkup.adultMarkup = lowAvailabilityMarkup.AdultMarkup;
               objLowAvailabilityMarkup.childMarkup = lowAvailabilityMarkup.ChildMarkup;
               lowAvailabilityMarkups.Add(objLowAvailabilityMarkup);
            }

            packageHandler.UpdateLowAvailabilityMarkup(lowAvailabilityMarkups, deal.id);
        }

        private void SaveCostings()
        {
            deal.baseMarkup = (txtBaseMarkup.Text != string.Empty && Validator.isDecimal(txtBaseMarkup.Text)) ? Convert.ToDecimal(txtBaseMarkup.Text) : 0;
            dealId = packageHandler.UpdateDeal(deal);
            SaveDurationCostings();
            SaveDurationMarkup();
            SaveWeekDayMarkup();
            SaveRoomtypeMarkup();
            SaveSupplierMarkup();
            SaveDepartureAirportMarkup();
            SaveDestinationAirportMarkup();
            SaveDateRangeMarkup();
            SaveLowAvailabilityMarkup();
            deal = packageHandler.GetDealById(dealId);
        }        
       
        //save deal
        
        private void SaveOfferDetails()
        {
            VisibleProgressBar(progressBarTP1, true);
            SetStepProgressBar(progressBarTP1);
            deal.name = txtDealName.Text.Trim();
            deal.starRating = (int)numericStars.Value;
            deal.dateOfPromotion = dtpSalesOn.Value;
            deal.endDateOfPromotion = dtpBookBy.Value;
            deal.Media = (ddlMedias.SelectedItem != null) ? gulliverQueryHandler.GetMediaByCode(((ComboBoxItem)ddlMedias.SelectedItem).Value.ToString()) : null;
            deal.DealType = (ddlDealTypes.SelectedItem != null) ? gulliverQueryHandler.GetDealTypeById(Convert.ToInt32(((ComboBoxItem)ddlDealTypes.SelectedItem).Value.ToString())) : null;
            SetStepProgressBar(progressBarTP1);
            deal.cruiseDeal = cbCruiseDeal.Checked;

            if (txtCommission.Text.Trim() != string.Empty)
            {
                if (!Validator.ValidPrice(txtCommission.Text))
                {
                    KryptonMessageBox.Show("Commission is not valid, please check correct!");
                    return;
                }
            }

            deal.commission = (txtCommission.Text.Trim() != string.Empty) ? Convert.ToDecimal(txtCommission.Text) : 0;
            deal.dealCode = txtDealCode.Text.Trim();
            deal.productType = (cmbProducttypes.SelectedItem == null) ? string.Empty : cmbProducttypes.SelectedItem.ToString();
            deal.productId = string.Empty; // need to setup new field in gulliver UI
            deal.occupancy = (cbOcupancy.CheckedItems != null) ? string.Join("#", cbOcupancy.CheckedItems.Cast<ComboBoxItem>().Select(o => o.Value.ToString()).ToArray()) : string.Empty;
            deal.boards = (cbBoards.CheckedItems != null) ? string.Join("#", cbBoards.CheckedItems.Cast<ComboBoxItem>().Select(o => o.Value.ToString()).ToArray()) : string.Empty;
            SetStepProgressBar(progressBarTP1);

            if (deal.id != 0)
            {
                SetStepProgressBar(progressBarTP1);
                SaveHotelContracts();
                SaveManulContracts();
                SetStepProgressBar(progressBarTP1);
                SaveChildAge();
                SetStepProgressBar(progressBarTP1);
            }

            VisibleProgressBar(progressBarTP1, false);
        }
        
        private void SearchedHolidays(GulliverLibrary.Deal deal)
        {
            FlightsHandler.SearchRequest searchRequest = new FlightsHandler.SearchRequest();
            searchRequest.IncludeFAB = deal.isFAB;
            searchRequest.IncludeFlightSheet = deal.isFlightSheet;
            searchRequest.ImportFlights = false;
            searchRequest.IncludeOneWayMixMatch = deal.isFABMM;
            searchRequest.IncludeAmadeus = deal.isAmadeus;
            searchRequest.DepartureAirports = deal.departureAirports.Split('#').Distinct().ToList();
            searchRequest.Durations = deal.durations.Split('#').Select(i => Convert.ToInt32(i)).ToList();
            searchRequest.ArrivalAirports = deal.arrivalAirports.ToUpper().Split('#').ToList<string>();
            searchRequest.StartDate = deal.startDate.Date;
            searchRequest.EndDate = deal.endDate.Date;
            searchRequest.SelectedDurations = searchRequest.Durations;
            searchRequest.DurationsNotInSelectedDurations = new List<int>();
            searchRequest.SelectedDepartures = searchRequest.DepartureAirports;
            searchRequest.DeparturesNotInSelectedDepartures = new List<string>();
            searchRequest.IncludeBaggages = (deal.baggageType.Trim() == "0") ? false : true;
            searchRequest.BaggageType = deal.baggageType;
            searchRequest.SelectedStartDate = searchRequest.StartDate;
            searchRequest.SelectedEndDate = searchRequest.EndDate;
            searchRequest.getChepestFromEachSupplier = cbSelectCheapestFromEachSupplier.Checked;
            packages = packageHandler.GenerateHolidays(searchRequest, deal, false);
            flcsPackages packageForm = new flcsPackages(packages, dealId, false, new List<GulliverLibrary.Package>());
            packageForm.ShowDialog();
            deal = packageHandler.GetDealById(dealId);

            if (packageForm.saved)
            {
                FillPackages(packages);
                tabMain.SelectedTab = tabPage5;
            }
        }

        private void CompareHolidays(GulliverLibrary.Deal deal)
        {
            flcsFilterSearch objFilterSearch = new flcsFilterSearch(deal.durations.Split('#').ToList(), packageHandler.GetAllUKAirports().Select(a => a.airportCode.Trim()).ToList(), packageHandler.GetAllGermanAirports().Select(a => a.airportCode.Trim()).ToList(), packageHandler.GetAllUSAAirports().Select(a => a.airportCode.Trim()).ToList(), deal.departureAirports.Split('#').ToList(), deal.startDate, deal.endDate);
            objFilterSearch.ShowDialog();

            if (objFilterSearch.returnToMainwindow)
            {
                progressBar.Visible = false;
                Application.DoEvents();
                return;
            }

            FlightsHandler.SearchRequest searchRequest = new FlightsHandler.SearchRequest();
            searchRequest.IncludeFAB = deal.isFAB;
            searchRequest.IncludeFlightSheet = deal.isFlightSheet;
            searchRequest.ImportFlights = false;
            searchRequest.IncludeOneWayMixMatch = deal.isFABMM;
            searchRequest.IncludeAmadeus = deal.isAmadeus;
            searchRequest.DepartureAirports = deal.departureAirports.Split('#').ToList();
            searchRequest.Durations = deal.durations.Split('#').Select(i => Convert.ToInt32(i)).ToList();
            searchRequest.ArrivalAirports = deal.arrivalAirports.ToUpper().Split('#').ToList<string>();
            searchRequest.StartDate = deal.startDate.Date;
            searchRequest.EndDate = deal.endDate.Date;
            searchRequest.SelectedDurations = (objFilterSearch.selectedDurations != null) ? objFilterSearch.selectedDurations.Where(i => searchRequest.Durations.Contains(i)).ToList() : new List<int>();
            searchRequest.DurationsNotInSelectedDurations = searchRequest.Durations.Where(i => !searchRequest.SelectedDurations.Contains(i)).ToList();
            searchRequest.IncludeBaggages = (deal.baggageType.Trim() == "0") ? false : true;
            searchRequest.BaggageType = deal.baggageType;
            searchRequest.SelectedDepartures = (objFilterSearch.selectedDepartureAirports != null) ? objFilterSearch.selectedDepartureAirports : new List<string>();
            searchRequest.DeparturesNotInSelectedDepartures = searchRequest.DepartureAirports.Where(d => !searchRequest.SelectedDepartures.Contains(d)).ToList();
            searchRequest.SelectedStartDate = objFilterSearch.startDate;
            searchRequest.SelectedEndDate = objFilterSearch.endDate;
            searchRequest.getChepestFromEachSupplier = cbSelectCheapestFromEachSupplier.Checked;

            deal.PackageBackups = packageHandler.GetPackagesByPackageId(deal.id);
            packages = packageHandler.CompareHolidays(searchRequest, deal, true);
            flcsPackages packageForm = new flcsPackages(packages, dealId, true, new List<GulliverLibrary.Package>());
            packageForm.ShowDialog();

            if (packageForm.saved)
            {
                deal = packageHandler.GetDealById(dealId);
                FillPackages(packages);
                tabMain.SelectedTab = tabPage5;
            }
        }
        
        #endregion                      

        private void btnMakePageLive_Click(object sender, EventArgs e)
        {
            if (deal.id != 0)
            {
                string message = dataProcessor.MakePageLive(deal);
                if (message != string.Empty)
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    string url = ConfigurationManager.AppSettings["fleetwayLivePageURL"].ToString() + deal.DealInformation.pageName.Trim() + ".php";
                    System.Diagnostics.Process.Start(url);
                    this.Focus();
                }
            }
            else
                MessageBox.Show("Please save the offer before you genarte any page for Fleetway website!");
        }

        private void btnStopPage_Click(object sender, EventArgs e)
        {

            if (deal.id != 0)
            {
                save = true;
                string message = dataProcessor.UpdateFleetwayPage(deal, cbAirportByAvailability.Checked, true);

                if (message != string.Empty)
                {
                    lblError.Visible = true;
                    lblError.Text = message;
                }
                else
                {
                    email.SendStoppedPage(deal);
                    string url = ConfigurationManager.AppSettings["fleetwaydraftPageURL"].ToString() + deal.DealInformation.pageName.Trim() + ".php";
                    System.Diagnostics.Process.Start(url);
                    this.Focus();
                }
            }
            else
                MessageBox.Show("Please save the offer before you genarte any page for Fleetway website!");
        } 
                          
    }
}

