using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net;
using System.IO;
using HtmlAgilityPack;
namespace task2.VK
{
    class VK_User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string __id;
        private string __status;
        public string ID
        {
            get { return __id; }
            set
            {
                __id = value;
                OnPropertyChanged("ID");
            }
        }
        public string Status
        {
            get
            {
                return __status;
            }
            set
            {
                __status = value;
                OnPropertyChanged("Status");
            }
        }


        public VK_User(string id)
        {
            this.__id = id;
            try
            {

                var pageContent = LoadPage(this.__id);
                var document = new HtmlDocument();
                document.LoadHtml(pageContent);
                HtmlNodeCollection links1 = document.DocumentNode.SelectNodes(@"//*[@id='mcont']/div/div/div/div[2]/div");
                HtmlNodeCollection links = document.DocumentNode.SelectNodes(@"//*[@id='mcont']/div/div/div[1]/div/div[1]/div/span");
                if (links is null || links[0].InnerText == "")
                {
                    __status = links1[0].InnerText;
                }
                else
                {
                    __status = links[0].InnerText;
                }
            }
            catch (Exception)
            {
                __status = "Wrong URL";
            }
            if (__status == "" || __status is null || __status.Length > 50)
            {
                __status = "Happend something wrong";
            }
        }
        private static string LoadPage(string url)
        {
            var result = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var receiveStream = response.GetResponseStream();
                if (receiveStream != null)
                {
                    StreamReader readStream;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    result = readStream.ReadToEnd();
                    readStream.Close();
                }
                response.Close();
            }
            return result;
        }
    }
}