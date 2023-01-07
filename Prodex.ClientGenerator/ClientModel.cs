namespace Prodex.ClientGenerator
{
    public class ClientModel
    {
        public string Name { get; set; }

        public List<EndpointModel> Endpoints { get; set; }
    }

    public class EndpointModel
    {
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public string MethodName { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string Method { get; set; }
        public string Route { get; set; }
        public string ResponseType { get; set; }
    }

    public class Parameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

}
