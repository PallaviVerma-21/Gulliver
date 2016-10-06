using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Linq;
using DataGridViewAutoFilter;

namespace GulliverII
{
    public partial class flcsBackupPackage : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private int dealId;
        private List<GulliverLibrary.PackageBackup> packageBackups;
        private PackageGenerator.PackageHandler packageHandler;
        List<int> travelZooSuppliers;
        List<int> timesSuppliers;
        List<int> seSupplier;
        public List<GulliverLibrary.Package> packages;

        public flcsBackupPackage(int dealId, List<GulliverLibrary.PackageBackup> packageBackups, PackageGenerator.PackageHandler packageHandler)
        {
            InitializeComponent();
            this.packageHandler = packageHandler;
            this.dealId = dealId;
            this.packageBackups = packageBackups;
            FillHolidays(packageBackups);
        }

        private void FillHolidays(List<GulliverLibrary.PackageBackup> packageBackups)
        {
            this.packagesDS.PackageBackup.Clear();
            int count = 0;
             
            foreach (GulliverLibrary.PackageBackup  package in packageBackups)
            {
                this.packagesDS.PackageBackup.AddPackageBackupRow(package.id, count, (package.leading) ? 1 : 0, package.date, string.Empty,((package.tsRoomKey != null)?package.tsRoomKey: string.Empty), package.departureAirport, package.destinationAirport, package.duration, package.obDepartureTime.Trim(), package.obArrivalTime.Trim(), package.ibDepartureTime.Trim(), package.ibArrivalTime.Trim(), package.board.Trim(), Math.Round(package.flightPrice, 2), package.airline, package.obAirline,package.ibAirline, package.obFlightNo, package.ibFlightNo, package.roomType, package.occupancy,package.adults, package.children, package.infants, Math.Round(package.hotelPrice, 2), 0,package.caa, package.baggagePrice, package.transfers, package.extras, 0, 0,package.totalMarkup,0,package.carhireCosting,package.carParkingCosting, Math.Round(package.commission, 2),package.profit, Math.Round(package.nett, 2),Convert.ToInt32(package.sellAt),Math.Round(package.childNett,2), package.childSellat,package.flightSource, package.hotelSource,string.Empty, package.oldSellAt);
                count++;
            }
        }

        private void RestorePackages()
        {
            using (GulliverLibrary.Deal deal = packageHandler.GetDealById(dealId))
            {
                travelZooSuppliers = PackageGenerator.Tool.GetSuppliersBySuppliertype("traveltypesuppliers");
                timesSuppliers = PackageGenerator.Tool.GetSuppliersBySuppliertype("timestypesuppliers");
                seSupplier = PackageGenerator.Tool.GetSuppliersBySuppliertype("setypesuppliers");

                packages = (from h in packageBackups
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
                                obAirline = h.obAirline,
                                ibAirline = h.ibAirline,
                                hotelPrice = h.hotelPrice,
                                caa = h.caa,
                                baggagePrice = h.baggagePrice,
                                transfers = h.transfers,
                                extras = h.extras,
                                totalMarkup = h.totalMarkup,
                                commission = h.commission,
                                nett = h.nett,
                                sellAt = h.sellAt,
                                flightSource = h.flightSource,
                                hotelSource = h.hotelSource,
                                Deal = deal,
                                status = h.status,
                                carhireCosting = h.carhireCosting,
                                carParkingCosting = (h.extras - (h.caa + h.baggagePrice + h.transfers + h.carhireCosting)),
                                profit = ((travelZooSuppliers.Contains(deal.Media.id) || timesSuppliers.Contains(deal.Media.id)) ? h.totalMarkup : 0),
                                hotelKey = string.Empty,
                                tsRoomKey = (h.tsRoomKey != null)? h.tsRoomKey: string.Empty,
                                leading = false

                            }).ToList();
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            RestorePackages();
            this.Close();
        }

        private void dataGridViewPackageBackup_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell
             .GetFilterStatus(dataGridViewPackageBackup);
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
    }
}