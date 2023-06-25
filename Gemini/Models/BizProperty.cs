//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gemini.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BizProperty
    {
        public BizProperty()
        {
            this.BizPropertyAmenities = new HashSet<BizPropertyAmenity>();
            this.FPropertyGalleries = new HashSet<FPropertyGallery>();
        }
    
        public System.Guid Guid { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
        public string Address { get; set; }
        public decimal Area { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
        public Nullable<int> Garage { get; set; }
        public int Status { get; set; }
        public int PropertyType { get; set; }
        public string Description { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public string FloorPlans { get; set; }
        public decimal Price { get; set; }
        public int Sort { get; set; }
        public int ViewCount { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public System.Guid UpdatedBy { get; set; }
    
        public virtual SUser SUser { get; set; }
        public virtual ICollection<BizPropertyAmenity> BizPropertyAmenities { get; set; }
        public virtual ICollection<FPropertyGallery> FPropertyGalleries { get; set; }
    }
}