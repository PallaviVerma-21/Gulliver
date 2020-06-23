using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace GulliverII
{
    public partial class flcsTransferExcursionInfo : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        MySqlDataHandler.QueryHandler queryHandler;
        private long id;
        public bool updated = false;

        public flcsTransferExcursionInfo(long id)
        {
            InitializeComponent();
            this.id = id;
            queryHandler = new MySqlDataHandler.QueryHandler();
            FillData(id);
        }

        private void FillData(long id)
        {
            MySqlDataHandler.ExtraDescription extraDescription = queryHandler.GetExtraDescription(id);

            if (extraDescription != null)
            {
               ddlExtraTypes.SelectedItem = extraDescription.Type.Trim();
               txtTitle1.Text = extraDescription.Title.Trim();
               txtDescription1.Text = extraDescription.Description.Trim();
               txtObJourneyTime.Text = extraDescription.Outboundjourneytime;
               txtObJourneyDistance.Text = extraDescription.Outboundjourneydistance;
               txtMaximumCapacity.Text = extraDescription.Maximumcapcity;
               txtEstimatesStops.Text = extraDescription.Estimatedstops;
            }            
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlExtraTypes.SelectedItem != null && ddlExtraTypes.SelectedItem.ToString() != string.Empty && txtTitle1.Text != string.Empty)
                {
                    MySqlDataHandler.ExtraDescription extraDescription = queryHandler.GetExtraDescription(id);

                    if (extraDescription == null)
                    {
                        extraDescription = new MySqlDataHandler.ExtraDescription();
                        extraDescription.Id = 0;
                        extraDescription.SrRecNo = id;
                    }
                    
                    extraDescription.Type = ddlExtraTypes.SelectedItem.ToString();
                    extraDescription.Title = txtTitle1.Text.Trim();
                    extraDescription.Description = txtDescription1.Text.Trim();
                    extraDescription.Outboundjourneydistance = txtObJourneyDistance.Text;
                    extraDescription.Outboundjourneytime = txtObJourneyTime.Text;
                    extraDescription.Maximumcapcity = txtMaximumCapacity.Text;
                    extraDescription.Estimatedstops = txtEstimatesStops.Text;
                    queryHandler.UpdateExtraDescription(extraDescription);

                    updated = true;
                    MessageBox.Show("Information saved successfully!", "Transfer & Excursion Description", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
                else
                    MessageBox.Show("Type and Title can not be empty!", "Transfer & Excursion Description", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Error while saving, please check and try now!", "Transfer & Excursion Description", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryptonLabel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}