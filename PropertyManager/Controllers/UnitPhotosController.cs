
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PropertyManager.Models;
using AutoMapper;

namespace PropertyManager.Controllers
{
    public class UnitPhotosController : ApiController
    {
        private Manager m = new Manager();

        // GET: api/UnitPhotos
        [System.Web.Http.Route("api/unitphotos/getall")]
        public IHttpActionResult Get()
        {
            return Ok(m.UnitPhotoGetAll());
        }

        // GET: api/UnitPhotos/5
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue) { return NotFound(); }
   
            var o = m.UnitPhotoGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }    

        // ADD IMAGE IN PROJECT FOLDER AND ASSOCIATE IT WITH A UNIT
        // POST: api/UnitPhotos

        [System.Web.Http.Route("addUnitPhoto")]
     
        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var uploadFolder = "~/Album/FileUploads";
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);

            var provider = new CustomMultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var originalFileName = GetDeserializedFileName(result.FileData.First());

            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            var fileUploadObj = GetFormData<UnitPhotoBase>(result);
            var unitphoto = Mapper.Map<UnitPhotoAdd>(fileUploadObj);
            unitphoto.PathName = "/Album/FileUploads/" + originalFileName;
            m.UnitPhotoAdd(unitphoto);

            var returnData = unitphoto;
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        // GET NAME AND PATH OF THE FILE
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/Album/FileUploads"; 
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // GET DATA FROM JSON OBJECT SENT WITH PICTURE
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
            }

            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

        // EDIT IMAGE
        // FIND IMAGE TO BE EDITED, DELETE THIS IMAGE FROM FOLDER
        // ADD NEW IMAGE AND UPDATE DATA SENT (ASSOCIATED UNIT AND IMAGE PATH)
        // PUT: api/UnitPhotos/5
        [System.Web.Http.Route("editUnitPhoto")]
        public async Task<HttpResponseMessage> PostEdit()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }          

            var uploadFolder = "~/Album/FileUploads";
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);

            var provider = new CustomMultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var originalFileName = GetDeserializedFileName(result.FileData.First());

            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            var fileUploadObj = GetFormData<UnitPhotoBase>(result);

            var unitphoto = Mapper.Map<UnitPhotoBase>(fileUploadObj);
            
            var unitphotoEdit = m.UnitPhotoGetById(unitphoto.Id);

            var unitphoto2 = Mapper.Map<UnitPhotoAdd>(unitphotoEdit);

            m.UnitPhotoDelete(unitphotoEdit.Id);

            unitphoto2.PathName = "/Album/FileUploads/" + originalFileName;

            m.UnitPhotoAdd(unitphoto2);

            var returnData = unitphoto;
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }
      

        // DELETE: api/UnitPhotos/5
        public void Delete(int id)
        {
            m.UnitPhotoDelete(id);
        }
    }
}
