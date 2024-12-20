using Presentation.Models.Enums;

namespace Presentation.Models.Requests;

public class ShipmentRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public DeliveryState DeliveryState { get; set; }
    public DateTime DeliveryDate { get; set; }
}
