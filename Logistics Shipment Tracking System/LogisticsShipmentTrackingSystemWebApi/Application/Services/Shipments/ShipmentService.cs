using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Contracts;
using Application.Services.Shipments.Requests;
using Application.Services.Shipments.Responses;

using DataBase.Repositories.Contracts;

using Domain.Models.Entities;

using Microsoft.Extensions.Configuration;

namespace Application.Services.Shipments;
public class ShipmentService:IShipmentService
{
    private readonly IShipmentRepository _shipmentRepository;
    public ShipmentService(IShipmentRepository shipmentRepository)
    {
        _shipmentRepository = shipmentRepository;
    }

    public ShipmentResponse GetAll()
    {
        List<Shipment> shipmetns= _shipmentRepository.GetAll().ToList();
        ShipmentResponse response = new ShipmentResponse()
        {
            Shipments = shipmetns
        };

        return response;
    }

    public Shipment Get(Guid id) 
    {
        try
        {
            return _shipmentRepository.Get(id);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Shipment with this id does not exists");
        }
    }

    public Shipment CreateShipment(ShipmentRequest request)
    {
        Shipment shipment = new Shipment(Guid.NewGuid(),request.Name,request.DeliveryState,DateTime.Now,request.DeliveryDate);
        return _shipmentRepository.CreateShipment(shipment);
    }


    public void UpdateShipment(ShipmentUpdateRequest request)
    {
        try
        {
            Shipment shipment = _shipmentRepository.Get(request.Id);
            shipment.Name = request.Name;
            shipment.DeliveryState = request.DeliveryState;
            shipment.DeliveryDate = request.DeliveryDate;

        }catch (Exception ex)
        {
            throw new ArgumentException("Shipment with this id does not exists");
        }
    }


    public void Delete(Guid id)
    {
        try
        {
            Shipment shipment= _shipmentRepository.Get(id);
            _shipmentRepository.Delete(id);
        }catch(Exception ex)
        {
            throw new ArgumentException("Shipment with this id does not exists");
        }
    }

}
