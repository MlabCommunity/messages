﻿using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands;

public record UserOnlineCommand(Guid PrincipalId) : ICommand;