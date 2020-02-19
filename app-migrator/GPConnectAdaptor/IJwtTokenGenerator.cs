namespace GPConnectAdaptor
{
    public interface IJwtTokenGenerator
    {
        string GetToken(Scope scope);
    }
}