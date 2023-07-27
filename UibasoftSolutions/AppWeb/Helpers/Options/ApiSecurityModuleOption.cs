namespace AppWeb.Helpers.Options
{
    public class ApiSecurityModuleOption
    {
        public ApiSecurityModule[] UsersModule { get; set; }
    }

    public class ApiSecurityModule
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
