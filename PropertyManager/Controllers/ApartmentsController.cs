
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PropertyManager.Controllers
{
    public class ApartmentsController : ApiController
    {
        private Manager m = new Manager();

        // GET: api/Apartments
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Get()
        {
            return Ok(m.ApartmentGetAll());
        }

        // GET: api/Apartments/5
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue) { return NotFound(); }

            var o = m.ApartmentGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // ADD APARTMENT
        // CHECKS TO SEE IF APARTMENT IS ALREADY ASSOCIATED WITH A LEASE
        // POST: api/Apartments
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Post([FromBody]ApartmentAdd newItem)
        {          

            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var leaseInfo = m.LeaseGetByAptNumber(newItem.ApartmentNumber);
            if(leaseInfo != null)
            {
                return Content(HttpStatusCode.Conflict, "Apartment Already Exists");
            }

            var addedItem = m.ApartmentAdd(newItem);

            if (addedItem == null) { return BadRequest("Cannot add the object"); }            

            // HTTP 201 
            var uri = Url.Link("DefaultApi", new { id = addedItem.ApartmentNumber });

            return Created(uri, addedItem);
        }

        // PUT: api/Apartments/5
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Put(int id, [FromBody]ApartmentEdit editedItem)
        {
            if (editedItem == null)
            {
                return BadRequest("Must send an entity body with the request");
            }

            if (id != editedItem.ApartmentNumber)
            {
                return BadRequest("Invalid data in the entity body");
            }

            if (ModelState.IsValid)
            {
                var changedItem = m.ApartmentEdit(editedItem);

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

        // CHECK TO SEE IF THERE'S A LEASE ASSOCIATED WITH THE APARTMENT
        // DOESN'T DELETE IF IT FINDS A LEASE ASSOCIATED - RETURN ADVICE TO MANAGER
        // DELETE: api/Apartments/5
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Delete(int id)
        {
            var leaseInfo = m.LeaseGetByAptNumber(id);
            if (leaseInfo != null)
            {
                return Content(HttpStatusCode.Conflict, "Lease associated");
            }

            m.ApartmentDelete(id);
            return Ok();
        }
    }
}
