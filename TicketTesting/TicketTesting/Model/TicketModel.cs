using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TicketTesting.Model
{
    [Table("ticket", Schema = "tickettesting")]
    public class TicketModel
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
        [Column("email_contact")]
        public string emailContact { get; set; }
        [Column("status_id")]
        public int statusId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("created_at")]
        public DateTime createdAt { get; set; }
        [Column("created_by")]
        public int createdBy { get; set; }
        [Column("updated_at")]
        public DateTime? updatedAt { get; set; }
        [Column("updated_by")]
        public int? updatedBy { get; set; }
        [JsonIgnore]
        public virtual List<LogTicketModel> LogTickets { get; set; } = new List<LogTicketModel>();
        [ForeignKey("statusId")]
        public TicketStatusModel TicketStatusModel { get; set; }

    }
}
