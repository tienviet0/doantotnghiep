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
    
    public partial class SControl
    {
        public SControl()
        {
            this.FControlMenus = new HashSet<FControlMenu>();
        }
    
        public System.Guid Guid { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Note { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string SpriteCssClass { get; set; }
        public string EventClick { get; set; }
        public string Type { get; set; }
        public int Orderby { get; set; }
    
        public virtual ICollection<FControlMenu> FControlMenus { get; set; }
    }
}
