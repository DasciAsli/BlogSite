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
    using System.ComponentModel.DataAnnotations;

    public partial class UserDetails
    {
        public int UserId { get; set; }
        [DataType(DataType.DateTime)]
        public System.DateTime BirthDay { get; set; }
        public byte Age { get; set; }
        public string Website { get; set; }
        public string City { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
