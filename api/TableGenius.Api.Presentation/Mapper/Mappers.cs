using AutoMapper;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Entities.Reservations;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Mapper;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<Location, LocationRm>();
        CreateMap<LocationRm, Location>();

        CreateMap<LocationAssignment, LocationAssignmentRm>();
        CreateMap<LocationAssignmentRm, LocationAssignment>();

        CreateMap<Area, AreaRm>();
        CreateMap<AreaRm, Area>();

        CreateMap<Table, TableRm>();
        CreateMap<Table, TableWithStatusRm>();
        CreateMap<TableRm, Table>();

        CreateMap<Reservation, ReservationRm>();
        CreateMap<ReservationRm, Reservation>();

        CreateMap<AreaSlot, AreaSlotRm>();
        CreateMap<AreaSlotRm, AreaSlot>();

        CreateMap<ReservationAssignment, ReservationAssignmentRm>();
        CreateMap<ReservationAssignment, ReservationAssignmentInfoRm>();
        CreateMap<ReservationAssignmentRm, ReservationAssignment>();

        CreateMap<TableReservationAssignment, TableReservationAssignmentRm>();
        CreateMap<TableReservationAssignment, TableReservationAssignmentInfoRm>();
        CreateMap<TableReservationAssignmentRm, TableReservationAssignment>();
    }
}