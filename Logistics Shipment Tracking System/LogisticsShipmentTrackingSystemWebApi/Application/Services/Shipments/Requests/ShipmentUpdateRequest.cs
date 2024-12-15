using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Models.Enums;

namespace Application.Services.Shipments.Requests;
public class ShipmentUpdateRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DeliveryState DeliveryState { get; set; }
    public DateTime DeliveryDate { get; set; }
}
