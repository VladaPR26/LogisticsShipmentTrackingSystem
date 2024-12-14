using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataBase.Repositories.Contracts;

using Domain.Models.Entities;

namespace DataBase.Repositories;
public class UserRepository : IUserRepository
{
    public User GetUserByUsername(string username)
    {
        
        var user = DataBase.Users.Where(user => user.Value.Username == username).FirstOrDefault();


        // Proveri da li je user null
        if (user.Equals(default(KeyValuePair<Guid, User>)))
        {
            return null; 
        }

        return user.Value;
    }
}
