using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities;
public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }=String.Empty;
    public string Password { get; set; } = String.Empty;

    public User(Guid id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }
}
