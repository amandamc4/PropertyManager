
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Controllers
{
    public class UnitPhotoAdd
    {
        public string PathName { get; set; }
        public string Description { get; set; }
        public int UnitId { get; set; }
    }

    public class UnitPhotoBase : UnitPhotoAdd
    {
        public int Id { get; set; }
        
    }

   

    public class UnitPhotoEdit
    {
        public int Id { get; set; }
        public string PathName { get; set; }
        public string Description { get; set; }
        public int UnitId { get; set; }
    }
}