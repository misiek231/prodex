using System.Xml.Serialization;

namespace Prodex.Processes;

public static class Namespaces
{

    public const string MODEL = "http://www.omg.org/spec/BPMN/20100524/MODEL";
    public const string DC = "http://www.omg.org/spec/DD/20100524/DC";
    public const string BPMNDI = "http://www.omg.org/spec/BPMN/20100524/DI";
    public const string DI = "http://www.omg.org/spec/DD/20100524/DI";
}

[XmlRoot(ElementName = "definitions", Namespace = Namespaces.MODEL)]
public class Definitions
{

    [XmlElement(ElementName = "process", Namespace = Namespaces.MODEL)]
    public Process Process { get; set; }

    [XmlElement(ElementName = "BPMNDiagram", Namespace = Namespaces.BPMNDI)]
    public BPMNDiagram BPMNDiagram { get; set; }
}

[XmlRoot(ElementName = "process", Namespace = Namespaces.MODEL)]
public class Process : BaseElement
{
    [XmlElement(ElementName = "startEvent", Namespace = Namespaces.MODEL)]
    public StartEvent StartEvent { get; set; }

    [XmlElement(ElementName = "exclusiveGateway", Namespace = Namespaces.MODEL)]
    public List<ExclusiveGateway> ExclusiveGateway { get; set; }

    [XmlElement(ElementName = "sequenceFlow", Namespace = Namespaces.MODEL)]
    public List<SequenceFlow> SequenceFlow { get; set; }

    [XmlElement(ElementName = "userTask", Namespace = Namespaces.MODEL)]
    public List<UserTask> UserTask { get; set; }

    [XmlElement(ElementName = "serviceTask", Namespace = Namespaces.MODEL)]
    public List<ServiceTask> ServiceTask { get; set; }

    [XmlElement(ElementName = "sendTask", Namespace = Namespaces.MODEL)]
    public List<SendTask> SendTask { get; set; }

    [XmlElement(ElementName = "endEvent", Namespace = Namespaces.MODEL)]
    public EndEvent EndEvent { get; set; }

    [XmlAttribute(AttributeName = "isExecutable")]
    public bool IsExecutable { get; set; }

    public List<BaseElement> All => ExclusiveGateway.Cast<BaseElement>()
                                .Concat(UserTask.Cast<BaseElement>())
                                .Concat(ServiceTask.Cast<BaseElement>())
                                .Concat(SendTask.Cast<BaseElement>())
                                .Append(EndEvent)
                                .Append(StartEvent)
                                .ToList();

}

public class BaseElement
{

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "incoming", Namespace = Namespaces.MODEL)]
    public List<string> Incoming { get; set; }

    [XmlElement(ElementName = "outgoing", Namespace = Namespaces.MODEL)]
    public List<string> Outgoing { get; set; }
}

[XmlRoot(ElementName = "startEvent", Namespace = Namespaces.MODEL)]
public class StartEvent : BaseElement
{
}

[XmlRoot(ElementName = "exclusiveGateway", Namespace = Namespaces.MODEL)]
public class ExclusiveGateway : BaseElement
{

}

[XmlRoot(ElementName = "sequenceFlow", Namespace = Namespaces.MODEL)]
public class SequenceFlow
{

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "sourceRef")]
    public string SourceRef { get; set; }

    [XmlAttribute(AttributeName = "targetRef")]
    public string TargetRef { get; set; }
}

[XmlRoot(ElementName = "userTask", Namespace = Namespaces.MODEL)]
public class UserTask : BaseElement
{

}

[XmlRoot(ElementName = "serviceTask", Namespace = Namespaces.MODEL)]
public class ServiceTask : BaseElement
{

}

[XmlRoot(ElementName = "sendTask", Namespace = Namespaces.MODEL)]
public class SendTask : BaseElement
{

}

[XmlRoot(ElementName = "endEvent", Namespace = Namespaces.MODEL)]
public class EndEvent : BaseElement
{

}