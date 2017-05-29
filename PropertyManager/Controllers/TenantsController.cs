
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PropertyManager.Controllers
{
    public class TenantsController : ApiController
    {
        private Manager m = new Manager();

        // GET: api/Tenants
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Get()
        {
            return Ok(m.TenantGetAll());
        }

        // GET: api/Tenants/5
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue) { return NotFound(); }
   
            var o = m.TenantGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        [Route("api/tenants/email/{email}/find")]
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult GetByEmail(string email)
        {
            if (email == "") { return NotFound(); }
 
            var o = m.TenantGetByEmail(email);

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // POST: api/Tenants
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Post([FromBody]TenantAdd newItem)
        {
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var addedItem = m.TenantAdd(newItem);

            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201
            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });

            return Created(uri, addedItem);
        }

        // RE SEND ACTIVATIN CODE TO TENANT BY EMAIL
        [Route("api/tenants/{email}/sendActivation")]
        [HttpPost]
        public async Task<IHttpActionResult> sendEmail(string email)
        {
            var hashPassword = m.TenantGetActivationCode(email);

            if(hashPassword == null)
            {
                return Content(HttpStatusCode.NotFound, "Tenant not found");
            }


            EmailService sendemail = new EmailService();

            Microsoft.AspNet.Identity.IdentityMessage message = new Microsoft.AspNet.Identity.IdentityMessage();

            message.Subject = "Your Activation Code";
            message.Destination = email;
            message.Body = "Here is your activation code: <b>" + hashPassword + " </b>. <br/>Please use this to register for an account with Property Cloud by clicking <a href='http://propertycloud1.azurewebsites.net/#/register'> HERE </a> or access http://propertycloud1.azurewebsites.net/#/register . ";


            await sendemail.SendAsync(message);

            return Ok();
        }

        // PUT: api/Tenants/5
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Put(int id, [FromBody]TenantEdit editedItem)
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
                var changedItem = m.TenantEdit(editedItem);

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

        // DELETE: api/Tenants/5
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Delete(int id)
        {
            var leaseInfo = m.LeaseGetByTenantId(id);
            if (leaseInfo != null)
            {
                return Content(HttpStatusCode.Conflict, "Lease associated");
            }

            m.TenantDelete(id);
            return Ok();
        }
    }
}
