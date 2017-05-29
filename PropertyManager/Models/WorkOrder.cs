
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Models
{
    public class WorkOrder
    {     
        public int Id { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}