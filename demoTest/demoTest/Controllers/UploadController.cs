using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using demoTest.Models;

namespace demoTest.Controllers
{
    public class UploadController : Controller
    {
        WebTestEntities webTestEntities = new WebTestEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user, HttpPostedFileBase fileUpload, List<HttpPostedFileBase> fileUploads)//FileUpload model, 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (fileUpload == null)
            {
                return View();
            }
            if (fileUploads == null) { return View(); }

            var fileName = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/App_Data/TestFiles"), fileName);
            fileUpload.SaveAs(path);

            var fileNames = new List<string>();

            if (fileUploads.Count > 0)
            {
                foreach (var file in fileUploads)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileNameNew = Path.GetFileName(file.FileName);
                        var pathNew = Path.Combine(Server.MapPath("~/App_Data/TestFiles"), fileNameNew);
                        fileUpload.SaveAs(pathNew);

                        fileNames.Add(fileNameNew);
                    }
                }
            }

            user.Image = fileName;
            if (fileNames.Count > 0)
                user.OtherFiles = string.Join(", ", fileNames);
            webTestEntities.Users.Add(user);
            webTestEntities.SaveChanges();
            return View();
        }
    }

    //public class FileUpload
    //{
    //    public HttpPostedFileBase ImageFile { get; set; }
    //    public List<HttpPostedFileBase> ImageFiles { get; set; }
    //}
}