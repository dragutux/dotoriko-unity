using System;
using System.Net;
using System.IO;
using System.Collections;
using UnityEngine;

namespace DotOriko.Network.Rest {
    public class RequestManager : DotOrikoComponent {

        private WebAsync asyncRequest;

        public Action<string> OnRequestSuccess;
        public Action<string> OnRequestFailed;

        public static RequestManager Init() {
            var obj = new GameObject();
            obj.name = "[DotOriko] Request Manager";
            var rm = obj.AddComponent<RequestManager>();
            return rm;
        }

        public void GET(string url) {
            var request = WebRequest.Create(url);
            request.Method = "GET";

            this.StartCoroutine(this.MakeRequest(request));
        }

        public void POST(string url) {
            var request = WebRequest.Create(url);
            request.Method = "POST";
            throw new NotImplementedException();
        }

        private IEnumerator MakeRequest(WebRequest request) {
            var en = asyncRequest.GetResponse(request);
            while (en.MoveNext()) { yield return en.Current; }

            var resp = asyncRequest.requestState.webResponse;
            using (var stream = new MemoryStream()) {
                byte[] buffer = new byte[2048];
                int bytesRead;
                try {
                    while ((bytesRead = resp.GetResponseStream()
                        .Read(buffer, 0, buffer.Length)) > 0) 
                        stream.Write(buffer, 0, bytesRead);

                    byte[] result = stream.ToArray();
                    var str = System.Text.Encoding.UTF8.GetString(result);
                    if (this.OnRequestSuccess != null) this.OnRequestSuccess(str);
                } catch (Exception e) {
                    if(this.OnRequestFailed != null) this.OnRequestFailed(e.Message);
                }
            }
        }
    }
}
