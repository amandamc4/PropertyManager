
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Controllers
{
    public class OccupantAdd
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public int ApartmentNumber { get; set; }
        public int TenantId { get; set; }
    }

    public class OccupantBase : OccupantAdd
    {
        public int Id { get; set; }
    }

    public class OccupantEdit
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public int ApartmentNumber { get; set; }
        public int TenantId { get; set; }
    }
}