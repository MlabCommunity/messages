using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands.Handlers;

public record UserOnlineCommand(Guid PrincipalId) : ICommand;
