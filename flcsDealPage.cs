using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Configuration;
using System.Linq;

namespace GulliverII
{
    public partial class flcsDealPage : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private GulliverLibrary.Deal deal;
        private PackageGenerator.PackageHandler dealPageHandler;
        private LandingPageHandler.DataProcessor dataProcessor;
        private List<GulliverLibrary.DealOptionalExtra> optionalCostings;
        private PackageGenerator.Email email;
        private bool save = false;

        public flcsDealPage(int id)
        {
            
            InitializeComponent();
            dealPageHandler = new PackageGenerator.PackageHandler(true);
            deal = dealPageHandler.GetDealById(id);
            dataProcessor = new LandingPageHandler.DataProcessor();

            
            this.tabControl2.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl2_DrawItem);
            ddlDurations.DataSource = deal.durations.Split('#').ToList();
            List<string> currencys = dealPageHandler.GetAllCurrencys();
            cmbCurrency.DataSource = currencys;
            
            email = new PackageGenerator.Email();

            if (deal.DealInformation != null)
                FillDealInformation();
            else
                FillDefaultImages();
        }

        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font tabFont;
            Brush backBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080")); //Set background color
            Brush foreBrush = new SolidBrush(Color.White);//Set foreground color

            if (e.Index == this.tabControl2.SelectedIndex)
            {
                tabFont = new Font("Calibri", 14, FontStyle.Bold, GraphicsUnit.Pixel);
                backBrush = new SolidBrush(Color.White);
                foreBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080"));
            }
            else
                tabFont = new Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Pixel);

            string tabName = this.tabControl2.TabPages[e.Index].Text;
            this.tabControl2.TabPages[e.Index].Width = 200;
            this.tabControl2.TabPages[e.Index].Height = 100;
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

        private void FillDefaultImages()
        {
            gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "best deal page image", string.Empty, string.Empty);
            gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "top left image", string.Empty, string.Empty);
            gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "top right image", string.Empty, string.Empty);
            gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "bottom left image", string.Empty, string.Empty);
            gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "bottom right image", string.Empty, string.Empty);
            gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "body image", string.Empty, string.Empty);
            gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "slider image", string.Empty, string.Empty);
        }

        private void FillDealInformation()
        {
            
            txtMainHeader.Text = (deal.DealInformation.mainHeader != null) ? deal.DealInformation.mainHeader : string.Empty;
            txtSubHeader.Text = (deal.DealInformation.subHeader != null) ? deal.DealInformation.subHeader : string.Empty;
            txtLongitude.Text = (deal.DealInformation.longitude != null) ? deal.DealInformation.longitude : string.Empty;
            txtLatitude.Text = (deal.DealInformation.latitude != null) ? deal.DealInformation.latitude : string.Empty;
            txtYouTubeLink.Text = dealPageHandler.GetLinkByName("YouTube Link", deal.id);
            txtDealIntro.Text = (deal.DealInformation.introduction != null) ? deal.DealInformation.introduction : string.Empty;
            txtChildPrice.Text = (deal.DealInformation.childPrices != null) ? deal.DealInformation.childPrices : string.Empty;
            txtOptionalExtras.Text = (deal.DealInformation.optionalExtras != null) ? deal.DealInformation.optionalExtras : string.Empty;
            txtPleasenote.Text = (deal.DealInformation.pleaseNote != null) ? deal.DealInformation.pleaseNote : string.Empty;
            deal.DealInformation.HotelInformation = dealPageHandler.GetHotelInformationByGeoCodes(deal.DealInformation.longitude.Trim(), deal.DealInformation.latitude.Trim());      
            if (deal.DealInformation.HotelInformation != null)
            {
                txtHotelText.Text = (deal.DealInformation.HotelInformation.hotelBodyText != null) ? deal.DealInformation.HotelInformation.hotelBodyText : string.Empty;
                txtDestinationText.Text = (deal.DealInformation.HotelInformation.destinationText != null) ? deal.DealInformation.HotelInformation.destinationText : string.Empty;
                txtCountryText.Text = (deal.DealInformation.HotelInformation.countryText != null) ? deal.DealInformation.HotelInformation.countryText : string.Empty;
                txtKeyInformationText.Text = (deal.DealInformation.HotelInformation.keyInformation != null) ? deal.DealInformation.HotelInformation.keyInformation.Trim() : string.Empty;
                txtAccessibilityText.Text = (deal.DealInformation.HotelInformation.accessibility != null) ? deal.DealInformation.HotelInformation.accessibility.Trim() : string.Empty;
                txtDestinationTitle.Text = (deal.DealInformation.HotelInformation.destinationHeader != null) ? deal.DealInformation.HotelInformation.destinationHeader.Trim() : string.Empty;
                txtCountryTitle.Text = (deal.DealInformation.HotelInformation.countryHeader != null) ? deal.DealInformation.HotelInformation.countryHeader.Trim() : string.Empty;
                txtHotelTitle.Text = (deal.DealInformation.HotelInformation.hotelHeader != null) ? deal.DealInformation.HotelInformation.hotelHeader : string.Empty;
            }

            txtTripAdvisorLink.Text = dealPageHandler.GetLinkByName("Trip Advisor Link", deal.id);   
            
            cmbCurrency.SelectedItem = (deal.DealInformation.dealCurrency == null || deal.DealInformation.dealCurrency == string.Empty) ? "GBP" : deal.DealInformation.dealCurrency.Trim();
            cmbLanuages.SelectedItem = (deal.DealInformation.language == null || deal.DealInformation.language == string.Empty) ? "English" : deal.DealInformation.language;
            ddlPriorities.SelectedItem = (deal.DealInformation.priority != null) ? deal.DealInformation.priority.ToString() : "0";
            cbGoLiveOnBestDealPage.Checked = (deal.DealInformation.goLiveOnBestDealPage != null) ? deal.DealInformation.goLiveOnBestDealPage : false;
            txtPageName.Text = (deal.DealInformation.pageName != null) ? deal.DealInformation.pageName : string.Empty;
            txtLeadPrice.Text = (deal.DealInformation.leadPrice != null) ? deal.DealInformation.leadPrice : string.Empty;
            txtBestDealHeader.Text = (deal.DealInformation.bestDealHeader != null) ? deal.DealInformation.bestDealHeader : string.Empty;
            txtBestDealDescription.Text = (deal.DealInformation.bestDealDescription != null) ? deal.DealInformation.bestDealDescription : string.Empty;
            ddlBrand.SelectedItem = (deal.DealInformation.brand != null) ? deal.DealInformation.brand : string.Empty;
            txtTopHeader.Text = (deal.DealInformation.topHeader != null) ? deal.DealInformation.topHeader : string.Empty;
            ddlDurations.SelectedItem = (deal.DealInformation.defaultDuration != 0) ? deal.DealInformation.defaultDuration.ToString() : "0";
            rbDays.Checked = (deal.DealInformation.diplayNightsOrDays != null && deal.DealInformation.diplayNightsOrDays.Trim() == "Days") ? true : false;
            rbNights.Checked = (deal.DealInformation.diplayNightsOrDays == null || (deal.DealInformation.diplayNightsOrDays != null && deal.DealInformation.diplayNightsOrDays.Trim() == "Nights") || deal.DealInformation.diplayNightsOrDays == string.Empty) ? true : false;
            optionalCostings = (deal.DealInformation.optionalExtras != null) ? deal.DealExtras.ToList() : new List<GulliverLibrary.DealOptionalExtra>();
            txtHotelLink.Text = dealPageHandler.GetLinkByName("Hotel Website Link", deal.id);
            txtChannelLink.Text = dealPageHandler.GetLinkByName("Channel Page Link", deal.id);   
            FillImages();
            FillReviews();            
            btnStopPage.Enabled = true;
            btnMakePageLive.Enabled = true;
            btnUpdateFleetwayPage.Enabled = true;
        }

        private void FillImages()
        {
            if (deal.DealImages == null || deal.DealImages.Count == 0)
            {
                deal.DealImages = new List<GulliverLibrary.Image>();
                gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "best deal page image", string.Empty, string.Empty);
                gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "top left image", string.Empty, string.Empty);
                gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "top right image", string.Empty, string.Empty);
                gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "bottom left image", string.Empty, string.Empty);
                gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "bottom right image", string.Empty, string.Empty);
                gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "body image", string.Empty, string.Empty);
                gulliverIIDS.Image.AddImageRow(0, "Delete", string.Empty, "slider image", string.Empty, string.Empty);
            }
            else
            {
                foreach (GulliverLibrary.Image image in deal.DealImages)
                    gulliverIIDS.Image.AddImageRow(image.id, "Delete", image.reference, image.title, image.altText, image.description);
            }

            imageId.Visible = false;
        }

        private void FillReviews()
        {
            if (deal.DealReviews != null)
            {
                foreach (GulliverLibrary.Review review in deal.DealReviews)
                    gulliverIIDS.Review.AddReviewRow(review.id, "Delete", review.date, review.source, review.stars, review.title, review.text, review.link);
            }

            reviewId.Visible = false;
        }

        private void SetStepProgressBar(ToolStripProgressBar progressBar)
        {
            progressBar.Value++;
            System.Threading.Thread.Sleep(1);
        }

        private void VisibleProgressBar(ToolStripProgressBar progressBar, bool visible)
        {
            progressBar.Visible = visible;

            if (visible)
            {
                progressBar.Value = 10;
                progressBar.Step = 10;
                SetStepProgressBar(progressBar);
                System.Threading.Thread.Sleep(1);
            }
        }
        
        private void SaveDealInformartion()
        {
            SetStepProgressBar(progressBar);
            List<GulliverLibrary.Link> links = new List<GulliverLibrary.Link>();
            GulliverLibrary.Link link = new GulliverLibrary.Link();
            link.name = "YouTube Link";
            link.Deal = deal;
            link.url = txtYouTubeLink.Text.Trim();
            if (link.url.Trim() != string.Empty)
                links.Add(link);

            deal.DealInformation.mainHeader = txtMainHeader.Text.Trim();
            deal.DealInformation.Deal = deal;
            deal.DealInformation.subHeader = txtSubHeader.Text.Trim();
            deal.DealInformation.longitude = txtLongitude.Text.Trim();
            deal.DealInformation.latitude = txtLatitude.Text.Trim();

            //deal.DealInformation.HotelInformation = gulliverQueryHandler.GetHotelInformationByGeoCodes(deal.DealInformation.longitude.Trim(), deal.DealInformation.latitude.Trim());


            GulliverLibrary.HotelInformation hotelInformation = new GulliverLibrary.HotelInformation();
            hotelInformation.longitude = txtLongitude.Text.Trim();
            hotelInformation.latitude = txtLatitude.Text.Trim();
            hotelInformation.hotelHeader = txtHotelTitle.Text.Trim();
            hotelInformation.hotelBodyText = txtHotelText.Text.Trim();
            hotelInformation.destinationText = txtDestinationText.Text.Trim();
            hotelInformation.countryText = txtCountryText.Text.Trim();
            hotelInformation.accessibility = txtAccessibilityText.Text;
            hotelInformation.keyInformation = txtKeyInformationText.Text;
            hotelInformation.destinationHeader = txtDestinationTitle.Text.Trim();
            hotelInformation.countryHeader = txtCountryTitle.Text.Trim();
            dealPageHandler.UpdateHotelInformation(hotelInformation);

            deal.DealInformation.HotelInformation = hotelInformation;
            deal.DealInformation.introduction = txtDealIntro.Text.Trim();
            deal.DealInformation.childPrices = txtChildPrice.Text.Trim();
            deal.DealInformation.optionalExtras = txtOptionalExtras.Text.Trim();
            deal.DealInformation.pleaseNote = txtPleasenote.Text.Trim();

            GulliverLibrary.Link linkTA = new GulliverLibrary.Link();
            linkTA.name = "Trip Advisor Link";
            linkTA.url = txtTripAdvisorLink.Text.Trim();
            linkTA.Deal = deal;
            if (linkTA.url.Trim() != string.Empty)
                links.Add(linkTA);

            
            deal.DealInformation.dealCurrency = (cmbCurrency.SelectedItem != null) ? cmbCurrency.SelectedItem.ToString() : "GBP";
            deal.DealInformation.language = (cmbLanuages.SelectedItem != null) ? cmbLanuages.SelectedItem.ToString() : "English";
            deal.DealInformation.pageName = txtPageName.Text.Trim();
            deal.DealInformation.leadPrice = txtLeadPrice.Text.Trim();
            deal.DealInformation.bestDealHeader = txtBestDealHeader.Text.Trim();
            deal.DealInformation.bestDealDescription = txtBestDealDescription.Text.Trim();
            deal.DealInformation.brand = (ddlBrand.SelectedItem != null) ? ddlBrand.SelectedItem.ToString() : string.Empty;
            deal.DealInformation.topHeader = txtTopHeader.Text.Trim();
            deal.DealInformation.defaultDuration = (ddlDurations.SelectedItem != null && ddlDurations.SelectedItem != string.Empty) ? Convert.ToInt32(ddlDurations.SelectedItem) : 0;
            deal.DealInformation.diplayNightsOrDays = (rbDays.Checked) ? "Days" : "Nights";
            deal.DealInformation.priority = Convert.ToInt32(ddlPriorities.SelectedItem);
            deal.DealInformation.goLiveOnBestDealPage = cbGoLiveOnBestDealPage.Checked;

            GulliverLibrary.Link linkHW = new GulliverLibrary.Link();
            linkHW.name = "Hotel Website Link";
            linkHW.Deal = deal;
            linkHW.url = txtHotelLink.Text.Trim();
            if (linkTA.url.Trim() != string.Empty)
                links.Add(linkHW);

            GulliverLibrary.Link pageLink = new GulliverLibrary.Link();
            pageLink.name = "Landing Page Link";
            pageLink.Deal = deal;
            pageLink.url = ConfigurationManager.AppSettings["fleetwayLivePageURL"].ToString() + deal.DealInformation.pageName.Trim() + ".php";
            if (pageLink.url.Trim() != string.Empty)
                links.Add(pageLink);

            GulliverLibrary.Link channelLink = new GulliverLibrary.Link();
            channelLink.name = "Channel Page Link";
            channelLink.Deal = deal;
            channelLink.url = txtChannelLink.Text.Trim();
            if (channelLink.url.Trim() != string.Empty)
                links.Add(channelLink);

            List<GulliverLibrary.Image> dealImages = new List<GulliverLibrary.Image>();
            foreach (GulliverIIDS.ImageRow imageRow in this.gulliverIIDS.Image)
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

            SetStepProgressBar(progressBar);
            List<GulliverLibrary.Review> dealReviews = new List<GulliverLibrary.Review>();

            foreach (GulliverIIDS.ReviewRow reviewRow in this.gulliverIIDS.Review)
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
            deal.Links = links;
            SetStepProgressBar(progressBar);
            progressBar.Value = progressBar.Maximum;
            VisibleProgressBar(progressBar, false);
        }

        //images

        private void dataGridviewImages_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridviewImages.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewImages.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGridviewImages.CurrentCell = (dataGridviewImages.Rows.Count == 0) ? dataGridviewImages.Rows[0].Cells[0] : null;

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

            if (e.RowIndex >= 0 && dGVReviews.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dGVReviews.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dGVReviews.CurrentCell = (dGVReviews.Rows.Count == 0) ? dGVReviews.Rows[0].Cells[0] : null;
                try
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
                catch { }
            }

        }

        private void dGVReviews_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dGVReviews.Rows[e.RowIndex].Cells[0].Value == null)
            {
                dGVReviews.Rows[e.RowIndex].Cells[0].Value = "0";
                dGVReviews.Rows[e.RowIndex].Cells[1].Value = "Delete";
                dGVReviews.Rows[e.RowIndex].Cells[2].Value = DateTime.Today;
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
       

        private void btnSavePage_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblMessage.Visible = false;
            VisibleProgressBar(progressBar, true);
            System.Threading.Thread.Sleep(500);


            try
            {
                if (deal.DealInformation == null)
                {
                    deal.DealInformation = new GulliverLibrary.DealInformation();
                    deal.DealInformation.HotelInformation = dealPageHandler.GetHotelInformationByGeoCodes(txtLongitude.Text, txtLatitude.Text);
                }

                if (txtLongitude.Text.Trim() == string.Empty || txtLatitude.Text.Trim() == string.Empty)
                return;
                
                SaveDealInformartion();                

                if (deal.id != 0)
                {
                    dealPageHandler.SaveDealInformation(deal);
                    btnStopPage.Enabled = true;
                    btnMakePageLive.Enabled = true;
                    btnUpdateFleetwayPage.Enabled = true;
                }
                else
                    lblError.Text = "Please save the deal before add any deal information for page!";

                MessageBox.Show("Information has been saved successfully!", "Save!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("Error while saving the details, please check and try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblError.Visible = true;
            }
            VisibleProgressBar(progressBar, false);
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
            flcsOptionalExtra objOptionalExtras = new flcsOptionalExtra(optionalCostings, deal.id, dealPageHandler);
            objOptionalExtras.ShowDialog();
            optionalCostings = objOptionalExtras.optioanlExtras;
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
                deal = dealPageHandler.GetDealById(deal.id);
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


        
    }
}