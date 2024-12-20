using Presentation.Models.Enums;

namespace Presentation.Models.Entities;

public class Shipment
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DeliveryState DeliveryState { get; set; }
    public DateTime CreatingDate { get; set; }
    public DateTime DeliveryDate { get; set; }
}
