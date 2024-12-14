using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Models.Entities;
using Domain.Models.Enums;

namespace DataBase;
public static class DataBase
{
    public static Dictionary<Guid,User> Users { get; set; }
    public static Dictionary<Guid,Shipment> Shipments { get; set;}

    static DataBase()
    {
        Users = new Dictionary<Guid,User>();
        Shipments = new Dictionary<Guid,Shipment>();

        // Moking Users
        User user1 = new User(Guid.NewGuid(), "test1", BCrypt.Net.BCrypt.HashPassword("test1"));
        User user2 = new User(Guid.NewGuid(), "test2", BCrypt.Net.BCrypt.HashPassword("test2"));
        User user3 = new User(Guid.NewGuid(), "test3", BCrypt.Net.BCrypt.HashPassword("test3"));
        Users.Add(user1.Id,user1);
        Users.Add(user2.Id,user2);
        Users.Add(user3.Id,user3);

        //Moking Shipments
        Shipment shipment1=new Shipment(Guid.NewGuid(),"shipment1",DeliveryState.OnTheWay,DateTime.Now,new DateTime(2025,1,21));
        Shipment shipment2 = new Shipment(Guid.NewGuid(), "shipment2", DeliveryState.Delivered, DateTime.Now, new DateTime(2025, 2, 22));
        Shipment shipment3 = new Shipment(Guid.NewGuid(), "shipment3", DeliveryState.InWarehouse, DateTime.Now, new DateTime(2025, 3, 23));
        Shipments.Add(shipment1.Id,shipment1);
        Shipments.Add(shipment2.Id, shipment2);
        Shipments.Add(shipment3.Id, shipment3);
    }
}
