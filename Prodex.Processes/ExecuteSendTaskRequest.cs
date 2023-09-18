using MediatR;

namespace Prodex.Processes;

public class ExecuteSendTaskRequest : IRequest<object>
{
    public IProcessable Processable { get; set; }
    public ProcessStep ExecutedStep { get; set; }
    public long UserId { get; set; }

    public ExecuteSendTaskRequest(IProcessable processable, ProcessStep executedStep, long userId)
    {
        Processable = processable;
        ExecutedStep = executedStep;
        UserId = userId;
    }
}
