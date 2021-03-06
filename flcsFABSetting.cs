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
    public partial class flcsFABSetting : ComponentFactory.Krypton.Toolkit.KryptonForm 
    {
        GulliverLibrary.QueryHandler gulliverQueryHandler;

        public flcsFABSetting()
        {
            gulliverQueryHandler = new GulliverLibrary.QueryHandler();
            InitializeComponent();
            FillData();
        }

        private void FillData()
        {
            string fabServer = gulliverQueryHandler.GetMiscSettingByName("fabServer").value.Trim();
            string maxRequestPerTime = gulliverQueryHandler.GetMiscSettingByName("maxDaysForRequest").value.Trim();

           //ddlFABServer.SelectedItem = (fabServer.Trim() == "http://pixie.xmltravel.eu/FindAndBook") ? "Pixie" : "Zara";
            ddlFABServer.SelectedItem = (fabServer.Trim() == "https://ca.xmltravel.com/FindAndBook") ? "MultiCom" : "MultiCom";
            ddlMaxDaysPerRequest.SelectedItem = maxRequestPerTime.Trim();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (GulliverLibrary.MiscSetting miscSetting = gulliverQueryHandler.GetMiscSettingByName("fabServer"))
            {
                miscSetting.value = (ddlFABServer.SelectedItem.ToString() == "MultiCom") ? "https://ca.xmltravel.com/FindAndBook" : "http://172.16.7.14/FindAndBook";
                //miscSetting.value = (ddlFABServer.SelectedItem.ToString() == "MultiCommLive") ? "https://ca.xmltravel.com/FindAndBook" : "https://ca.xmltravel.com/FindAndBook";
              gulliverQueryHandler.SaveMisSetting(miscSetting);
            }

            using (GulliverLibrary.MiscSetting miscSettingII = gulliverQueryHandler.GetMiscSettingByName("maxDaysForRequest"))
            {
              miscSettingII.value = ddlMaxDaysPerRequest.SelectedItem.ToString();
              gulliverQueryHandler.SaveMisSetting(miscSettingII);
            }

            this.Close();
        }
    }
}