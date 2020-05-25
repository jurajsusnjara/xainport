using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XainportWeb.Models
{
    public class Covid19AttestationModel
    {
        [Key]
        public string XainportId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime TestedDateTime { get; set; }

        public Covid19Result Covid19Data { get; set; }

        public string CreatedBy { get; set; }

        public enum Covid19Result
        {
            Positive,
            Negative,
            Inconclusive
        }
    }
}
