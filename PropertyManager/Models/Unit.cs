
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropertyManager.Models
{
    public class Unit
    {
        public Unit()
        {
            UnitPhotos = new List<UnitPhoto>();

            Apartments = new HashSet<Apartment>();
        }
                
        public int Id { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double SquareFeet { get; set; }
        public int? MaxOccupants { get; set; }
        public bool Balcony { get; set; }
        public bool Dishwasher { get; set; }
        public bool Laundry { get; set; }
        public ICollection<UnitPhoto> UnitPhotos { get; set; }
        public ICollection<Apartment> Apartments { get; set; }

    }
}