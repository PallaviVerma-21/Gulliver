using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Linq;

namespace GulliverII
{
    public partial class flcsOtherChannels : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private GulliverLibrary.QueryHandler gulliverQueryHandler;
        private GulliverLibrary.Deal deal;
        private PackageGenerator.PackageHandler packageHandler;

        public flcsOtherChannels(GulliverLibrary.QueryHandler gulliverQueryHandler, GulliverLibrary.Deal deal)
        {
            InitializeComponent();
            this.gulliverQueryHandler = gulliverQueryHandler;
            packageHandler = new PackageGenerator.PackageHandler(false);
            this.deal = deal;
            FillMedia();
        }

        private void FillMedia()
        {
            List<GulliverLibrary.Media> medias = gulliverQueryHandler.GetAllMedias();
            List<int> otherChannelsIds = deal.OtherChannels.ToList().Select(c => c.Media.id).ToList();

            foreach (GulliverLibrary.Media media in medias.OrderBy(m => m.supplier))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = media.supplier;
                item.Value = media.id;
               
                if(otherChannelsIds.Contains(media.id))                   
                 cbChannels.Items.Add(item,true);
                else
                cbChannels.Items.Add(item);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<GulliverLibrary.OtherChannel> otherChannels =  new List<GulliverLibrary.OtherChannel>();

            foreach (ComboBoxItem item in cbChannels.CheckedItems)
            {
                GulliverLibrary.OtherChannel otherChannel = new GulliverLibrary.OtherChannel();
                otherChannel.Deal = deal;
                otherChannel.Media = gulliverQueryHandler.GetMediaByCode(item.Value.ToString());
                otherChannels.Add(otherChannel);
            }

            packageHandler.UpdateOtherChannels(otherChannels, deal.id);
            this.Close();
        }
    }
}