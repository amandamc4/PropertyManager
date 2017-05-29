
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Models
{
    public class Lease
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? SecurityDeposit { get; set; }
        public double MonthlyRent { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual Apartment Apartment { get; set; }
    }
}