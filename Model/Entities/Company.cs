using Core.Model;

namespace Model.Entities
{
    public class Company : IEntity, ISoftDeletableEntity, IAuditableEntity, ILoggableEntity
    {
        public Guid Id { get; set; }
        public string Since { get; set; } = null!;
        public string? Logo { get; set; }
        public string Address { get; set; } = null !;
        public string EmailAdresses { get; set; } = null !;
        public string PhoneNumbers { get; set; } = null !;
        public string? FaxNumbers { get; set; }
        public string WorkingStartTime { get; set; } = null !;
        public string WorkingEndTime { get; set; } = null !;
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? GapesJSLicenseKey { get; set; }
        public string? CKEditorLicenseKey { get; set; }
       
        #region Inherited Props
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreateDateUtc { get; set; }
        public DateTime? UpdateDateUtc { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateUtc { get; set; } 
        #endregion



        public List<string> GetPhoneNumbers()
        {
            var temp = this.PhoneNumbers.Split(',')?.ToList();
            return temp ?? new List<string>();
        }
        public void SetPhoneNumbers(List<string> phoneList)
        {
            this.PhoneNumbers = string.Join(',', phoneList);
        }


        public List<string> GetEmailAdresses()
        {
            var temp = this.EmailAdresses.Split(',')?.ToList();
            return temp ?? new List<string>();
        }
        public void SetEmailAdresses(List<string> emailAdressList)
        {
            this.EmailAdresses = string.Join(',', emailAdressList);
        }


        public List<string> GetFaxNumbers()
        {
            var temp = this.FaxNumbers?.Split(',')?.ToList();
            return temp ?? new List<string>();
        }
        public void SetFaxNumbers(List<string> faxNumberList)
        {
            this.FaxNumbers = string.Join(',', faxNumberList);
        }
    }
}