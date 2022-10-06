namespace Lapka.Messages.Api.Requests;

public record SendMessageRequest(Guid ReceiverId,string Content);