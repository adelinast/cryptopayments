using System;
using System.Configuration;

namespace CryptoPayments
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadConfigurationStringDb();

            Application application = new Application();

            application.SetCurrentDir(args[0]);
            application.Run();
        }

        static void ReadConfigurationStringDb()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading setting");
            }
        }
    }
}
