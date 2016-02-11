using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;

namespace pillindl {
    class TwVideo {
        string url;
        public string duration;             // seconds
        public string urlThumbVideo;        // thumbnail
        public string creationDate;         // timestamp
        public string videoTitle;

        public string urlVideo;             // url video
        public string contentType;          // mime
        // list video w/metadata:
        // - resolution (key)
        //   dictionary:
        //   - url
        //   - size
        //   - content-type
        public Dictionary<int, Dictionary<string, string>> listVideos = new
               Dictionary<int, Dictionary<string, string>>();
        public List<int> listQuality = new List<int>();
        public string ua = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0";

        // private
        private string htmlData;
        private string json;

        // for send messages to window form controls
        public delegate void StatusDelegate(string text);
        public event StatusDelegate Label;


        // constructor
        public TwVideo() {
            this.url = null;

            this.duration = null;
            this.urlThumbVideo = null;
            this.creationDate = null;
            this.listVideos.Clear();
            this.videoTitle = null;

            this.htmlData = null;
            this.json = null;

        }

        public bool getVideoInfo(string url = "") {
            log("TwitterDL init");
            if (url == string.Empty) {
                return false;
            }

            // url: https://twitter.com/xoxorobin19/status/618232790899798016
            // conver to twcard-link: https://twitter.com/i/cards

            String rgx = @"^.*?\/(\d.+?)$";
            string idvid = "";

            // getting id from url
            if (getDataByRegex(url, rgx, true).Count() <= 1) {
                // id fail
                return false;
            } else {
                idvid = getDataByRegex(url, rgx, true)[1];
            }

            this.videoTitle = idvid;
            // card link
            url = "https://twitter.com/i/cards/tfw/v1/" + idvid + "?cardname=__entity_video&hide_attribution=true&earned=true";

            // get HTML
            this.htmlData = getHTML(url);
            if (this.htmlData == null) { // HTML failure
                return false;
            }
            //get JSON
            rgx = "data-player-config=\\\"(\\{.*?\\})\\\"";
            this.json = getValidJson(this.htmlData, rgx);
            if (this.json == null) {
                log("Get JSON string");
                return false;
            }

            //get video info
            getListVideo(this.json);

            this.listQuality = this.listVideos.Keys.ToList();
            this.listQuality.Sort();

            // get information and save to variables
            log("Get metadata");
            getDuration(this.json);
            getUrlThumb(this.json);
            //getVideoTitle();

            log("Metadata OK");
            return true;
        }

        /// <summary>
        /// Get HTML data from URL
        /// </summary>
        /// <param name="url">(string) Valid URL</param>
        /// <returns>html</returns>
        private string getHTML(string url = "") {
            string html = "";
            if (url == "") {
                return null;
            }
            try {
                log("Connect to Twitter CDN...");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                request.UserAgent = this.ua;

                if (response.StatusCode == HttpStatusCode.OK) {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    readStream = new StreamReader(receiveStream);
                    html = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                    log("Getting data...");
                    return html;
                } else {
                    return null;
                }
            } catch (Exception e) {
                log("HTTP request failure.");
                return null;
            }
        }

        /// <summary>
        /// Get JSON with metadata video
        /// </summary>
        /// <param name="html">HTML data</param>
        /// <returns>List</returns>
        private string getValidJson(string html, string regex = "") {
            List<string> res = new List<string>();
            string json = "";
            res = getDataByRegex(html, regex);
            if (res == null) {
                return null;
            }
            json = WebUtility.HtmlDecode(res[1]);
            return json;
        }

        /// <summary>
        /// Get text from main text, using regex.
        /// </summary>
        /// <param name="text">(string) Main text</param>
        /// <param name="rgx">(string) Regex</param>
        /// <param name="echo">(bool) enable/disable regex result </param>
        /// <returns>List</returns>
        public List<string> getDataByRegex(string text, string rgx, bool echo = false) {
            Regex regex = new Regex(rgx);
            Match match = regex.Match(text);
            List<string> res = new List<string>();
            if (match.Success) {
                foreach (var value in match.Groups) {
                    if (echo == true) {
                    }
                    res.Add(value.ToString());
                }
                return res;
            } else {
                return null; // not found
            }
        }

        // list video w/metadata
        private void getListVideo(string json) {
            log("Getting video link...");
            json = "[" + json + "]";
            JArray arr = JArray.Parse(json);

            // url video
            this.urlVideo = arr[0]["playlist"][0]["source"].ToString();

            // content-type
            this.contentType = arr[0]["playlist"][0]["contentType"].ToString();

            // resolution
            //string rgx = "[\\d]+x[\\d]+";
            string rgx = "^.+?/[\\d]+x([\\d].+)+/.+$";
            string res = getDataByRegex(this.urlVideo, rgx)[1];

            // add metadata to list
            this.listVideos.Add(Int32.Parse(res),
                new Dictionary<string, string> {
                        {"url", this.urlVideo },
                        {"content-type", this.contentType },
                        {"size", this.getVideoSize(this.urlVideo) }
                    }
                );
            log("Link OK");
        }

        // get information and save to variables
        // duration
        private void getDuration(string json) {
            json = "[" + json + "]";
            JArray arr = JArray.Parse(json);
            this.duration = this.getDataByRegex(arr[0]["duration"].ToString(), @"(\d+)?[.,]")[1];
        }
        // creation date (timestamp format)
        private void getCreationDate(string json) {
            //log("getDateCreation()");
            //json = "[" + json + "]";
            //log("getDateCreation(): json: length: " + json.Length.ToString());
            //JArray arr = JArray.Parse(json);
            //this.creationDate = arr[0]["request"]["timestamp"].ToString();
        }
        // url thumbnail
        private void getUrlThumb(string json) {
            json = "[" + json + "]";
            JArray arr = JArray.Parse(json);
            this.urlThumbVideo = arr[0]["posterImageUrl"].ToString();
        }

        /// <summary>
        /// Get size of video from url.
        /// Using HEAD request.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private string getVideoSize(string uri) {
            log("Get video size");
            string length = "";
            try {
                Thread.Sleep(1000);
                // http request
                HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(uri);
                // limit timeout
                wReq.Timeout = 5000;
                // referer
                wReq.Referer = uri;
                // user agent
                wReq.UserAgent = this.ua;
                // method (HEAD)
                wReq.Method = "HEAD";

                // get response
                HttpWebResponse wRes = (HttpWebResponse)wReq.GetResponse();
                // get length
                length = wRes.Headers.Get("Content-Length");
                log("debug (TwVideo.cs): length: " + length);
                wReq.GetResponse().Close();
            } catch (Exception e) {
                log("debug (TwVideo.cs): EXCEPTION: " + e.Message);
                length = "0";
            }
            return length;
        }

        /// <summary>
        /// Verify if string is a valid number
        /// </summary>
        /// <param name="str">(string) Number in string</param>
        /// <returns>(bool)</returns>
        private bool isNum(string str) {
            int n;
            return int.TryParse(str, out n);
        }


        /*****************************************************/

        /// <summary>
        /// Send messages to console (only debug)
        /// </summary>
        /// <param name="text">Text to console</param>
        private void log(string text) {
            string ts = DateTime.UtcNow.ToString("yyyyMMdd HHmmss.ffff");
            Console.WriteLine("[" + ts + "] (TwVideo.cs) - " + text);

            // send text to label object
            if (Label != null) {
                Label("[tw] " + text);
            }
            // allow form events (show text on label)
            Application.DoEvents();
        }
    }
}
