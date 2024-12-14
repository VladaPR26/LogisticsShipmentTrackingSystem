using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Services.Users.Requests;
using Application.Services.Users.Responses;


namespace Application.Contracts;
public interface IUserService
{
    LoginResponse Login(LoginRequest request);
}
