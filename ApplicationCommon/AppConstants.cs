namespace ApplicationCommon
{
    public class AppConstants
    {
        public const string TenantId = "TenantId";
        public const long DefaultTenantId = 1;
        public const string Unknown = "Unknown";
        public const string ImageFolder = "UploadedImages";

        public const string CompanyProfiles = "CompanyProfiles";
        public const string Blogs = "Blogs";
        public const string HomeContents = "HomeContents";
        public const string BlogContents = "BlogContents";
        public const string NewsContents = "News";
        public const string HomeContentMids = "HomeContentMids";

        public static List<Dictionary<string, string>> ImageSources = new List<Dictionary<string, string>>()
        {
            new Dictionary<string, string>() { { "Key", "CP" }, { "Value", CompanyProfiles } },
            new Dictionary<string, string>() { { "Key", "Blog" }, { "Value", Blogs } },
            new Dictionary<string, string>() { { "Key", "Home" }, { "Value", HomeContents } },
        };
    }
}
