using CSharpFunctionalExtensions;

namespace Application.Service.SeedWork
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }
}