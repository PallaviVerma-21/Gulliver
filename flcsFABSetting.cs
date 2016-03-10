using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Gulliver
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

            ddlFABServer.SelectedItem = (fabServer.Trim() == "http://172.16.7.4/FindAndBook") ? "Zara" : "Pixie";
            ddlMaxDaysPerRequest.SelectedItem = maxRequestPerTime.Trim();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GulliverLibrary.MiscSetting miscSetting = gulliverQueryHandler.GetMiscSettingByName("fabServer");
            miscSetting.value = (ddlFABServer.SelectedItem.ToString() == "Zara") ? "http://172.16.7.4/FindAndBook" : "http://172.16.7.14/FindAndBook";
            gulliverQueryHandler.SaveMisSetting(miscSetting);

            GulliverLibrary.MiscSetting miscSettingII = gulliverQueryHandler.GetMiscSettingByName("maxDaysForRequest");
            miscSettingII.value = ddlMaxDaysPerRequest.SelectedItem.ToString();
            gulliverQueryHandler.SaveMisSetting(miscSettingII);
            this.Close();
        }
    }
}