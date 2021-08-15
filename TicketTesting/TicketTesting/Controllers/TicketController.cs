using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TicketTesting.Business;
using TicketTesting.Model;

namespace TicketTesting.Controllers
{
    public class TicketController : BaseController
    {
        private readonly TicketBusiness _ticketBuesiness;
        public TicketController(TicketBusiness ticketBusiness)
        {
            _ticketBuesiness = ticketBusiness;
        }
        [HttpPost("/api/ticket")]
        public TicketModel CreateTicket([FromBody] TicketModel ticketModel)
        {
            int userId = GetUserIdFromHeader(Request);
            return _ticketBuesiness.CreateTicket(ticketModel, userId);
        }
        [HttpPatch("/api/ticket/{ticketId}")]
        public TicketModel UpdateTicket(int ticketId, [FromBody] UpdateTicketRequest updateTicketRequest)
        {
            int userId = GetUserIdFromHeader(Request);
             return _ticketBuesiness.UpdateTicket(ticketId, updateTicketRequest,userId);
        }
        [HttpGet("/api/tickets")]
        public List<TicketModel> GetTickets([FromQuery] string status)
        {
            return _ticketBuesiness.GetTickets(status);
        }
    }
}
