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

        public static void ShareFiles(UserInfo userInfo)
        {
            
        }

        public static void AddFile(string[] selectedFilePaths)
        {
            string userFilePath = App.sub + @"/" + App.given_name;

            try
            {
                HttpClient httpClient = new HttpClient();

                foreach (string selectedFilePath in selectedFilePaths)
                {
                    var fileStream = System.IO.File.Open(selectedFilePath, FileMode.Open);
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

        public static async Task<string[]> GetFileList()
        {
            string apiUri = BaseUri + "/" + App.sub + "/" + App.given_name;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    HttpResponseMessage response = await client.GetAsync("api/get-files/" + App.sub + "/" + App.given_name);
                    string[] paths = null;
                    if (response.IsSuccessStatusCode)
                    {
                        paths = await response.Content.ReadAsAsync<string[]>();
                    }

                    return paths;
                }
            }
            catch
            {
            }

            return null;
        }
    }
}