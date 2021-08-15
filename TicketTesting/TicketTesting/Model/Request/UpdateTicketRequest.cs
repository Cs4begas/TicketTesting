using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketTesting.Model
{
    public class UpdateTicketRequest
    {
        public string title { get; set; }
        public string description { get; set; }
        [EmailAddress(ErrorMessage = "กรุณากรอก format email")]
        public string emailContact { get; set; }
        public string status { get; set; }
    }
}
