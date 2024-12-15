using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Models.Entities;

namespace DataBase.Repositories.Contracts;
public interface IShipmentRepository
{
    IEnumerable<Shipment> GetAll();
    Shipment Get(Guid id);
    Shipment CreateShipment(Shipment shipment);
    void Delete(Guid id);
}
