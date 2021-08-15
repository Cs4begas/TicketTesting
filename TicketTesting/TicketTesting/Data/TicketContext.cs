using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketTesting.Model;

namespace TicketTesting.Data
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {
        }
        public DbSet<TicketModel> Tickets { get; set; }
        public DbSet<LogTicketModel> LogTickets { get; set; }
        public DbSet<TicketStatusModel> TicketStatuses { get; set; }
    }
}
