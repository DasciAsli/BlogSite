//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AD_BlogProject_2021.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Blogs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Blogs()
        {
            this.Comments = new HashSet<Comments>();
            this.Tags = new HashSet<Tags>();
        }
    
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageURL { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime RegisterDate { get; set; }
    
        public virtual BlogDetails BlogDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tags> Tags { get; set; }
    }
}