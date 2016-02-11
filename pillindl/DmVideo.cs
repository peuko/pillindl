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
//using System.Web;

namespace pillindl {
    class DmVideo {
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
        public string ua = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:45.0) Gecko/20100101 Firefox/45.0";

        // private
        private string htmlData;
        private string json;

        // for send messages to window form controls
        public delegate void StatusDelegate(string text);
        public event StatusDelegate Label;

        // constructor
        public DmVideo() {
            this.duration = null;
            this.urlThumbVideo = null;
            this.creationDate = null;
            this.listVideos.Clear();
            this.videoTitle = null;

            this.htmlData = null;
            this.json = null;
        }

        public bool getVideoInfo(string url = "") {
            log("DailymotionDL init");
            string regex1 = @"buildPlayer\((.*)?\)";

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

            // get video metadata
            getListVideo(this.json);

            // create quaility list
            this.listQuality = this.listVideos.Keys.ToList();
            this.listQuality.Sort();

            log("Get metadata");
            // get title
            this.videoTitle = this.getVideoTitle(this.htmlData);
            if (this.videoTitle == null) {
                return false;
            }
            log("-> Video Title: " + this.videoTitle);

            // get duration
            getDuration(this.json);

            // get thumbnail
            getUrlThumb(this.json);

            //get creation date
            getCreationDate(this.json);

            log("Metadata OK");
            return true;
        }

        /// <summary>
        /// Get Title of Dailymotion video
        /// </summary>
        /// <param name="html">HTML data</param>
        /// <returns>(string) Title</returns>
        private string getVideoTitle(string html) {
            string rgx = "<span[\\n\\r].*?id=\\\"video_title\\\"[\\n\\r].*title=\\\"(.+)?\\\""; // regex
            string title = string.Empty;
            try {
                title = getDataByRegex(html, rgx, true)[1];
            } catch (Exception ex) {
                return null;
            }
            return title;
        }

        /// <summary>
        /// Get list of videos and metadata
        /// </summary>
        /// <param name="json">(string) JSON data</param>
        private void getListVideo(string json) {
            log("Get metadata");

            JObject jobj = JObject.Parse(json);
            // get only progressive json node
            string progr = jobj["metadata"]["qualities"].ToString(Formatting.None);
            // parsing json node
            progr = "[" + progr + "]";
            JArray jprog = JArray.Parse(progr);
            this.listVideos.Clear();
            // getting nodes and add to list (dictionary)
            foreach (JObject itm in jprog.Children<JObject>()) {
                foreach (JProperty pr in itm.Properties()) {
                    log("Get metadata: adding link to list");
                    if (isNum(pr.Name)) {
                        this.listVideos.Add(Int32.Parse(pr.Name),
                            new Dictionary<string, string> {
                                {"url", pr.Value[0]["url"].ToString() }
                                , {"content-type", pr.Value[0]["type"].ToString() }
                                , {"size", this.getVideoSize(pr.Value[0]["url"].ToString()) }
                            }
                        );
                    }
                }
            }
        }

        /**********************************************
         * GENERIC METHODS
         * Work for all cases. Only copy-paste to other class.
         */

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
                log("Connect to Dailymotion server...");
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
                log("HTML request failure");
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
        /// Get size of video from url.
        /// Using HEAD request.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private string getVideoSize(string uri) {
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

                log("getVideoSize(): length: " + length);
                // close request
                wReq.GetResponse().Close();
            } catch (Exception e) {
                //log("getVideoSize(): EXCEPTION: " + e.Message);
                //log("getVideoSize(): DEBUG: ua: " + this.ua);
                length = "0";
            }

            return length;
        }

        // get information and save to variables
        // duration
        private void getDuration(string json) {
            json = "[" + json + "]";
            JArray arr = JArray.Parse(json);
            this.duration = arr[0]["metadata"]["duration"].ToString();
        }
        // creation date (timestamp format)
        private void getCreationDate(string json) {
            json = "[" + json + "]";
            JArray arr = JArray.Parse(json);
            this.creationDate = arr[0]["metadata"]["created_time"].ToString();
        }
        // url thumbnail
        private void getUrlThumb(string json) {
            json = "[" + json + "]";
            JArray arr = JArray.Parse(json);
            this.urlThumbVideo = arr[0]["metadata"]["poster_url"].ToString();
        }


        /// <summary>
        /// Send messages to console (only debug)
        /// </summary>
        /// <param name="text">Text to console</param>
        private void log(string text) {
            string ts = DateTime.UtcNow.ToString("yyyyMMdd HHmmss.ffff");
            Console.WriteLine("[" + ts + "] (DmVideo.cs) - " + text);

            // send text to label object
            if (Label != null) {
                Label("[dm] " + text);
            }
            // allow form events (show text on label)
            Application.DoEvents();
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
    }
}
