namespace AppWeb.Helpers.Options
{
    public class AuthJwtOptions
    {
        public string Secret { get; set; }
        public int ExpiresMin { get; set; }
    }
}
