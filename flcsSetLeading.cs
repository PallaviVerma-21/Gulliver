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

namespace GulliverII
{
    public partial class flcsSetLeading : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private PackageGenerator.PackageHandler packageHandler;
        private int dealId;
        private List<GulliverLibrary.LeadingPrice> leadingPrices;


        public flcsSetLeading(int dealId, PackageGenerator.PackageHandler packageHandler)
        {
            this.dealId = dealId;
            this.packageHandler = packageHandler;
            InitializeComponent();
        }

        #region methods

        private void FillLeadings()
        {
            List<GulliverLibrary.LeadingPrice> leadingPrices = packageHandler.GetAllLeadPricesByDeal(dealId);
            this.packagesDS1.LeadingPrices.Clear();

            foreach (GulliverLibrary.LeadingPrice  leading in leadingPrices)
                this.packagesDS1.LeadingPrices.AddLeadingPricesRow(leading.id, leading.departureAirport, leading.duration,leading.occupancy,leading.leadingPrice, leading.lockTheLeading);
        }

        #endregion

        #region events

        private void showAllLeadings_Click(object sender, EventArgs e)
        {
          DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dataGridViewLeadings);
        }

        private void flcsSetLeading_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (GulliverLibrary.Deal deal = packageHandler.GetDealById(dealId))
            {
                leadingPrices = (from l in this.packagesDS1.LeadingPrices
                                 select new GulliverLibrary.LeadingPrice
                                 {
                                     id = l.id,
                                     Deal = deal,
                                     duration = l.Duration,
                                     occupancy = l.Occupancy,
                                     departureAirport = l.DepartureAirport,
                                     leadingPrice = l.Leading_Price,
                                     lockTheLeading = l.LockTheLeading

                                 }).ToList();
                packageHandler.UpdateLeadingPrices(leadingPrices, deal.id);
            }
        }

        #endregion
    }
}