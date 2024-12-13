using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Models.Enums;

namespace Domain.Models.Entities;
public class Shipment
{
    public Guid Id {  get; set; }
    public string Name { get; set; } = String.Empty;
    public DeliveryState DeliveryState { get; set; }
    public DateTime CreatingDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public Shipment(Guid id, string name, DeliveryState deliveryState, DateTime creatingDate, DateTime deliveryDate)
    {
        Id = id;
        Name = name;
        DeliveryState = deliveryState;
        CreatingDate = creatingDate;
        DeliveryDate = deliveryDate;
    }
}
