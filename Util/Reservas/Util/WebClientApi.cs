using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Util.Reservas.Util
{
    public class WebApiClient
    {
        private HttpClient httpClient;
        public WebApiClient(string serverUrl)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serverUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("X-host", System.Environment.MachineName);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/octet-stream"));
        }
        public T PostJson<T>(HttpContent content, String path)
        {
            T r = default(T);
            Task taskUpload = httpClient.PostAsync(path, content).ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    var response = task.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        r = response.Content.ReadAsAsync<T>().Result;
                        // Read other header values if you want..
                        foreach (var header in response.Content.Headers)
                        {
                            Debug.WriteLine("{0}: {1}", header.Key, string.Join(",", header.Value));
                        }
                    }
                    else
                    {
                        throw new ExternalException(
                            string.Format("Status Code: {0} - {1}; Body: {2}", response.StatusCode, response.ReasonPhrase,
                                response.Content.ReadAsStringAsync().Result));
                    }
                }
                else
                {
                    Debug.Write(string.Format("Erro na conexão: {0}, {1}", task.Exception, task.Exception.InnerException));
                    throw task.Exception;
                    //                    throw new JsonAsdiExternalException(
                    //                        string.Format("Erro na conexão: {0}", task.Exception));
                }
            });
            taskUpload.Wait();
            httpClient.Dispose();
            return r;
        }
        public T GetJson<T>(String path)
        {
            T r = default(T);
            Task taskUpload = httpClient.GetAsync(path).ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    var response = task.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        r = response.Content.ReadAsAsync<T>().Result;
                        // Read other header values if you want..
                        foreach (var header in response.Content.Headers)
                        {
                            Debug.WriteLine("{0}: {1}", header.Key, string.Join(",", header.Value));
                        }
                    }
                    else
                    {
                        throw new ExternalException(
                            string.Format("Status Code: {0} - {1}; Body: {2}", response.StatusCode, response.ReasonPhrase,
                                response.Content.ReadAsStringAsync().Result));
                    }
                }
                else
                {
                    Debug.Write(string.Format("Erro na conexão: {0}, {1}", task.Exception, task.Exception.InnerException));
                    throw task.Exception;
                    //                    throw new JsonAsdiExternalException(
                    //                        string.Format("Erro na conexão: {0}", task.Exception));
                }
            });
            taskUpload.Wait();
            httpClient.Dispose();
            return r;
        }
    }
}
