using Application.DTOs.Input;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("v1/api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserInputModel createUser)
        {
            try
            {
                await _userService.CreateUser(createUser);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

    }
}

/*
{
  "email": "teste",
  "friendsList": null,
  "preferences": {
    "fun": {
      "Bar": 1,
      "Show": 2,
      "Cinema": 3,
      "Casa": 4,
      "Restaurante":5
    },
    "hobbie": {
      "Ler": 1,
      "Filmes": 2,
      "Series": 3,
      "Esportes":4
    }
  }
}
*/