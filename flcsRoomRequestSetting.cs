using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Configuration;

namespace GulliverII
{
    public partial class flcsRoomRequestSetting : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private string id;
        private int dealId;
        private PackageGenerator.PackageHandler packageHandler;
        private GulliverLibrary.HotelContract hotelContract;
        private GulliverLibrary.RoomRequestSetting roomRequestSetting;

        public flcsRoomRequestSetting(string id, int dealId)
        {
           this.id = id;
           this.dealId = dealId;
           packageHandler = new PackageGenerator.PackageHandler(false, ConfigurationManager.AppSettings["enviroment"].ToString());
           hotelContract = packageHandler.GetHotelContractByRecno(Convert.ToInt32(id), dealId);
            if(hotelContract != null)
           roomRequestSetting = packageHandler.GetRoomRequestSettingByContractIdAndDeal(hotelContract.id, hotelContract.Deal.id);
            else
            roomRequestSetting = packageHandler.GetRoomRequestSettingByContractIdAndDeal(Convert.ToInt32(id), dealId);
           InitializeComponent();
           FillRoomRequestSettings();
        }

        #region methods
     
        private void FillRoomRequestSettings()
        {
            if (roomRequestSetting != null)
            {
               txtEmailTo.Text = roomRequestSetting.emailTo.Trim();
               txtEmailCC.Text = roomRequestSetting.emailCC.Trim();
               txtEmailFrom.Text = roomRequestSetting.emailFrom.Trim();

               if (roomRequestSetting.emailType == null || roomRequestSetting.emailType == string.Empty || roomRequestSetting.emailType.Trim() == "Group")
                  rbnGroup.Checked = true;
               else
                 rbnNotGroup.Checked = true;

                cbEnableRoomReservation.Checked = roomRequestSetting.isRoomRequestEnable;
            }
            else
            {
              txtEmailTo.Text = "accomreq@fleetway.com";
              txtEmailCC.Text = "accomreq@fleetway.com";
              txtEmailFrom.Text = "accomreq@fleetway.com";
              rbnNotGroup.Checked = true;
            }
        }

        private void SaveRoomRequestSettings()
        {
            if (roomRequestSetting == null)
            {
                roomRequestSetting = new GulliverLibrary.RoomRequestSetting();
                if (hotelContract != null)
                {
                   roomRequestSetting.Deal = hotelContract.Deal;
                   roomRequestSetting.offercontractId = hotelContract.id;
                }
                else
                {
                   roomRequestSetting.Deal = packageHandler.GetDealById(dealId);
                   roomRequestSetting.offercontractId = 0;
                }                
            }

            roomRequestSetting.emailTo = string.Join("#", txtEmailTo.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
            roomRequestSetting.emailCC = string.Join("#", txtEmailCC.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
            roomRequestSetting.emailFrom = txtEmailFrom.Text;
            roomRequestSetting.emailType = (rbnGroup.Checked) ? "Group" : "NotGroup";
            roomRequestSetting.isRoomRequestEnable = cbEnableRoomReservation.Checked;
            roomRequestSetting.Deal.enableRoomRequest = cbEnableRoomReservation.Checked;

            packageHandler.UpdateRoomRequestSetting(roomRequestSetting);
            MessageBox.Show("Details saved successfully!");
        }

        #endregion

        #region events

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRoomRequestSettings();
            this.Close();
        }
        #endregion

       
    }
}