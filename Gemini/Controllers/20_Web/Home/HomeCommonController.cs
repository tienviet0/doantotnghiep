using System.Linq;
using System.Web.Mvc;
using Gemini.Models._03_Biz;
using Gemini.Models._20_Web;
using Gemini.Models._02_Cms.U;
using Gemini.Controllers.Bussiness;
using System;
using Gemini.Models._01_Hethong;

namespace Gemini.Controllers._20_Web.Home
{
    public class HomeCommonController : GeminiController
    {
        [ChildActionOnly]
        public ActionResult Header()
        {
            var user = GetSettingUser();

            if (user != null && user.Guid != Guid.Empty)
                ViewBag.LoginTitle = "Quản trị - " + user.Username;
            else
                ViewBag.LoginTitle = "Đăng nhập";

            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }

        public ActionResult Category(int? type, int? propertyType, int? sortBy, int? page)
        {
            ViewBag.SortBy = sortBy.GetValueOrDefault(0);
            ViewBag.Page = page.GetValueOrDefault(0);

            ViewBag.TypeName = "Danh mục";
            ViewBag.PropertyTypeName = "Dự án";

            if (type != null)
                ViewBag.TypeName = BizProperty_Type.dicDesc[type.Value];

            if (propertyType != null)
                ViewBag.PropertyTypeName = BizProperty_PropertyType.dicDesc[propertyType.Value];

            var model = new CategoryModel();

            var properties = from bp in DataGemini.BizProperties
                             join su in DataGemini.SUsers on bp.UpdatedBy equals su.Guid
                             where bp.Status != BizProperty_Status.Sold
                                && (bp.Type == type.Value || type == null)
                                && (bp.PropertyType == propertyType.Value || propertyType == null)
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
                                 Sort = bp.Sort,
                                 ViewCount = bp.ViewCount,

                                 UpdatedByUserName = su.Username,
                             };

            ViewBag.TotalItem = properties.Count();

            switch (sortBy)
            {
                case BizProperty_SortBy.NewToOld:
                    properties = properties.OrderByDescending(x => x.CreatedAt);
                    break;
                case BizProperty_SortBy.OldToNew:
                    properties = properties.OrderBy(x => x.CreatedAt);
                    break;
                case BizProperty_SortBy.PriceLowToHigh:
                    properties = properties.OrderBy(x => x.Price);
                    break;
                case BizProperty_SortBy.PriceHighToLow:
                    properties = properties.OrderByDescending(x => x.Price);
                    break;
                case BizProperty_SortBy.MostView:
                    properties = properties.OrderByDescending(x => x.ViewCount);
                    break;
                default:
                    properties = properties.OrderByDescending(x => x.CreatedAt);
                    break;
            }

            model.Properties = properties.Skip(page.GetValueOrDefault(1) - 1).Take(6).ToList();

            foreach (var item in model.Properties)
            {
                item.ListGallery = (from fr in DataGemini.FPropertyGalleries
                                    join im in DataGemini.UGalleries on fr.GuidGallery equals im.Guid
                                    where fr.GuidProperty == item.Guid
                                    select new UGalleryModel
                                    {
                                        Image = im.Image,
                                        CreatedAt = im.CreatedAt
                                    }).OrderBy(x => x.CreatedAt).Take(1).ToList();

                var tmpLinkImg = item.ListGallery;
                if (tmpLinkImg.Count == 0)
                    item.LinkImg0 = "/Content/Custom/empty-album.png";
                else
                    item.LinkImg0 = tmpLinkImg[0].Image;

                item.TypeName = BizProperty_Type.dicDesc[item.Type];
                item.PropertyTypeName = BizProperty_PropertyType.dicDesc[item.PropertyType];
                item.StatusName = BizProperty_Status.dicDesc[item.Status];
            }

            return View(model);
        }

        public ActionResult Search(string address, int? type, int? propertyType, int? beds, int? baths, int? garages, decimal? area, decimal? price, int? sortBy, int? page)
        {
            ViewBag.SortBy = sortBy.GetValueOrDefault(0);
            ViewBag.Page = page.GetValueOrDefault(0);

            ViewBag.TypeName = "Tìm kiếm";
            ViewBag.PropertyTypeName = "Dự án";

            var model = new SearchModel();

            var properties = from bp in DataGemini.BizProperties
                             join su in DataGemini.SUsers on bp.UpdatedBy equals su.Guid
                             where bp.Status != BizProperty_Status.Sold
                                && (address == null || address == "" || bp.Address.Contains(address))
                                && (bp.Type == type.Value || type == null || type == 0)
                                && (bp.PropertyType == propertyType.Value || propertyType == null || propertyType == 0)
                                && (bp.Beds == beds.Value || beds == null || beds == 0)
                                && (bp.Baths == baths.Value || baths == null || baths == 0)
                                && (bp.Garage == garages.Value || garages == null || garages == 0)
                                && (bp.Area >= area.Value || area == null || area == 0)
                                && (bp.Price >= price.Value || price == null || price == 0)
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
                                 Sort = bp.Sort,
                                 ViewCount = bp.ViewCount,

                                 UpdatedByUserName = su.Username,
                             };

            ViewBag.TotalItem = properties.Count();

            switch (sortBy)
            {
                case BizProperty_SortBy.NewToOld:
                    properties = properties.OrderByDescending(x => x.CreatedAt);
                    break;
                case BizProperty_SortBy.OldToNew:
                    properties = properties.OrderBy(x => x.CreatedAt);
                    break;
                case BizProperty_SortBy.PriceLowToHigh:
                    properties = properties.OrderBy(x => x.Price);
                    break;
                case BizProperty_SortBy.PriceHighToLow:
                    properties = properties.OrderByDescending(x => x.Price);
                    break;
                case BizProperty_SortBy.MostView:
                    properties = properties.OrderByDescending(x => x.ViewCount);
                    break;
                default:
                    properties = properties.OrderByDescending(x => x.CreatedAt);
                    break;
            }

            model.Properties = properties.Skip(page.GetValueOrDefault(1) - 1).Take(6).ToList();

            foreach (var item in model.Properties)
            {
                item.ListGallery = (from fr in DataGemini.FPropertyGalleries
                                    join im in DataGemini.UGalleries on fr.GuidGallery equals im.Guid
                                    where fr.GuidProperty == item.Guid
                                    select new UGalleryModel
                                    {
                                        Image = im.Image,
                                        CreatedAt = im.CreatedAt
                                    }).OrderBy(x => x.CreatedAt).Take(1).ToList();

                var tmpLinkImg = item.ListGallery;
                if (tmpLinkImg.Count == 0)
                    item.LinkImg0 = "/Content/Custom/empty-album.png";
                else
                    item.LinkImg0 = tmpLinkImg[0].Image;

                item.TypeName = BizProperty_Type.dicDesc[item.Type];
                item.PropertyTypeName = BizProperty_PropertyType.dicDesc[item.PropertyType];
                item.StatusName = BizProperty_Status.dicDesc[item.Status];
            }

            return View(model);
        }

        public ActionResult Property(Guid guid)
        {
            var model = new PropertyModel();
            model.SendEmailModel = new PropertySendEmailModel();

            try
            {
                var propertyInDB = DataGemini.BizProperties.FirstOrDefault(x => x.Guid == guid);
                propertyInDB.ViewCount++;

                DataGemini.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            var property = (from bp in DataGemini.BizProperties
                            join su in DataGemini.SUsers on bp.UpdatedBy equals su.Guid
                            where bp.Status != BizProperty_Status.Sold
                               && bp.Guid == guid
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
                                Sort = bp.Sort,
                                ViewCount = bp.ViewCount,

                                UpdatedByUserName = su.Username,
                            }).FirstOrDefault();

            property.ListGallery = (from fr in DataGemini.FPropertyGalleries
                                    join im in DataGemini.UGalleries on fr.GuidGallery equals im.Guid
                                    where fr.GuidProperty == guid
                                    select new UGalleryModel
                                    {
                                        Image = im.Image,
                                        CreatedAt = im.CreatedAt
                                    }).OrderBy(x => x.CreatedAt).ToList();

            property.ListAmenity = DataGemini.BizPropertyAmenities.Where(x => x.GuidProperty == guid).ToList();

            property.TypeName = BizProperty_Type.dicDesc[property.Type];
            property.PropertyTypeName = BizProperty_PropertyType.dicDesc[property.PropertyType];
            property.StatusName = BizProperty_Status.dicDesc[property.Status];

            model.Property = property;
            model.User = new SUserModel(DataGemini.SUsers.FirstOrDefault(x => x.Guid == property.CreatedBy));

            return View(model);
        }

        public ActionResult PropertySendEmail(PropertyModel model)
        {
            SendEmails(CrmEmailTemplate_Code.AgentAdvise, model);

            if (model.SendEmailModel != null)
                return Redirect(Request.UrlReferrer.ToString());
            else
                return RedirectToAction("Error");
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}