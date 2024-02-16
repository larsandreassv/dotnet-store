
public class EndpointBuilder {
    private readonly List<IEndpoint> _endpoints = new();
    private string _baseRoute = "api";
    private uint _version = 1;

    public string Route => $"{_baseRoute}/v{_version}";

    public static EndpointBuilder Create()
    {
        return new EndpointBuilder();
    }

    public EndpointBuilder AddListEndpoint<T, TDto>() where T : class
    {
        _endpoints.Add(new ListEndpoint<T, TDto>(Route));
        return this;
    }

    public EndpointBuilder AddSingleEndpoint<T, TDto>() where T : class, IIdEntity
    {
        _endpoints.Add(new SingleEndpoint<T, TDto>(Route));
        return this;
    }

    public EndpointBuilder AddDeleteEndpoint<T>()
    {
        throw new NotImplementedException();
    }

    public EndpointBuilder AddCreateEndpoint<T1, T2>()
    {
        throw new NotImplementedException();
    }

    public EndpointBuilder AddUpdateEndpoint<T1, T2>()
    {
        throw new NotImplementedException();
    }

    public void Map(IEndpointRouteBuilder endpoints)
    {
        foreach (var endpoint in _endpoints)
        {
            endpoint.MapEndpoint(endpoints);
        }
    }
}
