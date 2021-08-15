using System;
using System.Collections.Generic;
using System.Linq;
using TicketTesting.Enum;
using TicketTesting.Model;
using TicketTesting.Repository;
using TicketTesting.Validation;

namespace TicketTesting.Business
{
    public class TicketBusiness
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketBusiness(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public TicketModel CreateTicket(TicketModel ticketModel, int userId)
        {
            TicketValidation.ValidateCreateTicket(ticketModel);
            ticketModel.statusId = (int)TicketStatus.Pending;
            ticketModel.createdBy = userId;
            return _ticketRepository.CreateTicket(ticketModel);
        }
        public TicketModel UpdateTicket(int ticketId, UpdateTicketRequest updateTicketRequest, int userId)
        {
            TicketValidation.ValidateUpdateTicketRequest(updateTicketRequest);
            TicketModel ticketModel = _ticketRepository.GetAsQueryAble<TicketModel>(x => x.id == ticketId).FirstOrDefault();
            TicketValidation.ValidateExistingTicketModel(ticketModel);
            TicketStatusModel ticketStatusModel = _ticketRepository.GetAsQueryAble<TicketStatusModel>(x => x.description == updateTicketRequest.status).FirstOrDefault();
            TicketValidation.ValidateEmptyTicketStatus(ticketStatusModel);
            LogTicketModel logTicketModel = CreateLogTicket(ticketModel);
            ticketModel.LogTickets.Add(logTicketModel);
            MapUpdateRequestToTicketModel(updateTicketRequest, ticketModel, ticketStatusModel.id, userId);
            return _ticketRepository.UpdateTicket(ticketModel);
        }

        private void MapUpdateRequestToTicketModel(UpdateTicketRequest updateTicketRequest, TicketModel ticketModel, int ticketStatusId, int userId)
        {
            if (!string.IsNullOrWhiteSpace(updateTicketRequest.title))
            {
                ticketModel.title = updateTicketRequest.title;
            }
            if (!string.IsNullOrWhiteSpace(updateTicketRequest.description))
            {
                ticketModel.description = updateTicketRequest.description;
            }
            if (!string.IsNullOrWhiteSpace(updateTicketRequest.emailContact))
            {
                TicketValidation.ValidateEmailAddress(updateTicketRequest.emailContact);
                ticketModel.emailContact = updateTicketRequest.emailContact;
            }
            ticketModel.statusId = ticketStatusId;
            ticketModel.updatedBy = userId;
            ticketModel.updatedAt = DateTime.Now;
        }


        private LogTicketModel CreateLogTicket(TicketModel ticket)
        {
            LogTicketModel logTicket = new LogTicketModel();
            logTicket.title = ticket.title;
            logTicket.description = ticket.description;
            logTicket.emailContact = ticket.emailContact;
            logTicket.statusId = ticket.statusId;
            logTicket.createdBy = ticket.createdBy;
            return logTicket;
        }

        public List<TicketModel> GetTickets(string status)
        {
            List<TicketModel> ticketModels;
            if (string.IsNullOrEmpty(status))
            {
                 ticketModels = _ticketRepository.GetAsQueryAble<TicketModel>(orderBy: x => x.OrderBy(y => y.statusId).ThenBy(z => z.updatedAt)).ToList();
            }
            else
            {
                TicketStatusModel ticketStatusModel = _ticketRepository.GetAsQueryAble<TicketStatusModel>(x => x.description == status).FirstOrDefault();
                TicketValidation.ValidateEmptyTicketStatus(ticketStatusModel);
                ticketModels = _ticketRepository.GetAsQueryAble<TicketModel>(predicate: x => x.statusId == ticketStatusModel.id,orderBy: x => x.OrderBy(y => y.statusId).ThenByDescending(z => z.updatedAt)).ToList();
            }
            return ticketModels;
        }
    }
}
