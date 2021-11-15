using AD_BlogProject_2021.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AD_BlogProject_2021.Models.ViewModel
{
    public class AboutViewModel
    {
        public List<Abouts> Abouts { get; set; }
        public List<Skills> Skills { get; set; }
        public List<Tags> Tags { get; set; }
    }
}