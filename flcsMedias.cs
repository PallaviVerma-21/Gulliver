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
    public partial class flcsMedias : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private GulliverLibrary.QueryHandler gulliverQueryHandler;

        public flcsMedias()
        {
            gulliverQueryHandler = new GulliverLibrary.QueryHandler();
            InitializeComponent();
            FillMedias();
        }

        private void FillMedias()
        {
            packagesDS.Media.Rows.Clear();
            List<GulliverLibrary.Media> medias = gulliverQueryHandler.GetAllMedias();

            foreach (GulliverLibrary.Media media in medias)
                packagesDS.Media.AddMediaRow("Delete", media.id, media.supplier.Trim(), media.channelCode.Trim(), media.commission, ((media.pleaseNote != null)? media.pleaseNote.Trim(): string.Empty));            
        }
    }
}