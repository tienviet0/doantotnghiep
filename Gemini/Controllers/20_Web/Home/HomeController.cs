using System.Linq;
using System.Web.Mvc;
using Gemini.Models._20_Web;
using Gemini.Models._03_Biz;
using Gemini.Models._02_Cms.U;
using Gemini.Controllers.Bussiness;

namespace Gemini.Controllers._20_Web.Home
{
    public class HomeController : GeminiController
    {
        [Route("~/")]
        public ActionResult Index()
        {
            var model = new HomeModel();

            model.NewestProperties = (from bp in DataGemini.BizProperties
                                      join su in DataGemini.SUsers on bp.UpdatedBy equals su.Guid
                                      where bp.Status != BizProperty_Status.Sold
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
                                          ListGallery = (from fr in DataGemini.FPropertyGalleries
                                                         join im in DataGemini.UGalleries on fr.GuidGallery equals im.Guid
                                                         where fr.GuidProperty == bp.Guid
                                                         select new UGalleryModel
                                                         {
                                                             Image = im.Image,
                                                             CreatedAt = im.CreatedAt
                                                         }).OrderBy(x => x.CreatedAt).Take(1).ToList(),
                                      }).OrderByDescending(s => s.Sort).Take(3).ToList();


            foreach (var item in model.NewestProperties)
            {
                var tmpLinkImg = item.ListGallery;
                if (tmpLinkImg.Count == 0)
                    item.LinkImg0 = "/Content/Custom/empty-album.png";
                else
                    item.LinkImg0 = tmpLinkImg[0].Image;

                item.TypeName = BizProperty_Type.dicDesc[item.Type];
                item.PropertyTypeName = BizProperty_PropertyType.dicDesc[item.PropertyType];
                item.StatusName = BizProperty_Status.dicDesc[item.Status];
            }

            model.MostViewProperties = (from bp in DataGemini.BizProperties
                                        join su in DataGemini.SUsers on bp.UpdatedBy equals su.Guid
                                        where bp.Status != BizProperty_Status.Sold
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
                                            ListGallery = (from fr in DataGemini.FPropertyGalleries
                                                           join im in DataGemini.UGalleries on fr.GuidGallery equals im.Guid
                                                           where fr.GuidProperty == bp.Guid
                                                           select new UGalleryModel
                                                           {
                                                               Image = im.Image,
                                                               CreatedAt = im.CreatedAt
                                                           }).OrderBy(x => x.CreatedAt).Take(1).ToList(),
                                        }).OrderByDescending(s => s.ViewCount).Take(3).ToList();

            foreach (var item in model.MostViewProperties)
            {
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
    }
}