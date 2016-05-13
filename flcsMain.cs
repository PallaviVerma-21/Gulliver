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

namespace GulliverII
{
    public partial class flcsMain : ComponentFactory.Krypton.Toolkit.KryptonForm, IDisposable
    {
        private MySqlDataHandler.QueryHandler queryHandler;
        private PackageGenerator.Email email;
        private GulliverLibrary.QueryHandler gulliverQueryHandler;
        private GulliverLibrary.Deal deal;
        private PackageGenerator.PackageHandler packageHandler;
        private PackageGenerator.ICosting icosting;
        private LandingPageHandler.DataProcessor dataProcessor;
        private List<int> selectedTripperExtras;
        List<GulliverLibrary.Package> packages;

        int supplierId = 0;
        int dealId = 0;
        private List<string> visibleColumns;


        public flcsMain()
        {

            InitializeComponent();
            email = new PackageGenerator.Email();
            queryHandler = new MySqlDataHandler.QueryHandler();
            gulliverQueryHandler = new GulliverLibrary.QueryHandler();
            packageHandler = new PackageGenerator.PackageHandler(true);
            dataProcessor = new LandingPageHandler.DataProcessor();
            visibleColumns = new List<string>();

            SetupDefaultWindow(true);
            FillMedias(0);
            FillDealType(0);
        }

        public flcsMain(int id)
        {
            InitializeComponent();
            email = new PackageGenerator.Email();
            queryHandler = new MySqlDataHandler.QueryHandler();
            gulliverQueryHandler = new GulliverLibrary.QueryHandler();
            packageHandler = new PackageGenerator.PackageHandler(true);
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

            deal.startDate = Convert.ToDateTime("01/01/1753");
            deal.endDate = Convert.ToDateTime("01/01/1753");
            deal.lastupdatedTime = Convert.ToDateTime("01/01/1753");
            deal.dateOfPromotion = Convert.ToDateTime("01/01/1753");
            deal.endDateOfPromotion = Convert.ToDateTime("01/01/1753");

            SetText(txtSearchbox, "Search Transfers here ...");
            SetText(txtSearchBoard, "Search Board Basis here ...");

            if (newDeal)
            {
                FillTripperExtras(new List<int>(), string.Empty, false);
                btnUpdateDeal.Visible = false;
                List<GulliverLibrary.Extra> extras = new List<GulliverLibrary.Extra>();
                GulliverLibrary.Extra extra = new GulliverLibrary.Extra();
                extra.description = "transfers";
                extra.isIncluded = false;
                extra.adultPrice = 0;
                extra.childPrice = 0;
                dtpBookBy.Value = DateTime.Today;
                dtpSalesOn.Value = DateTime.Today;
                dtpEndDate.Value = DateTime.Today;
                dtpStartDate.Value = DateTime.Today;
                GulliverLibrary.Extra extraI = new GulliverLibrary.Extra();
                extraI.description = "CAA";
                extraI.isIncluded = false;
                extraI.adultPrice = 7;
                extraI.childPrice = 0;
                extras.Add(extraI);
                extras.Add(extra);
                FillExtras(extras);
                lblOfferCreatedBy.Text = "Created by " + Environment.UserName;
                lblOfferCreatedBy.Visible = true;

            }

            this.tabMain.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabMain.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);

            this.tabControl3.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl3.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl3_DrawItem);

            this.tabControl4.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.tabControl4.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl2_DrawItem);

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
                    using (DataGridViewColumn dataGridViewColumn = dataGridViewHolidays.Columns.Cast<DataGridViewColumn>().SingleOrDefault(c => c.HeaderText.ToString() == column))
                    {
                        if (dataGridViewColumn != null)
                            dataGridViewColumn.Visible = true;
                    }
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

            SecretEscapeFormToolStripMenuItem.Visible = visible;
        }

        private void VisibleDailyMailFile(int mediaId)
        {
            if (mediaId == 37)
                DailyMailToolStripMenuItem.Visible = true;
            else
                DailyMailToolStripMenuItem.Visible = false;
        }

        private void VisibleTelegraph(int mediaId)
        {
            if (mediaId == 35)
            {
                txtProductCode.Visible = true;
                lblProductCode.Visible = true;
            }
            else
            {
                txtProductCode.Visible = false;
                lblProductCode.Visible = false;
            }
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

                if (occupncyArray.Count() == 3)
                {
                    if (occupncyArray[1].ToString() != "0")
                        enableChildrenAges = true;


                    if (occupncyArray[2].ToString() != "0")
                        enableInfantAges = true;
                }
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
            System.Threading.Thread.Sleep(1);
        }

        private void VisibleProgressBar(ProgressBar progressBar, bool visible)
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

        #endregion

        #region events

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

        private void btnUpdtePageContent_Click(object sender, EventArgs e)
        {
            if (deal.id != 0)
            {
                deal = packageHandler.GetDealById(deal.id);
                if (deal.DealInformation != null && deal.DealInformation.longitude != null && deal.DealInformation.latitude != null)
                {
                    deal.DealInformation.HotelInformation = packageHandler.GetHotelInformationByGeoCodes(deal.DealInformation.longitude.Trim(), deal.DealInformation.latitude.Trim());
                    string message = dataProcessor.UpdateFleetwayPage(deal, false, false);
                    if (message != string.Empty)
                    {
                        MessageBox.Show(message);
                    }
                    else
                    {
                        string url = ConfigurationManager.AppSettings["fleetwaydraftPageURL"].ToString() + deal.DealInformation.pageName.Trim() + ".php";
                        System.Diagnostics.Process.Start(url);
                        this.Focus();
                    }
                }
                else
                    MessageBox.Show("Please update deal information before generat the page!");
            }
            else
                MessageBox.Show("Please save the offer before you genarate any page for Fleetway website!");
        }

        private void btnRoomRequestSetting_Click(object sender, EventArgs e)
        {
            using (flcsRoomRequestSetting roomRequestSettingForm = new flcsRoomRequestSetting("0", deal.id))
            {
                roomRequestSettingForm.ShowDialog();
            }
        }

        private void txtRoomType_TextChanged(object sender, EventArgs e)
        {
            roomTypeCB.DataSource = new List<string>() { string.Empty, txtRoomType.Text.Trim().ToUpper() };

            if (deal != null && deal.id != 0)
            {
                List<GulliverLibrary.DurationCosting> durationCostings = gulliverQueryHandler.GetDurationCostingByDealId(deal.id);
                foreach (GulliverLibrary.DurationCosting durationCosting in durationCostings)
                    durationCosting.roomType = txtRoomType.Text.Trim().ToUpper();
                deal.DurationCostings = durationCostings;
                FillDurationCostings(durationCostings);
            }
        }

        private void cbAirportGroups_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool checkeD = cbAirportGroups.GetItemChecked(e.Index);

            if (cbAirportGroups.SelectedItem != null)
            {
                List<string> airports = ((ComboBoxItem)cbAirportGroups.SelectedItem).Value.ToString().Split('#').ToList();

                for (int i = 0; i <= cbDepartureAirports.Items.Count - 1; i++)
                {
                    string value = ((ComboBoxItem)cbDepartureAirports.Items[i]).Value.ToString().Trim().ToUpper();
                    if (airports.Contains(value))
                        cbDepartureAirports.SetItemChecked(i, !checkeD);
                }
            }
        }

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

            if (e.Index == this.tabControl4.SelectedIndex)
            {
                tabFont = new Font("Segoe UI", 11, FontStyle.Bold, GraphicsUnit.Pixel);
                backBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#73A2DB"));
                foreBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#004080"));
            }
            else
                tabFont = new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Pixel);

            string tabName = this.tabControl4.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            Rectangle r = e.Bounds;
            r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height);
            e.Graphics.DrawString(tabName, tabFont, foreBrush, r, sf);
            //Dispose objects
            sf.Dispose();

            if (e.Index == this.tabControl4.SelectedIndex)
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
            List<string> invalidContracts = new List<string>();
            List<string> exsisitingReconIds = (dataGridviewOfferContracts.Rows != null && dataGridviewOfferContracts.Rows.Count > 0) ?

             dataGridviewOfferContracts.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[1].Value != null).Select(r => r.Cells[1].Value.ToString().Trim()).ToList() : new List<string>();

            if (dataGridviewContracts.SelectedRows != null && dataGridviewContracts.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in dataGridviewContracts.SelectedRows)
                {
                    if (Convert.ToDateTime(r.Cells[6].Value.ToString()) > DateTime.Now)
                    {
                        if (!exsisitingReconIds.Contains(r.Cells[1].Value.ToString().Trim()))
                            ids.Add(Convert.ToInt32(r.Cells[1].Value.ToString()));
                    }
                    else
                        invalidContracts.Add(r.Cells[2].Value.ToString().Trim());
                }

                List<MySqlDataHandler.AcCGuiD> contracts = packageHandler.GetAccomGuidByRecNos(ids);
                FillOfferContracts(contracts);

                if (invalidContracts.Count > 0)
                    MessageBox.Show("Please note that these contracts (" + string.Join(", ", invalidContracts) + ") have been expried! ");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bool sucess = SaveOfferDetails();

            if (sucess)
            {
                if (cbCruiseDeal.Checked)
                    tabMain.SelectedTab = tabPage3;
                else
                    tabMain.SelectedTab = tabPage2;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("This will not save all changes - continue?", "Close Deal", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    this.Close();
                    break;

                case System.Windows.Forms.DialogResult.No:
                    break;
            }
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
            if (PackageGenerator.Tool.GetSuppliersBySuppliertype("setypesuppliers").Contains(deal.Media.id))
                SaveSecretEscapeCostings();

            SetStepProgressBar(progressBarTP4);
            CompareHolidays(deal);
            btnRemoveOnlyStoppedSales.Visible = true;
            //VisibleProgressBar(progressBarTP4, false);
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

        private void cbBoards_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortBoardBasis();
        }

        private void SortBoardBasis()
        {
            List<ComboBoxItem> allcontents = cbBoards.Items.Cast<ComboBoxItem>().ToList();
            List<ComboBoxItem> checkedContents = cbBoards.CheckedItems.Cast<ComboBoxItem>().ToList();
            List<ComboBoxItem> searchItems = cbBoards.Items.Cast<ComboBoxItem>().Where(i => i.Text.ToString().ToUpper().Contains(txtSearchBoard.Text.Trim().ToUpper())).ToList();


            searchItems = searchItems.Where(a => !checkedContents.Any(c => c == a)).ToList();
            allcontents = allcontents.Where(a => !checkedContents.Any(c => c == a)).ToList();
            allcontents = allcontents.Where(a => !searchItems.Any(c => c == a)).ToList();

            cbBoards.Items.Clear();

            foreach (ComboBoxItem item in checkedContents.OrderBy(c => c.Value))
                cbBoards.Items.Add(item, true);

            foreach (ComboBoxItem item in searchItems.OrderBy(c => c.Value))
                cbBoards.Items.Add(item, false);

            foreach (ComboBoxItem item in allcontents.OrderBy(a => a.Value))
                cbBoards.Items.Add(item, false);
        }

        private void txtSearchBoard_Enter(object sender, EventArgs e)
        {
            if (txtSearchBoard.ForeColor != Color.Black)
            {
                txtSearchBoard.Text = string.Empty;
                txtSearchBoard.ForeColor = Color.Black;
            }
        }

        private void txtSearchBoard_Leave(object sender, EventArgs e)
        {
            if (txtSearchBoard.Text.Trim() == string.Empty && ddlResorts.Items != null && ddlResorts.Items.Count > 0)
                SetText(txtSearchBoard, "Search Board Basis here ...");
        }

        private void txtSearchBoard_TextChanged(object sender, EventArgs e)
        {
            SortBoardBasis();
        }

        private void cbShowCosting_CheckedChanged(object sender, EventArgs e)
        {
            FillTripperExtras(selectedTripperExtras, txtSearchbox.Text.ToUpper().Trim(), cbShowCosting.Checked);
        }

        private void btnUpdatePrices_Click(object sender, EventArgs e)
        {
            using (flcsPackages packageForm = new flcsPackages(packageHandler, deal.Packages.ToList(), dealId, false, new List<GulliverLibrary.Package>()))
            {
                packageForm.ShowDialog();

                if (packageForm.saved)
                {
                    using (GulliverLibrary.QueryHandler gulliverQueryHandler = new GulliverLibrary.QueryHandler(@"Server=APP-1\SQLSERVER;Database=Gulliver;User ID=FleetwayServices;Password=flw388;"))
                    {
                        List<GulliverLibrary.Package> packagesList = gulliverQueryHandler.GetPackagesByDeal(dealId);
                        deal = packageHandler.GetDealById(dealId);
                        deal.Packages = packagesList;
                        FillPackages(packagesList);
                    }
                }
            }
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

            btnUpdateDeal.Visible = true;
            btnRemoveOnlyStoppedSales.Visible = true;
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
                using (GulliverLibrary.Media media = packageHandler.GetMediaById(mediaId))
                {
                    if (media != null)
                        txtCommission.Text = (media.commission != 0) ? media.commission.ToString() : string.Empty;
                }

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

                VisibleDailyMailFile(mediaId);
                VisibleTelegraph(mediaId);
            }
        }

        private void cbOcupancy_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableChildPrices(cbOcupancy.CheckedItems.Cast<ComboBoxItem>().Select(i => i.Value.ToString()).ToList());
            FillOccupancyComboBox(cbOcupancy.CheckedItems.Cast<ComboBoxItem>().Select(i => i.Value.ToString()).ToList());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                string id = dataGridviewOfferContracts.SelectedCells[1].Value.ToString();
                using (flcsRoomRequestSetting roomRequestSettingForm = new flcsRoomRequestSetting(id, dealId))
                {
                    roomRequestSettingForm.ShowDialog();
                }
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

        private void SecretEscapeFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                progressBarMenu.Visible = true;
                Application.DoEvents();
                progressBarMenu.Maximum = 5;
                progressBarMenu.Value = 1;
                progressBarMenu.Value++;
                System.Threading.Thread.Sleep(1000);
                progressBarMenu.Value++;
                packageHandler.GenerateSecretEscapeForm(dealId, folderBrowserDialog.SelectedPath);
                progressBarMenu.Value++;
                System.Threading.Thread.Sleep(1000);
                progressBarMenu.Value++;
                progressBarMenu.Step = progressBar.Maximum;
                progressBarMenu.Visible = false;
                Application.DoEvents();
                MessageBox.Show("Form has been saved to" + folderBrowserDialog.SelectedPath, "Secret Escape Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DailyMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim() != string.Empty)
            {
                progressBarMenu.Visible = true;
                Application.DoEvents();
                progressBarMenu.Maximum = 4;
                progressBarMenu.Value = 1;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV Documents (*.CSV)|*.csv";
                sfd.FileName = Text.Trim() + ".csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    progressBar.Value++;

                    using (PackageGenerator.Report report = new PackageGenerator.Report())
                    {
                        List<GulliverLibrary.Package> updatedPackages = packageHandler.GetPackagesByDeal(dealId);
                        report.WriteToDailyMailCSV(updatedPackages, sfd.FileName.Trim(), txtProductCode.Text.Trim());
                    }
                    progressBar.Value++;
                    MessageBox.Show("Export to " + sfd.FileName);

                }

                progressBarMenu.Value++;
                progressBarMenu.Visible = false;
                Application.DoEvents();
            }
            else
                MessageBox.Show("Please enter product id before you generate the file!");
        }

        private void emailToTechToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBarMenu.Visible = true;
            Application.DoEvents();
            progressBarMenu.Maximum = 5;
            progressBarMenu.Value = 1;
            progressBarMenu.Value++;
            System.Threading.Thread.Sleep(1000);
            progressBarMenu.Value++;
            email.SendTechSupport(deal);
            progressBarMenu.Value++;
            progressBarMenu.Visible = false;
            Application.DoEvents();
            MessageBox.Show("Email has been sent sucessfully! ");
        }

        //private void SaveDealInformartion()
        //{
        //    SetStepProgressBar(progressBarMenu);
        //    List<GulliverLibrary.Link> links = new List<GulliverLibrary.Link>();
        //    GulliverLibrary.Link link = new GulliverLibrary.Link();
        //    link.name = "YouTube Link";
        //    link.Deal = deal;
        //    link.url = txtYouTubeLink.Text.Trim();
        //    if (link.url.Trim() != string.Empty)
        //        links.Add(link);

        //    deal.DealInformation.mainHeader = txtMainHeader.Text.Trim();
        //    deal.DealInformation.Deal = deal;
        //    deal.DealInformation.subHeader = txtSubHeader.Text.Trim();
        //    deal.DealInformation.longitude = txtLongitude.Text.Trim();
        //    deal.DealInformation.latitude = txtLatitude.Text.Trim();

        //    //deal.DealInformation.HotelInformation = gulliverQueryHandler.GetHotelInformationByGeoCodes(deal.DealInformation.longitude.Trim(), deal.DealInformation.latitude.Trim());


        //        GulliverLibrary.HotelInformation hotelInformation = new GulliverLibrary.HotelInformation();
        //        hotelInformation.longitude = txtLongitude.Text.Trim();
        //        hotelInformation.latitude = txtLatitude.Text.Trim();
        //        hotelInformation.hotelHeader = txtHotelTitle.Text.Trim();
        //        hotelInformation.hotelBodyText = txtHotelText.Text.Trim();
        //        hotelInformation.destinationText = txtDestinationText.Text.Trim();
        //        hotelInformation.countryText = txtCountryText.Text.Trim();
        //        hotelInformation.accessibility = txtAccessibilityText.Text;
        //        hotelInformation.keyInformation = txtKeyInformationText.Text;
        //        hotelInformation.destinationHeader = txtDestinationTitle.Text.Trim();
        //        hotelInformation.countryHeader = txtCountryTitle.Text.Trim();
        //        gulliverQueryHandler.UpdateHotelInformation(hotelInformation);

        //    deal.DealInformation.HotelInformation = hotelInformation;
        //    deal.DealInformation.introduction = txtDealIntro.Text.Trim();
        //    deal.DealInformation.childPrices = txtChildPrice.Text.Trim();
        //    deal.DealInformation.optionalExtras = txtOptionalExtras.Text.Trim();
        //    deal.DealInformation.pleaseNote = txtPleasenote.Text.Trim();

        //    GulliverLibrary.Link linkTA = new GulliverLibrary.Link();
        //    linkTA.name = "Trip Advisor Link";
        //    linkTA.url = txtTripAdvisorLink.Text.Trim();
        //    linkTA.Deal = deal;
        //    if (linkTA.url.Trim() != string.Empty)
        //        links.Add(linkTA);

        //    deal.DealInformation.howToBook = txtHowToBook.Text.Trim();
        //    deal.DealInformation.dealCurrency = (cmbCurrency.SelectedItem != null) ? cmbCurrency.SelectedItem.ToString() : "GBP";
        //    deal.DealInformation.language = (cmbLanuages.SelectedItem != null) ? cmbLanuages.SelectedItem.ToString() : "English";
        //    deal.DealInformation.pageName = txtPageName.Text.Trim();
        //    deal.DealInformation.leadPrice = txtLeadPrice.Text.Trim();
        //    deal.DealInformation.bestDealHeader = txtBestDealHeader.Text.Trim();
        //    deal.DealInformation.bestDealDescription = txtBestDealDescription.Text.Trim();
        //    deal.DealInformation.brand = (ddlBrand.SelectedItem != null) ? ddlBrand.SelectedItem.ToString() : string.Empty;
        //    deal.DealInformation.topHeader = txtTopHeader.Text.Trim();
        //    deal.DealInformation.defaultDuration = (ddlDurations.SelectedItem != null && ddlDurations.SelectedItem != string.Empty) ? Convert.ToInt32(ddlDurations.SelectedItem) : 0;
        //    deal.DealInformation.diplayNightsOrDays = (rbDays.Checked) ? "Days" : "Nights";
        //    deal.DealInformation.priority = Convert.ToInt32(ddlPriorities.SelectedItem);
        //    deal.DealInformation.goLiveOnBestDealPage = cbGoLiveOnBestDealPage.Checked;

        //    GulliverLibrary.Link linkHW = new GulliverLibrary.Link();
        //    linkHW.name = "Hotel Website Link";
        //    linkHW.Deal = deal;
        //    linkHW.url = txtHotelLink.Text.Trim();
        //    if (linkTA.url.Trim() != string.Empty)
        //        links.Add(linkHW);

        //    GulliverLibrary.Link pageLink = new GulliverLibrary.Link();
        //    pageLink.name = "Landing Page Link";
        //    pageLink.Deal = deal;
        //    pageLink.url = ConfigurationManager.AppSettings["fleetwayLivePageURL"].ToString() + deal.DealInformation.pageName.Trim() + ".php";
        //    if (pageLink.url.Trim() != string.Empty)
        //        links.Add(pageLink);

        //    GulliverLibrary.Link channelLink = new GulliverLibrary.Link();
        //    channelLink.name = "Channel Page Link";
        //    channelLink.Deal = deal;
        //    channelLink.url = txtChannelLink.Text.Trim();
        //    if (channelLink.url.Trim() != string.Empty)
        //        links.Add(channelLink);

        //    List<GulliverLibrary.Image> dealImages = new List<GulliverLibrary.Image>();
        //    foreach (GulliverIIDS.ImageRow imageRow in this.GulliverIIDS.Image)
        //    {
        //        if ((imageRow.Reference != null) && (imageRow.Title != null))
        //        {
        //            try
        //            {
        //                GulliverLibrary.Image dealImage = new GulliverLibrary.Image();
        //                dealImage.id = imageRow.id;
        //                dealImage.Deal = deal;
        //                dealImage.reference = (imageRow.Reference != null) ? imageRow.Reference : string.Empty;
        //                dealImage.altText = (imageRow.Alt_Text != null) ? imageRow.Alt_Text : string.Empty;
        //                dealImage.description = (imageRow.Description != null) ? imageRow.Description : string.Empty;
        //                dealImage.title = (imageRow.Title != null) ? imageRow.Title : string.Empty;
        //                dealImages.Add(dealImage);
        //            }
        //            catch { }
        //        }
        //    }

        //    SetStepProgressBar(progressBarMenu);
        //    List<GulliverLibrary.Review> dealReviews = new List<GulliverLibrary.Review>();

        //    foreach (GulliverIIDS.ReviewRow reviewRow in this.GulliverIIDS.Review)
        //    {
        //        try
        //        {
        //            GulliverLibrary.Review review = new GulliverLibrary.Review();
        //            review.id = reviewRow.id;
        //            review.Deal = deal;
        //            review.date = reviewRow.Date;
        //            review.source = (reviewRow.Source != null) ? reviewRow.Source : string.Empty;
        //            review.stars = (reviewRow.Stars != null) ? reviewRow.Stars : 0;
        //            review.link = (reviewRow.Link != null) ? reviewRow.Link : string.Empty;
        //            review.text = (reviewRow.Text != null) ? reviewRow.Text : string.Empty;
        //            review.title = (reviewRow.Title != null) ? reviewRow.Title : string.Empty;
        //            dealReviews.Add(review);
        //        }
        //        catch { }
        //    }

        //    deal.DealImages = dealImages;
        //    deal.DealReviews = dealReviews;
        //    deal.Links = links;
        //    SetStepProgressBar(progressBarMenu);
        //    progressBarMenu.Value = progressBarMenu.Maximum;
        //    VisibleProgressBar(progressBarMenu, false);
        //}

        private void SaveDealInstruction()
        {
            if (deal.DealInstructions == null)
                deal.DealInstructions = new GulliverLibrary.DealInstruction();

            deal.DealInstructions.howToBook = txtHowToBook.Text.Trim();
            deal.DealInstructions.Deal = deal;
            deal.DealInstructions.importantUpsell = txtImportantUpsell.Text.Trim();
            deal.DealInstructions.pleaseNote = txtPleasenoteII.Text.Trim();
            packageHandler.UpdateDealInstruction(deal.DealInstructions);
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
                MessageBox.Show("Export to " + sfd.FileName, "Export Files");
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
                MessageBox.Show("Export to " + sfd.FileName, "Export Files");
            }

            progressBarMenu.Step = progressBar.Maximum;
            progressBarMenu.Visible = false;
            Application.DoEvents();
        }

        private void restoreFlightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (flcsBackupPackage objBackupPackage = new flcsBackupPackage(dealId, packageHandler.GetPackageBackupsByDeal(dealId), packageHandler))
            {
                objBackupPackage.ShowDialog();
            }
        }

        private void manipulateDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (flcsFilterColumns objFilterColumn = new flcsFilterColumns(this.packagesDS.PackageBackup.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList(), visibleColumns))
            {
                objFilterColumn.ShowDialog();
                visibleColumns = objFilterColumn.visibleColumns;
                VisibleColumns();
            }
        }

        private void setLeadingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (flcsSetLeading setLeadings = new flcsSetLeading(dealId, packageHandler))
            {
                setLeadings.ShowDialog();
            }
        }

        private void cbAllUS_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbUSAirports.Items.Count - 1; i++)
                cbUSAirports.SetItemCheckState(i, (cbAllUS.Checked ? CheckState.Checked : CheckState.Unchecked));
        }

        private void cbAllCanadianAirports_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbCanadianAirports.Items.Count - 1; i++)
                cbCanadianAirports.SetItemCheckState(i, (cbAllCanadianAirports.Checked ? CheckState.Checked : CheckState.Unchecked));
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

            for (int i = 0; i <= cbAirportGroups.Items.Count - 1; i++)
                cbAirportGroups.SetItemCheckState(i, (cbAllAirports.Checked ? CheckState.Checked : CheckState.Unchecked));
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
            FillTripperExtras(selectedTripperExtras, txtSearchbox.Text.ToUpper().Trim(), cbShowCosting.Checked);
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
            using (flcsAutoUpdateSetting autoUpdateSetting = new flcsAutoUpdateSetting(dealId))
            {
                autoUpdateSetting.Show();
            }
        }

        private void cbAllDurations_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= cbDurations.Items.Count - 1; i++)
                cbDurations.SetItemCheckState(i, (cbAllDurations.Checked ? CheckState.Checked : CheckState.Unchecked));

            List<string> durations = cbDurations.CheckedItems.Cast<string>().ToList();
            FillDurationComboBox(durations);
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
                txtProductCode.Text = (deal.productId != null) ? deal.productId : string.Empty;
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
                cbTakeFollowingDayFromDeparture.Checked = (deal.takeFollowingDateFromDeparture != null) ? deal.takeFollowingDateFromDeparture : false;
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
                cbFlightTypes.SetItemChecked(5, deal.isFABCache);
                rbnBGPP.Checked = (deal.baggageType.Trim() == "1") ? true : false;
                rbnBGTwo.Checked = (deal.baggageType.Trim() == "2") ? true : false;
                rbnNoBaggage.Checked = (deal.baggageType.Trim() == "0") ? true : false;
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

                FillTripperExtras(selectedTripperExtras, string.Empty, false);
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
                else
                    btnRemoveOnlyStoppedSales.Visible = false;

                if (deal.ManualHotelContracts != null && deal.ManualHotelContracts.Count > 0)
                {
                    FillHotelContracts(deal.ManualHotelContracts.ToList());
                    FillSupplements(deal.Supplements.ToList());
                    FillFreenights(deal.FreeNights.ToList());
                    FillBlackouts(deal.Blackouts.ToList());
                    FillDiscounts(deal.Discounts.ToList());
                }

                // tab 5
                if (deal.DealInstructions != null)
                    FillDealInstructions();

            }
        }

        private void FillCurrencyComboBox()
        {
            List<string> currencys = packageHandler.GetAllCurrencys();
            carParkingCurrency.DataSource = currencys;
            carHireCurrency.DataSource = currencys;
            supplementCurrencyCB.DataSource = currencys;

            List<string> types = new List<string> { "%", "price" };
            DiscountType.DataSource = types;
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
            FillNewDurationCostingForNewOccupancy(occupancys);
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
            List<string> occupancies = new List<string>();
            List<string> boards = new List<string>();

            foreach (MySqlDataHandler.AcCGuiD accomGuid in contracts.OrderByDescending(a => a.ValidFrom))
            {
                using (MySqlDataHandler.Expand building = packageHandler.GetBuildingByCodes(accomGuid.AcComCode.Trim()))
                {
                    boards.Add(accomGuid.BoardBasis.Trim().ToUpper());
                    int adults = (int)((building != null) ? building.PricedOCc.Value : 0);
                    int maxKids = (int)((building != null) ? (building.MaxKids != null) ? building.MaxKids.Value : 0 : 0);
                    int children = maxKids;
                    adults -= children;
                    string occupancy = adults + "," + children + ",0";
                    occupancies.Add(occupancy);
                    GulliverIIDS.HotelContracts.AddHotelContractsRow("Delete", 0, (int)accomGuid.SrRecNo, accomGuid.FullName.Trim(), ((building.Building != null) ? building.Building.Trim() + " " + building.PricedOCc + " Occupancy" : string.Empty), accomGuid.BoardBasis.Trim(), accomGuid.ValidFrom.Value, accomGuid.ValidTo.Value, false);
                }
            }

            for (int i = 0; i < cbBoards.Items.Count; i++)
                if (boards.Contains(((ComboBoxItem)cbBoards.Items[i]).Value))
                    cbBoards.SetItemChecked(i, true);
            SortBoardBasis();

            for (int i = 0; i < cbOcupancy.Items.Count; i++)
                if (occupancies.Contains(((ComboBoxItem)cbOcupancy.Items[i]).Value))
                    cbOcupancy.SetItemChecked(i, true);

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
            hotels = hotels.Where(h => h.ValidTo > DateTime.Today && !Convert.ToBoolean(h.Grouped.Value)).ToList();

            if (searchText != string.Empty && searchText != "Search Contracts here ...".ToUpper())
                hotels = hotels.Where(h => h.FullName.ToUpper().Contains(searchText.ToUpper())).ToList();

            foreach (MySqlDataHandler.AcCGuiD accomGuid in hotels.OrderByDescending(a => a.ValidFrom))
            {
                using (MySqlDataHandler.Expand building = packageHandler.GetBuildingByCodes(accomGuid.AcComCode.Trim()))
                {
                    tripper.Contracts.AddContractsRow("Select", (int)accomGuid.SrRecNo, accomGuid.FullName.Trim(), ((building != null) ? building.Building.Trim() + " " + building.PricedOCc + " Occupancy" : string.Empty), accomGuid.BoardBasis.Trim(), accomGuid.ValidFrom.Value, accomGuid.ValidTo.Value);
                }
            }
        }

        private void FillHotelContracts(List<GulliverLibrary.HotelContract> hotelContracts)
        {
            GulliverIIDS.HotelContracts.Rows.Clear();

            foreach (GulliverLibrary.HotelContract hotelContract in hotelContracts)
            {
                using (MySqlDataHandler.Expand building = packageHandler.GetBuildingByCodes(hotelContract.accomcode.Trim()))
                {
                    using (MySqlDataHandler.AcCGuiD accomGuid = packageHandler.GetAccomGuidByRecNo(hotelContract.recno))
                    {
                        GulliverIIDS.HotelContracts.AddHotelContractsRow("Delete", hotelContract.id, (int)hotelContract.recno, hotelContract.fullname.Trim(), building.Building.Trim() + " " + building.PricedOCc + " Occupancy", accomGuid.BoardBasis.Trim(), accomGuid.ValidFrom.Value, accomGuid.ValidTo.Value, hotelContract.isEntryRoom);
                    }
                }
            }

            FillRoomTypeComboBox(hotelContracts);
        }

        private void FillSupplements(List<GulliverLibrary.Supplement> supplements)
        {
            this.GulliverIIDS.Supplements.Clear();

            if (supplements != null)
                foreach (GulliverLibrary.Supplement s in supplements.OrderBy(s => s.fromDate))
                    this.GulliverIIDS.Supplements.AddSupplementsRow("Delete", s.id, s.fromDate, s.toDate, s.currency, s.price);
        }

        private void FillDiscounts(List<GulliverLibrary.Discount> discounts)
        {
            this.GulliverIIDS.Discounts.Clear();

            if (discounts != null)
                foreach (GulliverLibrary.Discount d in discounts.OrderBy(d => d.fromDate))
                    this.GulliverIIDS.Discounts.AddDiscountsRow("Delete", d.id, d.fromDate, d.toDate, d.discount, (d.type.Trim() == "1") ? "%" : d.type.Trim());
        }

        private void FillBlackouts(List<GulliverLibrary.Blackout> blackouts)
        {
            this.GulliverIIDS.Blackouts.Clear();

            if (blackouts != null)
                foreach (GulliverLibrary.Blackout b in blackouts.OrderBy(b => b.fromDate))
                    this.GulliverIIDS.Blackouts.AddBlackoutsRow("Delete", b.id, b.fromDate, b.toDate);
        }

        private void FillFreenights(List<GulliverLibrary.FreeNight> freeNights)
        {
            this.GulliverIIDS.Freenights.Clear();

            if (freeNights != null)
                foreach (GulliverLibrary.FreeNight f in freeNights.OrderBy(f => f.fromDate))
                    this.GulliverIIDS.Freenights.AddFreenightsRow("Delete", f.id, f.fromDate, f.toDate, f.actual, f.paid);
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

            //SortBoardBasis();
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

            List<TripperLibrary.AirportGroup> airportGroups = packageHandler.GetAllAirportGroups();
            foreach (TripperLibrary.AirportGroup airportGroup in airportGroups.OrderBy(a => a.name))
            {
                ComboBoxItem item = new ComboBoxItem();
               
                    item.Text = airportGroup.name.Trim();
                    item.Value = airportGroup.airports.Trim();
                    cbAirportGroups.Items.Add(item);
               
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

            List<TripperLibrary.CanadianAirport> candinAirports = packageHandler.GetAllCanadianAirports();

            foreach (TripperLibrary.CanadianAirport canadianAirport in candinAirports)
            {
               ComboBoxItem item = new ComboBoxItem();
                
                    item.Text = canadianAirport.airportCode.Trim();
                    item.Value = canadianAirport.airportCode.Trim();
                    cbCanadianAirports.Items.Add(item);
                
            }
        }

        private void FillNewDurationCostingForNewOccupancy(List<string> occupancys)
        {
            if (costingsDS.DurationCosting != null)
            {
                if (deal.durations != null && deal.durations != string.Empty)
                {
                    List<string> durationCostingOccupancys = (costingsDS.DurationCosting != null && costingsDS.DurationCosting.Count > 0) ? costingsDS.DurationCosting.Select(d => d.Occupancy).Distinct().ToList() : new List<string>();
                    List<string> newOccupanys = occupancys.Where(o => !durationCostingOccupancys.Any(d => d == o)).ToList();

                    foreach (string occupancy in newOccupanys)
                    {
                        foreach (string duration in deal.durations.Split('#'))
                        {
                            using (GulliverLibrary.DurationCosting durationCosting = deal.DurationCostings.SingleOrDefault(d => d.occupancy.Trim() == occupancy.Trim() && d.duration == Convert.ToInt32(duration)))
                            {
                                if (durationCosting != null)
                                    costingsDS.DurationCosting.AddDurationCostingRow("Delete", durationCosting.id, durationCosting.duration.ToString(), durationCosting.occupancy, durationCosting.roomType.Trim(), durationCosting.minSellAt, durationCosting.maxSellAt, durationCosting.minChildSellAt, durationCosting.maxChildSellAt, durationCosting.minMarkupFirstRange, durationCosting.minMarkupOtherRange, durationCosting.minMarkupOtherRangeType, durationCosting.increasedBy);
                                else
                                    costingsDS.DurationCosting.AddDurationCostingRow("Delete", 0, duration, occupancy, string.Empty, 0, 0, 0, 0, 0, 0, "£", 0);
                            }
                        }
                    }

                    List<string> removedOccupanys = durationCostingOccupancys.Where(o => !occupancys.Any(d => d == o)).ToList();
                    List<CostingsDS.DurationCostingRow> durationCostingRows = costingsDS.DurationCosting.Where(d => removedOccupanys.Contains(d.Occupancy)).ToList();
                    foreach (CostingsDS.DurationCostingRow occupancy in durationCostingRows)
                        costingsDS.DurationCosting.RemoveDurationCostingRow(occupancy);
                }
            }
        }

        private void FillNewDurationCostingForNewDuration(List<string> durations)
        {
            if (costingsDS.DurationCosting != null)
            {
                if (deal.occupancy != null && deal.occupancy != string.Empty)
                {
                    OccupancyComboBox.DataSource = deal.occupancy.Split('#').ToList();
                    List<string> durationCostingOccupancys = (costingsDS.DurationCosting != null && costingsDS.DurationCosting.Count > 0) ? costingsDS.DurationCosting.Select(d => d.Duration.ToString()).Distinct().ToList() : new List<string>();
                    List<string> newDurations = durations.Where(o => !durationCostingOccupancys.Any(d => d == o.ToString())).Select(o => o.ToString()).ToList();

                    foreach (string duration in newDurations)
                    {
                        foreach (string occupancy in deal.occupancy.Split('#'))
                        {
                            using (GulliverLibrary.DurationCosting durationCosting = deal.DurationCostings.SingleOrDefault(d => d.occupancy.Trim() == occupancy.Trim() && d.duration == Convert.ToInt32(duration)))
                            {
                                if (durationCosting != null)
                                    costingsDS.DurationCosting.AddDurationCostingRow("Delete", durationCosting.id, durationCosting.duration.ToString(), durationCosting.occupancy, durationCosting.roomType.Trim(), durationCosting.minSellAt, durationCosting.maxSellAt, durationCosting.minChildSellAt, durationCosting.maxChildSellAt, durationCosting.minMarkupFirstRange, durationCosting.minMarkupOtherRange, durationCosting.minMarkupOtherRangeType, durationCosting.increasedBy);
                                else
                                    costingsDS.DurationCosting.AddDurationCostingRow("Delete", 0, duration, occupancy, string.Empty, 0, 0, 0, 0, 0, 0, "£", 0);
                            }
                        }
                    }

                    List<string> removedDurations = durationCostingOccupancys.Where(o => !durations.Any(d => d.ToString() == o)).ToList();
                    List<CostingsDS.DurationCostingRow> durationCostingRows = costingsDS.DurationCosting.Where(d => removedDurations.Contains(d.Duration.ToString())).ToList();
                    foreach (CostingsDS.DurationCostingRow row in durationCostingRows)
                        costingsDS.DurationCosting.RemoveDurationCostingRow(row);
                }
            }
        }

        private void FillRoomTypeComboBox(List<GulliverLibrary.HotelContract> hotelContracts)
        {
            List<string> roomTypes = hotelContracts.Select(h => h.accomcode).Distinct().ToList();
            List<string> roomNames = new List<string>();
            List<string> roomNamesII = new List<string>();

            foreach (string roomType in roomTypes)
            {
                MySqlDataHandler.Expand expand = packageHandler.GetBuildingByCodes(roomType);
                roomNames.Add(expand.Building.Trim() + "-" + roomType);
                roomNamesII.Add(expand.Building.Trim());
            }

            roomTypeCombobox.DataSource = roomNames;
            roomNamesII.Add(string.Empty);
            roomTypeCB.DataSource = roomNamesII;
        }

        private void FillDepartureAirportComboBox(List<string> departureAirports)
        {
            departureAirportComboBox.DataSource = departureAirports;
        }

        private void FillDestinationAirportComboBox(List<string> destinationAirports)
        {
            destiantionAirportComboBox.DataSource = destinationAirports;
        }

        private void FillPackages(List<GulliverLibrary.Package> packages)
        {
            packagesDS.Package.Clear();
            int count = 0;
            foreach (GulliverLibrary.Package p in packages)
                packagesDS.Package.AddPackageRow("Delete", p.id, count, (p.leading) ? 1 : 0, p.date.ToString("MMMM"), p.date.DayOfWeek.ToString(), p.date, "", p.hotelKey, p.departureAirport.Trim(), p.destinationAirport.Trim(), p.duration, p.obDepartureTime, p.obArrivalTime, p.ibDepartureTime, p.ibArrivalTime, p.board, p.flightPrice, p.airline, p.obFlightNo, p.ibFlightNo, p.roomType, p.occupancy,
                    p.adults, p.children, p.infants, Math.Round(p.hotelPrice, 2), Math.Round(p.childHotelPrice), p.caa, p.baggagePrice, p.transfers, p.extras, p.childExtras, p.baseMarkup, p.totalMarkup, p.totalChildMarkup, p.carhireCosting, p.carParkingCosting, p.commission, p.profit, p.nett, p.sellAt, p.childNett, p.childSellat, ((p.searchType == 1) ? "FAB" : "Flightsheet"), p.status, p.oldSellAt, p.isStandardRoom);

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

        private void FillTripperExtras(List<int> tripperExtraRecnos, string searchText, bool showCost)
        {
            List<MySqlDataHandler.AcCGuiD> accomGuids = queryHandler.GetAccomGuidByApt("EXR");
            accomGuids = accomGuids.Where(a => a.ValidTo >= DateTime.Today && !Convert.ToBoolean(a.Grouped.Value)).ToList();

            List<MySqlDataHandler.Currency> currencies = currencies = queryHandler.GetCurrency();

            if (searchText != string.Empty && searchText != "Search Transfers here ...".ToUpper())
                accomGuids = accomGuids.Where(a => a.FullName.ToUpper().Contains(searchText.ToUpper())).ToList();

            GulliverIIDS.TripperExtra.Rows.Clear();

            foreach (MySqlDataHandler.AcCGuiD accomGuid in accomGuids.OrderByDescending(a => a.ValidFrom))
            {
                decimal extraCost = 0;
                if (showCost)
                {
                    List<MySqlDataHandler.NettAcC> costs = queryHandler.GetNetPricesByAccomGuid(accomGuid.Apt.Trim(), accomGuid.Resort.Trim(), accomGuid.AcComCode.Trim(), accomGuid.CodenAme.Trim());
                    List<MySqlDataHandler.NettAcC> selectedCosts = costs.Where(c => c.PaXNett != null).ToList();


                    if (costs != null && costs.Count > 0)
                    {
                        extraCost = (selectedCosts != null && selectedCosts.Count > 0) ? (decimal)selectedCosts.First().PaXNett.Value : 0;
                        decimal exchangeRate = (decimal)currencies.SingleOrDefault(c => c.Currency1.Trim().ToUpper() == costs.Where(a => a.Currency != null).First().Currency.Trim().ToUpper()).XRate.Value;
                        extraCost = Math.Round(extraCost / exchangeRate, 2);
                    }
                }
                GulliverIIDS.TripperExtra.AddTripperExtraRow("Delete", (int)accomGuid.SrRecNo, 0, accomGuid.FullName.Trim(), accomGuid.ValidFrom.Value, accomGuid.ValidTo.Value, extraCost, tripperExtraRecnos.Contains((int)accomGuid.SrRecNo));
            }

            dataGridviewTripperExtras.Columns[6].Visible = showCost;
        }

        private void FillSelectedOccupancy(string selectedOccupancys)
        {
            for (int i = 0; i < cbOcupancy.Items.Count; i++)
            {
                if (selectedOccupancys.Contains(((ComboBoxItem)cbOcupancy.Items[i]).Value.ToString()))
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
            SortBoardBasis();
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
            GulliverIIDS.ManualContract.Rows.Clear();

            if (manualHotelContracts != null && manualHotelContracts.Count > 0)
            {
                txtRoomType.Text = (manualHotelContracts.First().roomType != null) ? manualHotelContracts.First().roomType.Trim() : string.Empty;
                foreach (ComboBoxItem item in ddlCurrency.Items)
                {
                    if (item.Value.ToString() == manualHotelContracts.First().currency.Trim())
                        ddlCurrency.SelectedItem = item;
                }
                roomTypeCB.DataSource = new List<string>() { string.Empty, txtRoomType.Text.Trim().ToUpper() };
            }

            foreach (GulliverLibrary.ManualHotelContract manualContract in manualHotelContracts)
                GulliverIIDS.ManualContract.AddManualContractRow("Delete", manualContract.id, manualContract.fromDate.Date, manualContract.toDate.Date, manualContract.price, manualContract.allotment);
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
            GulliverIIDS.Carhire.Rows.Clear();

            foreach (GulliverLibrary.CarHire carHire in carHires)
                GulliverIIDS.Carhire.AddCarhireRow("Delete", carHire.id, carHire.startDate, carHire.endDate, carHire.currency.Trim(), carHire.amount);
        }

        private void FillCarParking(List<GulliverLibrary.CarParking> carParkings)
        {
            GulliverIIDS.CarParking.Rows.Clear();

            foreach (GulliverLibrary.CarParking carParking in carParkings)
                GulliverIIDS.CarParking.AddCarParkingRow("Delete", carParking.id, carParking.startDate, carParking.endDate, carParking.currency, carParking.amount);
        }

        private void FillExtras(List<GulliverLibrary.Extra> extras)
        {
            GulliverIIDS.Extra.Rows.Clear();

            foreach (GulliverLibrary.Extra extra in extras)
                GulliverIIDS.Extra.AddExtraRow("Delete", extra.id, extra.description.Trim(), extra.isIncluded, extra.adultPrice, extra.childPrice);
        }

        private void FillDurationCostings(List<GulliverLibrary.DurationCosting> durationCostings)
        {
            costingsDS.DurationCosting.Rows.Clear();

            if (durationCostings != null && durationCostings.Count > 0)
            {
                foreach (GulliverLibrary.DurationCosting durationCosting in durationCostings)
                    costingsDS.DurationCosting.AddDurationCostingRow("Delete", durationCosting.id, durationCosting.duration.ToString(), durationCosting.occupancy, ((durationCosting.roomType != null) ? durationCosting.roomType.Trim() : string.Empty), durationCosting.minSellAt, durationCosting.maxSellAt, durationCosting.minChildSellAt, durationCosting.maxChildSellAt, durationCosting.minMarkupFirstRange, durationCosting.minMarkupOtherRange, durationCosting.minMarkupOtherRangeType, durationCosting.increasedBy);
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
                        costingsDS.DurationCosting.AddDurationCostingRow("Delete", 0, duration, occupancy, string.Empty, 0, 0, 0, 0, 0, 0, "£", 0);
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

            for (int i = 0; i < cbCanadianAirports.Items.Count; i++)
            {
                if (departureAirports.Contains(cbCanadianAirports.Items[i].ToString()))
                    cbCanadianAirports.SetItemChecked(i, true);
            }

            for (int i = 0; i < cbGermanAirports.Items.Count; i++)
            {
                if (departureAirports.Contains(cbGermanAirports.Items[i].ToString()))
                    cbGermanAirports.SetItemChecked(i, true);
            }

            for (int i = 0; i < cbUSAirports.Items.Count; i++)
            {
                if (departureAirports.Contains(cbUSAirports.Items[i].ToString()))
                    cbUSAirports.SetItemChecked(i, true);
            }
        }

        private void FillDealInstructions()
        {
            if (deal.DealInstructions != null)
            {
                txtHowToBook.Text = ((deal.DealInstructions.howToBook != null) ? deal.DealInstructions.howToBook.Trim() : string.Empty);
                txtImportantUpsell.Text = ((deal.DealInstructions.importantUpsell != null) ? deal.DealInstructions.importantUpsell.Trim() : string.Empty);
                txtPleasenoteII.Text = ((deal.DealInstructions.pleaseNote != null) ? deal.DealInstructions.pleaseNote.Trim() : string.Empty);
            }
        }

        #endregion

        #region DataGridviewCostings

        //cell content
        private void dataGridviewOfferContracts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridviewOfferContracts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewOfferContracts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGridviewOfferContracts.CurrentCell = (dataGridviewOfferContracts.Rows.Count == 0) ? dataGridviewOfferContracts.Rows[0].Cells[0] : null;

                try
                {
                    switch (MessageBox.Show("This will delete selected hotel contract - continue?", "Delete Hotel Contract", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            string board = dataGridviewOfferContracts.Rows[e.RowIndex].Cells[3].Value.ToString().Trim().ToUpper();

                            List<string> boards = dataGridviewOfferContracts.Rows.Cast<DataGridViewRow>().Select(r => r.Cells[3].Value.ToString().Trim().ToUpper()).ToList();
                            List<string> selectedBoards = boards.Where(b => b == board).ToList();

                            if (selectedBoards.Count == 1)
                            {
                                for (int i = 0; i < cbBoards.Items.Count; i++)
                                    if (((ComboBoxItem)cbBoards.Items[i]).Value.ToString().ToUpper() == board)
                                        cbBoards.SetItemChecked(i, false);
                            }
                            dataGridviewOfferContracts.Rows.Remove((DataGridViewRow)dataGridviewOfferContracts.Rows[e.RowIndex]);
                            break;

                        case System.Windows.Forms.DialogResult.No:
                            return;
                    }
                }
                catch { }
            }
        }

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
            if (e.RowIndex >= 0 && dataGridViewExtras.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewExtras.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGridViewExtras.CurrentCell = (dataGridViewExtras.Rows.Count == 0) ? dataGridViewExtras.Rows[0].Cells[0] : null;

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
            if (e.RowIndex >= 0 && dataGridviewCarparking.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewCarparking.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGridviewCarparking.CurrentCell = (dataGridviewCarparking.Rows.Count == 0) ? dataGridviewCarparking.Rows[0].Cells[0] : null;

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
            if (e.RowIndex >= 0 && dataGridviewCarhire.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewCarhire.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGridviewCarhire.CurrentCell = (dataGridviewCarhire.Rows.Count == 0) ? dataGridviewCarhire.Rows[0].Cells[0] : null;

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
            if (e.RowIndex >= 0 && dataGVDurationCosting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVDurationCosting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGVDurationCosting.CurrentCell = (dataGVDurationCosting.Rows.Count == 0) ? dataGVDurationCosting.Rows[0].Cells[0] : null;
                try
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
                catch { }
            }
        }

        private void dataGVDurationCosting_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGVDurationCosting.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGVDurationCosting.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[4].Value = "";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[5].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[6].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[7].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[8].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[9].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[10].Value = "0";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[11].Value = "£";
                dataGVDurationCosting.Rows[e.RowIndex].Cells[12].Value = "0";
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
            if (e.RowIndex >= 0 && dataGVMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGVMarkups.CurrentCell = (dataGVMarkups.Rows.Count == 0) ? dataGVMarkups.Rows[0].Cells[0] : null;
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
            if (e.RowIndex >= 0 && dataGVWeekdayM.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVWeekdayM.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGVWeekdayM.CurrentCell = (dataGVWeekdayM.Rows.Count == 0) ? dataGVWeekdayM.Rows[0].Cells[0] : null;

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
            if (e.RowIndex >= 0 && dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVRoomTypeMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGVRoomTypeMarkups.CurrentCell = (dataGVRoomTypeMarkups.Rows.Count == 0) ? dataGVRoomTypeMarkups.Rows[0].Cells[0] : null;

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
            if (e.RowIndex >= 0 && dataGVSupplierMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVSupplierMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGVSupplierMarkups.CurrentCell = (dataGVSupplierMarkups.Rows.Count == 0) ? dataGVSupplierMarkups.Rows[0].Cells[0] : null;

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
            if (e.RowIndex >= 0 && dataGVDepartureApMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVDepartureApMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGVDepartureApMarkups.CurrentCell = (dataGVDepartureApMarkups.Rows.Count == 0) ? dataGVDepartureApMarkups.Rows[0].Cells[0] : null;

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
            if (e.RowIndex >= 0 && dataGVArrivalAirportMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVArrivalAirportMarkups.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGVArrivalAirportMarkups.CurrentCell = (dataGVArrivalAirportMarkups.Rows.Count == 0) ? dataGVArrivalAirportMarkups.Rows[0].Cells[0] : null;

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
            if (e.RowIndex >= 0 && dataGVDateRangeMarkup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVDateRangeMarkup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGVDateRangeMarkup.CurrentCell = (dataGVDateRangeMarkup.Rows.Count == 0) ? dataGVDateRangeMarkup.Rows[0].Cells[0] : null;

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
            if (e.RowIndex >= 0 && dataGVLowAvailabilityMarkup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGVLowAvailabilityMarkup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGVLowAvailabilityMarkup.CurrentCell = (dataGVLowAvailabilityMarkup.Rows.Count == 0) ? dataGVLowAvailabilityMarkup.Rows[0].Cells[0] : null;

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
            if (e.RowIndex >= 0 && dataGridViewHotelContracts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewHotelContracts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGridViewHotelContracts.CurrentCell = (dataGridViewHotelContracts.Rows.Count == 0) ? dataGridViewHotelContracts.Rows[0].Cells[0] : null;

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

        //suplement

        private void dataGridviewSupplemet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridviewSupplemet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewSupplemet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGridviewSupplemet.CurrentCell = (dataGridviewSupplemet.Rows.Count == 0) ? dataGridviewSupplemet.Rows[0].Cells[0] : null;

                switch (MessageBox.Show("This will delete selected supplement - continue?", "Supplement", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridviewSupplemet.Rows.Remove((DataGridViewRow)dataGridviewSupplemet.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }

        }

        private void dataGridviewSupplemet_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridviewSupplemet.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGridviewSupplemet.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGridviewSupplemet.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGridviewSupplemet.Rows[e.RowIndex].Cells[4].Value = "";
                dataGridviewSupplemet.Rows[e.RowIndex].Cells[5].Value = "0";
            }
        }

        private void dataGridviewSupplemet_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected supplement - continue?", "Delete Supplement", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGridviewSupplemet.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        //blackout

        private void dataGridviewBlackouts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridviewBlackouts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewBlackouts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGridviewBlackouts.CurrentCell = (dataGridviewBlackouts.Rows.Count == 0) ? dataGridviewBlackouts.Rows[0].Cells[0] : null;

                switch (MessageBox.Show("This will delete selected blackout - continue?", "Blackout", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridviewBlackouts.Rows.Remove((DataGridViewRow)dataGridviewBlackouts.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGridviewBlackouts_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected blackout - continue?", "Delete Blackout", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGridviewBlackouts.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        private void dataGridviewBlackouts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridviewBlackouts.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGridviewBlackouts.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGridviewBlackouts.Rows[e.RowIndex].Cells[1].Value = "0";
            }
        }

        //freenights

        private void dataGridviewFreenights_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridviewFreenights.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewFreenights.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGridviewFreenights.CurrentCell = (dataGridviewFreenights.Rows.Count == 0) ? dataGridviewFreenights.Rows[0].Cells[0] : null;

                switch (MessageBox.Show("This will delete selected freenight - continue?", "Freenight", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridviewFreenights.Rows.Remove((DataGridViewRow)dataGridviewFreenights.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGridviewFreenights_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected freenight - continue?", "Delete Freenight", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGridviewFreenights.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        private void dataGridviewFreenights_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridviewFreenights.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGridviewFreenights.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGridviewFreenights.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGridviewFreenights.Rows[e.RowIndex].Cells[4].Value = "0";
                dataGridviewFreenights.Rows[e.RowIndex].Cells[5].Value = "0";
            }
        }


        //discounts

        private void dataGridviewDiscounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridviewDiscounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridviewDiscounts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                dataGridviewDiscounts.CurrentCell = (dataGridviewDiscounts.Rows.Count == 0) ? dataGridviewDiscounts.Rows[0].Cells[0] : null;

                switch (MessageBox.Show("This will delete selected discounts - continue?", "Discounts", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridviewDiscounts.Rows.Remove((DataGridViewRow)dataGridviewDiscounts.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGridviewDiscounts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridviewDiscounts.Rows[e.RowIndex].Cells[1].Value == null)
            {
                dataGridviewDiscounts.Rows[e.RowIndex].Cells[0].Value = "Delete";
                dataGridviewDiscounts.Rows[e.RowIndex].Cells[1].Value = "0";
                dataGridviewDiscounts.Rows[e.RowIndex].Cells[4].Value = "0";
                dataGridviewDiscounts.Rows[e.RowIndex].Cells[5].Value = "%";
            }
        }

        private void dataGridviewDiscounts_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected discount - continue?", "Delete Discount", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGridviewDiscounts.Rows.Remove(e.Row);
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


        #endregion

        #region SaveMethods

        private void btnSaveDealInstructionInformation_Click(object sender, EventArgs e)
        {
            try
            {
                SaveDealInstruction();
                MessageBox.Show("Information has been saved successfully!", "Save!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving the details, please check and try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //tab 1
        public void SaveHotelContracts()
        {
            List<GulliverLibrary.HotelContract> hotelContracts = new List<GulliverLibrary.HotelContract>();

            if (dataGridviewOfferContracts.Rows.Count > 0)
            {
                foreach (GulliverIIDS.HotelContractsRow contract in GulliverIIDS.HotelContracts.Rows)
                {
                    using (GulliverLibrary.HotelContract hotelContract = new GulliverLibrary.HotelContract())
                    {
                        using (MySqlDataHandler.AcCGuiD tripperAccomGuid = packageHandler.GetAccomGuidByRecNo(contract.Recno))
                        {

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
                }
            }

            packageHandler.UpdateHotelContracts(hotelContracts, deal.id);
        }

        public void SaveManulContracts()
        {
            List<GulliverLibrary.ManualHotelContract> manualContracts = new List<GulliverLibrary.ManualHotelContract>();

            if (GulliverIIDS.ManualContract.Rows.Count > 0)
            {
                if (ddlCurrency.SelectedItem == null)
                {
                    MessageBox.Show("Please enter currecny for manual contracts!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtRoomType.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please enter room type for manual contracts!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (GulliverIIDS.ManualContractRow contract in GulliverIIDS.ManualContract.Rows)
                {
                    GulliverLibrary.ManualHotelContract manualContract = new GulliverLibrary.ManualHotelContract();
                    manualContract.id = contract.id;
                    manualContract.Deal = deal;
                    manualContract.allotment = contract.allotment;
                    manualContract.currency = ((ComboBoxItem)ddlCurrency.SelectedItem).Value.ToString();
                    manualContract.fromDate = contract.fromDate.Date;
                    manualContract.toDate = contract.toDate.Date;
                    manualContract.price = contract.price;
                    manualContract.roomType = txtRoomType.Text.Trim();
                    manualContracts.Add(manualContract);
                }

                packageHandler.UpdateManaulHotelContract(manualContracts, deal.id);
                SaveSupplements();
                SaveFreenights();
                SaveBlackouts();
                SaveDiscounts();
            }
        }

        private void SaveSupplements()
        {
            List<GulliverLibrary.Supplement> supplements = (from s in this.GulliverIIDS.Supplements
                                                            select new GulliverLibrary.Supplement
                                                              {
                                                                  id = s.id,
                                                                  fromDate = s.FromDate.Date,
                                                                  toDate = s.ToDate.Date,
                                                                  currency = s.Currency,
                                                                  price = s.PricePerPerson,
                                                                  Deal = deal
                                                              }).ToList();
            packageHandler.UpdateSupplements(supplements, deal.id);
        }

        public void SaveFreenights()
        {
            List<GulliverLibrary.FreeNight> freeNights = (from f in this.GulliverIIDS.Freenights
                                                          select new GulliverLibrary.FreeNight
                                                          {
                                                              id = f.id,
                                                              fromDate = f.FromDate.Date,
                                                              toDate = f.ToDate.Date,
                                                              actual = f.ActualDuration,
                                                              paid = f.PaidDuration,
                                                              Deal = deal
                                                          }).ToList();

            packageHandler.UpdateFreenights(freeNights, deal.id);
        }

        public void SaveDiscounts()
        {
            List<GulliverLibrary.Discount> discounts = (from d in this.GulliverIIDS.Discounts
                                                        select new GulliverLibrary.Discount
                                                          {
                                                              id = d.id,
                                                              fromDate = d.From.Date,
                                                              toDate = d.To.Date,
                                                              discount = d.Discount,
                                                              Deal = deal,
                                                              type = (d.Type.Trim() == "%" ? "1" : d.Type.Trim())
                                                          }).ToList();

            packageHandler.UpdateDiscounts(discounts, deal.id);
        }

        public void SaveBlackouts()
        {
            List<GulliverLibrary.Blackout> balckouts = (from b in this.GulliverIIDS.Blackouts
                                                        select new GulliverLibrary.Blackout
                                                          {
                                                              id = b.id,
                                                              fromDate = b.FromDate.Date,
                                                              toDate = b.ToDate.Date,
                                                              Deal = deal
                                                          }).ToList();

            packageHandler.UpdateBlackouts(balckouts, deal.id);
        }


        public void SaveChildAge()
        {
            using (GulliverLibrary.ChildrenAge childAge = new GulliverLibrary.ChildrenAge())
            {
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
        }

        //tab 2
        private bool SaveFlights()
        {
            try
            {
                SetStepProgressBar(progressBarTP2);
                deal.arrivalAirports = txtArrivalAirports.Text.Trim();
                deal.startDate = dtpStartDate.Value;
                deal.endDate = dtpEndDate.Value;
                deal.durations = string.Join("#", cbDurations.CheckedItems.Cast<string>().ToArray());

                List<string> departureAirports = new List<string>();
                departureAirports.AddRange(cbDepartureAirports.CheckedItems.Cast<ComboBoxItem>().Select(i => i.Value.ToString()));
                departureAirports.AddRange(cbUSAirports.CheckedItems.Cast<ComboBoxItem>().Select(i => i.Value.ToString()));
                departureAirports.AddRange(cbGermanAirports.CheckedItems.Cast<ComboBoxItem>().Select(i => i.Value.ToString()));
                departureAirports.AddRange(cbCanadianAirports.CheckedItems.Cast<ComboBoxItem>().Select(i => i.Value.ToString()));

                deal.departureAirports = string.Join("#", departureAirports.Distinct().ToArray());
                deal.isFAB = cbFlightTypes.GetItemChecked(0);
                deal.isFlightSheet = cbFlightTypes.GetItemChecked(1);
                deal.isAmadeus = cbFlightTypes.GetItemChecked(2);
                deal.isFABMM = cbFlightTypes.GetItemChecked(3);
                deal.isFABCache = cbFlightTypes.GetItemChecked(5);
                deal.filteredAirlines = string.Join("#", cbAirlines.CheckedItems.Cast<string>().ToArray());
                deal.baggageType = (rbnBGPP.Checked) ? "1" : ((rbnBGTwo.Checked) ? "2" : "0");
                deal.filteredWeekdays = string.Join("#", cbWeekDays.CheckedItems.Cast<string>().ToArray());
                deal.filteredFlightTimesOBDeparture = ftOBDepartureFrom.Text.Trim() + "#" + ftOBDepartureTo.Text.Trim();
                deal.filteredFlightTimesOBArrival = ftOBArrivalFrom.Text.Trim() + "#" + ftOBArrivalTo.Text.Trim();
                deal.filteredFlightTimesIBDeparture = ftIBDepartureFrom.Text.Trim() + "#" + ftIBDepartureTo.Text.Trim();
                deal.filteredFlightTimesIBArrival = ftIBArrivalFrom.Text.Trim() + "#" + ftIBArrivalTo.Text.Trim();
                deal.lastupdatedTime = DateTime.Now;
                deal.Media = (ddlMedias.SelectedItem != null) ? gulliverQueryHandler.GetMediaByCode(((ComboBoxItem)ddlMedias.SelectedItem).Value.ToString()) : null;
                deal.takeFollowingDateFromDeparture = cbTakeFollowingDayFromDeparture.Checked;

                if (deal.Media == null)
                {
                    MessageBox.Show("Please select valid media before you go to next step!");
                    return false;
                }

                deal.DealType = (ddlDealTypes.SelectedItem != null) ? gulliverQueryHandler.GetDealTypeById(Convert.ToInt32(((ComboBoxItem)ddlDealTypes.SelectedItem).Value.ToString())) : null;
                if (deal.DealType == null)
                {
                    MessageBox.Show("Please select valid deal type before you go to next step!");
                    return false;
                }

                dealId = packageHandler.UpdateDeal(deal);
                deal = packageHandler.GetDealById(dealId);
                SetStepProgressBar(progressBarTP2);
                //SaveHotelContracts();

                SetStepProgressBar(progressBarTP2);
                SaveChildAge();
                SetStepProgressBar(progressBarTP2);
                deal = packageHandler.GetDealById(dealId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving the details, please check and try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        }

        //tab 3
        private void SaveTripperExtars()
        {
            List<GulliverLibrary.TripperExtra> tripperExtras = new List<GulliverLibrary.TripperExtra>();

            foreach (GulliverIIDS.TripperExtraRow e in this.GulliverIIDS.TripperExtra)
            {
                if (selectedTripperExtras.Contains(e.recno))
                {
                    using (GulliverLibrary.TripperExtra tripperExtra = new GulliverLibrary.TripperExtra())
                    {
                        int recno = e.recno;
                        using (MySqlDataHandler.AcCGuiD accomGuid = queryHandler.GetAccomGuidByRecNo(recno))
                        {
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
                }
            }

            packageHandler.UpdateTripperExtras(tripperExtras, deal.id);
        }

        private void SaveCarHire()
        {
            List<GulliverLibrary.CarHire> carHires = new List<GulliverLibrary.CarHire>();

            foreach (GulliverIIDS.CarhireRow carhire in this.GulliverIIDS.Carhire)
            {
                using (GulliverLibrary.CarHire objCarHire = new GulliverLibrary.CarHire())
                {
                    objCarHire.amount = carhire.Amount;
                    objCarHire.currency = carhire.Currency;
                    objCarHire.Deal = deal;
                    objCarHire.endDate = carhire.EndDate;
                    objCarHire.startDate = carhire.StartDate;
                    objCarHire.id = carhire.id;
                    carHires.Add(objCarHire);
                }
            }

            packageHandler.UpdateCarHire(carHires, deal.id);
        }

        private void SaveCarParking()
        {
            List<GulliverLibrary.CarParking> carParkings = new List<GulliverLibrary.CarParking>();

            foreach (GulliverIIDS.CarParkingRow carParking in this.GulliverIIDS.CarParking)
            {
                using (GulliverLibrary.CarParking objCarparking = new GulliverLibrary.CarParking())
                {
                    objCarparking.amount = carParking.Amount;
                    objCarparking.currency = carParking.Currency;
                    objCarparking.Deal = deal;
                    objCarparking.endDate = carParking.EndDate;
                    objCarparking.startDate = carParking.StartDate;
                    objCarparking.id = carParking.id;
                    carParkings.Add(objCarparking);
                }
            }

            packageHandler.UpdateCarParking(carParkings, deal.id);
        }

        private void SaveExtra()
        {
            List<GulliverLibrary.Extra> extras = new List<GulliverLibrary.Extra>();

            foreach (GulliverIIDS.ExtraRow extraRow in this.GulliverIIDS.Extra)
            {
                using (GulliverLibrary.Extra objExtra = new GulliverLibrary.Extra())
                {
                    objExtra.adultPrice = extraRow.AdultPrice;
                    objExtra.childPrice = extraRow.ChildPrice;
                    objExtra.Deal = deal;
                    objExtra.description = extraRow.Description;
                    objExtra.isIncluded = extraRow.Include;
                    objExtra.id = extraRow.id;
                    extras.Add(objExtra);
                }
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
                using (GulliverLibrary.DurationCosting objDurCostings = new GulliverLibrary.DurationCosting())
                {
                    objDurCostings.Deal = deal;
                    objDurCostings.id = durationCosting.id;
                    objDurCostings.duration = Convert.ToInt32(durationCosting.Duration);
                    objDurCostings.roomType = durationCosting.RoomType.Trim();
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
            }

            packageHandler.UpdateDurationCostings(durationCostings, deal.id);
        }

        private void SaveSecretEscapeCostings()
        {
            using (GulliverLibrary.SecretEscapeMarkup secretEscapeMarkups = new GulliverLibrary.SecretEscapeMarkup())
            {
                secretEscapeMarkups.Deal = deal;
                secretEscapeMarkups.leadingMarkup = (txtMinimumMarkup.Text != string.Empty) ? Convert.ToDecimal(txtMinimumMarkup.Text) : 0;
                secretEscapeMarkups.standardMarkup = (txtStandardMarkup.Text != string.Empty) ? Convert.ToDecimal(txtStandardMarkup.Text) : 0;

                packageHandler.UpdateSecretEscapeMarkup(secretEscapeMarkups, deal.id);
            }
        }

        private void SaveDurationMarkup()
        {
            List<GulliverLibrary.DurationMarkup> durationMarkups = new List<GulliverLibrary.DurationMarkup>();

            foreach (CostingsDS.DurationMarkupRow durationMarkup in this.costingsDS.DurationMarkup)
            {
                using (GulliverLibrary.DurationMarkup objDurMarkup = new GulliverLibrary.DurationMarkup())
                {
                    objDurMarkup.Deal = deal;
                    objDurMarkup.duration = durationMarkup.Duration;
                    objDurMarkup.id = durationMarkup.id;
                    objDurMarkup.adultMarkup = durationMarkup.AdultMarkup;
                    objDurMarkup.childMarkup = durationMarkup.ChildMarkup;
                    durationMarkups.Add(objDurMarkup);
                }
            }
            packageHandler.UpdateDurationMarkup(durationMarkups, deal.id);
        }

        private void SaveWeekDayMarkup()
        {
            List<GulliverLibrary.WeekDayMarkup> weekDayMarkups = new List<GulliverLibrary.WeekDayMarkup>();

            foreach (CostingsDS.WeekdayMarkupRow weekDayMarkup in this.costingsDS.WeekdayMarkup)
            {
                using (GulliverLibrary.WeekDayMarkup objWeekdayMarkup = new GulliverLibrary.WeekDayMarkup())
                {
                    objWeekdayMarkup.Deal = deal;
                    objWeekdayMarkup.weekday = weekDayMarkup.Weekday;
                    objWeekdayMarkup.Deal = deal;
                    objWeekdayMarkup.id = weekDayMarkup.id;
                    objWeekdayMarkup.adultMarkup = weekDayMarkup.AdultMarkup;
                    objWeekdayMarkup.childMarkup = weekDayMarkup.ChildMarkup;
                    weekDayMarkups.Add(objWeekdayMarkup);
                }
            }

            packageHandler.UpdateWeekdayMarkup(weekDayMarkups, deal.id);
        }

        private void SaveRoomtypeMarkup()
        {
            List<GulliverLibrary.RoomTypeMarkup> roomTypeMarkups = new List<GulliverLibrary.RoomTypeMarkup>();

            foreach (CostingsDS.RoomTypeMarkupRow roomTypeMarkup in this.costingsDS.RoomTypeMarkup)
            {
                using (GulliverLibrary.RoomTypeMarkup objRoomtypeMarkup = new GulliverLibrary.RoomTypeMarkup())
                {
                    objRoomtypeMarkup.Deal = deal;
                    objRoomtypeMarkup.roomType = roomTypeMarkup.Name;
                    objRoomtypeMarkup.code = roomTypeMarkup.code;
                    objRoomtypeMarkup.id = roomTypeMarkup.id;
                    objRoomtypeMarkup.adultPrice = roomTypeMarkup.Adult;
                    objRoomtypeMarkup.childPrice = roomTypeMarkup.Child;
                    roomTypeMarkups.Add(objRoomtypeMarkup);
                }
            }

            packageHandler.UpdateRoomTypeMarkup(roomTypeMarkups, deal.id);
        }

        private void SaveSupplierMarkup()
        {
            List<GulliverLibrary.SupplierMarkup> supplierMarkups = new List<GulliverLibrary.SupplierMarkup>();

            foreach (CostingsDS.SupplierMarkupRow supplierMarkup in this.costingsDS.SupplierMarkup)
            {
                using (GulliverLibrary.SupplierMarkup objSupplierMarkup = new GulliverLibrary.SupplierMarkup())
                {
                    objSupplierMarkup.Deal = deal;
                    objSupplierMarkup.supplier = supplierMarkup.Supplier;
                    objSupplierMarkup.id = supplierMarkup.id;
                    objSupplierMarkup.adultMarkup = supplierMarkup.AdultMarkup;
                    supplierMarkups.Add(objSupplierMarkup);
                }
            }

            packageHandler.UpdateSupplierMarkup(supplierMarkups, deal.id);
        }

        private void SaveDepartureAirportMarkup()
        {
            List<GulliverLibrary.DepartureAirportMarkup> departureAirportMarkups = new List<GulliverLibrary.DepartureAirportMarkup>();

            foreach (CostingsDS.DepartureAirportMarkupRow departureAPMarkup in this.costingsDS.DepartureAirportMarkup)
            {
                using (GulliverLibrary.DepartureAirportMarkup objDepartureAPMarkup = new GulliverLibrary.DepartureAirportMarkup())
                {
                    objDepartureAPMarkup.Deal = deal;
                    objDepartureAPMarkup.airport = departureAPMarkup.DepartureAirport;
                    objDepartureAPMarkup.id = departureAPMarkup.id;
                    objDepartureAPMarkup.adultMarkup = departureAPMarkup.AdultMarkup;
                    objDepartureAPMarkup.childMarkup = departureAPMarkup.ChildMarkup;
                    departureAirportMarkups.Add(objDepartureAPMarkup);
                }
            }

            packageHandler.UpdateDepartureAirportMarkup(departureAirportMarkups, deal.id);
        }

        private void SaveDestinationAirportMarkup()
        {
            List<GulliverLibrary.DestinationAirportMarkup> destinationAirportMarkups = new List<GulliverLibrary.DestinationAirportMarkup>();

            foreach (CostingsDS.DestinationAirportMarkupRow destinationAPMarkup in this.costingsDS.DestinationAirportMarkup)
            {
                using (GulliverLibrary.DestinationAirportMarkup objDestinationAPMarkup = new GulliverLibrary.DestinationAirportMarkup())
                {
                    objDestinationAPMarkup.Deal = deal;
                    objDestinationAPMarkup.airport = destinationAPMarkup.Destination;
                    objDestinationAPMarkup.id = destinationAPMarkup.id;
                    objDestinationAPMarkup.adultMarkup = destinationAPMarkup.AdultMarkup;
                    objDestinationAPMarkup.childMarkup = destinationAPMarkup.ChildMarkup;
                    destinationAirportMarkups.Add(objDestinationAPMarkup);
                }
            }

            packageHandler.UpdateDestinationAirportMarkup(destinationAirportMarkups, deal.id);
        }

        private void SaveDateRangeMarkup()
        {
            List<GulliverLibrary.DateRangeMarkup> dateRangeMarkups = new List<GulliverLibrary.DateRangeMarkup>();

            foreach (CostingsDS.DateRangeMarkupRow destinationAPMarkup in this.costingsDS.DateRangeMarkup)
            {
                using (GulliverLibrary.DateRangeMarkup objDateRangeMarkup = new GulliverLibrary.DateRangeMarkup())
                {
                    objDateRangeMarkup.Deal = deal;
                    objDateRangeMarkup.startDate = destinationAPMarkup.StartDate;
                    objDateRangeMarkup.id = destinationAPMarkup.id;
                    objDateRangeMarkup.endDate = destinationAPMarkup.EndDate;
                    objDateRangeMarkup.adultMarkup = destinationAPMarkup.AdultMarkup;
                    objDateRangeMarkup.childMarkup = destinationAPMarkup.ChildMarkup;
                    dateRangeMarkups.Add(objDateRangeMarkup);
                }
            }

            packageHandler.UpdateDateRangeMarkup(dateRangeMarkups, deal.id);
        }

        private void SaveLowAvailabilityMarkup()
        {
            List<GulliverLibrary.LowAvailabilityMarkup> lowAvailabilityMarkups = new List<GulliverLibrary.LowAvailabilityMarkup>();

            foreach (CostingsDS.LowAvailabilityMarkupRow lowAvailabilityMarkup in this.costingsDS.LowAvailabilityMarkup)
            {
                using (GulliverLibrary.LowAvailabilityMarkup objLowAvailabilityMarkup = new GulliverLibrary.LowAvailabilityMarkup())
                {
                    objLowAvailabilityMarkup.Deal = deal;
                    objLowAvailabilityMarkup.startDate = lowAvailabilityMarkup.StartDate;
                    objLowAvailabilityMarkup.noOfRooms = lowAvailabilityMarkup.NoOfRooms;
                    objLowAvailabilityMarkup.id = lowAvailabilityMarkup.id;
                    objLowAvailabilityMarkup.endDate = lowAvailabilityMarkup.EndDate;
                    objLowAvailabilityMarkup.adultMarkup = lowAvailabilityMarkup.AdultMarkup;
                    objLowAvailabilityMarkup.childMarkup = lowAvailabilityMarkup.ChildMarkup;
                    lowAvailabilityMarkups.Add(objLowAvailabilityMarkup);
                }
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

        private bool SaveOfferDetails()
        {
            VisibleProgressBar(progressBarTP1, true);
            SetStepProgressBar(progressBarTP1);
            deal.name = txtDealName.Text.Trim();
            deal.starRating = (int)numericStars.Value;
            deal.dateOfPromotion = dtpSalesOn.Value;
            deal.endDateOfPromotion = dtpBookBy.Value;
            deal.Media = (ddlMedias.SelectedItem != null) ? gulliverQueryHandler.GetMediaByCode(((ComboBoxItem)ddlMedias.SelectedItem).Value.ToString()) : null;

            if (deal.Media == null)
            {
                MessageBox.Show("Please select valid media before you go to next step!");
                return false;
            }

            deal.DealType = (ddlDealTypes.SelectedItem != null) ? gulliverQueryHandler.GetDealTypeById(Convert.ToInt32(((ComboBoxItem)ddlDealTypes.SelectedItem).Value.ToString())) : null;
            SetStepProgressBar(progressBarTP1);
            deal.cruiseDeal = cbCruiseDeal.Checked;

            if (txtCommission.Text.Trim() != string.Empty)
            {
                if (!Validator.ValidPrice(txtCommission.Text))
                {
                    MessageBox.Show("Commission is not valid, please check correct!");
                    return false;
                }
            }

            deal.commission = (txtCommission.Text.Trim() != string.Empty) ? Convert.ToDecimal(txtCommission.Text) : 0;
            deal.dealCode = txtDealCode.Text.Trim();
            deal.productType = (cmbProducttypes.SelectedItem == null) ? string.Empty : cmbProducttypes.SelectedItem.ToString();
            deal.productId = txtProductCode.Text.Trim();
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

            return true;
        }

        private void SearchedHolidays(GulliverLibrary.Deal deal)
        {
            FlightsHandler.SearchRequest searchRequest = new FlightsHandler.SearchRequest();
            searchRequest.IncludeFAB = deal.isFAB;
            searchRequest.IncludeFlightSheet = deal.isFlightSheet;
            searchRequest.IncludeFABCache = deal.isFABCache;
            searchRequest.ImportFlights = false;
            searchRequest.IncludeOneWayMixMatch = deal.isFABMM;
            searchRequest.IncludeAmadeus = deal.isAmadeus;
            searchRequest.DepartureAirports = deal.departureAirports.Split('#').Distinct().ToList();
            searchRequest.Durations = (deal.durations != string.Empty) ? deal.durations.Split('#').Select(i => Convert.ToInt32(i)).ToList() : new List<int>();
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

            using (flcsPackages packageForm = new flcsPackages(packageHandler, packages, dealId, false, new List<GulliverLibrary.Package>()))
            {
                packageForm.ShowDialog();

                deal = packageHandler.GetDealById(dealId);
                VisibleProgressBar(progressBarTP4, false);

                if (packageForm.saved)
                {
                    using (GulliverLibrary.QueryHandler objQueryHandler = new GulliverLibrary.QueryHandler(@"Server=APP-1\SQLSERVER;Database=Gulliver;User ID=FleetwayServices;Password=flw388;"))
                    {
                        List<GulliverLibrary.Package> packagesList = objQueryHandler.GetPackagesByDeal(dealId);
                        deal = packageHandler.GetDealById(dealId);
                        packages = packagesList;
                        FillPackages(packages);
                        tabMain.SelectedTab = tabPage5;
                    }
                }
            }
        }

        private void CompareHolidays(GulliverLibrary.Deal deal)
        {
            using (flcsFilterSearch objFilterSearch = new flcsFilterSearch(deal.durations.Split('#').ToList(), packageHandler.GetAllUKAirports().Select(a => a.airportCode.Trim()).ToList(), packageHandler.GetAllGermanAirports().Select(a => a.airportCode.Trim()).ToList(), packageHandler.GetAllUSAAirports().Select(a => a.airportCode.Trim()).ToList(), deal.departureAirports.Split('#').ToList(), packageHandler.GetAllCanadianAirports().Select(a => a.airportCode.Trim()).ToList(), deal.startDate, deal.endDate))
            {
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
                searchRequest.IncludeFABCache = deal.isFABCache;
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

                packages = packageHandler.CompareHolidays(searchRequest, deal, false);
                VisibleProgressBar(progressBarTP4, false);
            }

            using (flcsPackages packageForm = new flcsPackages(packageHandler, packages, dealId, true, new List<GulliverLibrary.Package>()))
            {
                packageForm.ShowDialog();

                if (packageForm.saved)
                {
                    using (GulliverLibrary.QueryHandler objQueryHandler = new GulliverLibrary.QueryHandler(@"Server=APP-1\SQLSERVER;Database=Gulliver;User ID=FleetwayServices;Password=flw388;"))
                    {
                        List<GulliverLibrary.Package> packagesList = objQueryHandler.GetPackagesByDeal(dealId);
                        deal = packageHandler.GetDealById(dealId);
                        packages = packagesList;
                        FillPackages(packages);
                    }
                    tabMain.SelectedTab = tabPage5;
                }
            }
        }

        #endregion

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        ~flcsMain()
        {
            Dispose();
        }

        private void btnRemoveOnlyStoppedSales_Click(object sender, EventArgs e)
        {
            packages = packageHandler.RemoveOnlyStoppedSales(deal);

            using (flcsPackages packageForm = new flcsPackages(packageHandler, packages, dealId, false, new List<GulliverLibrary.Package>()))
            {
                packageForm.ShowDialog();

                deal = packageHandler.GetDealById(dealId);
                VisibleProgressBar(progressBarTP4, false);

                if (packageForm.saved)
                {
                    using (GulliverLibrary.QueryHandler objQueryHandler = new GulliverLibrary.QueryHandler(@"Server=APP-1\SQLSERVER;Database=Gulliver;User ID=FleetwayServices;Password=flw388;"))
                    {
                        List<GulliverLibrary.Package> packagesList = objQueryHandler.GetPackagesByDeal(dealId);
                        deal = packageHandler.GetDealById(dealId);
                        packages = packagesList;
                        FillPackages(packages);
                        tabMain.SelectedTab = tabPage5;
                    }
                }
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

