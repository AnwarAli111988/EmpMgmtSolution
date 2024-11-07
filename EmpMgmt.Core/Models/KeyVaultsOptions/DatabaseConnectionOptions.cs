namespace EmpMgmt.Core.Models.KeyVaultsOptions
{
    public class DatabaseConnectionOptions
    {
        public const string SectionName = "ConnectionString";
        public string? DefaultConnection { get; set; } = string.Empty;
    }
}
