using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using System.Web;

namespace pillindl {
    class VmVideo {
        public string duration;             // seconds
        public string urlThumbVideo;        // thumbnail
        public string creationDate;         // timestamp
        public string videoTitle;
        // list video w/metadata:
        // - resolution (key)
        //   dictionary:
        //   - url
        //   - size
        //   - content-type
        public Dictionary<int, Dictionary<string, string>> listVideos = new
               Dictionary<int, Dictionary<string, string>>();
        public List<int> listQuality = new List<int>();
        // private
        private string htmlData;
        private string json;
        private string ua = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0";

        // for send messages to window form controls
        public delegate void StatusDelegate(string text);
        public event StatusDelegate Label;

        // constructor
        public VmVideo() {
            this.duration = null;
            this.urlThumbVideo = null;
            this.creationDate = null;
            this.listVideos.Clear();
            this.videoTitle = null;

            this.htmlData = null;
            this.json = null;
        }

        public bool getVideoInfo(string url = "") {
            log("VimeoDL init");
            string regex1 = "\\\"(https:\\/\\/player\\.vimeo\\.com\\/video\\/[0-9].+?\\/config\\?.*?)\\\"";

            if (url == string.Empty) {
                return false;
            }
            // get HTML
            this.htmlData = getHTML(url);
            if (this.htmlData == null) {
                return false;
            }

            //get JSON
            this.json = getValidJson(this.htmlData, regex1);
            if (this.json == null) {
                return false;
            }

            string url2 = this.json;

            // get HTML
            this.json = getHTML(url2);
            if (this.json == null) {
                return false;
            }

            // get information and save to variables
            getListVideo(this.json);                // list videos

            this.listQuality = this.listVideos.Keys.ToList();
            this.listQuality.Sort();

            log("Get metadata");
            getDuration(this.json);                 // duration
            getUrlThumb(this.json);                 // thumbnail
            getCreationDate(this.json);             // creation date
            getVideoTitle(this.htmlData);

            log("Metadata OK");
            return true;
        }

        private void getVideoTitle(string html) {
            string rgx = "<h1 class=\\\"clip_info-header\\\">(.+)?</h1>"; // regex
            string title = string.Empty;
            try {
                title = WebUtility.HtmlDecode(getDataByRegex(html, rgx)[1]);
            } catch (Exception ex) {
                this.videoTitle = null;
            }
            this.videoTitle = title;
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
                log("Connect to Vimeo server...");
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
                    log("Get HTML data");
                    return html;
                } else {
                    return null;
                }
            } catch (Exception e) {
                log("HTTP request failure");
                return null;
            }
        }

        /// <summary>
        /// Get JSON with metadata video
        /// </summary>
        /// <param name="html">HTML data</param>
        /// <returns>List</returns>
        private string getValidJson(string html, string regex = "") {
            log("Get JSON string");
            List<string> res = new List<string>();
            string json = "";
            //string regex = "\\\"(https:\\/\\/player\\.vimeo\\.com\\/video\\/[0-9].+?\\/config\\?.*?)\\\"";
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
        /// <returns>List</returns>
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

        // get information and save to variables
        // duration
        private void getDuration(string json) {
            json = "[" + json + "]";
            JArray arr = JArray.Parse(json);
            this.duration = arr[0]["video"]["duration"].ToString();
        }
        // creation date (timestamp format)
        private void getCreationDate(string json) {
            json = "[" + json + "]";
            JArray arr = JArray.Parse(json);
            this.creationDate = arr[0]["request"]["timestamp"].ToString();
        }
        // url thumbnail
        private void getUrlThumb(string json) {
            json = "[" + json + "]";
            JArray arr = JArray.Parse(json);
            this.urlThumbVideo = arr[0]["video"]["thumbs"]["base"].ToString();
        }

        // list video w/metadata
        private void getListVideo(string json) {
            log("Get metadata");
            // pre-formatting json
            json = "[" + json + "]";
            // parsing json
            JArray jobj = JArray.Parse(json);
            // get only progressive json node
            string progr = jobj[0]["request"]["files"]["progressive"].ToString();
            // parsing json node
            JArray jprog = JArray.Parse(progr);
            this.listVideos.Clear();
            // getting nodes and add to list (dictionary)
            foreach (JObject itm in jprog.Children<JObject>()) {
                log("Get metadata: adding link to list");
                // add metadata to list
                this.listVideos.Add(Int32.Parse(itm["height"].ToString()),
                    new Dictionary<string, string> {
                            {"url", itm["url"].ToString() },
                            {"content-type", itm["mime"].ToString() },
                            {"size", this.getVideoSize(itm["url"].ToString()) }
                        }
                    );
            }
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
                Thread.Sleep(500);
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

                wReq.GetResponse().Close();
            } catch (Exception e) {
                length = "0";
            }
            return length;
        }

        /// <summary>
        /// get only one video metadata from resolution (key)
        /// </summary>
        /// <param name="res">int: video resolution (key)</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, string> urlVideoFromResolution(int res) {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try {
                dict = listVideos[res];
            } catch (Exception ex) {
                return null;
            }
            return dict;
        }

        /// <summary>
        /// Get only list of quality
        /// </summary>
        /// <param name="dict">Dictionary with list of videos</param>
        /// <returns>List</returns>
        private List<int> getListQuality(Dictionary<int, string> dict) {
            List<int> list = new List<int>();
            try {
                list = dict.Keys.ToList();
            } catch (Exception ex) {
                list = null;
            }
            return list;
        }


        /// <summary>
        /// Send messages to console (only debug)
        /// </summary>
        /// <param name="text">Text to console</param>
        private void log(string text) {
            string ts = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Console.WriteLine("[" + ts + "] (VmVideo.cs) - " + text);

            // send text to label object
            if (Label != null) {
                Label("[vm] " + text);
            }
            // allow form events (show text on label)
            Application.DoEvents();
        }
    }
}
