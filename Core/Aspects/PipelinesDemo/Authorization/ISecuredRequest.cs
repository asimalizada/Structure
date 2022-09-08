namespace Core.Aspects.Pipelines.Authorization;

public interface ISecuredRequest
{
    public string[] Roles { get; }
}