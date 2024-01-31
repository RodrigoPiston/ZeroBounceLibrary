using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroBounceLibrary.Models
{
    /// <summary>
    /// Represents the result of validating a batch of email addresses.
    /// </summary>
    public class BatchValidationResult
    {
        /// <summary>
        /// A list of validation results for each email in the batch.
        /// </summary>
        public List<EmailValidationResult> EmailBatch { get; set; }
    
        /// <summary>
        /// A list of errors encountered during the batch validation process.
        /// </summary>
        public List<BatchError> Errors { get; set; }

        /// <summary>
        /// Represents an error encountered during batch validation.
        /// </summary>
        public class BatchError
        {
            public string Error { get; set; }
            public string EmailAddress { get; set; }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("EmailBatch:");
            foreach (var emailResult in EmailBatch)
            {
                stringBuilder.AppendLine(emailResult.ToString());
            }

            stringBuilder.AppendLine("Errors:");
            foreach (var error in Errors)
            {
                stringBuilder.AppendLine($"Error: {error.Error}, EmailAddress: {error.EmailAddress}");
            }

            return stringBuilder.ToString();
        }

    }

}
