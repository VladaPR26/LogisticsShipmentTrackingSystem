using System.Text.Json.Serialization;

using Presentation.Models.Entities;

namespace Presentation.Models.Responses;

public class ShipmentResponse
{
    public List<Shipment> Shipments { get; set; } = new List<Shipment>();
}
