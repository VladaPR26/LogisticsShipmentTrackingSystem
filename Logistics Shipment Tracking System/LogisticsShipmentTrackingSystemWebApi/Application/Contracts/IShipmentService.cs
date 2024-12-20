using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Services.Shipments.Requests;
using Application.Services.Shipments.Responses;

using Domain.Models.Entities;

namespace Application.Contracts;
public interface IShipmentService
{
    ShipmentResponse GetAll();
    Shipment Get(Guid id);
    Shipment CreateShipment(ShipmentRequest request);
    void UpdateShipment(ShipmentRequest request);
    void Delete(Guid id);
}
