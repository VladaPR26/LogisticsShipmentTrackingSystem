using Application.Contracts;
using Application.Services.Shipments.Requests;
using Application.Services.Shipments.Responses;

using Domain.Models.Entities;

using FluentValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsShipmentTrackingSystemWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ShipmentController : ControllerBase
{
    private readonly IShipmentService _shipmentService;
    private readonly IValidator<ShipmentRequest> _shipmentRequestValidator;
    public ShipmentController(IShipmentService shipmentService,IValidator<ShipmentRequest> shipmentRequestValidator)
    {
        _shipmentService = shipmentService;
        _shipmentRequestValidator = shipmentRequestValidator;
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        ShipmentResponse response= _shipmentService.GetAll();
        return Ok(response);
    }

    [HttpGet("{id:Guid}")]
    public ActionResult Get(Guid id)
    {
        Shipment response = _shipmentService.Get(id);
        return Ok(response);
    }

    [HttpPost("create")]
    [Authorize]
    public ActionResult CreateShipment(ShipmentRequest request)
    {
        var validationResult = _shipmentRequestValidator.Validate(request);
        Shipment shipment = _shipmentService.CreateShipment(request);
        return CreatedAtAction(nameof(CreateShipment), shipment);
    }

    [HttpPut("update")]
    [Authorize]
    public ActionResult UpdateShipment(ShipmentRequest request)
    {
        var validationResult = _shipmentRequestValidator.Validate(request);
        _shipmentService.UpdateShipment(request);
        return NoContent();
    }


    [HttpDelete("{id:Guid}")]
    [Authorize]
    public ActionResult Delete(Guid id) 
    {
        _shipmentService.Delete(id);
        return NoContent();
    }

    
}
