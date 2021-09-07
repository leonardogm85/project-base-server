namespace ProjetoBase.Infrastructure.CrossCutting.Common.Settings
{
    public class AuthorizationTokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Expires { get; set; }
    }
}
