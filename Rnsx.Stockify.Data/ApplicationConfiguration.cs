using System.Configuration;

namespace Rnsx.Stockify.Data
{
    class ApplicationConfiguration
    {
        /// <summary>
        /// Get the connection string.
        /// <exception cref="ConfigurationErrorsException"></exception>
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
        }
    }
}