
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PropertyManager.Controllers
{
    public class LeasesController : ApiController
    {
        private Manager m = new Manager();

        // GET: api/Leases
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Get()
        {
            return Ok(m.LeaseGetAllWithInformation());
        }

        // GET: api/Leases/5
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue) { return NotFound(); }
  
            var o = m.LeaseGetByIdWithInformation(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        [Route("api/leases/id/{id}/find")]
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult GetByTenantId(int? id)
        {
            if (!id.HasValue) { return NotFound(); }
 
            var o = m.LeaseGetByTenantId(id);

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        [Route("api/leases/email/{email}/find")]
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult GetByTenantEmail(string email)
        {
            if (email == "") { return NotFound(); }

            var o = m.LeaseGetByTenantEmail(email);

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // CHECKS TO SEE IF APARTMENT EXISTS TO ASSOCIATE LEASE WITH
        // ALSO CHECKS IF APARTMENT REQUESTED IS NOT ALREADY ASSOCIATED WITH ANOTHER LEASE
        // SENDS ACTIVATION CODE TO TENANT FOR REGISTERING ON THE WEBSITE WHEN LEASE IS ADDED
        // POST: api/Leases
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IHttpActionResult> Post([FromBody]LeaseAdd newItem)
        {
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var apartment = m.ApartmentGetById(newItem.ApartmentNumber);
            if (apartment == null)
            {
                return Content(HttpStatusCode.NotFound, "Apartment Number not found");
            }

            var lease = m.LeaseGetByAptNumber(newItem.ApartmentNumber);
            if (lease != null)
            {
                return Content(HttpStatusCode.Conflict, "Apartment already associated with a lease");
            }

            var addedItem = m.LeaseAdd(newItem);

            if (addedItem == null) { return BadRequest("Cannot add the object"); }


            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });

            // SEND ACTIVATION CODE TO TENANT
            var hashPassword = m.TenantAddCode(addedItem.TenantId);

            var tenant = m.TenantGetById(addedItem.TenantId);

            await sendEmail(hashPassword, tenant.Email);

            return Created(uri, addedItem);
        }

        public async Task<IHttpActionResult> sendEmail(string hashPassword, string email)
        {

            EmailService sendemail = new EmailService();

            Microsoft.AspNet.Identity.IdentityMessage message = new Microsoft.AspNet.Identity.IdentityMessage();

            message.Subject = "Your Activation Code";
            message.Destination = email;
            message.Body = "Here is your activation code: <b>" + hashPassword + " </b>. <br/>Please use this to register for an account with Property Cloud by clicking <a href='http://propertycloud1.azurewebsites.net/#/register'> HERE </a> or access http://propertycloud1.azurewebsites.net/#/register . ";
          

            await sendemail.SendAsync(message);

            return Ok();
        }

        // PUT: api/Leases/5
        [Authorize(Roles = "Administrator, Manager")]
        public IHttpActionResult Put(int id, [FromBody]LeaseEdit editedItem)
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
                var changedItem = m.LeaseEdit(editedItem);

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

        // DELETE: api/Leases/5
        [Authorize(Roles = "Administrator, Manager")]
        public void Delete(int id)
        {
            m.LeaseDelete(id);
        }
    }
}
