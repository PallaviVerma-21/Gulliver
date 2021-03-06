using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using DataGridViewAutoFilter;
using System.Linq;
using PackageGenerator;

namespace GulliverII
{
    public partial class flcsPackages : ComponentFactory.Krypton.Toolkit.KryptonForm, IDisposable
    {
        private int dealId;
        private PackageGenerator.PackageHandler packageHandler;
        private PackageGenerator.ICosting icosting;
        private bool compared;
        private List<string> visibleColumns;
        private int supplierId;
        List<int> travelZooSuppliers;
        List<int> timesSuppliers;
        List<int> seSupplier;
        List<int> icelollysuppliers;
        private bool isDataGridViewFormatted = false;
        private List<GulliverLibrary.Package> packagesToBackUp;
        GulliverLibrary.Deal deal;
        public bool saved = false;
        public List<GulliverLibrary.Package> packages;
        private bool done10PercentCalculation = false;

        public flcsPackages(PackageGenerator.PackageHandler packageHandler, List<GulliverLibrary.Package> packages, int dealId, bool compared, List<GulliverLibrary.Package> packagesToBackUp)
        {
            InitializeComponent();
            travelZooSuppliers = PackageGenerator.Tool.GetSuppliersBySuppliertype("traveltypesuppliers");
            timesSuppliers = PackageGenerator.Tool.GetSuppliersBySuppliertype("timestypesuppliers");
            seSupplier = PackageGenerator.Tool.GetSuppliersBySuppliertype("setypesuppliers");
            icelollysuppliers = PackageGenerator.Tool.GetSuppliersBySuppliertype("icelollysuppliers");
            this.packageHandler = packageHandler;
            deal = packageHandler.GetDealById(dealId);
            icosting = PackageGenerator.FactoryCosting.GetCostingOBj(deal, deal.DurationCostings.ToList(), deal.SecretEscapeMarkup, packageHandler.GetFreezedAdultSellAtsHash(deal.id), packageHandler.GetFreezedChildSellAtsHash(deal.id));
            this.dealId = dealId;
            this.packagesToBackUp = packagesToBackUp;
            this.compared = compared;
            this.supplierId = deal.Media.id;

            DisplayButtonByMedia();
            DisplayHolidays(packages);
            DisplayDefaultColumns();
        }

        #region events

        private void btnDelete_Click(object sender, EventArgs e)
        {
            progressBar.Visible = true;
            progressBar.Maximum = 3;
            progressBar.Value = 1;
            Application.DoEvents();

            switch (MessageBox.Show("This will delete selected holidays - continue?", "Delete Holiday", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    progressBar.Value++;
                    foreach (DataGridViewRow row in dataGridViewHolidays.SelectedRows)
                        dataGridViewHolidays.Rows.Remove(row);
                    lblTotal.Text = (this.packagesDS.Package.Count > 0) ? "Total: " + this.packagesDS.Package.Count + " holidays" : "Total: " + this.packagesDS.Package.Count + " holiday";
                    progressBar.Value++;
                    break;
                case System.Windows.Forms.DialogResult.No:
                    progressBar.Value = 3;
                    return;
            }

            progressBar.Visible = false;
            Application.DoEvents();
        }

        private void dataGridViewHolidays_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (isDataGridViewFormatted)
                return;

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

            if (compared)
            {
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).ToList().ForEach(cell => cell.Style.BackColor = Color.White);
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).Where(cell => cell.Value != null && cell.Value.ToString().Contains("UP")).ToList().ForEach(cell => cell.Style.BackColor = Color.DarkRed);
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).Where(cell => cell.Value != null && cell.Value.ToString().Contains("DOWN")).ToList().ForEach(cell => cell.Style.BackColor = Color.DarkGreen);
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).Where(cell => cell.Value != null && (cell.Value.ToString().Contains("UP") || cell.Value.ToString().Contains("DOWN"))).ToList().ForEach(cell => cell.Style.ForeColor = Color.White);
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).Where(cell => cell.Value != null && cell.Value.ToString().Contains("STOPPED")).ToList().ForEach(cell => cell.Style.BackColor = Color.Yellow);
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).Where(cell => cell.Value != null && cell.Value.ToString().Contains("NEW")).ToList().ForEach(cell => cell.Style.BackColor = Color.Blue);
            }

            if (Tool.GetSuppliersBySuppliertype("setypesuppliers").Contains(supplierId))
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Where(row => row.Cells[flightId.Name].Value != null && row.Cells[flightId.Name].Value.ToString() == "1").Select(row => row.Cells[sellAt.Name]).ToList().ForEach(cell => cell.Style.BackColor = Color.Orange);
        }

        private void showAllLabelH_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dataGridViewHolidays);
        }

        private void btnCalculateTenPercentLeading_Click(object sender, EventArgs e)
        {
            if (done10PercentCalculation)
            {
                MessageBox.Show("Please clear previous calculation before recalculate!", "10% Leading Calculation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lblMsg.Text = "Processing ...";
            lblMsg.Visible = true;
            progressBar.Visible = true;
            progressBar.Maximum = 9;
            Application.DoEvents();
            progressBar.Value = 1;

            progressBar.Value++;
            List<GulliverLibrary.Package> packages = ReadPackagesByGridview();
            progressBar.Value++;
            List<GulliverLibrary.Package> stoppedHolidays = packages.Where(h => h.status == "Not available anymore").ToList();
            progressBar.Value++;
            packages = packages.Where(h => h.status != "Not available anymore").ToList();
            progressBar.Value++;

            if (!cbIncludeNew.Checked)
                packages = packages.Where(h => h.status != "New").ToList();

            progressBar.Value++;
            this.packages = ReadPackagesByGridview();
            List<GulliverLibrary.Package> processedPackages = icosting.CalcuateCostings(packages);
            progressBar.Value++;
            processedPackages.AddRange(stoppedHolidays);
            progressBar.Value++;
            isDataGridViewFormatted = true;
            DisplayHolidays(processedPackages);
            progressBar.Value++;
            progressBar.Visible = false;
            lblMsg.Text = string.Empty;
            Application.DoEvents();
            done10PercentCalculation = true;
            btnClear10PercentCalculation.Visible = true;
        }

        private void manipulateDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (flcsFilterColumns objFilterColumn = new flcsFilterColumns(this.packagesDS.PackageBackup.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList(), visibleColumns))
            {
                objFilterColumn.ShowDialog();
                visibleColumns = objFilterColumn.visibleColumns;
            }

            VisibleColumns();
        }

        private void VisibleDefaultColumns()
        {
            visibleColumns = dataGridViewHolidays.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => c.DataPropertyName.Trim()).ToList();
            string invisibleColumns = packageHandler.GetMiscSettingByKey("invisibleColumns").value;

            visibleColumns = visibleColumns.Where(v => !invisibleColumns.Split('#').Contains(v)).ToList();
            VisibleColumns();
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

        private void setLeadingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (flcsSetLeading setLeadings = new flcsSetLeading(dealId, packageHandler)) setLeadings.ShowDialog();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "Saving ...";
            lblMsg.Visible = true;
            progressBar.Visible = true;
            progressBar.Maximum = 13;
            Application.DoEvents();

            progressBar.Value = 1;
            List<GulliverLibrary.Package> packages = new List<GulliverLibrary.Package>();
            progressBar.Value++;
            decimal commission = (100 - deal.commission / 100);

            packages = ReadPackagesByGridview();

            if (compared)
            {
                progressBar.Value++;

                if (!cbIncludeNew.Checked)
                    packages = packages.Where(h => h.status != "New").ToList();
            }

            progressBar.Value++;

            if (compared && ((deal.Media.id == 1) || seSupplier.Contains(deal.Media.id) || PackageGenerator.Tool.GetSuppliersBySuppliertype("voyagesuppliers").Contains(deal.Media.id)))
            {
                if (packages != null && packages.Count > 0)
                {
                    icosting.WriteToCSVWithStatus(packages, PackageGenerator.Tool.GetSEFilePath() + "\\" + deal.name.Replace("(", string.Empty).Replace(")", string.Empty).Replace("/", string.Empty).Replace("\\", string.Empty) + ".csv");
                    PackageGenerator.Tool.EmailToAdvertising(packageHandler.GetEmailRecievers().Select(r => r.reciever).ToList(), PackageGenerator.Tool.GetSEFilePath() + "\\" + deal.name.Replace("(", string.Empty).Replace(")", string.Empty).Replace("/", string.Empty).Replace("\\", string.Empty) + ".csv");
                }
            }

            progressBar.Value++;
            packages = packages.Where(h => h.status != "Not available anymore").ToList();
            progressBar.Value++;

            string tsAPI = packageHandler.UpdatePackages(packages, deal.id, cbImportTS.Checked);

            progressBar.Value++;

            progressBar.Value++;
            deal.finalizePrices = cbFinalizePrices.Checked;
            progressBar.Value++;

            BackupHolidays(packages, deal);
            packages.ForEach(h => h.status = string.Empty);
            progressBar.Value++;
            isDataGridViewFormatted = true;
            DisplayHolidays(packages);
            this.packages = packages;
            progressBar.Value++;
            progressBar.Visible = false;
            packageHandler.UpdateLastUpdatedTime(deal);
            lblMsg.Text = "Saved successfully in Gulliver database!";

            if (cbNotifyMarketing.Checked)
            {
                Email email = new Email();
                email.NotifyMarketing(deal);
                KryptonMessageBox.Show("Notification has been sent to Marketing", "Prices updates notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            saved = true;
            Application.DoEvents();
            this.Close();
        }

        private void dataGridViewHolidays_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 29)
            {
                int selectedCellCount = dataGridViewHolidays.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    dataGridViewHolidays.ContextMenuStrip = cms;
                    cms.Visible = true;
                }
                else
                {
                    dataGridViewHolidays.ContextMenuStrip = null;
                    txtValue.Text = string.Empty;
                    cms.Visible = false;
                }
            }
            else if (e.ColumnIndex == 26 && !travelZooSuppliers.Contains(deal.Media.id))
            {
                int selectedCellCount = dataGridViewHolidays.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    dataGridViewHolidays.ContextMenuStrip = cmsCommission;
                    cmsCommission.Visible = true;
                }
                else
                {
                    dataGridViewHolidays.ContextMenuStrip = null;
                    txtCommission.Text = string.Empty;
                    cmsCommission.Visible = false;
                }
            }
            else if (e.ColumnIndex == 28)
            {
                int selectedCellCount = dataGridViewHolidays.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    dataGridViewHolidays.ContextMenuStrip = cmsMarkup;
                    cmsMarkup.Visible = true;
                }
                else
                {
                    dataGridViewHolidays.ContextMenuStrip = null;
                    txtMarkup.Text = string.Empty;
                    cmsMarkup.Visible = false;
                }
            }
            else if (e.ColumnIndex == 22)
            {
                int selectedCellCount = dataGridViewHolidays.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    dataGridViewHolidays.ContextMenuStrip = cmsBaggages;
                    cmsBaggages.Visible = true;
                }
                else
                {
                    dataGridViewHolidays.ContextMenuStrip = null;
                    txtBaggagePrice.Text = string.Empty;
                    cmsBaggages.Visible = false;
                }
            }
            else if (e.ColumnIndex == 37)
            {
                int selectedCellCount = dataGridViewHolidays.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    dataGridViewHolidays.ContextMenuStrip = cmsChildSellat;
                    cmsChildSellat.Visible = true;
                }
                else
                {
                    dataGridViewHolidays.ContextMenuStrip = null;
                    txtChildSellat.Text = string.Empty;
                    cmsChildSellat.Visible = false;
                }
            }
            else
            {
                dataGridViewHolidays.ContextMenuStrip = null;
                txtValue.Text = string.Empty;
                cms.Visible = false;
            }
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool found = true;
            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Value++;

            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                List<int> ids = new List<int>();

                try
                {
                    for (int i = 0; i < dataGridViewHolidays.Rows.Count; i++)
                    {
                        if (dataGridViewHolidays.Rows[i].Cells[29].Selected && found)
                            ids.Add(Convert.ToInt32(dataGridViewHolidays.Rows[i].Cells[3].Value));
                    }
                }
                catch { }

                if (ids.Count > 0)
                {
                    progressBar.Maximum = ids.Count + 10;

                    foreach (PackagesDS.PackageRow h in this.packagesDS.Package.Where(h => ids.Any(i => i == h.hiddenNumber)).ToList())
                    {
                        if (!cbDisableProfitField.Checked)
                        {
                            GulliverLibrary.Package package = new GulliverLibrary.Package();
                            package.commission = h.commission;
                            package.nett = h.nett;
                            package.sellAt = h.sellAt;
                            package.profit = h.profit;


                            GulliverLibrary.Package packageI = icosting.CalculateFinalCostings(package, deal.commission, "sellat", Convert.ToDecimal(txtValue.Text.Trim()));
                            h.sellAt = packageI.sellAt;
                            h.nett = packageI.nett;
                            h.commission = packageI.commission;
                            h.profit = packageI.profit;
                        }
                        else
                        {
                            h.sellAt = Convert.ToDecimal(txtValue.Text.Trim());
                            h.flightId = 1;

                        }
                        progressBar.Value++;

                    }
                }

                dataGridViewHolidays.ContextMenuStrip = null;
                txtValue.Text = string.Empty;
                cms.Visible = false;
                progressBar.Visible = false;
            }
        }

        private void txtCommission_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool found = true;

            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                List<int> ids = new List<int>();

                try
                {
                    for (int i = 0; i < dataGridViewHolidays.Rows.Count; i++)
                    {
                        if (dataGridViewHolidays.Rows[i].Cells[26].Selected && found)
                            ids.Add(Convert.ToInt32(dataGridViewHolidays.Rows[i].Cells[3].Value));
                    }
                }
                catch { }

                if (ids.Count > 0)
                {
                    foreach (PackagesDS.PackageRow h in this.packagesDS.Package.Where(h => ids.Any(i => i == h.hiddenNumber)).ToList())
                    {
                        // using (

                        GulliverLibrary.Package package = new GulliverLibrary.Package();
                        //{
                        package.commission = h.commission;
                        package.nett = h.nett;
                        package.sellAt = h.sellAt;
                        package.profit = h.profit;

                        //using (

                        GulliverLibrary.Package packageI = icosting.CalculateFinalCostings(package, deal.commission, "commission", Convert.ToDecimal(txtCommission.Text.Trim().Replace("%", string.Empty)));

                        //{
                        h.sellAt = packageI.sellAt;
                        h.nett = packageI.nett;
                        h.commission = packageI.commission;
                        h.profit = packageI.profit;
                        //}
                        //}
                    }
                }

                dataGridViewHolidays.ContextMenuStrip = null;
                txtValue.Text = string.Empty;
                cms.Visible = false;
            }
        }

        private void txtMarkup_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool found = true;

            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                List<int> ids = new List<int>();

                try
                {
                    for (int i = 0; i < dataGridViewHolidays.Rows.Count; i++)
                    {
                        if (dataGridViewHolidays.Rows[i].Cells[28].Selected && found)
                            ids.Add(Convert.ToInt32(dataGridViewHolidays.Rows[i].Cells[3].Value));
                    }
                }
                catch { }

                if (ids.Count > 0)
                {
                    foreach (PackagesDS.PackageRow h in this.packagesDS.Package.Where(h => ids.Any(i => i == h.hiddenNumber)).ToList())
                    {
                        //using (

                        GulliverLibrary.Package package = new GulliverLibrary.Package();
                        //)
                        //{
                        package.commission = h.commission;
                        package.nett = h.nett;
                        package.totalMarkup = h.totalMarkup;
                        package.sellAt = h.sellAt;
                        package.profit = h.profit;

                        //using (

                        GulliverLibrary.Package packageI = icosting.CalculateFinalCostings(package, deal.commission, "markup", Convert.ToDecimal(txtMarkup.Text.Trim()));

                        //{
                        h.sellAt = packageI.sellAt;
                        h.nett = packageI.nett;
                        h.totalMarkup = packageI.totalMarkup;
                        h.commission = packageI.commission;
                        h.profit = packageI.profit;
                        //}
                        // }
                    }
                }

                dataGridViewHolidays.ContextMenuStrip = null;
                txtValue.Text = string.Empty;
                cms.Visible = false;
            }
        }

        private void btnClear10PercentCalculation_Click(object sender, EventArgs e)
        {
            progressBar.Visible = true;
            progressBar.Maximum = 3;
            Application.DoEvents();
            progressBar.Value = 1;
            progressBar.Value++;
            isDataGridViewFormatted = true;
            DisplayHolidays(packages);
            progressBar.Value++;
            progressBar.Visible = false;
            Application.DoEvents();
            done10PercentCalculation = false;
            btnClear10PercentCalculation.Visible = false;
        }

        private void txtBaggagePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool found = true;

            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                List<int> ids = new List<int>();

                try
                {
                    for (int i = 0; i < dataGridViewHolidays.Rows.Count; i++)
                    {
                        if (dataGridViewHolidays.Rows[i].Cells[22].Selected && found)
                            ids.Add(Convert.ToInt32(dataGridViewHolidays.Rows[i].Cells[3].Value));
                    }
                }
                catch { }

                if (ids.Count > 0)
                {
                    foreach (PackagesDS.PackageRow h in this.packagesDS.Package.Where(h => ids.Any(i => i == h.hiddenNumber)).ToList())
                    {
                        GulliverLibrary.Package package = new GulliverLibrary.Package();
                        package.commission = h.commission;
                        package.nett = h.nett;
                        package.totalMarkup = h.totalMarkup;
                        package.sellAt = h.sellAt;
                        package.profit = h.profit;
                        package.baggagePrice = h.baggagePrice;
                        package.extras = h.extras;

                        GulliverLibrary.Package packageI = icosting.CalculateFinalCostings(package, deal.commission, "baggagePrice", Convert.ToDecimal(txtBaggagePrice.Text.Trim()));
                        h.sellAt = packageI.sellAt;
                        h.nett = packageI.nett;
                        h.totalMarkup = packageI.totalMarkup;
                        h.commission = packageI.commission;
                        h.profit = packageI.profit;
                        h.baggagePrice = packageI.baggagePrice;
                        h.extras = packageI.extras;
                    }
                }

                dataGridViewHolidays.ContextMenuStrip = null;
                txtBaggagePrice.Text = string.Empty;
                cmsBaggages.Visible = false;
            }
        }

        #endregion

        #region methods

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

        private List<GulliverLibrary.Package> ReadPackagesByGridview()
        {
            int count = 1;
            bool travelzooSupplier = Tool.GetTravelzooFilePath().Split(',').Contains(deal.Media.id.ToString());

            List<GulliverLibrary.Package> packages = new List<GulliverLibrary.Package>();
            packages = (from h in this.packagesDS.Package
                        // where h.id != null
                        select new GulliverLibrary.Package
                        {
                            id = count++,
                            date = h.date,
                            departureAirport = h.departureAirport,
                            destinationAirport = h.destinationAirport,
                            duration = h.duration,
                            obDepartureTime = h.obDepartureTime,
                            obArrivalTime = h.obArrivalTime,
                            ibDepartureTime = h.ibDepartureTime,
                            ibArrivalTime = h.ibArrivalTime,
                            board = h.board,
                            flightPrice = h.flightPrice,
                            airline = h.airline,
                            obAirline = h.obAirline,
                            ibAirline = h.ibAirline,
                            obFlightNo = h.obFlightNo,
                            ibFlightNo = h.ibFlightNo,
                            hotelName = h.hotelname,
                            roomType = h.roomType,
                            occupancy = h.occupancy,
                            adults = h.adults,
                            children = h.children,
                            infants = h.infants,
                            hotelPrice = h.hotelPrice,
                            caa = h.caa,
                            baggagePrice = h.baggagePrice,
                            transfers = h.transfers,
                            extras = h.extras,
                            totalMarkup = (h.totalMarkup == null) ? 0 : h.totalMarkup,
                            baseMarkup = h.baseMarkup,
                            commission = h.commission,
                            nett = h.nett,
                            sellAt = h.sellAt,
                            flightSource = h.searchType,
                            hotelSource = h.hotelSource,
                            Deal = deal,
                            status = h.status,
                            carhireCosting = h.carhireCosting,
                            carParkingCosting = h.carParkingCosting,
                            profit = (h.flightId == 1 && travelzooSupplier) ? Math.Round(h.sellAt - h.nett, 2) : h.profit,
                            hotelKey = h.hotelKey,
                            leading = (h.flightId == 1) ? true : false,
                            oldSellAt = h.oldSellAt,
                            childNett = h.childNett,
                            childSellat = h.childSellat,
                            childProfit = h.childProfit,
                            childHotelPrice = h.childHotelPrice,
                            childExtras = h.childExtras,
                            totalChildMarkup = h.totalChildMarkup,
                            isStandardRoom = h.isStandardRoom,
                            Finalized = h.IsChecked,
                            ChildFinalized = h.IsCFinalized,
                            tsRoomKey = h.tsRoomKey

                        }).ToList();

            return packages;
        }

        private void BackupHolidays(List<GulliverLibrary.Package> packages, GulliverLibrary.Deal deal)
        {
            List<GulliverLibrary.PackageBackup> exsistingPackups = packageHandler.GetPackageBackupsByDeal(deal.id);
            int count = 1;
            List<GulliverLibrary.PackageBackup> packageBackup = (from h in packages
                                                                 select new GulliverLibrary.PackageBackup
                                                                 {
                                                                     id = count++,
                                                                     date = h.date,
                                                                     hotelKey = h.hotelKey,
                                                                     tsRoomKey = (h.tsRoomKey != null) ? h.tsRoomKey : string.Empty,
                                                                     departureAirport = h.departureAirport,
                                                                     destinationAirport = h.destinationAirport,
                                                                     duration = h.duration,
                                                                     obDepartureTime = h.obDepartureTime,
                                                                     obArrivalTime = h.obArrivalTime,
                                                                     ibDepartureTime = h.ibDepartureTime,
                                                                     ibArrivalTime = h.ibArrivalTime,
                                                                     board = h.board,
                                                                     flightPrice = h.flightPrice,
                                                                     childFlightPrice = h.childFlightPrice,
                                                                     airline = h.airline,
                                                                     obAirline = h.obAirline,
                                                                     ibAirline = h.ibAirline,
                                                                     obFlightNo = h.obFlightNo,
                                                                     ibFlightNo = h.ibFlightNo,
                                                                     hotelName = h.hotelName,
                                                                     roomType = h.roomType,
                                                                     occupancy = h.occupancy,
                                                                     adults = h.adults,
                                                                     children = h.children,
                                                                     infants = h.infants,
                                                                     hotelPrice = h.hotelPrice,
                                                                     caa = h.caa,
                                                                     baggagePrice = h.baggagePrice,
                                                                     transfers = h.transfers,
                                                                     extras = h.extras,
                                                                     childExtras = h.childExtras,
                                                                     baseMarkup = h.baseMarkup,
                                                                     totalMarkup = h.totalMarkup,
                                                                     totalChildMarkup = h.totalChildMarkup,
                                                                     commission = h.commission,
                                                                     nett = h.nett,
                                                                     sellAt = h.sellAt,
                                                                     carParkingCosting = h.carParkingCosting,
                                                                     childHotelPrice = h.childHotelPrice,
                                                                     childNett = h.childNett,
                                                                     childSellat = h.childSellat,
                                                                     childProfit = h.childProfit,
                                                                     isStandardRoom = h.isStandardRoom,
                                                                     oldSellAt = h.oldSellAt,
                                                                     flightSource = h.flightSource,
                                                                     hotelSource = h.hotelSource,
                                                                     Deal = deal,
                                                                     status = h.status,
                                                                     carhireCosting = h.carhireCosting,
                                                                     profit = h.profit,
                                                                     leading = h.leading,
                                                                     deletedDate = DateTime.Now
                                                                 }).ToList();

            // first backup so it will be the same holiday as live
            if ((exsistingPackups == null || exsistingPackups.Count == 0))
                packageHandler.BackupHolidays(packageBackup, deal.id);
            else
            {
                // other backups, only backed up when there was holidays. 
                if (packageBackup != null && packageBackup.Count > 0)
                {
                    packageBackup = (from h in packageBackup
                                     select new GulliverLibrary.PackageBackup
                                     {
                                         id = h.id,
                                         date = h.date,
                                         hotelKey = h.hotelKey,
                                         departureAirport = h.departureAirport,
                                         destinationAirport = h.destinationAirport,
                                         duration = h.duration,
                                         obDepartureTime = h.obDepartureTime,
                                         obArrivalTime = h.obArrivalTime,
                                         ibDepartureTime = h.ibDepartureTime,
                                         ibArrivalTime = h.ibArrivalTime,
                                         board = h.board,
                                         flightPrice = h.flightPrice,
                                         childFlightPrice = h.childFlightPrice,
                                         airline = h.airline,
                                         obAirline = h.obAirline,
                                         ibAirline = h.ibAirline,
                                         obFlightNo = h.obFlightNo,
                                         ibFlightNo = h.ibFlightNo,
                                         hotelName = h.hotelName,
                                         roomType = h.roomType,
                                         occupancy = h.occupancy,
                                         adults = h.adults,
                                         children = h.children,
                                         infants = h.infants,
                                         hotelPrice = h.hotelPrice,
                                         caa = h.caa,
                                         baggagePrice = h.baggagePrice,
                                         transfers = h.transfers,
                                         extras = h.extras,
                                         childExtras = h.childExtras,
                                         baseMarkup = h.baseMarkup,
                                         totalMarkup = h.totalMarkup,
                                         totalChildMarkup = h.totalChildMarkup,
                                         commission = h.commission,
                                         nett = h.nett,
                                         sellAt = h.sellAt,
                                         carParkingCosting = h.carParkingCosting,
                                         childHotelPrice = h.childHotelPrice,
                                         childNett = h.childNett,
                                         childSellat = h.childSellat,
                                         isStandardRoom = h.isStandardRoom,
                                         oldSellAt = h.oldSellAt,
                                         flightSource = h.flightSource,
                                         Deal = deal,
                                         status = h.status,
                                         carhireCosting = h.carhireCosting,
                                         profit = h.profit,
                                         leading = h.leading,
                                         deletedDate = DateTime.Now
                                     }).ToList();

                    packageHandler.BackupHolidays(packageBackup, deal.id);
                }
            }
        }

        public void DisplayButtonByMedia()
        {
            if (seSupplier.Contains(supplierId))
            {
                btnCalculateTenPercentLeading.Visible = true;
                btnClear10PercentCalculation.Visible = false;
                cbDisableProfitField.Visible = false;
            }
            else if (travelZooSuppliers.Contains(supplierId) || icelollysuppliers.Contains(supplierId))
            {
                btnCalculateTenPercentLeading.Visible = false;
                btnClear10PercentCalculation.Visible = false;
                cbDisableProfitField.Visible = true;
            }
            else if ((timesSuppliers.Contains(supplierId)))
            {
                if (supplierId == 1)
                {
                    btnCalculateTenPercentLeading.Visible = true;
                    btnClear10PercentCalculation.Visible = false;
                    cbDisableProfitField.Visible = false;
                }
                else
                {
                    btnCalculateTenPercentLeading.Visible = false;
                    btnClear10PercentCalculation.Visible = false;
                    cbDisableProfitField.Visible = false;
                }
            }
            else
            {
                btnCalculateTenPercentLeading.Visible = false;
                btnClear10PercentCalculation.Visible = false;
                cbDisableProfitField.Visible = false;
            }
        }

        private void DisplayDefaultColumns()
        {
            List<string> visibleColumns = packageHandler.GetMiscSettingByKey("defaultVisibleColomns").value.Split('#').ToList();

            foreach (DataGridViewColumn column in dataGridViewHolidays.Columns.Cast<DataGridViewColumn>())
                if (visibleColumns.Contains(column.HeaderText))
                    column.Visible = true;
                else
                    column.Visible = false;

            if (compared)
            {
                string visibleColumnsCompared = packageHandler.GetMiscSettingByKey("visibleOnlyForComparedOption").value;

                foreach (string column in visibleColumnsCompared.Split('#'))
                    dataGridViewHolidays.Columns.Cast<DataGridViewColumn>().SingleOrDefault(c => c.HeaderText.ToString() == column).Visible = true;

                cbPackageColumns.Items.AddRange(visibleColumnsCompared.Split('#'));

            }

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

        private void DisplayHolidays(List<GulliverLibrary.Package> packages)
        {
            if (!compared)
                FillHolidays(packages);
            else
                FillComparedHolidays(packages);
        }

        private void FillComparedHolidays(List<GulliverLibrary.Package> packages)
        {
            this.packagesDS.Package.Clear();
            int count = 0;
            List<string> freezedAdultsSellats = packageHandler.GetFreezedAdultSellAts(dealId);
            List<string> freezedChildsSellats = packageHandler.GetFreezedChildSellAts(dealId);

           // packages = packages.Where(p => p.date == Convert.ToDateTime("11/04/2018") && p.duration == 7 && p.departureAirport == "LGW").ToList();

            foreach (GulliverLibrary.Package p in packages)
            {

                decimal avgpp = Math.Round(((p.sellAt * p.adults) + (p.childSellat * p.children)) / (p.adults + p.children));
                string key = p.date.ToString("dd-MM-yyyy") + "-" + p.departureAirport.Trim() + "-" + p.duration + "-" + p.board.Trim() + "-" + p.destinationAirport.Trim() + "-" + p.tsRoomKey + "-" + ((int)p.sellAt).ToString();
                bool frozen = (freezedAdultsSellats.Contains(key) && p.adults == 2 && p.children == 0 && p.infants == 0);
                this.packagesDS.Package.AddPackageRow("Delete", frozen, p.id, count, (p.leading) ? 1 : 0, p.date.ToString("MMMM"), p.date.DayOfWeek.ToString(), p.date, (p.status.Contains("New") ? "NEW" : (p.status.Contains("Not available") ? "STOPPED" : p.status.Contains("increased") ? "UP" : p.status.Contains("decreased") ? "DOWN" : string.Empty)), p.hotelKey, ((p.tsRoomKey != null) ? p.tsRoomKey : string.Empty), p.departureAirport, p.destinationAirport, p.duration, p.obDepartureTime.Trim(), p.obArrivalTime.Trim(), p.ibDepartureTime.Trim(), p.ibArrivalTime.Trim(), ((p.board != null) ? p.board.Trim() : string.Empty), Math.Round(p.flightPrice, 2), (p.airline != null) ? p.airline : string.Empty, ((p.obAirline == null || p.obAirline == string.Empty) ?(p.airline != null)? p.airline.Split('/').First().Trim(): string.Empty : p.obAirline.Trim()), ((p.ibAirline == null || p.ibAirline == string.Empty) ? (p.airline != null)?p.airline.Split('/').Last().Trim(): string.Empty : p.ibAirline.Trim()), p.obFlightNo, p.ibFlightNo, ((p.hotelName != null) ? p.hotelName : string.Empty), p.roomType, p.occupancy, p.adults, p.children, p.infants, Math.Round(p.hotelPrice, 2), Math.Round(p.childHotelPrice, 2), p.caa, p.baggagePrice, p.transfers, p.extras, p.childExtras, p.baseMarkup, p.totalMarkup, p.totalChildMarkup, p.carhireCosting, p.carParkingCosting, Math.Round(p.commission, 2), Math.Round(p.profit, 2), Math.Round(p.nett, 2), Convert.ToInt32(p.sellAt), avgpp, p.childNett, Math.Round((p.childProfit != null) ? p.childProfit : 0, 2), p.childSellat, freezedChildsSellats.Contains(p.date.ToString("dd-MM-yyyy") + "-" + p.departureAirport.Trim() + "-" + p.duration + "-" + p.board.Trim() + "-" + p.destinationAirport.Trim() + "-" + p.tsRoomKey + "-" + ((int)p.childSellat).ToString()), p.flightSource, p.hotelSource, (p.status != null ? p.status : string.Empty), ((p.oldSellAt != null) ? Convert.ToInt32(p.oldSellAt) : 0), p.isStandardRoom);
                count++;
            }

            if (isDataGridViewFormatted)
            {
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).ToList().ForEach(cell => cell.Style.BackColor = Color.White);
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).Where(cell => cell.Value != null && cell.Value.ToString().Contains("UP")).ToList().ForEach(cell => cell.Style.BackColor = Color.DarkRed);
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).Where(cell => cell.Value != null && cell.Value.ToString().Contains("DOWN")).ToList().ForEach(cell => cell.Style.BackColor = Color.DarkGreen);
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).Where(cell => cell.Value != null && (cell.Value.ToString().Contains("UP") || cell.Value.ToString().Contains("DOWN"))).ToList().ForEach(cell => cell.Style.ForeColor = Color.White);
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).Where(cell => cell.Value != null && cell.Value.ToString().Contains("STOPPED")).ToList().ForEach(cell => cell.Style.BackColor = Color.Yellow);
                dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Select(row => row.Cells[Summary.Name]).Where(cell => cell.Value != null && cell.Value.ToString().Contains("NEW")).ToList().ForEach(cell => cell.Style.BackColor = Color.Blue);
                isDataGridViewFormatted = false;
            }

            Summary.Visible = true;
            Status.Visible = true;
            cbIncludeNew.Visible = true;
            lblTotal.Text = (packages.Count > 0) ? "Total: " + packages.Count + " holidays" : "Total: " + packages.Count + " holiday";
        }

        public void FillHolidays(List<GulliverLibrary.Package> packages)
        {
            this.packagesDS.Package.Clear();
            DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dataGridViewHolidays);
            int count = 0;

            List<string> freezedAdultsSellats = packageHandler.GetFreezedAdultSellAts(dealId);
            List<string> freezedChildsSellats = packageHandler.GetFreezedChildSellAts(dealId);
            
            //packages = packages.Where(p => p.date == Convert.ToDateTime("11/04/2018") && p.duration == 7 && p.departureAirport == "LGW").ToList();

            foreach (GulliverLibrary.Package p in packages)
            {

                decimal avgpp = Math.Round(((p.sellAt * p.adults) + (p.childSellat * p.children)) / (p.adults + p.children));
                string key = p.date.ToString("dd-MM-yyyy") + "-" + p.departureAirport.Trim() + "-" + p.duration + "-" + p.board.Trim() + "-" + p.destinationAirport.Trim() + "-" + p.tsRoomKey + "-" + ((int)p.sellAt).ToString();
                bool frozen = (freezedAdultsSellats.Contains(key) && p.adults == 2 && p.children == 0 && p.infants == 0);
                this.packagesDS.Package.AddPackageRow("Delete", frozen, p.id, count, (p.leading) ? 1 : 0, p.date.ToString("MMMM"), p.date.DayOfWeek.ToString(), p.date, string.Empty, p.hotelKey, ((p.tsRoomKey != null) ? p.tsRoomKey : string.Empty), p.departureAirport, p.destinationAirport, p.duration, p.obDepartureTime.Trim(), p.obArrivalTime.Trim(), p.ibDepartureTime.Trim(), p.ibArrivalTime.Trim(), p.board.Trim(), Math.Round(p.flightPrice, 2), p.airline, (((p.obAirline == null || p.obAirline == string.Empty) && p.airline != null) ? p.airline.Split('/').First().Trim() : (p.obAirline != null) ? p.obAirline.Trim() : string.Empty), (((p.ibAirline == null || p.ibAirline == string.Empty) && p.airline != null) ? p.airline.Split('/').Last().Trim() : (p.ibAirline != null) ? p.ibAirline.Trim() : string.Empty), p.obFlightNo, p.ibFlightNo, ((p.hotelName != null) ? p.hotelName : string.Empty), p.roomType, p.occupancy, p.adults, p.children, p.infants, Math.Round(p.hotelPrice, 2), Math.Round(p.childHotelPrice, 2), p.caa, p.baggagePrice, p.transfers, Math.Round(p.extras, 2), p.childExtras, p.baseMarkup, p.totalMarkup, p.totalChildMarkup, p.carhireCosting, p.carParkingCosting, Math.Round(p.commission, 2), Math.Round(p.profit, 2), Math.Round(p.nett, 2), Convert.ToInt32(p.sellAt), avgpp, p.childNett, Math.Round((p.childProfit != null) ? p.childProfit : 0, 2), p.childSellat, freezedChildsSellats.Contains(p.date.ToString("dd-MM-yyyy") + "-" + p.departureAirport.Trim() + "-" + p.duration + "-" + p.board.Trim() + "-" + p.destinationAirport.Trim() + "-" + ((p.tsRoomKey != null)?p.tsRoomKey:string.Empty) + "-" + ((int)p.childSellat).ToString()), p.flightSource, p.hotelSource, (p.status != null ? p.status : string.Empty), ((p.oldSellAt != null) ? Convert.ToInt32(p.oldSellAt) : 0), p.isStandardRoom);
                count++;
            }

            if (isDataGridViewFormatted)
                isDataGridViewFormatted = false;

            lblTotal.Text = (packages.Count > 0) ? "Total: " + packages.Count + " holidays" : "Total: " + packages.Count + " holiday";
        }

        #endregion

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        ~flcsPackages()
        {
            Dispose();
        }

        private void txtChildSellat_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool found = true;
            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Value++;

            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                List<int> ids = new List<int>();

                try
                {
                    for (int i = 0; i < dataGridViewHolidays.Rows.Count; i++)
                    {
                        if (dataGridViewHolidays.Rows[i].Cells[37].Selected && found)
                            ids.Add(Convert.ToInt32(dataGridViewHolidays.Rows[i].Cells[3].Value));
                    }
                }
                catch { }

                if (ids.Count > 0)
                {
                    progressBar.Maximum = ids.Count + 10;

                    foreach (PackagesDS.PackageRow h in this.packagesDS.Package.Where(h => ids.Any(i => i == h.hiddenNumber)).ToList())
                    {
                        h.childSellat = Convert.ToDecimal(txtChildSellat.Text.Trim());

                        GulliverLibrary.Package package = new GulliverLibrary.Package();
                        package.commission = h.commission;
                        package.childNett = h.childNett;
                        package.childSellat = h.childSellat;
                        package.childProfit = h.childProfit;

                        h.childProfit = icosting.CalculateChildProfit(package);                        
                        progressBar.Value++;
                    }
                }

                dataGridViewHolidays.ContextMenuStrip = null;
                txtChildSellat.Text = string.Empty;
                cmsChildSellat.Visible = false;
                progressBar.Visible = false;
            }

        }
    }
}