using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Windows.Forms;

namespace pillindl {
    class Video {
        public string server;
        public string url;
        public string duration;
        public string urlThumb;
        public string creationDate;
        public string videoTitle;

        public Label lbl;
        //public ToolStripStatusLabel lbl; // not work

        public Dictionary<int, Dictionary<string, string>> listVideoMetadata = new
               Dictionary<int, Dictionary<string, string>>();
        public List<int> listVideoQuality = new List<int>();

        private string ua = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0";

        public Video() {
            this.server = null;
            this.url = null;
            this.duration = null;
            this.urlThumb = null;
            this.creationDate = null;
            this.videoTitle = null;

            listVideoMetadata.Clear();
            listVideoQuality.Clear();
        }


        public string getServerFromUrl(string url) {
            string srv = string.Empty;
            string rgx = @"^https?:\/\/.*(vimeo|dailymotion|twitter)\.com\/.*$";
            try {
                srv = getDataByRegex(url, rgx)[1];
            } catch (Exception ex) {
                log("ERROR: " + ex.Message);
                return null;
            }

            return srv;
        }

        public bool getVideoInfoFromUrl(string url) {
            string srv = getServerFromUrl(url);

            if (srv == null) {
                return false;
            }
            switch (srv) {
                case "vimeo":
                    return getVimeoVideoInfo(url);
                    //break;
                case "dailymotion":
                    return getDailymotionVideoInfo(url);
                    //break;
                case "twitter":
                    return getTwitterVideoInfo(url);
                    //break;
                default:
                    return false;
            }

            //return true;
        }

        private bool getVimeoVideoInfo(string url) {
            VmVideo vm = new VmVideo();
            vm.Label += new VmVideo.StatusDelegate(displayLog);

            if (!vm.getVideoInfo(url)) {
                return false;
            }
            try {
                this.creationDate = vm.creationDate;
                this.duration = vm.duration;
                this.urlThumb = vm.urlThumbVideo;
                this.listVideoMetadata = vm.listVideos;
                this.listVideoQuality = vm.listQuality;
                this.videoTitle = vm.videoTitle;
            } catch (Exception ex) {
                return false;
            }
            return true;
        }

        // dailymotion
        private bool getDailymotionVideoInfo(string url) {
            DmVideo vm = new DmVideo();
            vm.Label += new DmVideo.StatusDelegate(displayLog);

            if (!vm.getVideoInfo(url)) {
                return false;
            }
            try {
                this.creationDate = vm.creationDate;
                this.duration = vm.duration;
                this.urlThumb = vm.urlThumbVideo;
                this.listVideoMetadata = vm.listVideos;
                this.listVideoQuality = vm.listQuality;
                this.videoTitle = vm.videoTitle;
            } catch (Exception ex) {
                return false;
            }
            return true;
        }

        // twitter
        private bool getTwitterVideoInfo(string url) {
            TwVideo vm = new TwVideo();
            vm.Label += new TwVideo.StatusDelegate(displayLog);

            if (!vm.getVideoInfo(url)) {
                return false;
            }
            try {
                this.creationDate = vm.creationDate;
                this.duration = vm.duration;
                this.urlThumb = vm.urlThumbVideo;
                this.listVideoMetadata = vm.listVideos;
                this.listVideoQuality = vm.listQuality;
                this.videoTitle = vm.videoTitle;
            } catch (Exception ex) {
                return false;
            }
            return true;
        }

        // for thread
        public void displayLog(string message) {
            if (lbl.InvokeRequired) {
                lbl.Invoke(
                    new TwVideo.StatusDelegate(displayLog),
                    new Object[] { message });
            } else {
                lbl.Text = message;
            }
        }


        /// <summary>
        /// Get text from main text, using regex.
        /// </summary>
        /// <param name="text">(string) Main text</param>
        /// <param name="rgx">(string) Regex</param>
        /// <returns>Array</returns>
        public List<string> getDataByRegex(string text, string rgx) {
            Regex regex = new Regex(rgx);
            Match match = regex.Match(text);
            List<string> res = new List<string>();
            if (match.Success) {
                foreach (var value in match.Groups) {
                    res.Add(value.ToString());
                }
                return res;
            } else {
                return null; // not found
            }
        }

        // for logging
        private void log(string text) {
            Console.WriteLine(text);
        }
    }
}
