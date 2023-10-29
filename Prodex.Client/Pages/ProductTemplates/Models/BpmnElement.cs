using System.Text.Json.Serialization;

namespace Prodex.Client.Pages.ProductTemplates.Models;

public class BpmnElement
{
    public string Id { get; set; }
    public string Type { get; set; }
    public SourceRef SourceRef { get; set; }

    public ElementType TypeEnum => Type switch
    {
        "bpmn:ServiceTask" => ElementType.ServiceTaks,
        "bpmn:SendTask" => ElementType.SendTask,
        "bpmn:UserTask" => ElementType.UserTask,
        "bpmn:ExclusiveGateway" => ElementType.ExclusiveGateway,
        "bpmn:SequenceFlow" => ElementType.SequenceFlow,

        _ => ElementType.None
    };
}

public class SourceRef
{
    [JsonPropertyName("$type")]
    public string Type { get; set; }
    public string Id { get; set; }

    public ElementType TypeEnum => Type switch
    {
        "bpmn:ServiceTask" => ElementType.ServiceTaks,
        "bpmn:SendTask" => ElementType.SendTask,
        "bpmn:UserTask" => ElementType.UserTask,
        "bpmn:ExclusiveGateway" => ElementType.ExclusiveGateway,
        "bpmn:SequenceFlow" => ElementType.SequenceFlow,

        _ => ElementType.None
    };
}

public enum ElementType
{
    None,

    ServiceTaks,
    UserTask,
    SendTask,
    ExclusiveGateway,
    SequenceFlow
}