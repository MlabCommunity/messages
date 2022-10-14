namespace Lapka.Messages.Core.Entities;

public class Worker
{
    public Guid WorkerId { get; private set; }
    public Guid ShelterId { get; private set; }

    private Worker()
    {
    }
    
    public Worker(Guid workerId, Guid shelterId)
    {
        WorkerId = workerId;
        ShelterId = shelterId;
    }
}