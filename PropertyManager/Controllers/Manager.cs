
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PropertyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PropertyManager.Controllers
{
    public class Manager
    {
        private ApplicationDbContext ds = new ApplicationDbContext();

        // FIND USER BY EMAIL
        public UserBase getByEmail(string email)
        {           
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = userManager.FindByEmailAsync(email).Result;

            return (user == null) ? null : Mapper.Map<UserBase>(user);

        }

        // GENERATES RANDOM PASSWORD FOR MANAGER REGISTRATION
        // GENERATES RANDOM ACTIVATION CODE FOR TENANT REGISTRATION
        public string GeneratePassword()
        {
            string PasswordLength = "6";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";


            char[] sep = {
            ','
        };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }
            return NewPassword;
        }

        // ****************************************** ANNOUNCEMENT SECTION *****************************************

        public IEnumerable<AnnouncementBase> AnnouncementGetAll()
        {
            var c = ds.Announcements.OrderBy(a => a.Title);

            return Mapper.Map<IEnumerable<AnnouncementBase>>(c);
        }

        public IEnumerable<AnnouncementBase> AnnouncementGetAllActive()
        {
            var today = DateTime.Now;

            var c = ds.Announcements.Where(a => a.ExpireDate < today).OrderBy(a => a.Title);

            return Mapper.Map<IEnumerable<AnnouncementBase>>(c);
        }

        public AnnouncementBase AnnouncementGetById(int id)
        {
            var c = ds.Announcements.SingleOrDefault(a => a.Id == id);

            return (c == null) ? null : Mapper.Map<AnnouncementBase>(c);
        }

        public AnnouncementBase AnnouncementAdd(AnnouncementAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }            
            var addedItem = ds.Announcements.Add(Mapper.Map<Announcement>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<AnnouncementBase>(addedItem);
        }

        public AnnouncementBase AnnouncementEdit(AnnouncementEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.Announcements.SingleOrDefault(e => e.Id == editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<AnnouncementBase>(storedItem);
            }
        }

        public HttpResponseMessage AnnouncementDelete(int id)
        {
            var storedItem = ds.Announcements.Find(id);

            var response = new HttpResponseMessage();
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.Announcements.Remove(storedItem);
                    ds.SaveChanges();
                    response.Headers.Add("DeleteMessage", "Delete successful");
                }
                catch (Exception) {
                    
                    response.Headers.Add("DeleteMessage", "Could not delete");                   
                }
            }
            return response;
        }

        // ****************************************** EMPLOYEEE SECTION *****************************************

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            var c = ds.Employees.OrderBy(a => a.Title);

            return Mapper.Map<IEnumerable<EmployeeBase>>(c);
        }

        public EmployeeBase EmployeeGetById(int id)
        {
            var c = ds.Employees.SingleOrDefault(a => a.Id == id);

            return (c == null) ? null : Mapper.Map<EmployeeBase>(c);
        }

        public EmployeeBase EmployeeAdd(EmployeeAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }
            var addedItem = ds.Employees.Add(Mapper.Map<Employee>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<EmployeeBase>(addedItem);
        }

        public EmployeeBase EmployeeEdit(EmployeeEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.Employees.SingleOrDefault(e => e.Id == editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<EmployeeBase>(storedItem);
            }
        }

        public void EmployeeDelete(int id)
        {
            var storedItem = ds.Employees.Find(id);
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.Employees.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        // *************************************************** INVENTORY SECTION *****************************************
        public IEnumerable<InventoryBase> InventoryGetAll()
        {
            var c = ds.Inventory.OrderBy(a => a.Id);

            return Mapper.Map<IEnumerable<InventoryBase>>(c);
        }

        public InventoryBase InventoryGetById(int id)
        {
            var c = ds.Inventory.SingleOrDefault(a => a.Id == id);

            return (c == null) ? null : Mapper.Map<InventoryBase>(c);
        }

        public InventoryBase InventoryAdd(InventoryAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }
            var addedItem = ds.Inventory.Add(Mapper.Map<Inventory>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<InventoryBase>(addedItem);
        }

        public InventoryBase InventoryEdit(InventoryEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.Inventory.SingleOrDefault(e => e.Id == editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<InventoryBase>(storedItem);
            }
        }

        public void InventoryDelete(int id)
        {
            var storedItem = ds.Inventory.Find(id);
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.Inventory.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        // *************************************************** FACILITY SECTION *****************************************

        public IEnumerable<FacilityBase> FacilityGetAll()
        {
            var c = ds.Facilities.OrderBy(a => a.FacilityName);

            return Mapper.Map<IEnumerable<FacilityBase>>(c);
        }

        public FacilityBase FacilityGetById(int id)
        {
            var c = ds.Facilities.SingleOrDefault(a => a.Id == id);

            return (c == null) ? null : Mapper.Map<FacilityBase>(c);
        }

        public FacilityBase FacilityAdd(FacilityAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }          
            var addedItem = ds.Facilities.Add(Mapper.Map<Facility>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<FacilityBase>(addedItem);
        }

        public FacilityBase FacilityEdit(FacilityEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.Facilities.SingleOrDefault(e => e.Id == editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<FacilityBase>(storedItem);
            }
        }
        public void FacilityDelete(int id)
        {
            var storedItem = ds.Facilities.Find(id);
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.Facilities.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        //**************************************** FACILITY BOOKING *****************************************************

        public IEnumerable<FacilityBookingBase> FacilityBookingGetAllWithFacility()
        {
            var s = ds.FacilityBookings.Include("Facility").OrderBy(j => j.Id);
            return Mapper.Map<IEnumerable<FacilityBookingBase>>(s);
        }

        public IEnumerable<FacilityBookingBase> FacilityBookingGetAllActiveWithFacility()
        {
            var today = DateTime.Now;

            var s = ds.FacilityBookings.Include("Facility").Where(a => a.BookedDate < today).OrderBy(j => j.Id);
            return Mapper.Map<IEnumerable<FacilityBookingBase>>(s);
        }

        public FacilityBookingBase FacilityBookingGetByIdWithFacility(int id)
        {
            var o = ds.FacilityBookings.Include("Facility").SingleOrDefault(j => j.Id == id);
            return (o == null) ? null : Mapper.Map<FacilityBookingBase>(o);
        }

        public IEnumerable<FacilityBookingBase> FacilityBookingGetByDate(FacilityBookingAdd newItem)
        {
            var o = ds.FacilityBookings.Where(j => j.BookedDate == newItem.BookedDate && j.Facility.Id == newItem.FacilityId);
            return (o == null) ? null : Mapper.Map<IEnumerable<FacilityBookingBase>>(o);
        }

        public IEnumerable<FacilityBookingBase> FacilityBookingGetByDateEdit(FacilityBookingEdit newItem)
        {
            var o = ds.FacilityBookings.Where(j => j.BookedDate == newItem.BookedDate && j.Facility.Id == newItem.FacilityId);
            return (o == null) ? null : Mapper.Map<IEnumerable<FacilityBookingBase>>(o);
        }

        public FacilityBookingBase FacilityBookingAdd(FacilityBookingAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }
            else
            {
                var associatedItem = ds.Facilities.Find(newItem.FacilityId);
                if (associatedItem == null)
                {
                    return null;
                }

                var associatedTenant = ds.Tenants.Find(newItem.TenantId);
                if (associatedTenant == null)
                {
                    return null;
                }

                FacilityBooking addedItem = Mapper.Map<FacilityBooking>(newItem);

                addedItem.Facility = associatedItem;
                addedItem.Tenant = associatedTenant;

                ds.FacilityBookings.Add(addedItem);
                ds.SaveChanges();

                return Mapper.Map<FacilityBookingBase>(addedItem);
            }
        }

        public FacilityBookingBase FacilityBookingEdit(FacilityBookingEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }

            var storedItem = ds.FacilityBookings.Find(editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {                
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<FacilityBookingBase>(storedItem);
            }
        }

        public void FacilityBookingDelete(int id)
        {
            var storedItem = ds.FacilityBookings.Find(id);

            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.FacilityBookings.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        //**************************************** SERVICE SECTION ******************************************************

        public IEnumerable<ServiceBase> ServiceGetAll()
        {
            var c = ds.Services.OrderBy(a => a.ServiceName);

            return Mapper.Map<IEnumerable<ServiceBase>>(c);
        }


        public IEnumerable<ServiceForServiceRequest> ServiceGetAllForRequest()
        {
            var c = ds.Services.OrderBy(a => a.ServiceName);

            return Mapper.Map<IEnumerable<ServiceForServiceRequest>>(c);
        }
        public ServiceBase ServiceGetById(int id)
        {
            var c = ds.Services.SingleOrDefault(a => a.Id == id);

            return (c == null) ? null : Mapper.Map<ServiceBase>(c);
        }

        public ServiceWithServiceRequests ServiceGetByIdWithServiceRequests(int id)
        {
            var o = ds.Services.Include("ServiceRequests").SingleOrDefault(j => j.Id == id);

            return (o == null) ? null : Mapper.Map<ServiceWithServiceRequests>(o);
        }

        public ServiceBase ServiceAdd(ServiceAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }
            var addedItem = ds.Services.Add(Mapper.Map<Service>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<ServiceBase>(addedItem);
        }
        public ServiceBase ServiceEdit(ServiceEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.Services.SingleOrDefault(e => e.Id == editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<ServiceBase>(storedItem);
            }
        }

        public void ServiceDelete(int id)
        {
            var storedItem = ds.Services.Find(id);
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.Services.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        //**************************************** SERVICE REQUEST SECTION ***********************************************

        public IEnumerable<ServiceRequestBase> ServiceRequestGetAll()
        {
            var s = ds.ServiceRequests.OrderBy(a => a.Id);
            return Mapper.Map<IEnumerable<ServiceRequestBase>>(s);
        }

        public IEnumerable<ServiceRequestWithService> ServiceRequestGetAllWithService()
        {
            var s = ds.ServiceRequests.Include("Service").OrderBy(j => j.Id);
            return Mapper.Map<IEnumerable<ServiceRequestWithService>>(s);
        }

        public IEnumerable<ServiceRequestWithService> ServiceRequestGetAllActiveWithService()
        {
            var today = DateTime.Now;

            var s = ds.ServiceRequests.Include("Service").Where(j => j.CompletionDate < today).OrderBy(j => j.Id);
            return Mapper.Map<IEnumerable<ServiceRequestWithService>>(s);
        }

        public ServiceRequestWithService ServiceRequestGetByIdWithService(int id)
        {
            var o = ds.ServiceRequests.Include("Service").SingleOrDefault(j => j.Id == id);
            return (o == null) ? null : Mapper.Map<ServiceRequestWithService>(o);
        }

        public ServiceRequestWithService ServiceRequestAdd(ServiceRequestAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }
            else
            {
                var associatedItem = ds.Services.Find(newItem.ServiceId);
                if (associatedItem == null)
                {
                    return null;
                }

                var today = DateTime.Now;

                ServiceRequest addedItem = Mapper.Map<ServiceRequest>(newItem);

                addedItem.Service = associatedItem;

                if(newItem.RequestDate == null)
                {
                    addedItem.RequestDate = today;
                }
                

                ds.ServiceRequests.Add(addedItem);
                ds.SaveChanges();

                return Mapper.Map<ServiceRequestWithService>(addedItem);
            }
        }

        public ServiceRequestBase ServiceRequestEdit(ServiceRequestEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }

            var storedItem = ds.ServiceRequests.Find(editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                if(editedItem.RequestDate == null && storedItem.RequestDate == null)
                {
                    var today = DateTime.Now;
                    storedItem.RequestDate = today;
                }
                

                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<ServiceRequestBase>(storedItem);
            }
        }

        public void ServiceRequestDelete(int id)
        {
            var storedItem = ds.ServiceRequests.Find(id);

            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.ServiceRequests.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        //************************************************UNIT SECTION****************************************************

        public IEnumerable<UnitBase> UnitGetAll()
        {
            var c = ds.Units.Include("UnitPhotos").OrderBy(a => a.Id);

            return Mapper.Map<IEnumerable<UnitBase>>(c);
        }

        public UnitBase UnitGetById(int id)
        {
            var c = ds.Units.Include("UnitPhotos").Include("Apartments").SingleOrDefault(a => a.Id == id);

            return (c == null) ? null : Mapper.Map<UnitBase>(c);
        }

        public UnitBase UnitAdd(UnitAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }
            var addedItem = ds.Units.Add(Mapper.Map<Unit>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<UnitBase>(addedItem);
        }

        public UnitBase UnitEdit(UnitEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.Units.SingleOrDefault(e => e.Id == editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<UnitBase>(storedItem);
            }
        }
        public void UnitDelete(int id)
        {
            var storedItem = ds.Units.Include("UnitPhotos").SingleOrDefault(a => a.Id == id);
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
               
                foreach(UnitPhoto unitphoto in storedItem.UnitPhotos)
                {
                    var filePath = "~" + unitphoto.PathName;
                    
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(filePath));
                    };
                }
                try
                {                 
                    ds.Units.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        //***********************************************UNITPHOTO SECTION ***********************************************
        public IEnumerable<UnitPhotoBase> UnitPhotoGetAll()
        {
            var c = ds.UnitPhotos.Include("Unit").OrderBy(a => a.Id);

            return Mapper.Map<IEnumerable<UnitPhotoBase>>(c);
        }

        public UnitPhotoBase UnitPhotoGetById(int id)
        {
            var c = ds.UnitPhotos.Include("Unit").SingleOrDefault(a => a.Id == id);

            return (c == null) ? null : Mapper.Map<UnitPhotoBase>(c);
        }

        public UnitPhotoBase UnitPhotoGetByUnitId(int unitId)
        {
            var c = ds.UnitPhotos.SingleOrDefault(a => a.Id == unitId);

            return (c == null) ? null : Mapper.Map<UnitPhotoBase>(c);
        }
        public UnitPhotoBase UnitPhotoAdd(UnitPhotoAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }
            var associatedItem = ds.Units.Find(newItem.UnitId);
            if (associatedItem == null)
            {
                return null;
            }
            var addedItem = Mapper.Map<UnitPhoto>(newItem);
            addedItem.Unit = associatedItem;

            ds.UnitPhotos.Add(addedItem);
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<UnitPhotoBase>(addedItem);
        }

        public UnitPhotoBase UnitPhotoEdit(UnitPhotoEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.UnitPhotos.SingleOrDefault(e => e.Id == editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<UnitPhotoBase>(storedItem);
            }
        }


        public void UnitPhotoDelete(int id)
        {
            var storedItem = ds.UnitPhotos.Find(id);
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                var filePath = "~" + storedItem.PathName;
                try
                {
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(filePath));
                    }

                    ds.UnitPhotos.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        // ********************************************** APARTMENT SECTION***************************************************
        public IEnumerable<ApartmentWithUnit> ApartmentGetAll()
        {
            var c = ds.Apartments.Include("Unit").OrderBy(a => a.ApartmentNumber);

            return Mapper.Map<IEnumerable<ApartmentWithUnit>>(c);
        }

        public ApartmentWithUnit ApartmentGetById(int id)
        {
            var c = ds.Apartments.Include("Unit").SingleOrDefault(a => a.ApartmentNumber == id);

            return (c == null) ? null : Mapper.Map<ApartmentWithUnit>(c);
        }
        public ApartmentWithUnit ApartmentAdd(ApartmentAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }         

            else
            {
                var apartment = ds.Apartments.Find(newItem.ApartmentNumber);
                if(apartment != null)
                {
                    return null;
                }

                var associatedUnit = ds.Units.Find(newItem.UnitId);
                if (associatedUnit == null)
                {
                    return null;
                }
               
                Apartment addedItem = Mapper.Map<Apartment>(newItem);

                addedItem.Unit = associatedUnit;

                ds.Apartments.Add(addedItem);
                ds.SaveChanges();

                return Mapper.Map<ApartmentWithUnit>(addedItem);
            }
        }

        public ApartmentWithUnit ApartmentEdit(ApartmentEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.Apartments.SingleOrDefault(e => e.ApartmentNumber == editedItem.ApartmentNumber);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<ApartmentWithUnit>(storedItem);
            }
        }

        public void ApartmentDelete(int id)
        {
            var storedItem = ds.Apartments.Find(id);
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.Apartments.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        //************************************** TENANT SECTION ***********************************************************

        public IEnumerable<TenantBase> TenantGetAll()
        {
            var c = ds.Tenants.OrderBy(a => a.LastName);

            var tenants = Mapper.Map<IEnumerable<TenantBase>>(c);

            foreach(TenantBase tenant in tenants)
            {
                var leaseInfo = ds.Leases.Include("Apartment").SingleOrDefault(a => a.Tenant.Id == tenant.Id);
                var leaseMap = Mapper.Map<LeaseForTenant>(leaseInfo);
                if(leaseInfo != null)
                {
                    tenant.Lease = leaseMap;
                }               
            }

            return tenants;
        }

        // GET ALL TENANTS NOT ASSOCIATED WITH LEASE
        public IEnumerable<TenantBase> TenantGetAllNotLease()
        {
            var leases = ds.Leases.OrderBy(a => a.Id);

            var c = ds.Tenants.Where(p => leases.All(p2 => p2.Tenant.Id != p.Id));

            return (c == null) ? null : Mapper.Map<IEnumerable<TenantBase>>(c);

        }

        public TenantBase TenantGetById(int id)
        {
            var c = ds.Tenants.SingleOrDefault(a => a.Id == id);  
            
            var leaseInfo = ds.Leases.Include("Apartment").SingleOrDefault(a => a.Tenant.Id == id);
            var leaseMap = Mapper.Map<LeaseForTenant>(leaseInfo);

            var newTenant = Mapper.Map<TenantBase>(c);
            newTenant.Lease = leaseMap;

            return (c == null) ? null : newTenant;
        }

        // ADD ACTIVATION CODE FOR TENANT REGISTRATION
        public string TenantAddCode(int id)
        {
            var c = ds.Tenants.SingleOrDefault(a => a.Id == id);

            var hashPassword = GeneratePassword();

            c.ActivationCode = hashPassword;

            ds.SaveChanges();

            return (c == null) ? null : hashPassword;
        }

        public TenantBase TenantGetByEmail(string email)
        {
            var c = ds.Tenants.SingleOrDefault(a => a.Email == email);

            return (c == null) ? null : Mapper.Map<TenantBase>(c);
        }

        public string TenantGetActivationCode(string email)
        {
            var c = ds.Tenants.SingleOrDefault(a => a.Email == email);

            return (c == null) ? null : c.ActivationCode;
        }

        public TenantBase TenantAdd(TenantAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }
            var addedItem = ds.Tenants.Add(Mapper.Map<Tenant>(newItem));
            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<TenantBase>(addedItem);
        }

        public TenantBase TenantEdit(TenantEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.Tenants.SingleOrDefault(e => e.Id == editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<TenantBase>(storedItem);
            }
        }

        public void TenantDelete(int id)
        {
            var storedItem = ds.Tenants.Find(id);
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.Tenants.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        //************************************** LEASE SECTION ***********************************************************

        public IEnumerable<LeaseWithBasicInformation> LeaseGetAllWithInformation()
        {
            var s = ds.Leases.Include("Tenant").Include("Apartment").OrderBy(j => j.Id);    

            return Mapper.Map<IEnumerable<LeaseWithBasicInformation>>(s);
        }

        public LeaseWithBasicInformation LeaseGetByIdWithInformation(int id)
        {
            var o = ds.Leases.Include("Tenant").Include("Apartment").SingleOrDefault(j => j.Id == id);
            return (o == null) ? null : Mapper.Map<LeaseWithBasicInformation>(o);
        }

        public LeaseBase LeaseGetByAptNumber(int id)
        {
            var o = ds.Leases.SingleOrDefault(j => j.Apartment.ApartmentNumber == id);
            return (o == null) ? null : Mapper.Map<LeaseBase>(o);
        }

        public LeaseWithInformation LeaseGetByTenantId(int? id)
        {
            var o = ds.Leases.Include("Tenant").SingleOrDefault(j => j.Tenant.Id == id);
            return (o == null) ? null : Mapper.Map<LeaseWithInformation>(o);
        }

        public LeaseWithBasicInformation LeaseGetByTenantEmail(string email)
        {
            var o = ds.Leases.Include("Apartment").SingleOrDefault(j => j.Tenant.Email == email);
            return (o == null) ? null : Mapper.Map<LeaseWithBasicInformation>(o);
        }

        public LeaseBase LeaseAdd(LeaseAdd newItem)
        {    
            if (newItem == null)
            {
                return null;
            }
            else
            {
      
                var associatedApartment = ds.Apartments.Find(newItem.ApartmentNumber);
                if (associatedApartment == null)
                {
                    return null;
                }

                var associatedTenant = ds.Tenants.Find(newItem.TenantId);
                if (associatedTenant == null)
                {
                    return null;
                }
       
                Lease addedItem = Mapper.Map<Lease>(newItem);

                addedItem.Apartment = associatedApartment;               
                addedItem.Tenant = associatedTenant;
                ds.Leases.Add(addedItem);

                var editedApt = Mapper.Map<ApartmentBase>(associatedApartment);
                editedApt.Status = "Occupied";
                ds.Entry(associatedApartment).CurrentValues.SetValues(editedApt);

                ds.SaveChanges();

                return Mapper.Map<LeaseBase>(addedItem);
            }
        }

        public LeaseBase LeaseEdit(LeaseEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }

            var storedItem = ds.Leases.Find(editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<LeaseBase>(storedItem);
            }
        }

        public void LeaseDelete(int id)
        {
            var storedItem = ds.Leases.Find(id);

            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    var associatedApartment = ds.Apartments.Find(storedItem.Apartment.ApartmentNumber);
                    var editedApt = Mapper.Map<ApartmentBase>(associatedApartment);
                    editedApt.Status = "Vacant";
                    ds.Entry(associatedApartment).CurrentValues.SetValues(editedApt);

                    ds.Leases.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        //****************************** WORK ORDER SECTION *******************************************************************

        public IEnumerable<WorkOrderWithTenant> WorkOrderGetAll()
        {
            var c = ds.WorkOrders.Include("Tenant").OrderBy(a => a.RequestDate);

            var workOrders = Mapper.Map<IEnumerable<WorkOrderWithTenant>>(c);

            foreach (WorkOrderWithTenant workOrder in workOrders)
            {
                var leaseInfo = ds.Leases.Include("Apartment").SingleOrDefault(a => a.Tenant.Id == workOrder.Tenant.Id);
                var leaseMap = Mapper.Map<LeaseForTenant>(leaseInfo);
                if(leaseInfo != null)
                {
                    workOrder.Tenant.Lease = leaseMap;
                }
                
            }

            return workOrders;
        }

        public WorkOrderBase WorkOrderGetById(int id)
        {
            var c = ds.WorkOrders.SingleOrDefault(a => a.Id == id);

            return (c == null) ? null : Mapper.Map<WorkOrderBase>(c);
        }

        public IEnumerable<WorkOrderBase> WorkOrderGetByTenantId(int id)
        {
            var c = ds.WorkOrders.Where(a => a.Tenant.Id == id);

            return (c == null) ? null : Mapper.Map<IEnumerable<WorkOrderBase>>(c);
        }

        public WorkOrderBase WorkOrderAdd(WorkOrderAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }

            var associatedTenant = ds.Tenants.Find(newItem.TenantId);
            if (associatedTenant == null)
            {
                return null;
            }

            WorkOrder addedItem = Mapper.Map<WorkOrder>(newItem);  
            
            var today = DateTime.Now;

            addedItem.Tenant = associatedTenant;
            addedItem.RequestDate = today;

            ds.WorkOrders.Add(addedItem);
            ds.SaveChanges();

            return Mapper.Map<WorkOrderBase>(addedItem);
        }

        public WorkOrderBase WorkOrderEdit(WorkOrderEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.WorkOrders.SingleOrDefault(e => e.Id == editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<WorkOrderBase>(storedItem);
            }
        }

        public void WorkOrderDelete(int id)
        {
            var storedItem = ds.WorkOrders.Find(id);
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.WorkOrders.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

        // ******************** OCCUPANTS SECTION *********************************************************************
        public IEnumerable<OccupantBase> OccupantGetAll()
        {
            var c = ds.Occupants.OrderBy(a => a.ApartmentNumber);

            return Mapper.Map<IEnumerable<OccupantBase>>(c);
        }

        public OccupantBase OccupantGetById(int id)
        {
            var c = ds.Occupants.SingleOrDefault(a => a.Id == id);         

            return (c == null) ? null : Mapper.Map<OccupantBase>(c);
        }

        public OccupantBase OccupantAdd(OccupantAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }

            var associatedTenant = ds.Tenants.Find(newItem.TenantId);
            if (associatedTenant == null)
            {
                return null;
            }

            var associatedApartment = ds.Apartments.Find(newItem.ApartmentNumber);
            if (associatedApartment == null)
            {
                return null;
            }

            Occupant addedItem = Mapper.Map<Occupant>(newItem);

            addedItem.Tenant = associatedTenant; 

            ds.Occupants.Add(addedItem);
            ds.SaveChanges();
   
            return (addedItem == null) ? null : Mapper.Map<OccupantBase>(addedItem);
        }

        public OccupantBase OccupantEdit(OccupantEdit editedItem)
        {
            if (editedItem == null)
            {
                return null;
            }
            var storedItem = ds.Occupants.SingleOrDefault(e => e.Id == editedItem.Id);

            if (storedItem == null)
            {
                return null;
            }
            else
            {
                ds.Entry(storedItem).CurrentValues.SetValues(editedItem);
                ds.SaveChanges();

                return Mapper.Map<OccupantBase>(storedItem);
            }
        }

        public void OccupantDelete(int id)
        {
            var storedItem = ds.Occupants.Find(id);
            if (storedItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    ds.Occupants.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

    }
}