using ClassManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ClassManager.ViewModels
{
    public class StudentIndexViewModel
    {
        //public IQueryable<Student> Students { get; set; }
        public IPagedList<Student> Students { get; set; }
        public string Search { get; set; }
        public IEnumerable<EnrollmentYearWithCount> EnYearWithCount { get; set; }
        public string EnrollmentYear { get; set; }

        public string SortBy { get; internal set; }
        public Dictionary<string, string> Sorts { get; set; }

        public IEnumerable<SelectListItem> EnYearFilterItems
        {
            get
            {
                var ret = EnYearWithCount.Select(cc => new SelectListItem
                {
                    Value = cc.EnrollmentYearStr,
                    Text = cc.EnYearStrWithCount
                });
                return ret;
            }
        }
    }

    public class EnrollmentYearWithCount
    {
        public int StudentCount { get; set; }
        public string EnrollmentYearStr { get; set; }
        public string EnYearStrWithCount
        {
            get
            {
                return EnrollmentYearStr + " (" + StudentCount.ToString() + ")";
            }
        }
    }
}