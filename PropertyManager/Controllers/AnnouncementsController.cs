
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PropertyManager.Controllers
{
    public class AnnouncementsController : ApiController
    {
        private Manager m = new Manager();

        // GET: api/Announcements
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Get()
        {
            return Ok(m.AnnouncementGetAll());
        }

        // GET: api/Announcements/5
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue) { return NotFound(); }

            var o = m.AnnouncementGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // POST: api/Announcements
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Post([FromBody]AnnouncementAdd newItem)
        {
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var addedItem = m.AnnouncementAdd(newItem);

            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });

            return Created(uri, addedItem);
        }

        // PUT: api/Announcements/5
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Put(int id, [FromBody]AnnouncementEdit editedItem)
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
                var changedItem = m.AnnouncementEdit(editedItem);

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

        // DELETE: api/Announcements/5
        [Authorize(Roles = "Administrator, Manager")]
        public HttpResponseMessage Delete(int id)
        {
            var response = m.AnnouncementDelete(id);           
            return response;
        }
    }
}
