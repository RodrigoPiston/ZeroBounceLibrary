using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Net;
using System.Reflection.Emit;
using System.Reflection;
using System.Security.Principal;
using System.Text;

using Xunit;

namespace ZeroBounceLibrary.Models
{
    /// <summary>
    /// Represents the result of validating a single email address.
    /// </summary>
    public class EmailValidationResult
    {
        public string Address { get; set; }
        public string Status { get; set; }
        public string SubStatus { get; set; }
        public bool FreeEmail { get; set; }
        public string DidYouMean { get; set; }
        public string Account { get; set; }
        public string Domain { get; set; }
        public string DomainAgeDays { get; set; }
        public string SmtpProvider { get; set; }
        public string MxRecord { get; set; }
        public string MxFound { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string ProcessedAt { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Address: {Address}");
            stringBuilder.AppendLine($"Status: {Status}");
            stringBuilder.AppendLine($"SubStatus: {SubStatus}");
            stringBuilder.AppendLine($"FreeEmail: {FreeEmail}");
            stringBuilder.AppendLine($"DidYouMean: {DidYouMean}");
            stringBuilder.AppendLine($"Account: {Account}");
            stringBuilder.AppendLine($"Domain: {Domain}");
            stringBuilder.AppendLine($"DomainAgeDays: {DomainAgeDays}");
            stringBuilder.AppendLine($"SmtpProvider: {SmtpProvider}");
            stringBuilder.AppendLine($"MxRecord: {MxRecord}");
            stringBuilder.AppendLine($"MxFound: {MxFound}");
            stringBuilder.AppendLine($"FirstName: {FirstName}");
            stringBuilder.AppendLine($"LastName: {LastName}");
            stringBuilder.AppendLine($"Gender: {Gender}");
            stringBuilder.AppendLine($"Country: {Country}");
            stringBuilder.AppendLine($"Region: {Region}");
            stringBuilder.AppendLine($"City: {City}");
            stringBuilder.AppendLine($"ZipCode: {ZipCode}");
            stringBuilder.AppendLine($"ProcessedAt: {ProcessedAt}");

            return stringBuilder.ToString();
        }
    }

}
