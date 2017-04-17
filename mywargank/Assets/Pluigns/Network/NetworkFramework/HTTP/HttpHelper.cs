namespace Simon.HTTP
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using LitJson;
    using Protocol;

    public class HttpHelper : MonoBehaviour
    {
        private static HttpHelper _instance;
        private static string guessKey = "yrlwclwh119119";

        void Awake()
        {
            _instance = this;
        }

        public static HttpHelper Instance
        {
            get { return _instance; }
        }

        private string url;

        public string URL
        {
            get { return this.url; }
            set { this.url = value; }
        }

        public void HTTPGet(string interfaceString, Action<string> cb)
        {
            this.DoGet(interfaceString, res =>
            {
                if (res != null)
                {
                    JsonData resJD = JsonMapper.ToObject(res);
                    string resString = resJD["msg"].ToJson();
                    string realRes = AES128Helper.Decrypt(resString, guessKey);
                    cb.Invoke(realRes);
                }
                else
                {
                    cb.Invoke(null);
                }
            });
        }

        public void HTTPPost(string interfaceString, string jsonString, Action<string> cb)
        {
            string msg = AES128Helper.Encrypt(jsonString, guessKey);
            JsonData jd = new JsonData();
            jd["msg"] = msg;

            this.DoPost(interfaceString, jd.ToJson(), res =>
            {
                if (res != null)
                {
                    JsonData resJD = JsonMapper.ToObject(res);
                    string resString = resJD["msg"].ToJson();
                    string realRes = AES128Helper.Decrypt(resString, guessKey);
                    cb.Invoke(realRes);
                }
                else
                {
                    cb.Invoke(null);
                }
            });
        }

        private void DoPost(string interfaceString, string jsonString, Action<string> cb)
        {
            IEnumerator routine = this.Post(this.GetRealURL(interfaceString), jsonString, cb);
            base.StartCoroutine(routine);
        }

        private void DoGet(string interfaceString, Action<string> cb)
        {
            IEnumerator routine = this.Get(this.GetRealURL(interfaceString), cb);
            base.StartCoroutine(routine);
        }

        private IEnumerator Get(string url, Action<string> cb)
        {
            WWW www = new WWW(url);
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                cb.Invoke(www.text);
            }
            else
            {
                cb.Invoke(null);
            }
            www.Dispose();
        }

        private IEnumerator Post(string url, string jsonString, Action<string> cb)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json; charset=utf-8");
            byte[] data = Encoding.UTF8.GetBytes(jsonString);
            WWW www = new WWW(url, data, headers);
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                cb.Invoke(www.text);
            }
            else
            {
                cb.Invoke(null);
            }
            www.Dispose();
        }

        private string GetRealURL(string interfaceString)
        {
            return this.url + "/" + interfaceString;
        }
    }
}