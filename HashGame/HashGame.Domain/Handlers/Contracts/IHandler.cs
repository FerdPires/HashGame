using HashGame.Domain.Commands.Contracts;

namespace HashGame.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
