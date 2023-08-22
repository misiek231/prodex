namespace Prodex.Client.Pages.ProductTemplates.Models;

public class BpmnElement
{
    public string Id { get; set; }
    public string Type { get; set; }

    public ElementType TypeEnum => Type switch
    {
        "bpmn:ServiceTask" => ElementType.ServiceTaks,
        "bpmn:SendTask" => ElementType.SendTask,
        "bpmn:UserTask" => ElementType.UserTask,

        _ => ElementType.None
    };
}


public enum ElementType
{
    None,

    ServiceTaks,
    UserTask,
    SendTask
}