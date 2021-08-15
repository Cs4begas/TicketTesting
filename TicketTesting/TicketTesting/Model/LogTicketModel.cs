using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TicketTesting.Model
{
    [Table("log_ticket", Schema = "tickettesting")]
    public class LogTicketModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]
        [Column("id")]
        public int id { get; set; }
        [Column("title")]
        public string title { get; set; }
        [Column("description")]
        public string description { get; set; }
        [EmailAddress(ErrorMessage = "กรุณากรอก format email")]
        [Column("email_contact")]
        public string emailContact { get; set; }
        [Column("status_id")]
        public int statusId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("created_at")]
        public DateTime createdAt { get; set; }
        [Column("created_by")]
        public int createdBy { get; set; }
        [Column("ticket_id")]
        public int ticketId { get; set; }
        [ForeignKey("ticketId")]
        public TicketModel Ticket { get; set; }

    }
}
