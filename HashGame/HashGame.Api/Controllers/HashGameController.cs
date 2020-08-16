using Microsoft.AspNetCore.Mvc;
using HashGame.Domain.Commands;
using HashGame.Domain.Handlers;

namespace HashGame.Api.Controllers
{
    [ApiController]
    [Route("v1/hashgame")]
    public class HashGameController : ControllerBase
    {

        [Route("new-game")]
        [HttpPost]
        public CreateGameCommandResult Create(
            [FromServices] GameHandler handler
        )
        {
            return (CreateGameCommandResult)handler.NewGame();
        }

        [Route("make-movement")]
        [HttpPost]
        public GenericCommandResult MakeMovement(
            [FromBody] MakeMovementCommand command,
            [FromServices] GameHandler handler
        )
        {
            return (GenericCommandResult)handler.Handle(command);
        }
    }
}