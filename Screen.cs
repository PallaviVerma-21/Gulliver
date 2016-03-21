using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Threading;

namespace Gulliver
{
    public partial class Screen : Form
    {
        public Screen()
        {
            InitializeComponent();
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.Text = "Gulliver 2 - " + Application.ProductVersion + "V";
            notifyIcon.BalloonTipText = "Gulliver 2 - " + Application.ProductVersion + "V";

            PackageGenerator.Tool.gulliverDefaultpath = ConfigurationManager.AppSettings["gulliverDefaultPath"].ToString().Replace("%LOCALAPPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            PackageGenerator.Tool.gulliverBucket = ConfigurationManager.AppSettings["gulliverBucket"].ToString().Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            PackageGenerator.Tool.gulliverNewBucket = ConfigurationManager.AppSettings["gulliverNewBucket"].ToString().Replace("%LOCALAPPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            PackageGenerator.Tool.userSettingFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PDF Writer\\Gulliver";
            CreatePDFFolderStructure();
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            lblVersion.Text = Application.ProductVersion + "V";
        }
        
        public void CreatePDFFolderStructure()
        {
            if (!System.IO.Directory.Exists(PackageGenerator.Tool.gulliverDefaultpath))
                System.IO.Directory.CreateDirectory(PackageGenerator.Tool.gulliverDefaultpath);

            if (!System.IO.Directory.Exists(PackageGenerator.Tool.gulliverNewBucket))
            {
                System.IO.Directory.CreateDirectory(PackageGenerator.Tool.gulliverNewBucket);

                if (System.IO.Directory.Exists(PackageGenerator.Tool.gulliverBucket))
                {
                    FileInfo[] flist = new DirectoryInfo(PackageGenerator.Tool.gulliverBucket).GetFiles();

                    foreach (FileInfo f in flist)
                        File.Move(f.FullName, PackageGenerator.Tool.gulliverNewBucket + "\\" + f.Name);

                    Directory.Delete(PackageGenerator.Tool.gulliverBucket);
                }
            }

            if (!System.IO.Directory.Exists(PackageGenerator.Tool.userSettingFilePath))
                System.IO.Directory.CreateDirectory(PackageGenerator.Tool.userSettingFilePath);


            System.IO.File.Copy(ConfigurationManager.AppSettings["gulliverSettingFlies"].ToString() + "\\settings.ini", PackageGenerator.Tool.userSettingFilePath + "\\settings.ini", true);
            System.IO.StreamReader reader = new StreamReader(PackageGenerator.Tool.userSettingFilePath + "\\settings.ini");
            string s = reader.ReadToEnd().Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            reader.Close();
            System.IO.StreamWriter writer = new StreamWriter(PackageGenerator.Tool.userSettingFilePath + "\\settings.ini", false);
            writer.WriteLine(s);
            writer.Close();

            if (!Directory.Exists("C:\\TEMP"))
                Directory.CreateDirectory("C:\\TEMP");
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bgWorker.CancellationPending)
            {
                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(PackageGenerator.Tool.gulliverDefaultpath);
                System.IO.FileInfo[] files = directory.GetFiles("*.pdf", System.IO.SearchOption.TopDirectoryOnly);

                if (files.Count() > 0)
                {
                    foreach (System.IO.FileInfo fi in files)
                    {
                        try //need to try because it could still be being written
                        {
                            fi.MoveTo(PackageGenerator.Tool.gulliverNewBucket + DateTime.Now.Month.ToString().PadLeft(2, '0') +
                            DateTime.Now.Day.ToString().PadLeft(0, '2') + " " + DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                            DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0') +
                            DateTime.Now.Millisecond.ToString().PadLeft(3, '0') + ".pdf");
                        }
                        catch (Exception ex) { }
                    }

                    PackageGenerator.Tool.newFile = true;
                }

                System.Threading.Thread.Sleep(1000);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            flcsLibrary libraryWindow = new flcsLibrary();
            libraryWindow.ShowDialog();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
            BeginInvoke(new MethodInvoker(delegate
            {
                Hide();  
            }));

            timer.Enabled = false;
        }

        private void mixAndMatchFlightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsMain formMain = new flcsMain();
            formMain.ShowDialog();
        }

        private void libraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flcsLibrary libraryWindow = new flcsLibrary();
            libraryWindow.ShowDialog();
        }      
        
    }
}
