
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Controllers
{
    public class AnnouncementAdd
    {
        public AnnouncementAdd()
        {
            StartDate = DateTime.Now;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }

    public class AnnouncementBase : AnnouncementAdd
    {
        public int Id { get; set; }
    }

    public class AnnouncementEdit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}