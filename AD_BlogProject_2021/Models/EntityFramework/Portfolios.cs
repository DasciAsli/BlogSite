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
    
    public partial class Portfolios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Portfolios()
        {
            this.Tags = new HashSet<Tags>();
        }
    
        public int PortfolioId { get; set; }
        public string ImageUrl { get; set; }
        public string ProjeName { get; set; }
        public string ProjeUrl { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime RegisterDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tags> Tags { get; set; }
    }
}
