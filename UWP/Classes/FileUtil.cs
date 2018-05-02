using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Windows.Storage;
using RestSharp;

namespace UWP.Classes
{
    static class FileUtil
    {
        private static string BaseUri = "http://localhost:57769/";
        public static string BaseApiPath { get; } = @"C:\Users\Mykolas\Documents\Repos\Drobox\API\UsersData\";

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
                // ignored
            }
        }
        
        public static void DeleteFile(string filePath)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var response = client.PostAsJsonAsync("api/delete-file", filePath).Result;
                    if (response.IsSuccessStatusCode)
                    {
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void CreateFolder(string filePath)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var response = client.PostAsJsonAsync("api/create-folder", filePath).Result;
                    if (response.IsSuccessStatusCode)
                    {
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void ShareFiles(string sourcePath, string destPath)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var full = sourcePath + "~" + destPath;
                    client.BaseAddress = new Uri(BaseUri);
                    var response = client.PostAsJsonAsync("api/share-files", full).Result;
                    if (response.IsSuccessStatusCode)
                    {
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void CopyDir(string sourcePath, string destPath)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var full = sourcePath + "~" + destPath + "~" + App.sub;
                    client.BaseAddress = new Uri(BaseUri);
                    var response = client.PostAsJsonAsync("api/copy-dir", full).Result;
                    if (response.IsSuccessStatusCode)
                    {
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void AddFile(string[] selectedFilePaths, string userPath = "")
        {
            string userFilePath = App.sub;
            if (string.IsNullOrEmpty(userPath))
            {
                userFilePath += "/" + App.given_name;
            }
            else
            {
                userFilePath += "/" + userPath;
                userFilePath = userFilePath.Replace('\\', '/');
            }
            
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
            catch
            {
                // ignored
            }
        }
        
        public static async Task<string[]> GetFileList(string folder)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    if (folder != "")
                    {
                        folder = folder + "\\";
                    }

                    client.BaseAddress = new Uri(BaseUri);
                    var response = client.PostAsJsonAsync("api/get-file-path/", App.sub + "\\" + folder).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var paths = await response.Content.ReadAsAsync<string[]>();
                        return paths;
                    }
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }

        public static async void GetFile(string file)
        {
            try
            {
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var response = client.PostAsJsonAsync("api/get-file", App.sub + "\\" + file).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var fileName = file.Split('\\').Last();

                        var contentBytes = await response.Content.ReadAsByteArrayAsync();
                        Stream stream = new MemoryStream(contentBytes);
                        StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
                        var fileStream = System.IO.File.Create(installedLocation.Path + "\\Files\\" + fileName);
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(fileStream);
                        fileStream.Close();
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void PostImageInfo(ImageInfo info)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var response = client.PostAsJsonAsync("api/add-image-info", info).Result;
                    if (response.IsSuccessStatusCode)
                    {
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public static async Task<ImageInfo> GetImageInfo(string path)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var response = client.PostAsJsonAsync("api/get-image-info", path).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var imageInfo = await response.Content.ReadAsAsync<ImageInfo>();
                        return imageInfo;
                    }
                }

                return null;
            }
            catch
            {
                // ignored
            }

            return null;
        }
    }
}