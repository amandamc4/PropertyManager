
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyManager.Controllers
{
    public class ApartmentAdd
    {
        [Key]
        public int ApartmentNumber { get; set; }
        public int FloorNumber { get; set; }
        public string Status { get; set; }
        public int UnitId { get; set; }
    }

    public class ApartmentBase : ApartmentAdd
    {
    }
    public class ApartmentForLease
    {
        public int ApartmentNumber { get; set; }
    }

    public class ApartmentEdit
    {
        [Key]
        public int ApartmentNumber { get; set; }
        public int FloorNumber { get; set; }
        public string Status { get; set; }
        public int UnitId { get; set; }
    }

    public class ApartmentWithUnit : ApartmentBase
    {
        public UnitForApartment Unit { get; set; }
    }
}