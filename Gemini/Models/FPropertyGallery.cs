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
    
    public partial class FPropertyGallery
    {
        public System.Guid Guid { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public System.Guid GuidProperty { get; set; }
        public System.Guid GuidGallery { get; set; }
    
        public virtual BizProperty BizProperty { get; set; }
        public virtual UGallery UGallery { get; set; }
    }
}
