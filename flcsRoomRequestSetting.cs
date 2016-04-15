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
    public partial class flcsRoomRequestSetting : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private string id;
        private PackageGenerator.PackageHandler packageHandler;
        private GulliverLibrary.HotelContract hotelContract;
        private GulliverLibrary.RoomRequestSetting roomRequestSetting;

        public flcsRoomRequestSetting(string id, int dealId)
        {
           this.id = id;
           packageHandler = new PackageGenerator.PackageHandler(false);
           hotelContract = packageHandler.GetHotelContractByRecno(Convert.ToInt32(id), dealId);
           roomRequestSetting = packageHandler.GetRoomRequestSettingByContractIdAndDeal(hotelContract.id, hotelContract.Deal.id);
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
                roomRequestSetting.Deal = hotelContract.Deal;
                roomRequestSetting.offercontractId = hotelContract.id;
            }

            roomRequestSetting.emailTo = string.Join("#", txtEmailTo.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
            roomRequestSetting.emailCC = string.Join("#", txtEmailCC.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
            roomRequestSetting.emailFrom = txtEmailFrom.Text;
            roomRequestSetting.emailType = (rbnGroup.Checked) ? "Group" : "NotGroup";
            roomRequestSetting.isRoomRequestEnable = cbEnableRoomReservation.Checked;

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

        private void flcsRoomRequestSetting_Load(object sender, EventArgs e)
        {

        }
    }
}