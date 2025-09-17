namespace HP.Common.Configuration.Extensions
{
    public static class SettingsExtensions
    {
       
        public static string GetAwsServiceByName(this Settings settings, string name)
        {
            return settings.AwsServiceEndpoints[name];
        }
    }
}
