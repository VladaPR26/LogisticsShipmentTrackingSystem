using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Models.Entities;

namespace Application.Services.Shipments.Responses;
public class ShipmentResponse
{
    public List<Shipment> Shipments { get; set; }=new List<Shipment>();
}
