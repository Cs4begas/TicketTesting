using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketTesting.Model;

namespace TicketTesting.Repository
{
    public interface ITicketRepository
    {
        public TicketModel CreateTicket(TicketModel ticketModel);
        public TicketModel UpdateTicket(TicketModel ticketModel);
        public IQueryable<T> GetAsQueryAble<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool isNoTracking = true) where T : class;

    }
}
