using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Usebackpack.Common
{
    /// <summary>
    /// Clas to store the methods of web request
    /// Author: Kumar Abhinav,Pooja Agarwal,Prbhat Mishra,Akanksha Mishra, Nishtha Phutela
    /// Creation Date:16th Oct 2013
    /// Modified Date:16th Oct 2013
    /// </summary>
    public static class HttpExtensions
    {
        private static Task<Stream> GetRequestStreamAsync(this WebRequest request)
        {
            return Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null);
        }

        public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
        {
            var taskComplete = new TaskCompletionSource<HttpWebResponse>();

            request.BeginGetResponse(asyncResponse =>
            {
                try
                {
                    HttpWebRequest responseRequest = (HttpWebRequest)asyncResponse.AsyncState;
                    HttpWebResponse someResponse = (HttpWebResponse)responseRequest.EndGetResponse(asyncResponse);
                    taskComplete.TrySetResult(someResponse);
                }
                catch (WebException webExc)
                {
                    HttpWebResponse failedResponse = (HttpWebResponse)webExc.Response;
                    taskComplete.TrySetResult(failedResponse);
                }
            }, request);

            return taskComplete.Task;
        }

        public async static Task<T> GetValueFromRequest<T>(this HttpWebRequest request, string postData = null)
        {
            T returnValue = default(T);

            if (!string.IsNullOrEmpty(postData))
            {
                byte[] requestBytes = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = requestBytes.Length;

                using (var postStream = await request.GetRequestStreamAsync())
                {
                    await postStream.WriteAsync(requestBytes, 0, requestBytes.Length);
                }
            }
            else
            {
                request.Method = "GET";
            }

            var response = await request.GetResponseAsync();

            if (response != null)
            {
                using (var receiveStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(receiveStream))
                    {
                        var json = await reader.ReadToEndAsync();

                        var serializer = new DataContractJsonSerializer(typeof(T));

                        using (var tempStream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                        {
                            return (T)serializer.ReadObject(tempStream);
                        }
                    }
                }
            }

            return returnValue;
        }
    }
}
