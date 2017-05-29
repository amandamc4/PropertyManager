
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PropertyManager.Controllers
{
    public class FacilityBookingsController : ApiController
    {
        private Manager m = new Manager();

        // GET: api/FacilityBookings
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Get()
        {
            return Ok(m.FacilityBookingGetAllWithFacility());
        }

        // GET: api/FacilityBookings/5
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue) { return NotFound(); }
   
            var o = m.FacilityBookingGetByIdWithFacility(id.GetValueOrDefault());

            if (o == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(o);
            }
        }

        // VALIDATES DATE AND TIME
        // CHECKS TO SEE IF THERE'S A BOOKING ON THE SAME DAY
        // IF IT FINDS ANOTHER BOOKING, CHECKS TO SEE IF THEIR TIMES OVERLAP
        // ALSO CHECKS TO SEE IF FACILITY IS OPEN AT THE TIME OF THE REQUESTED BOOKING
        // POST: api/FacilityBookings
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Post([FromBody]FacilityBookingAdd newItem)
        {
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var facility = m.FacilityGetById(newItem.FacilityId);
            if(facility != null)
            {
                var facilityOpenHour = facility.OpenTime.Value.Hour;
                var facilityOpenMin = facility.OpenTime.Value.Minute;
                var facilityCloseHour = facility.CloseTime.Value.Hour;
                var facilityCloseMin = facility.CloseTime.Value.Minute;

                var startTimeHour = newItem.StartTime.Value.Hour;
                var startTimeMin = newItem.StartTime.Value.Minute;

                var endTimeHour = newItem.EndTime.Value.Hour;
                var endTimeMin = newItem.EndTime.Value.Minute;

                if(startTimeHour < facilityOpenHour || startTimeHour > facilityCloseHour)
                {
                    return Content(HttpStatusCode.Conflict, "The facility is closed at this start time");
                }
                else if(startTimeHour == facilityOpenHour && startTimeMin < facilityOpenMin){
                    return Content(HttpStatusCode.Conflict, "The facility is closed at this start time");
                }    
                else if(endTimeHour > facilityCloseHour)
                {
                    return Content(HttpStatusCode.Conflict, "The facility is closed at this end time");
                }
                else if(endTimeHour == facilityCloseHour && endTimeMin > facilityCloseMin)
                {
                    return Content(HttpStatusCode.Conflict, "The facility is closed at this end time");
                }               
            }            

            var booking = m.FacilityBookingGetByDate(newItem);
            if(booking != null)
            {
                foreach(FacilityBookingBase book in booking)
                {
                    var startTimeHour = newItem.StartTime.Value.Hour;
                    var startTimeMin = newItem.StartTime.Value.Minute;
                    var endTimeHour = newItem.EndTime.Value.Hour;
                    var endTimeMin = newItem.EndTime.Value.Minute;

                    var bookstartTimeHour = book.StartTime.Value.Hour;
                    var bookstartTimeMin = book.StartTime.Value.Minute;
                    var bookendTimeHour = book.EndTime.Value.Hour;
                    var bookendTimeMin = book.EndTime.Value.Minute;

                    if (startTimeHour >= bookstartTimeHour && startTimeHour < bookendTimeHour)
                    {
                        return Content(HttpStatusCode.Conflict, "There's a booking at this time");
                    }
                    else if(startTimeHour < bookstartTimeHour && endTimeHour >= bookstartTimeHour)
                    {
                        return Content(HttpStatusCode.Conflict, "There's a booking at this time");
                    }
                   
                    else if (startTimeHour < bookstartTimeHour && endTimeHour >= bookstartTimeHour)
                    {
                        return Content(HttpStatusCode.Conflict, "There's a booking at this time");
                    }
                    else if (startTimeHour >= bookendTimeHour && startTimeMin < bookendTimeMin)
                    {
                        return Content(HttpStatusCode.Conflict, "There's a booking at this time");
                    }
                }
            }

            var addedItem = m.FacilityBookingAdd(newItem);

            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201
            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });

            return Created(uri, addedItem);
        }

        // VALIDATES DATE AND TIME
        // CHECKS TO SEE IF THERE'S A BOOKING ON THE SAME DAY
        // IF IT FINDS ANOTHER BOOKING, CHECKS TO SEE IF THEIR TIMES OVERLAP
        // ALSO CHECKS TO SEE IF FACILITY IS OPEN AT THE TIME OF THE REQUESTED BOOKING
        // PUT: api/FacilityBookings/5
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public IHttpActionResult Put(int id, [FromBody]FacilityBookingEdit editedItem)
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
                var facility = m.FacilityGetById(editedItem.FacilityId);
                if (facility != null)
                {
                    var facilityOpenHour = facility.OpenTime.Value.Hour;
                    var facilityOpenMin = facility.OpenTime.Value.Minute;
                    var facilityCloseHour = facility.CloseTime.Value.Hour;
                    var facilityCloseMin = facility.CloseTime.Value.Minute;

                    var startTimeHour = editedItem.StartTime.Value.Hour;
                    var startTimeMin = editedItem.StartTime.Value.Minute;

                    var endTimeHour = editedItem.EndTime.Value.Hour;
                    var endTimeMin = editedItem.EndTime.Value.Minute;

                    if (startTimeHour < facilityOpenHour || startTimeHour > facilityCloseHour)
                    {
                        return Content(HttpStatusCode.Conflict, "The facility is closed at this start time");
                    }
                    else if (startTimeHour == facilityOpenHour && startTimeMin < facilityOpenMin)
                    {
                        return Content(HttpStatusCode.Conflict, "The facility is closed at this start time");
                    }
                    else if (endTimeHour > facilityCloseHour)
                    {
                        return Content(HttpStatusCode.Conflict, "The facility is closed at this end time");
                    }
                    else if (endTimeHour == facilityCloseHour && endTimeMin > facilityCloseMin)
                    {
                        return Content(HttpStatusCode.Conflict, "The facility is closed at this end time");
                    }
                }

                var booking = m.FacilityBookingGetByDateEdit(editedItem);
                if (booking != null)
                {
                    foreach (FacilityBookingBase book in booking)
                    {
                        if (book.Id != editedItem.Id)
                        {
                            if (editedItem.StartTime >= book.StartTime && editedItem.StartTime <= book.EndTime)
                            {
                                return Content(HttpStatusCode.Conflict, "There's a booking at this time");
                            }
                            else if (editedItem.StartTime < book.StartTime && editedItem.EndTime >= book.StartTime)
                            {
                                return Content(HttpStatusCode.Conflict, "There's a booking at this time");
                            }
                        }
                    }
                }

                var changedItem = m.FacilityBookingEdit(editedItem);

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

        // DELETE: api/FacilityBookings/5
        [Authorize(Roles = "Administrator, Manager, Tenant")]
        public void Delete(int id)
        {
            m.FacilityBookingDelete(id);
        }
    }
}
