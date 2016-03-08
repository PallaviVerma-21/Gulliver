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

namespace Gulliver
{
    public partial class flcsPackages : ComponentFactory.Krypton.Toolkit.KryptonForm
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
        private bool isDataGridViewFormatted = false;
        private List<GulliverLibrary.Package> packagesToBackUp;
        GulliverLibrary.Deal deal;
        public bool saved = false;

        public flcsPackages(List<GulliverLibrary.Package> packages, int dealId, bool compared, List<GulliverLibrary.Package> packagesToBackUp)
        {
          InitializeComponent();
          travelZooSuppliers = PackageGenerator.Tool.GetSuppliersBySuppliertype("traveltypesuppliers");
          timesSuppliers = PackageGenerator.Tool.GetSuppliersBySuppliertype("timestypesuppliers");
          seSupplier = PackageGenerator.Tool.GetSuppliersBySuppliertype("setypesuppliers");
          packageHandler = new PackageGenerator.PackageHandler();
          deal = packageHandler.GetDealById(dealId);

          this.dealId = dealId;
          this.packagesToBackUp = packagesToBackUp;
          this.compared = compared;
          this.supplierId = deal.Media.id;
                      
          DisplayButtonByMedia();         
          DisplayHolidays(packages);
          DisplayDefaultColumns();
        }

        #region events

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

           dataGridViewHolidays.Rows.Cast<DataGridViewRow>().Where(row => row.Cells[flightId.Name].Value != null && row.Cells[flightId.Name].Value.ToString() == "1").Select(row => row.Cells[sellAt.Name]).ToList().ForEach(cell => cell.Style.BackColor = Color.Orange);
        }

        private void showAllLabelH_Click(object sender, EventArgs e)
        {
          DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dataGridViewHolidays);
        }
                
        private void btnCalculateTenPercentLeading_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "Processing ...";
            lblMsg.Visible = true;
            progressBar.Visible = true;
            progressBar.Maximum = 9;
            Application.DoEvents();
            progressBar.Value = 1;
            icosting = PackageGenerator.FactoryCosting.GetCostingOBj(deal.Media.id, deal.id);
            progressBar.Value++;
            List<GulliverLibrary.Package> packages = ReadePackagesByGridview();
            progressBar.Value++;
            List<GulliverLibrary.Package> stoppedHolidays = packages.Where(h => h.status == "Not available anymore").ToList();
            progressBar.Value++;
            packages = packages.Where(h => h.status != "Not available anymore").ToList();
            progressBar.Value++;

            if (!cbIncludeNew.Checked)
                packages = packages.Where(h => h.status != "New").ToList();

            progressBar.Value++;
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
            icosting = PackageGenerator.FactoryCosting.GetCostingOBj(deal.Media.id, deal.id);
            packages = ReadePackagesByGridview();

            if (compared)
            {
                progressBar.Value++;

                if (!cbIncludeNew.Checked)
                    packages = packages.Where(h => h.status != "New").ToList();
            }

            progressBar.Value++;

            if (compared && (seSupplier.Contains(deal.Media.id) || PackageGenerator.Tool.GetSuppliersBySuppliertype("voyagesuppliers").Contains(deal.Media.id)))
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
            packageHandler.UpdatePackages(packages, deal.id);
            progressBar.Value++;

            progressBar.Value++;
            deal.finalizePrices = cbFinalizePrices.Checked;
            packageHandler.SaveDealInformation(deal);
            progressBar.Value++;
            packages = packageHandler.GetPackagesByDeal(dealId);

            BackupHolidays(packages, deal);

            packages.ForEach(h => h.status = string.Empty);
            progressBar.Value++;
            isDataGridViewFormatted = true;
            DisplayHolidays(packages);
            progressBar.Value++;
            progressBar.Visible = false;
            packageHandler.UpdateLastUpdatedTime(deal);
            lblMsg.Text = "Saved successfully!";
            Application.DoEvents();
            saved = true;
            this.Close();
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

        private void VisibleColumns()
        {
            foreach (DataGridViewColumn column in dataGridViewHolidays.Columns.Cast<DataGridViewColumn>())
            {
                if ((!visibleColumns.Contains(column.DataPropertyName)) || column.DataPropertyName == "hotelKey" || column.DataPropertyName == "id" || column.DataPropertyName == "hiddenNumber" || column.DataPropertyName == "flightId")
                    column.Visible = false;
                else
                    column.Visible = true;
            }
        }

        private List<GulliverLibrary.Package> ReadePackagesByGridview()
        {
            List<GulliverLibrary.Package> packages = new List<GulliverLibrary.Package>();
            packages = (from h in this.packagesDS.Package
                        // where h.id != null
                        select new GulliverLibrary.Package
                        {
                            id = h.id,
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
                            obFlightNo = h.obFlightNo,
                            ibFlightNo = h.ibFlightNo,
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
                            totalMarkup = h.totalMarkup,
                            baseMarkup = h.baseMarkup,
                            commission = h.commission,
                            nett = h.nett,
                            sellAt = h.sellAt,
                            searchType = (h.searchType == "FAB") ? 1 : 0,
                            Deal = deal,
                            status = h.status,
                            carhireCosting = h.carhireCosting,
                            carParkingCosting = h.
                            profit = h.profit,
                            hotelKey = h.hotelKey,
                            leading = (h.flightId == 1) ? true : false,
                            oldSellAt = h.oldSellAt,
                            childNett = h.childNett,
                            childSellat = h.childSellat,
                            //childProfit = h.childP
                            childHotelPrice = h.childHotelPrice,
                            childExtras = h.childExtras,
                            totalChildMarkup = h.totalChildMarkup

                        }).ToList();

            return packages;
        }

        private void BackupHolidays(List<GulliverLibrary.Package> packages, GulliverLibrary.Deal deal)
        {
            List<GulliverLibrary.PackageBackup> packageBackup = (from h in packages
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
                                                                     airline = h.airline,
                                                                     obFlightNo = h.obFlightNo,
                                                                     ibFlightNo = h.ibFlightNo,
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
                                                                     baseMarkup = h.baseMarkup,
                                                                     totalMarkup = h.totalMarkup,
                                                                     commission = h.commission,
                                                                     nett = h.nett,
                                                                     sellAt = h.sellAt,
                                                                     searchType = h.searchType,
                                                                     Deal = deal,
                                                                     status = h.status,
                                                                     carhireCosting = h.carhireCosting,
                                                                     profit = h.profit,
                                                                     leading = h.leading,
                                                                     deletedDate = DateTime.Now
                                                                 }).ToList();

            // first backup so it will be the same holiday as live
            if ((deal.PackageBackups == null || deal.PackageBackups.Count == 0))
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
                                         airline = h.airline,
                                         obFlightNo = h.obFlightNo,
                                         ibFlightNo = h.ibFlightNo,
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
                                         baseMarkup = h.baseMarkup,
                                         totalMarkup = h.totalMarkup,
                                         commission = h.commission,
                                         nett = h.nett,
                                         sellAt = h.sellAt,
                                         searchType = h.searchType,
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
                btnCalculateTenPercentLeading.Visible = true;
            else if (travelZooSuppliers.Contains(supplierId))
                btnCalculateTenPercentLeading.Visible = false;
            else if ((timesSuppliers.Contains(supplierId)))
                btnCalculateTenPercentLeading.Visible = false;
            else
                btnCalculateTenPercentLeading.Visible = false;
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

            foreach (GulliverLibrary.Package p in packages)
            {
                this.packagesDS.Package.AddPackageRow("Delete", p.id, count, (p.leading) ? 1 : 0, p.date.ToString("MMMM"), p.date.DayOfWeek.ToString(), p.date, (p.status.Contains("New") ? "NEW" : (p.status.Contains("Not available") ? "STOPPED" : p.status.Contains("increased") ? "UP" : p.status.Contains("decreased") ? "DOWN" : string.Empty)), p.hotelKey, p.departureAirport, p.destinationAirport, p.duration, p.obDepartureTime.Trim(), p.obArrivalTime.Trim(), p.ibDepartureTime.Trim(), p.ibArrivalTime.Trim(), p.board.Trim(), Math.Round(p.flightPrice, 2), p.airline, p.obFlightNo, p.ibFlightNo, p.roomType, p.occupancy, p.adults, p.children, p.infants, Math.Round(p.hotelPrice, 2), Math.Round(p.childHotelPrice, 2), p.caa, p.baggagePrice, p.transfers, p.extras, p.childExtras, p.baseMarkup, p.totalMarkup, p.totalChildMarkup, p.carhireCosting, p. Math.Round(p.commission, 2), Math.Round(p.profit, 2), Math.Round(p.nett, 2), Convert.ToInt32(p.sellAt), p.childNett, p.childSellat, ((p.searchType == 1) ? "FAB" : "Flightsheet"), (p.status != null ? p.status : string.Empty), ((p.oldSellAt != null) ? Convert.ToInt32(p.oldSellAt) : 0));
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

            foreach (GulliverLibrary.Package p in packages)
            {
                this.packagesDS.Package.AddPackageRow("Delete", p.id, count, (p.leading) ? 1 : 0, p.date.ToString("MMMM"), p.date.DayOfWeek.ToString(), p.date, string.Empty, p.hotelKey, p.departureAirport, p.destinationAirport, p.duration, p.obDepartureTime.Trim(), p.obArrivalTime.Trim(), p.ibDepartureTime.Trim(), p.ibArrivalTime.Trim(), p.board.Trim(), Math.Round(p.flightPrice, 2), p.airline, p.obFlightNo, p.ibFlightNo, p.roomType, p.occupancy, p.adults, p.children, p.infants, Math.Round(p.hotelPrice, 2), Math.Round(p.childHotelPrice, 2), p.caa, p.baggagePrice, p.transfers, p.extras, p.childExtras, p.baseMarkup, p.totalMarkup, p.totalChildMarkup, p.carhireCosting,p. , Math.Round(p.commission, 2), Math.Round(p.profit, 2), Math.Round(p.nett, 2), Convert.ToInt32(p.sellAt), p.childNett, p.childSellat, ((p.searchType == 1) ? "FAB" : "Flightsheet"), (p.status != null ? p.status : string.Empty), ((p.oldSellAt != null) ? Convert.ToInt32(p.oldSellAt) : 0));
                count++;
            }

            if (isDataGridViewFormatted)
                isDataGridViewFormatted = false;

            lblTotal.Text = (packages.Count > 0) ? "Total: " + packages.Count + " holidays" : "Total: " + packages.Count + " holiday";

        }

        #endregion
    }
}