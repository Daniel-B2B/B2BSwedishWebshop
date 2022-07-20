namespace Nop.Plugin.Misc.GroupBuy.Models
{
    public class EmailModel
    {
        public string EmailSubject { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToAddress { get; set; }
        public string CCAddress { get; set; }
        public string BCCAddress { get; set; }
        public string EmailBody { get; set; }
        public string EmailAttachmentFilePath { get; set; }
       

        public EmailModel()
        {
            EmailSubject = EmailSubject;
            FromEmail = FromEmail;
            FromName = FromName;
            ToAddress = ToAddress;
            CCAddress = CCAddress;
            BCCAddress = BCCAddress;
            EmailBody = EmailBody;
            EmailAttachmentFilePath = EmailAttachmentFilePath;

        }
    }
}