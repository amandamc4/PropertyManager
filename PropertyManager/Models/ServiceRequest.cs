
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Models
{
    public class ServiceRequest
    {
        public ServiceRequest()
        {
            RequestDate = DateTime.Now.Date;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Notes { get; set; }
        public virtual Service Service { get; set; }
    }
}