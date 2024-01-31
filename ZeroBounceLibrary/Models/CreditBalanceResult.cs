using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroBounceLibrary.Models
{
    /// <summary>
    /// Represents the credit balance result from the ZeroBounce API.
    /// </summary>
    public class CreditBalanceResult
    {
        /// <summary>
        /// The number of credits remaining in the account.
        /// </summary>
        public string Credits { get; set; }

        public override string ToString()
        {
            return $"Credits: {Credits}";
        }

    }
}
