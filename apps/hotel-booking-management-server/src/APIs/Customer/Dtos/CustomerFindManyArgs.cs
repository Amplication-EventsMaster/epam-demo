using HotelBookingManagement.APIs.Common;
using HotelBookingManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
