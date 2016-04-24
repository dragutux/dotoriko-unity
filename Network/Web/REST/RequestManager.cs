using System;
using System.Net;
using System.IO;
using System.Collections;
using UnityEngine;

using DotOriko.Core;

namespace DotOriko.Network.Web.REST {
    public class RequestManager : DotOrikoSingleton<RequestManager> {

        private WebAsync asyncRequest = new WebAsync();

        public void GET(string url, Action<int, string> callback) {
            var request = WebRequest.Create(url);
            request.Method = "GET";

            this.StartCoroutine(this.MakeRequest(request, callback));
        }

        public void POST(string url) {
            var request = WebRequest.Create(url);
            request.Method = "POST";
            throw new NotImplementedException();
        }

        private IEnumerator MakeRequest(WebRequest request, Action<int, string> callback) {
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
                    
                    // send success response
                    callback(0, str);
                } catch (Exception e) {
                    // send error response
                    callback(1, e.Message);
                }
            }
        }
    }
}
