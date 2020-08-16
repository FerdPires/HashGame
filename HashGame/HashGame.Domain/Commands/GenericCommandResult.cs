using HashGame.Domain.Commands.Contracts;

namespace HashGame.Domain.Commands
{
    public class GenericCommandResult : ICommandResult
    {
        public GenericCommandResult() { }

        public GenericCommandResult(bool Success, string Message, string Winner)
        {
            success = Success;
            msg = Message;
            winner = Winner;
        }

        public bool success { get; set; }
        public string msg { get; set; }
        public string winner { get; set; }
    }
}