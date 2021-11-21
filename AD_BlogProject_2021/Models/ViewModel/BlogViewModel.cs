using AD_BlogProject_2021.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AD_BlogProject_2021.Models.ViewModel
{
    public class BlogViewModel
    {
        public List<Blogs> Blogs { get; set; }
        public List<Tags> Tags { get; set; }
    }
}