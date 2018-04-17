using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using RestSharp;

namespace UWP.Classes
{
    static class Util
    {
        private static string BaseUri = "http://localhost:57769/";

        public static void PostUser(UserInfo userInfo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var response = client.PostAsJsonAsync("api/add-user", userInfo).Result;
                    if (response.IsSuccessStatusCode)
                    {
                    }
                }
            }
            catch
            {
            }
        }

        public static void AddFile(string[] selectedFilePaths)
        {
            string userFilePath = App.sub + @"/" + App.given_name;

            try
            {
                HttpClient httpClient = new HttpClient();

                foreach (string selectedFilePath in selectedFilePaths)
                {
                    var fileStream = File.Open(selectedFilePath, FileMode.Open);
                    var fileInfo = new FileInfo(selectedFilePath);

                    var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(fileStream), "\"file\"",
                        string.Format("\"{0}\"", fileInfo.Name + "~" + userFilePath)
                    );

                    Task taskUpload = httpClient.PostAsync(BaseUri + "api/uploadfile", content).ContinueWith(task =>
                    {
                        if (task.Status == TaskStatus.RanToCompletion)
                        {
                            var response = task.Result;

                            if (response.IsSuccessStatusCode)
                            {
                                foreach (var header in response.Content.Headers)
                                {
                                    Debug.WriteLine("{0}: {1}", header.Key, string.Join(",", header.Value));
                                }
                            }
                            else
                            {
                                Debug.WriteLine("Status Code: {0} - {1}", response.StatusCode, response.ReasonPhrase);
                                Debug.WriteLine("Response Body: {0}", response.Content.ReadAsStringAsync().Result);
                            }
                        }

                        fileStream.Dispose();
                    });

                    taskUpload.Wait();
                }

                httpClient.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
    }
}