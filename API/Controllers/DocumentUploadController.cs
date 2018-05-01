using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Description;
using API.Classes;
using API.Models;

namespace API.Controllers
{
    public class DocumentUploadController : ApiController
    {
        [HttpPost]
        [Route("api/uploadfile")]
        public async Task<HttpResponseMessage> MediaUpload()
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
            NameValueCollection formData = provider.FormData;
            IList<HttpContent> files = provider.Files;

            HttpContent file1 = files[0];
            var thisFileName = file1.Headers.ContentDisposition.FileName.Trim('\"');

            var pathAndName = thisFileName.Split('~');
            thisFileName = pathAndName[0];
            var filePath = pathAndName[1];

            Stream input = await file1.ReadAsStreamAsync();
            string tempDocUrl = WebConfigurationManager.AppSettings["DocsUrl"];
            
            var path = HttpRuntime.AppDomainAppPath;
            var directoryName = System.IO.Path.Combine(path, "UsersData");

            var changedPath = filePath.Replace('/', '\\');

            var filename = System.IO.Path.Combine(directoryName, changedPath + "\\" + thisFileName);
            
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            string DocsPath = tempDocUrl + "/" + "UsersData" + "/" + filePath + "/";
            var URL = DocsPath + thisFileName;

            using (Stream file = File.OpenWrite(filename))
            {
                input.CopyTo(file);
                file.Close();
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("DocsUrl", URL);
            return response;
        }

        [HttpPost]
        [Route("api/delete-file")]
        public IHttpActionResult DeleteFile([FromBody] string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Ok("OK");
            }
            else if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath, true);
                return Ok("OK");
            }
            return Ok("NO_FILE");
        }

        [HttpPost]
        [Route("api/share-files")]
        public IHttpActionResult ShareFiles([FromBody] string data)
        {
            var paths = data.Split('~');
            paths[1] = @"C:\Users\Mykolas\Documents\Repos\Drobox\API\UsersData\" + paths[1];
            var fileName = paths[0].Split('\\').Last();
            File.Copy(Path.Combine(paths[0]), Path.Combine(paths[1] + "\\" + fileName), true);
            return Ok();
        }

        [HttpPost]
        [Route("api/copy-dir")]
        public IHttpActionResult CopyDir([FromBody] string data)
        {
            var paths = data.Split('~');
            var folderName = paths[0].Split('\\').Last();
            try
            {
                
                paths[1] = @"C:\Users\Mykolas\Documents\Repos\Drobox\API\UsersData\" + paths[1];
                
                Directory.CreateDirectory(paths[1] + folderName);
                var filesInDir = Directory.GetFiles(paths[0], "*.*", SearchOption.AllDirectories);
                foreach (string file in filesInDir)
                {
                    var name = file.Split('\\').Last();
                    File.Copy(Path.Combine(file), Path.Combine(paths[1] + "\\" + folderName + "\\" + name), true);
                }
            }
            catch
            {
                Directory.Delete(paths[1] + folderName, true);
            }

            return Ok();
        }

        [HttpPost]
        [Route("api/get-file-path")]
        public IHttpActionResult GetFilePath([FromBody] string userPath)
        {
            string path = @"C:\Users\Mykolas\Documents\Repos\Drobox\API\UsersData\" + userPath;
            var files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            var dirs = Directory.GetDirectories(path);
            var full = files.Concat(dirs).ToArray();
            return Ok(full);
        }

        [HttpPost]
        [Route("api/get-file")]
        public HttpResponseMessage GetFiles([FromBody] string userPath)
        {
            string path = @"C:\Users\Mykolas\Documents\Repos\Drobox\API\UsersData\" + userPath;
            //path = @"C:\Users\Mykolas\Documents\Repos\Drobox\API\UsersData\100570058492936480965\MrM\xxx.pdf";


            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        [HttpPost]
        [Route("api/create-folder")]
        public IHttpActionResult CreateFolder([FromBody] string userPath)
        {
            string path = @"C:\Users\Mykolas\Documents\Repos\Drobox\API\UsersData\" + userPath;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Ok();
        }
    }
}
