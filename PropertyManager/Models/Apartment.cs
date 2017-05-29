
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PropertyManager.Models
{
    public class Apartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApartmentNumber { get; set; }
        public int FloorNumber { get; set; }
        public int UnitId { get; set; }
        public string Status { get; set; }
        public virtual Unit Unit { get; set; }
        
    }
}