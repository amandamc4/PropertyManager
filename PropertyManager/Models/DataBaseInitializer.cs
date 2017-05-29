
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using PropertyManager.Controllers;

namespace PropertyManager.Models
{
    public class DataBaseInitializer
    {
        public static async void LoadUserAccounts()
        {
            var ds = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ds));

            if (userManager.Users.Count() == 0)
            {
                var user = new ApplicationUser
                {
                    UserName = "propertycloudadm@gmail.com",
                    Email = "propertycloudadm@gmail.com",
                    GivenName = "Administrator",
                    Surname = "Adm",
                    Role = "Administrator"
                };

                var result = await userManager.CreateAsync(user, "PropertyApp123");
                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "Administrator"));
                }

                var manager = new ApplicationUser
                {
                    UserName = "propertycloudmanager@gmail.com",
                    Email = "propertycloudmanager@gmail.com",
                    GivenName = "Amanda",
                    Surname = "Cruz",
                    Role = "Manager"
                };
                var result2 = await userManager.CreateAsync(manager, "PropertyApp123");
                if (result2.Succeeded)
                {
                    await userManager.AddClaimAsync(manager.Id, new Claim(ClaimTypes.Role, "Manager"));
                }
            }
        }

        public static async void LoadSampleData()
        {
            Manager m = new Controllers.Manager();

            if (m.UnitGetAll().Count() == 0)
            {
                var unit = new UnitAdd();

                unit.Bedrooms = 2;
                unit.Bathrooms = 1;
                unit.SquareFeet = 100.5;
                unit.MaxOccupants = 3;
                unit.Balcony = true;
                unit.Dishwasher = true;
                unit.Laundry = false;
                m.UnitAdd(unit);

                unit.Bedrooms = 1;
                unit.Bathrooms = 1;
                unit.SquareFeet = 87.5;
                unit.MaxOccupants = 2;
                unit.Balcony = true;
                unit.Dishwasher = false;
                unit.Laundry = false;
                m.UnitAdd(unit);

                unit.Bedrooms = 3;
                unit.Bathrooms = 2;
                unit.SquareFeet = 130.0;
                unit.MaxOccupants = 5;
                unit.Balcony = true;
                unit.Dishwasher = true;
                unit.Laundry = true;
                m.UnitAdd(unit);
            }

            if(m.ApartmentGetAll().Count() == 0)
            {
                var apt = new ApartmentAdd();

                apt.ApartmentNumber = 520;
                apt.FloorNumber = 5;
                apt.Status = "Occupied";
                apt.UnitId = 1;

                m.ApartmentAdd(apt);

                apt.ApartmentNumber = 603;
                apt.FloorNumber = 6;
                apt.Status = "Occupied";
                apt.UnitId = 2;

                m.ApartmentAdd(apt);

                apt.ApartmentNumber = 1705;
                apt.FloorNumber = 17;
                apt.Status = "Occupied";
                apt.UnitId = 3;

                m.ApartmentAdd(apt);
            }

            if(m.TenantGetAll().Count() == 0)
            {
                var tenant = new TenantAdd();

                tenant.FirstName = "Amanda";
                tenant.LastName = "Marques";
                tenant.MobilePhone = "647-535-7732";
                tenant.HomePhone = "";
                tenant.Email = "amanda.mc4@hotmail.com";
                tenant.BirthDate = new DateTime(1988, 12, 23);
                m.TenantAdd(tenant);

                tenant.FirstName = "Jonathan";
                tenant.LastName = "Desmond";
                tenant.MobilePhone = "536-85-96415";
                tenant.HomePhone = "365-459-8752";
                tenant.Email = "jonathandesmond91@gmail.com";
                tenant.BirthDate = new DateTime(1990, 10, 18);
                m.TenantAdd(tenant);

                tenant.FirstName = "Carlos";
                tenant.LastName = "Wellinton";
                tenant.MobilePhone = "963-125-4789";
                tenant.HomePhone = "964-585-3658";
                tenant.Email = "carlos@tenant.ca";
                tenant.BirthDate = new DateTime(1994, 09, 21);
                m.TenantAdd(tenant);
            }

            if(m.LeaseGetAllWithInformation().Count() == 0)
            {
                var lease = new LeaseAdd();
                lease.StartDate = new DateTime(2016, 01, 01);
                lease.EndDate = new DateTime(2018, 01, 01);
                lease.SecurityDeposit = 500.00;
                lease.MonthlyRent = 1050.60;
                lease.ApartmentNumber = 520;
                lease.TenantId = 1;

                m.LeaseAdd(lease);
    
                lease.StartDate = new DateTime(2017, 08, 20);
                lease.EndDate = new DateTime(2018, 08, 19);
                lease.SecurityDeposit = 430.00;
                lease.MonthlyRent = 1200.50;
                lease.ApartmentNumber = 603;
                lease.TenantId = 2;

                m.LeaseAdd(lease);

                lease.StartDate = new DateTime(2016, 04, 01);
                lease.EndDate = new DateTime(2017, 04, 01);
                lease.SecurityDeposit = 525.00;
                lease.MonthlyRent = 1400.80;
                lease.ApartmentNumber = 1705;
                lease.TenantId = 3;

                m.LeaseAdd(lease);

                var user = new ApplicationUser
                {
                    UserName = "amanda.mc4@hotmail.com",
                    Email = "amanda.mc4@hotmail.com",
                    GivenName = "Amanda",
                    Surname = "Marques",
                    Role = "Tenant"
                };

                var ds = new ApplicationDbContext();
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(ds));

                var result = await userManager.CreateAsync(user, "Asdf4321!");
                if (result.Succeeded)
                {
                    // Add claims                   
                    await userManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "Tenant"));
                }
            }

            if(m.EmployeeGetAll().Count() == 0)
            {
                var employee = new EmployeeAdd();
                employee.LastName = "Marques";
                employee.FirstName = "Amanda";
                employee.Title = "Supervisor";
                employee.BirthDate = new DateTime(1988, 12, 23);
                employee.HireDate = new DateTime(2017, 02, 06);
                employee.Address = "Wellesley St";
                employee.City = "Toronto";
                employee.State = "ON";
                employee.Country = "Canada";
                employee.PostalCode = "M4X-1G5";
                employee.Email = "amandamc@hotmail.com";
                employee.Phone = "647-589-5357";
                m.EmployeeAdd(employee);

                employee.LastName = "Desmond";
                employee.FirstName = "Jonathan";
                employee.Title = "Manager";
                employee.BirthDate = new DateTime(1990, 11, 16);
                employee.HireDate = new DateTime(2017, 01, 05);
                employee.Address = "Pond Rd";
                employee.City = "Toronto";
                employee.State = "ON";
                employee.Country = "Canada";
                employee.PostalCode = "M6H 1Y7";
                employee.Email = "jonathan@hotmail.com";
                employee.Phone = "647-896-5236";
                m.EmployeeAdd(employee);

                employee.LastName = "Capello";
                employee.FirstName = "Silvia";
                employee.Title = "Clerk";
                employee.BirthDate = new DateTime(1958, 01, 09);
                employee.HireDate = new DateTime(2016, 04, 28);
                employee.Address = "Aluisio St";
                employee.City = "Toronto";
                employee.State = "ON";
                employee.Country = "Canada";
                employee.PostalCode = "J7G 1S3";
                employee.Email = "silvia@hotmail.com";
                employee.Phone = "647-652-3105";
                m.EmployeeAdd(employee);
            }

            if(m.AnnouncementGetAll().Count() == 0)
            {
                var announce = new AnnouncementAdd();
                announce.Title = "Laundry closed";
                announce.StartDate = new DateTime(2017, 05, 05);
                announce.ExpireDate = new DateTime(2017, 05, 06);
                announce.Description = "The laundry room will be closed for maintenance";
                m.AnnouncementAdd(announce);
               
                announce.Title = "Cleaning of corridor";
                announce.StartDate = new DateTime(2017, 04, 20);
                announce.ExpireDate = new DateTime(2017, 04, 22);
                announce.Description = "The carpet will be cleaned on the upcoming weekend";
                m.AnnouncementAdd(announce);

                announce.Title = "New Recycle Bins";
                announce.StartDate = new DateTime(2017, 03, 10);
                announce.ExpireDate = new DateTime(2017, 03, 11);
                announce.Description = "The apartments will be receiving new recycle bins starting next week";
                m.AnnouncementAdd(announce);
            }

            if (m.FacilityGetAll().Count() == 0)
            {
                var facility = new FacilityAdd();
                facility.FacilityName = "Pool";
                facility.Description = "Open Pool";
                facility.Location = "40th floor";
                facility.Status = "Open";
                facility.OpenTime = new DateTime(2017, 01, 01, 08, 0, 0);
                facility.CloseTime = new DateTime(2017, 02, 01, 18, 0, 0);
                m.FacilityAdd(facility);

                facility.FacilityName = "Movie Room";
                facility.Description = "Movie Theater";
                facility.Location = "Lobby";
                facility.Status = "Open";
                facility.OpenTime = new DateTime(2017, 02, 01, 10, 0, 0);
                facility.CloseTime = new DateTime(2017, 02, 01, 20, 30, 0);
                m.FacilityAdd(facility);

            }

            if(m.InventoryGetAll().Count() == 0)
            {
                var inventory = new InventoryAdd();
                inventory.ProductName = "Cleaning";
                inventory.Supplier = "Lisol";
                inventory.Quantity = 20;
                m.InventoryAdd(inventory);

                inventory.ProductName = "Garbage Bag";
                inventory.Supplier = "My Garbages";
                inventory.Quantity = 100;
                m.InventoryAdd(inventory);

                inventory.ProductName = "Window cleaning";
                inventory.Supplier = "Windex";
                inventory.Quantity = 87;
                m.InventoryAdd(inventory);
            }      
            
            if(m.ServiceGetAll().Count() == 0)
            {
                var service = new ServiceAdd();
                service.ServiceName = "Gardening";
                service.CompanyName = "Gardening Express";
                service.PhoneNumber = "416-547-8963";
                service.Email = "gardexpress@test.ca";
                service.Address = "346 Allen Rd";
                m.ServiceAdd(service);

                service.ServiceName = "Pest Control";
                service.CompanyName = "Pest Rock";
                service.PhoneNumber = "647-514-8965";
                service.Email = "pest@test.ca";
                service.Address = "1549 Dufferin St";
                m.ServiceAdd(service);

                service.ServiceName = "Cleaning";
                service.CompanyName = "Nice Cleaning";
                service.PhoneNumber = "907-534-8698";
                service.Email = "niceclean@test.ca";
                service.Address = "15 Bloor St";
                m.ServiceAdd(service);
                
            }   
            
            if(m.WorkOrderGetAll().Count() == 0)
            {
                var workOrder = new WorkOrderAdd();
                workOrder.Description = "Fixing light bulb";
                workOrder.Notes = "In the living room";
                workOrder.RequestDate = new DateTime(2017, 02, 26);
                workOrder.TenantId = 1;
                m.WorkOrderAdd(workOrder);

                workOrder.Description = "Kill bugs";
                workOrder.Notes = "Many bugs in the house";
                workOrder.RequestDate = new DateTime(2017, 04, 05);
                workOrder.TenantId = 2;
                m.WorkOrderAdd(workOrder);

                workOrder.Description = "Leaking in the kitchen";
                workOrder.Notes = "My sink is full of water";
                workOrder.RequestDate = new DateTime(2017, 05, 22);
                workOrder.TenantId = 3;
                m.WorkOrderAdd(workOrder);
            }
            
            if(m.ServiceRequestGetAll().Count() == 0)
            {
                var request = new ServiceRequestAdd();
                request.Description = "Monthly Cleaning";
                request.RequestDate = new DateTime(2017, 03, 01);
                request.ServiceId = 3;
                m.ServiceRequestAdd(request);

                request.Description = "Doing gardening";
                request.RequestDate = new DateTime(2016, 05, 05);
                request.ServiceId = 1;
                m.ServiceRequestAdd(request);

                request.Description = "Building Pest Control";
                request.RequestDate = new DateTime(2017, 04, 23);
                request.ServiceId = 2;
                m.ServiceRequestAdd(request);
            }
            
            if(m.FacilityBookingGetAllWithFacility().Count() == 0)
            {
                var booking = new FacilityBookingAdd();

                booking.BookedDate = new DateTime(2017, 05, 22);
                booking.StartTime = new DateTime(2017, 05, 22, 10, 30, 0);
                booking.EndTime = new DateTime(2017, 05, 22, 17, 0, 0);
                booking.TenantId = 1;
                booking.FacilityId = 1;
                booking.Notes = "No Notes";
                m.FacilityBookingAdd(booking);

                booking.BookedDate = new DateTime(2017, 10, 01);
                booking.StartTime = new DateTime(2017, 10, 01, 13, 0, 0);
                booking.EndTime = new DateTime(2017, 10, 01, 14, 45, 0);
                booking.TenantId = 2;
                booking.FacilityId = 2;
                booking.Notes = "";
                m.FacilityBookingAdd(booking);

                booking.BookedDate = new DateTime(2017, 10, 01);
                booking.StartTime = new DateTime(2017, 10, 01, 15, 0, 0);
                booking.EndTime = new DateTime(2017, 10, 01, 18, 0, 0);
                booking.TenantId = 3;
                booking.FacilityId = 2;
                booking.Notes = "";
                m.FacilityBookingAdd(booking);

                booking.BookedDate = new DateTime(2017, 04, 05);
                booking.StartTime = new DateTime(2017, 04, 05, 10, 0, 0);
                booking.EndTime = new DateTime(2017, 04, 05, 13, 10, 0);
                booking.TenantId = 1;
                booking.FacilityId = 2;
                booking.Notes = "";
                m.FacilityBookingAdd(booking);
            }

            if (m.OccupantGetAll().Count() == 0)
            {
                var occupant = new OccupantAdd();

                occupant.FirstName = "Silvia";
                occupant.LastName = "Capello";
                occupant.MobilePhone = "647-415-8957";
                occupant.WorkPhone = "416-548-9569";
                occupant.Email = "silvia@test.ca";
                occupant.BirthDate = new DateTime(1958, 01, 09);
                occupant.ApartmentNumber = 520;
                occupant.TenantId = 1;
                m.OccupantAdd(occupant);

                occupant.FirstName = "Renan";
                occupant.LastName = "Marques";
                occupant.MobilePhone = "647-485-9874";
                occupant.WorkPhone = "416-123-5647";
                occupant.Email = "renan@test.ca";
                occupant.BirthDate = new DateTime(1990, 07, 02);
                occupant.ApartmentNumber = 520;
                occupant.TenantId = 1;
                m.OccupantAdd(occupant);

                occupant.FirstName = "Jennifer";
                occupant.LastName = "Aniston";
                occupant.MobilePhone = "905-452-7415";
                occupant.WorkPhone = "416-987-4256";
                occupant.Email = "jennifer@test.ca";
                occupant.BirthDate = new DateTime(1992, 03, 20);
                occupant.ApartmentNumber = 603;
                occupant.TenantId = 2;
                m.OccupantAdd(occupant);

                occupant.FirstName = "Mayra";
                occupant.LastName = "Borne";
                occupant.MobilePhone = "905-471-3589";
                occupant.WorkPhone = "416-416-8526";
                occupant.Email = "mayra@test.ca";
                occupant.BirthDate = new DateTime(2000, 05, 21);
                occupant.ApartmentNumber = 603;
                occupant.TenantId = 2;
                m.OccupantAdd(occupant);

                occupant.FirstName = "Tania";
                occupant.LastName = "Wellinton";
                occupant.MobilePhone = "905-965-7854";
                occupant.WorkPhone = "364-654-5984";
                occupant.Email = "tania@test.ca";
                occupant.BirthDate = new DateTime(1960, 03, 27);
                occupant.ApartmentNumber = 1705;
                occupant.TenantId = 3;
                m.OccupantAdd(occupant);


            }
        }
    }
}