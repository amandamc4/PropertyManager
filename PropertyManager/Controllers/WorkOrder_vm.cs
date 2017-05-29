
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Controllers
{
    public class WorkOrderAdd
    {
        public WorkOrderAdd()
        {
            RequestDate = DateTime.Now;
        }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int TenantId { get; set; }
    }

    public class WorkOrderBase : WorkOrderAdd
    {
        public int Id { get; set; }
    }

    public class WorkOrderWithTenant : WorkOrderBase
    {
        public TenantBase Tenant { get; set; }
    }

    public class WorkOrderEdit
    {
        public WorkOrderEdit()
        {
            RequestDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}