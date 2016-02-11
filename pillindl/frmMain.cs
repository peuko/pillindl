using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;
using System.Net;

using System.Xml;
using System.Xml.Linq;

namespace pillindl {
    public partial class frmMain : Form {
        // config file
        string cfgFile = Application.StartupPath + @"\config.xml";

        // dictionary with url and quality
        Dictionary<string, string> lstVid = new Dictionary<string, string>();
        // list of quality (resolution)
        List<int> qualityVideo = new List<int>();

        string urlVideo = string.Empty;

        // define class
        Video vm = new Video();

        // variables for Download
        WebClient webClient;            // webclient
        Stopwatch sw = new Stopwatch(); // idem
        bool downloading = false;       // downloading some file

        // Save directory
        String path = string.Empty;
        // filename
        String filename = string.Empty;
        // extension
        string extension = ".mp4";
        
        // class ready for to use

        public frmMain() {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e) {
            // create config file if not exist
            if (!File.Exists(cfgFile)) {
                createConfigFile();
            }


            // read config data
            XmlDocument xd = new XmlDocument();
            xd.Load(cfgFile);
            XmlNode dp = xd.SelectSingleNode("/app/dlpath"); // destination path
            // add path from xml to global variable
            path = dp.InnerText;
            lblSaveDirectory.Text = path;

            controlsDownload(false);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            if(downloading == true) {
                string dlmsg = "Downloading video.\nCancel now?";
                    DialogResult dlgDl = MessageBox.Show(dlmsg, "Downloading...",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation
                        );
                if (dlgDl == DialogResult.No) {
                    e.Cancel = true;
                } else {
                    e.Cancel = false;
                    saveConfigFile();           // save config
                    webClient.CancelAsync();    // cancel download thread
                    while(webClient.IsBusy) {   // wait until end of download thread
                        lblStatus.Text = "Waiting for thread...";
                        Application.DoEvents();
                    }
                    downloading = false;
                    statusDownload("Download cancelled.");
                    controlsDuringDownload(true);
                    btnDownload.Text = "Download";
                }
            }
        }

        private void btnMenuSaveFolder_Click(object sender, EventArgs e) {
            mnuSaveFolder.Show(btnMenuSaveFolder, new Point(0,btnMenuSaveFolder.Height));
        }

        private void clearControls() {
            txtVideoTitle.Text = String.Empty;
            lblVideoDuration.Text = String.Empty;
            lblVideoSize.Text = String.Empty;
            cbListQuality.Items.Clear();
        }

        private void btnGetInfo_Click(object sender, EventArgs e) {
            // first, check if url is vimeo/dailymotion/twitter
            if(vm.getServerFromUrl(txtURL.Text) == null) {
                MessageBox.Show("URL invalid.\n Check that is Vimeo/Dailymotion/Twittet URL valid.",
                    "URL invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // clear text, labels and combobox
            clearControls();

            // disable download controls
            controlsDownload(false);

            // label Status (to change/improve)
            vm.lbl = lblStatus;

            // get information of video
            if (!vm.getVideoInfoFromUrl(txtURL.Text)) {
                MessageBox.Show("ERROR: no data from: " + txtURL.Text,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // clear combobox
            cbListQuality.Items.Clear();

            // rellenar combobox desde lista
            foreach (int itm in vm.listVideoQuality) {
                cbListQuality.Items.Add(itm.ToString()); // agregar a combobox
            }

            //
            cbListQuality.SelectedIndex = cbListQuality.Items.Count - 1;

            // diccionario con los videos
            lstVid.Clear();

            txtVideoTitle.Text = vm.videoTitle;

            controlsDownload(true);
            return;
        }

        private void btnResetTitle_Click(object sender, EventArgs e) {
            txtVideoTitle.Text = vm.videoTitle;
        }

        private void cbListQuality_SelectedIndexChanged(object sender, EventArgs e) {
            if (cbListQuality.SelectedIndex == -1) {
                MessageBox.Show("No item selected","!", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            } else {
                urlVideo = vm.listVideoMetadata[Int32.Parse(cbListQuality.Text)]["url"];
                lblVideoSize.Text = bytes2kmg(Int64.Parse(vm.listVideoMetadata[Int32.Parse(cbListQuality.Text)]["size"]));
                lblVideoDuration.Text = sec2hms(Int32.Parse(vm.duration));
            }
        }


        // functions

        // delete invalid characters for filename
        private string validFilename(string filename) {
            string invalid = "\\/*\"<>?|:";
            foreach (char c in invalid) {
                filename = filename.Replace(c.ToString(), "");
            }
            return filename;
        }

        // convert seconds to hh:mm:ss format
        private string sec2hms(int sec) {
            string time = string.Format("{0:00}:{1:00}:{2:00}", sec / 3600, ((sec / 60) % 60), sec % 60);
            return time;
        }

        // convert bytes to kb, mb, gb
        private string bytes2kmg(long bytes) {
            double result = 0;
            string ms = string.Empty;
            result = bytes;
            ms = "b";
            if(bytes >= Math.Pow(2,10)) {
                result = bytes / Math.Pow(2,10);
                ms = "kiB";
            }
            if (bytes >= Math.Pow(2,20)) {
                result = bytes / Math.Pow(2,20);
                ms = "MiB";
            }
            if (bytes >= Math.Pow(2, 30)) {
                result = bytes / Math.Pow(2, 30);
                ms = "GiB";
            }


            return Math.Round(result, 2) + ms;
        }

        private void selectDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
            DialogResult diag = fbFolder.ShowDialog();
            if (diag == DialogResult.OK) {
                lblSaveDirectory.Text = fbFolder.SelectedPath;
                path = fbFolder.SelectedPath;
            }
        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Process.Start(lblSaveDirectory.Text);
            } catch {
                MessageBox.Show("Unable to open: " + lblSaveDirectory.Text,
                    "Error to open dir", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void mnuAmeblo_Click(object sender, EventArgs e) {
            string msg = "Download videos from Ameblo (japanese blog).\n\n";
            msg += "Not implemented. (Soon...)";
            MessageBox.Show(msg,"TODO", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnDownload_Click(object sender, EventArgs e) {
            if(downloading == false) {

                string msg = "Error while access to '" + path + "'.\n";
                msg += "Directory not exist or invalid (Maybe you haven't privileges).";

                // check if directory exist / valid
                if (!Directory.Exists(path)) {
                    MessageBox.Show(
                        msg,
                        "Directory invalid",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // check if file exist
                filename = validFilename(txtVideoTitle.Text);
                string resource = path + "\\" + filename + extension;

                if (File.Exists(resource)) {
                    msg = "The filename: '" + resource + "' already exist.\n\n";
                    msg += "[Yes] Overwrite\n[No] Rename and download\n[Cancel] Abort download";

                    DialogResult dialog = MessageBox.Show(
                        msg, "Filename exist",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                    switch (dialog) {
                        case DialogResult.Yes:
                            // call downloader(resource)
                            downloader(
                                vm.listVideoMetadata[Int32.Parse(cbListQuality.Text)]["url"],
                                resource
                            );
                            return;
                        //break;
                        case DialogResult.No:
                            // rename to "something (copy).mp4"
                            resource = path + "\\" + filename + " (copy)" + extension;
                            // call downloader(resource)
                            downloader(
                                vm.listVideoMetadata[Int32.Parse(cbListQuality.Text)]["url"],
                                resource
                            );
                            return;
                        //break;
                        case DialogResult.Cancel:
                            return;
                    }
                }
                downloader(vm.listVideoMetadata[Int32.Parse(cbListQuality.Text)]["url"], resource);

                // end of download
            } else {
                string dlmsg = "Downloading video.\nCancel now?";
                DialogResult dlgDl = MessageBox.Show(dlmsg, "Downloading...",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation
                    );
                if (dlgDl == DialogResult.Yes) {
                    webClient.CancelAsync();
                    downloading = false;
                    statusDownload("Download cancelled.");
                    controlsDuringDownload(true);
                    btnDownload.Text = "Download";
                    return;
                }
            }

        }

        // downloader
        private void downloader(string url, string destination) {
            downloading = true;
            controlsDuringDownload(false);
            btnDownload.Text = "Cancel";
            DownloadFile(url, destination);
        }

        // enable/disable controls

        // disable download controls
        private void controlsDownload(bool status) {
            btnResetTitle.Enabled = status;
            txtVideoTitle.Enabled = status;
            cbListQuality.Enabled = status;
            btnMenuSaveFolder.Enabled = status;
            lblSaveDirectory.Enabled = status;
            btnDownload.Enabled = status;
        }

        // enable/disable controls during download (all controls except Download/Cancel button)
        private void controlsDuringDownload(bool status) {
            txtURL.Enabled = status;
            btnGetInfo.Enabled = status;

            btnResetTitle.Enabled = status;
            txtVideoTitle.Enabled = status;
            cbListQuality.Enabled = status;
            btnMenuSaveFolder.Enabled = status;
            lblSaveDirectory.Enabled = status;
        }

        // reset status and progressbar
        private void statusDownload(string status = "") {
            stlblPercentProgress.Text = "0%";
            stpbProgressbar.Value = 0;
            lblStatus.Text = status;
        }

        private void createConfigFile() {
            // create new config file
            XmlDocument xd = new XmlDocument();
            XmlDeclaration xdecl = xd.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement xroot = xd.DocumentElement;
            xd.InsertBefore(xdecl, xroot);

            // root
            XmlElement xapp = xd.CreateElement(string.Empty, "app", string.Empty);
            xd.AppendChild(xapp);
            // destination path
            XmlElement xdlp = xd.CreateElement(string.Empty, "dlpath", string.Empty);
            // append to root
            xapp.AppendChild(xdlp);
            // default values
            xdlp.InnerText = "";

            xd.Save(cfgFile);
        }

        private void saveConfigFile() {
            if (File.Exists(cfgFile)) {
                // save config values to xml file
                XmlDocument xd = new XmlDocument();
                xd.Load(cfgFile);
                xd.SelectSingleNode("/app/dlpath").InnerText = lblSaveDirectory.Text;
                xd.Save(cfgFile);
            } else {
                Console.WriteLine("[X] error: xml>save>" + lblSaveDirectory.Text);
            }
        }


        /* ********************************************************************
         * DESCARGA DE ARCHIVOS
         */
        public void DownloadFile(string urlAddress, string location) {
            using (webClient = new WebClient()) {
                webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:43.0) Gecko/20100101 Firefox/43.0");
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                // The variable that will be holding the url address (making sure it starts with http://)
                Uri URL = new Uri(urlAddress); //urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("http://" + urlAddress);

                // Start the stopwatch which we will be using to calculate the download speed
                sw.Start();

                try {
                    // Start downloading the file
                    webClient.DownloadFileAsync(URL, location);
                } catch (Exception ex) {
                    MessageBox.Show("Error during download: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // The event that will fire whenever the progress of the WebClient is changed
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            // Update the progressbar percentage only when the value is not the same.
            stpbProgressbar.Value = e.ProgressPercentage;

            // Show the percentage on our label.
            stlblPercentProgress.Text = e.ProgressPercentage.ToString() + "%";

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            lblStatus.Text = string.Format("{0} MB's of {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        // The event that will trigger when the WebClient is completed
        private void Completed(object sender, AsyncCompletedEventArgs e) {
            if (e.Cancelled) {
                // TODO: delete partial file
                return;
            }
            if (e.Error != null) {
                string error = e.Error.Message;
                MessageBox.Show("Error during download: \n" + error,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // enable controls
                controlsDuringDownload(true);
                // status: ready
                
                return;
            }
            // Reset the stopwatch.
            sw.Reset();
            MessageBox.Show("Download complete!",
                "Download status", MessageBoxButtons.OK, MessageBoxIcon.Information);

            controlsDuringDownload(true);
            statusDownload("Download complete.");
            downloading = false;
            btnDownload.Text = "Download";

            //stDownloadReady();
        }

        private void mnuCheckNewVersion_Click(object sender, EventArgs e) {
            Process.Start("http://wenupix.cl/wp/pillinvdl/");
        }

        private void mnuAbout_Click(object sender, EventArgs e) {
            Form f = new frmAbout();
            f.ShowDialog();
        }
        /*
* FIN FUNCIONES DESCARGA DE ARCHIVOS
**********************************************************************/
    }
}
