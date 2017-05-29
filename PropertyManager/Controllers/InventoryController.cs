
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PropertyManager.Controllers
{
    public class InventoryController : ApiController
    {
        private Manager m = new Manager();

        // GET: api/Inventory
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Get()
        {
            return Ok(m.InventoryGetAll());
        }

        // GET: api/Inventory/5
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue) { return NotFound(); }
  
            var o = m.InventoryGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // POST: api/Inventory
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Post([FromBody]InventoryAdd newItem)
        {
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var addedItem = m.InventoryAdd(newItem);

            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201
            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });

            return Created(uri, addedItem);
        }

        // PUT: api/Inventory/5
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Put(int id, [FromBody]InventoryEdit editedItem)
        {
            if (editedItem == null)
            {
                return BadRequest("Must send an entity body with the request");
            }

            if (id != editedItem.Id)
            {
                return BadRequest("Invalid data in the entity body");
            }

            if (ModelState.IsValid)
            {
                var changedItem = m.InventoryEdit(editedItem);

                if (changedItem == null)
                {
                    // HTTP 400
                    return BadRequest("Cannot edit the object");
                }
                else
                {
                    // HTTP 200
                    return Ok(changedItem);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Inventory/5
        [Authorize(Roles = "Administrator, Manager")]
        public void Delete(int id)
        {
            m.InventoryDelete(id);
        }
    }
}
