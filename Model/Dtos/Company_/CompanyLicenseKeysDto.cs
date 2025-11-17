using Core.Model;

namespace Model.Dtos.Company_
{
    public class CompanyLicenseKeysDto : IDto
    {
        public string? GapesJSLicenseKey { get; set; }
        public string? CKEditorLicenseKey { get; set; } 
    }
}