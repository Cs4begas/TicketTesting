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
       /// <summary>
       /// Post Ticket title,description,emailContact
       /// </summary>
       /// <param name="ticketModel"></param>
       /// <returns></returns>
        [HttpPost("/api/tickets")]
        public TicketModel CreateTicket([FromBody] TicketModel ticketModel)
        {
            int userId = GetUserIdFromHeader(Request);
            return _ticketBuesiness.CreateTicket(ticketModel, userId);
        }
        /// <summary>
        ///  Patch Ticket title,description,emailContact,status
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="updateTicketRequest"></param>
        /// <returns></returns>
        [HttpPatch("/api/tickets/{ticketId}")]
        public TicketModel UpdateTicket(int ticketId, [FromBody] UpdateTicketRequest updateTicketRequest)
        {
            int userId = GetUserIdFromHeader(Request);
             return _ticketBuesiness.UpdateTicket(ticketId, updateTicketRequest,userId);
        }
        /// <summary>
        /// Get Tickets Query by status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("/api/tickets")]
        public List<TicketModel> GetTickets([FromQuery] string status)
        {
            return _ticketBuesiness.GetTickets(status);
        }
    }
}
