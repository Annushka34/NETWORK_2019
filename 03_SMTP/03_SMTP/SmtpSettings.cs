namespace _03_SMTP
{
    static internal class SmtpSettings
    {
        public static string Host { get; set; }
        public static int Port { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        static SmtpSettings()
        {
            //----add in references Configuration
            Host = System.Configuration.ConfigurationManager.AppSettings["host"];
            Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["port"]);
            UserName = System.Configuration.ConfigurationManager.AppSettings["username"];
            Password = System.Configuration.ConfigurationManager.AppSettings["password"];
        }
        
    }
}