using System;
using HashGame.Domain.Commands.Contracts;

namespace HashGame.Domain.Commands
{
    public class CreateGameCommandResult : ICommandResult
    {
        public CreateGameCommandResult() { }

        public CreateGameCommandResult(bool Success, string Message, Guid Id, string first_player)
        {
            success = Success;
            msg = Message;
            id = Id;
            firstPlayer = first_player;
        }

        public bool success { get; set; }
        public string msg { get; set; }
        public Guid id { get; set; }
        public string firstPlayer { get; set; }
    }
}