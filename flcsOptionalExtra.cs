using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Linq;

namespace Gulliver
{
    public partial class flcsOptionalExtra : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public List<GulliverLibrary.DealOptionalExtra> optioanlExtras;
        private PackageGenerator.PackageHandler packageHandler;
        private int dealId;

        public flcsOptionalExtra(List<GulliverLibrary.DealOptionalExtra> optioanlExtras, int dealId, PackageGenerator.PackageHandler packageHandler)
        {
           this.packageHandler = packageHandler;
           this.dealId = dealId;
           this.optioanlExtras = optioanlExtras;
           InitializeComponent();
           FillOptionalExtras();
        }

        #region methods

        private void FillOptionalExtras()
        {
            this.gulliverDS.OptionalExtra.Clear();

            foreach (GulliverLibrary.DealOptionalExtra extra in optioanlExtras)
                this.gulliverDS.OptionalExtra.AddOptionalExtraRow(extra.id, "Delete", extra.description.Trim(), extra.included, extra.cost);
        }

        private void flcsOptionalExtra_FormClosing(object sender, FormClosingEventArgs e)
        {
            optioanlExtras = (from c in this.gulliverDS.OptionalExtra
                            where !c.IsDescriptionNull() && !c.IsCostNull()
                            select new GulliverLibrary.DealOptionalExtra
                            {
                                id = c.id,
                                description = c.Description.Trim(),
                                included = c.Included,
                                cost = c.Cost
                            }).ToList();
        }

        #endregion

        #region events

        private void dataGridViewExtras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewExtras.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridViewExtras.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
            {
                switch (MessageBox.Show("This will delete selected extra - continue?", "Delete Extra", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        dataGridViewExtras.Rows.Remove((DataGridViewRow)dataGridViewExtras.Rows[e.RowIndex]);
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        return;
                }
            }
        }

        private void dataGridViewExtras_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewExtras.Rows[e.RowIndex].Cells[0].Value == null)
            {
                dataGridViewExtras.Rows[e.RowIndex].Cells[0].Value = "0";
                dataGridViewExtras.Rows[e.RowIndex].Cells[1].Value = "Delete";
                dataGridViewExtras.Rows[e.RowIndex].Cells[3].Value = false;
                dataGridViewExtras.Rows[e.RowIndex].Cells[4].Value = 0;
            }
        }

        private void dataGridViewExtras_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            switch (MessageBox.Show("This will delete selected extra - continue?", "Delete Extra", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    dataGridViewExtras.Rows.Remove(e.Row);
                    break;

                case System.Windows.Forms.DialogResult.No:
                    e.Cancel = true;
                    return;
            }
        }

        #endregion
    }

     
}