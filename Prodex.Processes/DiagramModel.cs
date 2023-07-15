using System.Xml.Serialization;

namespace Prodex.Processes;

[XmlRoot(ElementName = "Bounds", Namespace = Namespaces.DC)]
public class Bounds : Waypoint
{
    [XmlAttribute(AttributeName = "width")]
    public int Width { get; set; }

    [XmlAttribute(AttributeName = "height")]
    public int Height { get; set; }
}

[XmlRoot(ElementName = "BPMNShape", Namespace = Namespaces.BPMNDI)]
public class BPMNShape
{

    [XmlElement(ElementName = "Bounds", Namespace = Namespaces.DC)]
    public Bounds Bounds { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "bpmnElement")]
    public string BpmnElement { get; set; }

    [XmlAttribute(AttributeName = "isMarkerVisible")]
    public bool IsMarkerVisible { get; set; }
}

[XmlRoot(ElementName = "waypoint", Namespace = Namespaces.DI)]
public class Waypoint
{

    [XmlAttribute(AttributeName = "x")]
    public int X { get; set; }

    [XmlAttribute(AttributeName = "y")]
    public int Y { get; set; }
}

[XmlRoot(ElementName = "BPMNEdge", Namespace = Namespaces.BPMNDI)]
public class BPMNEdge
{

    [XmlElement(ElementName = "waypoint", Namespace = Namespaces.DI)]
    public List<Waypoint> Waypoint { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "bpmnElement")]
    public string BpmnElement { get; set; }
}

[XmlRoot(ElementName = "BPMNPlane", Namespace = Namespaces.BPMNDI)]
public class BPMNPlane
{

    [XmlElement(ElementName = "BPMNShape", Namespace = Namespaces.BPMNDI)]
    public List<BPMNShape> BPMNShape { get; set; }

    [XmlElement(ElementName = "BPMNEdge", Namespace = Namespaces.BPMNDI)]
    public List<BPMNEdge> BPMNEdge { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "bpmnElement")]
    public string BpmnElement { get; set; }
}

[XmlRoot(ElementName = "BPMNDiagram", Namespace = Namespaces.BPMNDI)]
public class BPMNDiagram
{

    [XmlElement(ElementName = "BPMNPlane", Namespace = Namespaces.BPMNDI)]
    public BPMNPlane BPMNPlane { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }
}