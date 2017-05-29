
using PropertyManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyManager.Controllers
{
    public class UnitAdd
    {
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double SquareFeet { get; set; }
        public int? MaxOccupants { get; set; }
        public bool Balcony { get; set; }
        public bool Dishwasher { get; set; }
        public bool Laundry { get; set; }
    }

    public class UnitBase : UnitAdd
    {
        public UnitBase()
        {
            UnitPhotos = new List<UnitPhotoBase>();
        }
        public int Id { get; set; }
        public ICollection<UnitPhotoBase> UnitPhotos { get; set; }
        public ICollection<ApartmentBase> Apartments { get; set; }
    }


    public class UnitEdit
    {
        public int Id { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double SquareFeet { get; set; }
        public int? MaxOccupants { get; set; }
        public bool Balcony { get; set; }
        public bool Dishwasher { get; set; }
        public bool Laundry { get; set; }
    }

    public class UnitForApartment
    {
        public int Id { get; set; }
        public int Bedrooms { get; set; }
    }
}