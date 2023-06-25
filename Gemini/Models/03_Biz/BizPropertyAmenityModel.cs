using System;
using Gemini.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gemini.Models._03_Biz
{
    public class BizPropertyAmenityModel
    {
        public int IsUpdate { get; set; }

        #region Properties
        [ScaffoldColumn(false)]
        public Guid Guid { get; set; }

        public Guid GuidProperty { get; set; }

        [StringLength(255, ErrorMessageResourceName = "ErrorMaxLength255", ErrorMessageResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredFill")]
        public String Name { get; set; }
        #endregion

        #region Constructor
        public BizPropertyAmenityModel()
        {

        }

        public BizPropertyAmenityModel(BizPropertyAmenity item)
        {
            Guid = item.Guid;
            GuidProperty = item.GuidProperty;
            Name = item.Name;
        }
        #endregion

        #region Function
        public void Setvalue(BizPropertyAmenity item)
        {
            if (IsUpdate == 0)
            {
                item.Guid = Guid.NewGuid();
            }
            item.GuidProperty = GuidProperty;
            item.Name = Name;
        }
        #endregion
    }
}