
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyManager.Controllers
{
    public class FacilityBookingAdd
    {
        public DateTime? BookedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime? StartTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime? EndTime { get; set; }
        public string Notes { get; set; }
        public int TenantId { get; set; }
        public int FacilityId { get; set; }
    }

    public class FacilityBookingBase : FacilityBookingAdd
    {
        public int Id { get; set; }
        public FacilityBase Facility { get; set; }
        public TenantBase Tenant { get; set; }
    }

    public class FacilityBookingEdit
    {
        public int Id { get; set; }
        public DateTime? BookedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime? StartTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime? EndTime { get; set; }
        public string Notes { get; set; }
        public int FacilityId { get; set; }
    }
}