using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands;

public record UserOfflineCommand(Guid PrincipalId) : ICommand;