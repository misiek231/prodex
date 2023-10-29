using MediatR;

namespace Prodex.Processes;

public class CheckConditionsRequest : IRequest<CheckConditionsResult>
{
    public IProcessable Processable { get; set; }
    public List<string> FlowsToChek { get; set; }


    public CheckConditionsRequest(IProcessable processable, List<string> flowsToChek)
    {
        Processable = processable;
        FlowsToChek = flowsToChek;
    }
}

public class CheckConditionsResult
{
    public List<string> EnabledFlows { get; set; }
}
