using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Contracts;

using DataBase.Repositories.Contracts;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Shipments;
public class ShipmentService:IShipmentService
{
    private readonly IShipmentRepository _shipmentRepository;
    public ShipmentService(IShipmentRepository shipmentRepository)
    {
        _shipmentRepository = shipmentRepository;
    }

}
