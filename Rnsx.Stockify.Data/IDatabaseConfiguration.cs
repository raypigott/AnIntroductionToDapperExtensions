using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Rnsx.Stockify.Data.IntegrationTests")]
namespace Rnsx.Stockify.Data
{
    public interface IDatabaseConfiguration
    {
        string GetConnectionString();
    }
}
