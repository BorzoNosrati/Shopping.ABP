using System.Threading.Tasks;

namespace Shopping.ABP.Data;

public interface IABPDbSchemaMigrator
{
    Task MigrateAsync();
}
