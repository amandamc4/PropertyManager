
using PropertyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Controllers
{
    public class ServiceRequestAdd
    {
        public ServiceRequestAdd()
        {
            RequestDate = DateTime.Now.Date;
        }

        public string Description { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Notes { get; set; }
        public int ServiceId { get; set; }
    }

    public class ServiceRequestBase : ServiceRequestAdd {
        public int Id { get; set; }
    }

    public class ServiceRequestWithService : ServiceRequestBase
    {
        public ServiceForServiceRequest Service { get; set; }
    }

    public class ServiceRequestEdit
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Notes { get; set; }
        public int ServiceId { get; set; }
    }
}