using System;
using System.Collections.Generic;

namespace payfurl.sdk.Models.PaymentLink
{
    public class CreatePaymentLink
    {
        public string Title { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public List<string> AllowedPaymentTypes { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ConfirmationMessage { get; set; }
        public string RedirectUrl { get; set; }
        public CallToAction? CallToAction { get; set; }
        public int? LimitPayments { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        
        public static string EncodeImage(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            if (!System.IO.File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist.", nameof(filePath));
            }

            var imageBytes = System.IO.File.ReadAllBytes(filePath);
            if (imageBytes == null || imageBytes.Length < 4)
            {
                throw new ArgumentException("Invalid file bytes.", nameof(imageBytes));
            }

            string contentType;
            switch (imageBytes[0])
            {
                case 0x89 when imageBytes[1] == 0x50 && imageBytes[2] == 0x4E && imageBytes[3] == 0x47:
                    contentType = "image/png";
                    break;
                case 0xFF when imageBytes[1] == 0xD8 && imageBytes[imageBytes.Length - 2] == 0xFF && imageBytes[imageBytes.Length - 1] == 0xD9:
                    contentType = "image/jpeg";
                    break;
                case 0x47 when imageBytes[1] == 0x49 && imageBytes[2] == 0x46 && imageBytes[3] == 0x38:
                    contentType = "image/gif";
                    break;
                default:
                    throw new ArgumentException("Unsupported image format.", nameof(filePath));
            }
            
            return $"data:{contentType};base64,{Convert.ToBase64String(imageBytes)}";
        }
    }
}