using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;


namespace PropertyManager.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618
            //Announcement
            Mapper.CreateMap<Controllers.AnnouncementAdd, Models.Announcement>();
            Mapper.CreateMap<Models.Announcement, Controllers.AnnouncementBase>();
            Mapper.CreateMap<Controllers.AnnouncementEdit, Controllers.AnnouncementBase>();

            //Employee
            Mapper.CreateMap<Controllers.EmployeeAdd, Models.Employee>();
            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase>();
            Mapper.CreateMap<Controllers.EmployeeEdit, Controllers.EmployeeBase>();

            //Inventory
            Mapper.CreateMap<Controllers.InventoryAdd, Models.Inventory>();
            Mapper.CreateMap<Models.Inventory, Controllers.InventoryBase>();
            Mapper.CreateMap<Controllers.InventoryEdit, Controllers.InventoryBase>();

            //Facility
            Mapper.CreateMap<Controllers.FacilityAdd, Models.Facility>();
            Mapper.CreateMap<Models.Facility, Controllers.FacilityBase>();
            Mapper.CreateMap<Controllers.FacilityEdit, Controllers.FacilityBase>();

            //Facility Booking
            Mapper.CreateMap<Controllers.FacilityBookingAdd, Models.FacilityBooking>();
            Mapper.CreateMap<Models.FacilityBooking, Controllers.FacilityBookingBase>();
            Mapper.CreateMap<Controllers.FacilityBookingEdit, Controllers.FacilityBookingBase>();

            //Service
            Mapper.CreateMap<Controllers.ServiceAdd, Models.Service>();
            Mapper.CreateMap<Models.Service, Controllers.ServiceBase>();
            Mapper.CreateMap<Models.Service, Controllers.ServiceWithServiceRequests>();
            Mapper.CreateMap<Models.Service, Controllers.ServiceForServiceRequest>(); 
            Mapper.CreateMap<Controllers.ServiceEdit, Controllers.ServiceBase>();

            //Service Request
            Mapper.CreateMap<Models.ServiceRequest, Controllers.ServiceRequestBase>();
            Mapper.CreateMap<Models.ServiceRequest, Controllers.ServiceRequestWithService>();         
            Mapper.CreateMap<Controllers.ServiceRequestAdd, Models.ServiceRequest>();
            Mapper.CreateMap<Controllers.ServiceRequestEdit, Controllers.ServiceRequestBase>();

            //Unit
            Mapper.CreateMap<Controllers.UnitAdd, Models.Unit>();
            Mapper.CreateMap<Models.Unit, Controllers.UnitBase>();
            Mapper.CreateMap<Controllers.UnitEdit, Controllers.UnitBase>();
            Mapper.CreateMap<Models.Unit, Controllers.UnitForApartment>();

            //User
            Mapper.CreateMap<Models.ApplicationUser, Controllers.UserBase>();

            //UnitPhoto
            Mapper.CreateMap<Controllers.UnitPhotoAdd, Models.UnitPhoto>();
            Mapper.CreateMap<Models.UnitPhoto, Controllers.UnitPhotoBase>();
            Mapper.CreateMap<Controllers.UnitPhotoBase, Controllers.UnitPhotoEdit>();

            //Apartment
            Mapper.CreateMap<Controllers.ApartmentAdd, Models.Apartment>();
            Mapper.CreateMap<Models.Apartment, Controllers.ApartmentBase>();
            Mapper.CreateMap<Models.Apartment, Controllers.ApartmentForLease>();
            Mapper.CreateMap<Controllers.ApartmentEdit, Controllers.ApartmentBase>();
            Mapper.CreateMap<Models.Apartment, Controllers.ApartmentWithUnit>();

            //Tenant
            Mapper.CreateMap<Controllers.TenantAdd, Models.Tenant>();
            Mapper.CreateMap<Models.Tenant, Controllers.TenantBase>();
            Mapper.CreateMap<Models.Tenant, Controllers.TenantForLease>();
            Mapper.CreateMap<Controllers.TenantEdit, Controllers.TenantBase>();

            //Lease
            Mapper.CreateMap<Models.Lease, Controllers.LeaseBase>();
            Mapper.CreateMap<Models.Lease, Controllers.LeaseWithInformation>();
            Mapper.CreateMap<Models.Lease, Controllers.LeaseWithBasicInformation>();
            Mapper.CreateMap<Models.Lease, Controllers.LeaseForTenant>();
            Mapper.CreateMap<Controllers.LeaseAdd, Models.Lease>();
            Mapper.CreateMap<Controllers.LeaseEdit, Controllers.LeaseBase>();

            //WorkOrder
            Mapper.CreateMap<Controllers.WorkOrderAdd, Models.WorkOrder>();
            Mapper.CreateMap<Models.WorkOrder, Controllers.WorkOrderBase>();
            Mapper.CreateMap<Models.WorkOrder, Controllers.WorkOrderWithTenant>();
            Mapper.CreateMap<Controllers.WorkOrderEdit, Controllers.WorkOrderBase>();

            //Occupant
            Mapper.CreateMap<Controllers.OccupantAdd, Models.Occupant>();
            Mapper.CreateMap<Models.Occupant, Controllers.OccupantBase>();
            Mapper.CreateMap<Controllers.OccupantEdit, Controllers.OccupantBase>();

#pragma warning restore CS0618
        }
    }
}