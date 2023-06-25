using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Gemini.Controllers.Bussiness;
using Gemini.Models._03_Biz;
using Kendo.Mvc.Extensions;

namespace Gemini.Controllers._03_Biz
{
    public class PropertyStatisticController : GeminiController
    {
        [Authorize]
        public ActionResult Index()
        {
            var user = GetSettingUser();

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
                                                 ViewCount = bp.ViewCount,

                                                 UpdatedByUserName = su.Username,
                                             }).OrderByDescending(s => s.Sort).ToList();

            models.ForEach(x =>
            {
                x.TypeName = BizProperty_Type.dicDesc[x.Type];
                x.PropertyTypeName = BizProperty_PropertyType.dicDesc[x.PropertyType];
                x.StatusName = BizProperty_Status.dicDesc[x.Status];
            });

            return View(models);
        }
    }
}