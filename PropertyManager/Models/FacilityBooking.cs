
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyManager.Models
{
    public class FacilityBooking
    {
        public int Id { get; set; }
        public DateTime? BookedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime? StartTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime? EndTime { get; set; }
        public string Notes { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual Facility Facility { get; set; }
    }
}