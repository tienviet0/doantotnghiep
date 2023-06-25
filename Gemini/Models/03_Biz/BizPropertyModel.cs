using System;
using System.Web;
using System.Net;
using Gemini.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gemini.Models._02_Cms.U;

namespace Gemini.Models._03_Biz
{
    public class BizPropertyModel
    {
        public int IsUpdate { get; set; }

        #region Properties
        [ScaffoldColumn(false)]
        public Guid Guid { get; set; }

        public bool Active { get; set; }

        public int Type { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ErrorMaxLength255", ErrorMessageResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public String Address { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public decimal Area { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public int Beds { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public int Baths { get; set; }

        public int? Garage { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public int Status { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public int PropertyType { get; set; }

        public String Description { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ErrorMaxLength255", ErrorMessageResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public String Long { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ErrorMaxLength255", ErrorMessageResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public String Lat { get; set; }

        public String FloorPlans { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public decimal Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public int Sort { get; set; }

        public int ViewCount { get; set; }

        [Editable(false)]
        public DateTime? CreatedAt { get; set; }

        [Editable(false)]
        public Guid CreatedBy { get; set; }

        [Editable(false)]
        public DateTime? UpdatedAt { get; set; }

        [Editable(false)]
        public Guid UpdatedBy { get; set; }
        #endregion

        public List<UGalleryModel> ListGallery { get; set; }

        public List<BizPropertyAmenity> ListAmenity { get; set; }

        public String[] AmenityValues { get; set; }

        public string StatusName { get; set; }

        public string TypeName { get; set; }

        public string PropertyTypeName { get; set; }

        public string CreatedByUserName { get; set; }

        public string UpdatedByUserName { get; set; }

        public string LinkImg0 { get; set; }

        #region Constructor
        public BizPropertyModel()
        {

        }

        public BizPropertyModel(BizProperty item)
        {
            Guid = item.Guid;
            Active = item.Active;
            Type = item.Type;
            Address = item.Address;
            Area = item.Area;
            Beds = item.Beds;
            Baths = item.Baths;
            Garage = item.Garage;
            Status = item.Status;
            PropertyType = item.PropertyType;
            Description = HttpUtility.HtmlDecode(item.Description);
            Long = item.Long;
            Lat = item.Lat;
            FloorPlans = HttpUtility.UrlDecode(item.FloorPlans);
            Price = item.Price;
            Sort = item.Sort;
            ViewCount = item.ViewCount;
            CreatedAt = item.CreatedAt;
            CreatedBy = item.CreatedBy;
            UpdatedAt = item.UpdatedAt;
            UpdatedBy = item.UpdatedBy;
        }
        #endregion

        #region Function
        public void Setvalue(BizProperty item, Guid? guid = null)
        {
            if (IsUpdate == 0)
            {
                if (guid == null || guid == Guid.Empty)
                {
                    item.Guid = Guid.NewGuid();
                }
                else
                {
                    item.Guid = guid.Value;
                }
                item.CreatedBy = CreatedBy;
                item.CreatedAt = DateTime.Now;
            }
            item.Active = Active;
            item.Type = Type;
            item.Address = Address;
            item.Area = Area;
            item.Beds = Beds;
            item.Baths = Baths;
            item.Garage = Garage;
            item.Status = Status;
            item.PropertyType = PropertyType;
            item.Description = Description;
            item.Long = Long;
            item.Lat = Lat;
            item.FloorPlans = !string.IsNullOrWhiteSpace(FloorPlans) ? WebUtility.HtmlDecode(FloorPlans).Replace("%2F", "/").Replace("%20", " ") : null;
            item.Price = Price;
            item.Sort = Sort;
            item.ViewCount = ViewCount;
            item.UpdatedAt = DateTime.Now;
            item.UpdatedBy = UpdatedBy;
        }
        #endregion
    }
}