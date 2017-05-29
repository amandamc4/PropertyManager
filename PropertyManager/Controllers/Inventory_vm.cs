
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyManager.Controllers
{
    public class InventoryAdd
    {
        public string ProductName { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
    }

    public class InventoryBase : InventoryAdd
    {
        public int Id { get; set; }
    }

    public class InventoryEdit
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
    }
}