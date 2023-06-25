using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Gemini.Controllers.Bussiness;
using Gemini.Models;
using Gemini.Models._03_Biz;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using static Kendo.Mvc.UI.UIPrimitives;
using Constants = Gemini.Controllers.Bussiness.Constants;

namespace Gemini.Controllers._03_Biz
{
    public class BizPropertyController : GeminiController
    {
        #region Main
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            GetSettingUser();
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var user = GetSettingUser();
            var userControl = Session["MainControls"] as List<string>;

            List<BizPropertyModel> models = (from bp in DataGemini.BizProperties
                                             join su in DataGemini.SUsers on bp.UpdatedBy equals su.Guid
                                             where bp.CreatedBy == user.Guid
                                                    || user.IsAdmin
                                             select new BizPropertyModel
                                             {
                                                 Guid = bp.Guid,
                                                 Active = bp.Active,
                                                 Type = bp.Type,
                                                 Address = bp.Address,
                                                 CreatedAt = bp.CreatedAt,
                                                 CreatedBy = bp.CreatedBy,
                                                 UpdatedAt = bp.UpdatedAt,
                                                 UpdatedBy = bp.UpdatedBy,
                                                 Area = bp.Area,
                                                 Beds = bp.Beds,
                                                 Baths = bp.Baths,
                                                 Garage = bp.Garage,
                                                 Status = bp.Status,
                                                 PropertyType = bp.PropertyType,
                                                 Description = bp.Description,
                                                 Long = bp.Long,
                                                 Lat = bp.Lat,
                                                 FloorPlans = bp.FloorPlans,
                                                 Price = bp.Price,
                                                 Sort = bp.Sort + 1,

                                                 UpdatedByUserName = su.Username,
                                             }).OrderByDescending(s => s.Sort).ToList();

            models.ForEach(x =>
            {
                x.TypeName = BizProperty_Type.dicDesc[x.Type];
                x.PropertyTypeName = BizProperty_PropertyType.dicDesc[x.PropertyType];
                x.StatusName = BizProperty_Status.dicDesc[x.Status];
            });

            DataSourceResult result = models.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult Create()
        {
            var bizProperty = new BizProperty();
            try
            {
                var viewModel = new BizPropertyModel(bizProperty) { Guid = Guid.NewGuid(), IsUpdate = 0, Active = true, AmenityValues = new string[] { } };
                var maxSort = DataGemini.BizProperties.Max(x => x.Sort);
                viewModel.Sort = maxSort + 1;
                return PartialView("Edit", viewModel);
            }
            catch (Exception)
            {
                return Redirect("/Error/ErrorList");
            }
        }

        public ActionResult Edit(Guid guid)
        {
            var user = GetSettingUser();

            var bizProperty = new BizProperty();
            try
            {
                bizProperty = DataGemini.BizProperties.FirstOrDefault(c => c.Guid == guid);
                var viewModel = new BizPropertyModel(bizProperty) { IsUpdate = 1 };
                viewModel.AmenityValues = DataGemini.BizPropertyAmenities.Where(x => x.GuidProperty == guid).Select(x => x.Name).ToArray();
                return PartialView("Edit", viewModel);
            }
            catch (Exception)
            {
                return Redirect("/Error/ErrorList");
            }
        }

        public ActionResult Delete(Guid guid)
        {
            var bizProperty = new BizProperty();
            try
            {
                bizProperty = DataGemini.BizProperties.FirstOrDefault(c => c.Guid == guid);
                DataGemini.BizProperties.Remove(bizProperty);

                if (SaveData("BizProperty") && bizProperty != null)
                {
                    DataReturn.ActiveCode = bizProperty.Guid.ToString();
                    DataReturn.StatusCode = Convert.ToInt16(HttpStatusCode.OK);
                }
                else
                {
                    DataReturn.StatusCode = Convert.ToInt16(HttpStatusCode.BadRequest);
                    DataReturn.MessagError = Constants.CannotDelete + " Date : " + DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return Json(DataReturn, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update(BizPropertyModel viewModel)
        {
            var user = GetSettingUser();
            var bizProperty = new BizProperty();
            try
            {
                viewModel.UpdatedBy = viewModel.CreatedBy = user.Guid;
                if (viewModel.IsUpdate == 0)
                {
                    viewModel.Status = BizProperty_Status.InProgress;
                    viewModel.Setvalue(bizProperty, viewModel.Guid);
                    DataGemini.BizProperties.Add(bizProperty);
                }
                else if (viewModel.IsUpdate == 1)
                {
                    bizProperty = DataGemini.BizProperties.FirstOrDefault(c => c.Guid == viewModel.Guid);
                    viewModel.Setvalue(bizProperty);
                }

                var existsAmentities = DataGemini.BizPropertyAmenities.Where(x => x.GuidProperty == viewModel.Guid).ToList();
                if (existsAmentities != null && existsAmentities.Any())
                {
                    DataGemini.BizPropertyAmenities.RemoveRange(existsAmentities);
                }

                if (viewModel.AmenityValues != null && viewModel.AmenityValues.Length > 0)
                {
                    foreach (var item in viewModel.AmenityValues)
                    {
                        BizPropertyAmenity newItem = new BizPropertyAmenity()
                        {
                            Guid = Guid.NewGuid(),
                            GuidProperty = viewModel.Guid,
                            Name = item
                        };

                        DataGemini.BizPropertyAmenities.Add(newItem);
                    }
                }

                if (SaveData("BizProperty") && bizProperty != null)
                {
                    DataReturn.ActiveCode = bizProperty.Guid.ToString();
                    DataReturn.StatusCode = Convert.ToInt16(HttpStatusCode.OK);
                }
                else
                {
                    DataReturn.StatusCode = Convert.ToInt16(HttpStatusCode.Conflict);
                    DataReturn.MessagError = Constants.CannotUpdate + " Date : " + DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                if (viewModel.IsUpdate == 0)
                {
                    DataGemini.BizProperties.Remove(bizProperty);
                }
                HandleError(ex);
            }

            return Json(DataReturn, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult DeleteFPropertyGallery(string guidProperty, string guidGallery)
        {
            try
            {
                var listGuidGallery = guidGallery.Split(';').ToList();
                var listFRemove = DataGemini.FPropertyGalleries.Where(x => listGuidGallery.Contains(x.GuidGallery.ToString())).ToList();
                DataGemini.FPropertyGalleries.RemoveRange(listFRemove);

                var listGRemove = DataGemini.UGalleries.Where(x => listGuidGallery.Contains(x.Guid.ToString())).ToList();
                DataGemini.UGalleries.RemoveRange(listGRemove);

                DataGemini.SaveChanges();

                DataReturn.ActiveCode = guidProperty;
                DataReturn.StatusCode = Convert.ToInt16(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return Json(DataReturn, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveFPropertyGallery(string guidProperty, string guidGallery)
        {
            try
            {
                var listRemove = DataGemini.FPropertyGalleries.Where(x => x.GuidProperty == new Guid(guidProperty)).ToList();
                DataGemini.FPropertyGalleries.RemoveRange(listRemove);
                var listGuidGallery = guidGallery.Split(';');
                foreach (var item in listGuidGallery)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        FPropertyGallery fPropertyGallery = new FPropertyGallery();
                        fPropertyGallery.Guid = Guid.NewGuid();
                        fPropertyGallery.CreatedAt = DateTime.Now;
                        fPropertyGallery.CreatedBy = GetUserInSession();
                        fPropertyGallery.GuidGallery = new Guid(item);
                        fPropertyGallery.GuidProperty = new Guid(guidProperty);
                        DataGemini.FPropertyGalleries.Add(fPropertyGallery);

                    }
                }
                DataGemini.SaveChanges();

                DataReturn.ActiveCode = guidProperty;
                DataReturn.StatusCode = Convert.ToInt16(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }

            return Json(DataReturn, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(HttpPostedFileBase File_path1, string guidProperty)
        {
            var physicalPath = "";
            var nameFile = Path.GetFileName(File_path1.FileName);
            guidProperty = guidProperty ?? String.Empty;
            if (File_path1 != null)
            {
                string tmp = Server.MapPath("~/Content/UserFiles/Images/Property/" + guidProperty + "/");
                if (System.IO.File.Exists(Path.Combine(tmp, nameFile)))
                {
                    System.IO.File.Delete(Path.Combine(tmp, nameFile));
                }
                physicalPath = Path.Combine(Server.MapPath("~/Content/UserFiles/Images/Property/" + guidProperty + "/"), nameFile);

                VerifyDir(physicalPath);
                WriteFileFromStream(File_path1.InputStream, physicalPath);
                SaveGallery(nameFile, guidProperty);
            }
            return Json(new { physicalPath = "" + physicalPath + "" }, "text/plain");
        }

        public static void VerifyDir(string path)
        {
            try
            {
                var list = path.Split(new string[] { "\\" }, StringSplitOptions.None);
                var directory = path.Replace("\\" + list[list.Count() - 1], "");
                DirectoryInfo dir = new DirectoryInfo(directory);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
            catch { }
        }

        public static void WriteFileFromStream(Stream stream, string toFile)
        {
            using (FileStream fileToSave = new FileStream(toFile, FileMode.Create))
            {
                stream.CopyTo(fileToSave);
            }
        }

        public void SaveGallery(string fileName1, string guidProperty)
        {
            try
            {
                Guid guidGroup = Guid.Empty;
                var group = DataGemini.UGroups.FirstOrDefault(s => s.Name == "BizProperty" && s.Type == "GALLERY_GROUP");
                if (group == null)
                {
                    UGroup posGroup = new UGroup();
                    guidGroup = posGroup.Guid = Guid.NewGuid();
                    posGroup.Name = "BizProperty";
                    posGroup.Active = true;
                    posGroup.CreatedAt = posGroup.UpdatedAt = DateTime.Now;
                    posGroup.CreatedBy = posGroup.UpdatedBy = GetUserInSession();
                    posGroup.Type = "GALLERY_GROUP";
                    DataGemini.UGroups.Add(posGroup);
                }
                else
                {
                    guidGroup = group.Guid;
                }
                UGallery uGallery = new UGallery();
                uGallery.Name = fileName1;
                uGallery.Guid = Guid.NewGuid();
                uGallery.GuidGroup = guidGroup;
                uGallery.Active = true;
                uGallery.CreatedAt = uGallery.UpdatedAt = DateTime.Now;
                uGallery.CreatedBy = uGallery.UpdatedBy = GetUserInSession();
                uGallery.Image = "/Content/UserFiles/Images/Property/" + guidProperty + "/" + fileName1;
                DataGemini.UGalleries.Add(uGallery);

                FPropertyGallery fPropertyGallery = new FPropertyGallery();
                fPropertyGallery.Guid = Guid.NewGuid();
                fPropertyGallery.CreatedAt = DateTime.Now;
                fPropertyGallery.CreatedBy = GetUserInSession();
                fPropertyGallery.GuidGallery = uGallery.Guid;
                fPropertyGallery.GuidProperty = new Guid(guidProperty);
                DataGemini.FPropertyGalleries.Add(fPropertyGallery);

                DataGemini.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        [HttpPost]
        public ActionResult FloorPlansSave([DataSourceRequest] DataSourceRequest request, IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                if (files != null && files.Any())
                {
                    var file = files.FirstOrDefault();
                    if (file.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(Guid.NewGuid() + "_" + file.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content/UserFiles/FloorPlans/"), fileName);
                        file.SaveAs(path);

                        return Json(new { path = "/Content/UserFiles/FloorPlans/" + fileName }, "text/plain");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return null;
        }
    }

    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;

        public MemoryPostedFile(byte[] fileBytes, string fileName)
        {
            this.fileBytes = fileBytes;
            this.FileName = fileName;
            this.InputStream = new MemoryStream(fileBytes);
        }

        public override int ContentLength => fileBytes.Length;

        public override string FileName { get; }

        public override Stream InputStream { get; }
    }
}