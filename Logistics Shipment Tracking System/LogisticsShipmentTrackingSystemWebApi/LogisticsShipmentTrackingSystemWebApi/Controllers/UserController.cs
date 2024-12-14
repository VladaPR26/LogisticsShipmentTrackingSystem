using Application.Contracts;
using Application.Services.Users.Requests;

using Microsoft.AspNetCore.Mvc;

namespace LogisticsShipmentTrackingSystemWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost("login")]
    public ActionResult Login(LoginRequest request)
    {
        var response = _userService.Login(request);
        return Ok(response);
    }
}
