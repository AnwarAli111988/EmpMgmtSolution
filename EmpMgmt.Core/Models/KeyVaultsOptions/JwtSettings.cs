namespace EmpMgmt.Core.Models.KeyVaultsOptions
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiryDays { get; set; } = 1;
        public bool RequireHttpsMetadata { get; set; } = false;
    }
}
