using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            //if (formData["ClientDocs"] == "ClientDocs")
            //{
            
            
            var path = HttpRuntime.AppDomainAppPath;
            var directoryName = System.IO.Path.Combine(path, "UsersData");

            var idAndName = filePath.Split('/');
            var filename = System.IO.Path.Combine(directoryName, idAndName[0] + "\\" + idAndName[1] + "\\" + thisFileName);
            
            //userFilePath = System.IO.Path.Combine()
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            string DocsPath = tempDocUrl + "/" + "UsersData" + "/" + filePath + "/";
            var URL = DocsPath + thisFileName;

            // }

            using (Stream file = File.OpenWrite(filename))
            {
                input.CopyTo(file);
                file.Close();
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("DocsUrl", URL);
            return response;
        }

        [HttpDelete]
        [Route("api/deletefile")]
        public IHttpActionResult DeleteFile([FromBody] string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Ok("OK");
            }
            return Ok("NO_FILE");
        }

        [HttpPost]
        [Route("api/sharefiles")]
        public IHttpActionResult ShareFiles(SharedFilesData data)
        {
            foreach (var file in data.Files)
            {
                if (!File.Exists(file))
                {
                    return Ok("FATAL_ERROR");
                }
            }
            
            ManageFiles.CreateUserFolderGuid(data.sub, data.Files);

            return Ok("OK");
        }
    }
}
