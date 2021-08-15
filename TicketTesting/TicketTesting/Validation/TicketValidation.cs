using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using TicketTesting.Handler;
using TicketTesting.Model;

namespace TicketTesting.Validation
{
    public static class TicketValidation
    {
        public static void ValidateCreateTicket(TicketModel ticketModel)
        {
            if (string.IsNullOrWhiteSpace(ticketModel.title))
            {
                throw new ValidationException("กรุณากรอก title");
            }
            if (string.IsNullOrWhiteSpace(ticketModel.description))
            {
                throw new ValidationException("กรุณากรอก description");
            }
            if (string.IsNullOrWhiteSpace(ticketModel.emailContact))
            {
                throw new ValidationException("กรุณากรอก email");
            }
            ValidateEmailAddress(ticketModel.emailContact);
        }
        public static void ValidateEmailAddress(string emailAddress)
        {
            if (!IsValidEmail(emailAddress))
            {
                throw new ValidationException("กรุณากรอก format email ที่ถูกต้อง");
            }
        }
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static void ValidateExistingTicketModel(TicketModel ticketModel)
        {
            if (ticketModel == null)
            {
                throw new ValidationException("ไม่พบ ticketId ที่จะ update");
            }
        }
        public static void ValidateUpdateTicketRequest(UpdateTicketRequest updateTicketRequest)
        {
            if (updateTicketRequest == null)
            {
                throw new ValidationException("กรุณาใส่ request ที่จะ update");
            }
            else
            {
                bool checkEmptyRequest = string.IsNullOrWhiteSpace(updateTicketRequest.title) && string.IsNullOrWhiteSpace(updateTicketRequest.emailContact) && string.IsNullOrWhiteSpace(updateTicketRequest.description);
                bool checkEmptyStatus = string.IsNullOrWhiteSpace(updateTicketRequest.status);
                if (checkEmptyRequest && checkEmptyStatus)
                {
                    throw new ValidationException("กรุณาใส่ request ที่จะ update");
                }
            }
        }
        public static void ValidateEmptyTicketStatus(TicketStatusModel ticketStatusModel)
        {
            if (ticketStatusModel == null)
            {
                throw new ValidationException("ไม่พบ ticket status");
            }
        }
    }
}
