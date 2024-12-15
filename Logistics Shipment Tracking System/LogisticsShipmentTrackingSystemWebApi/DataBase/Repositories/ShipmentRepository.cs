using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataBase.Repositories.Contracts;

using Domain.Models.Entities;

namespace DataBase.Repositories;
public class ShipmentRepository : IShipmentRepository
{

    public Shipment Get(Guid id)
    {
        return DataBase.Shipments[id];
    }

    public IEnumerable<Shipment> GetAll()
    {
        return DataBase.Shipments.Values.ToList();
    }
    public Shipment CreateShipment(Shipment shipment)
    {
        DataBase.Shipments.Add(shipment.Id,shipment);
        return shipment;
    }
    public void Delete(Guid id) 
    { 
        DataBase.Shipments.Remove(id);
    }
}
