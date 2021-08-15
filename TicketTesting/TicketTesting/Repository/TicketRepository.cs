using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketTesting.Data;
using TicketTesting.Model;
using Microsoft.EntityFrameworkCore;

namespace TicketTesting.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketContext _context;
        public TicketRepository(TicketContext context)
        {
            _context = context;
        }
        public TicketModel CreateTicket(TicketModel ticketModel)
        {
            try
            {
                _context.Tickets.Add(ticketModel);
                _context.SaveChanges();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }
            return ticketModel;
        }
        public TicketModel UpdateTicket(TicketModel ticketModel)
        {
            try
            {
                _context.Tickets.Update(ticketModel);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw;
            }
            return ticketModel;
        }

        public IQueryable<T> GetAsQueryAble<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool isNoTracking = true) where T : class
        {
            IQueryable<T> query = this._context.Set<T>();
            if (predicate != null)
            {
                query.Where(predicate);
            }
            if (isNoTracking)
                query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includeProperties != null)
                foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            if (orderBy != null)
                return orderBy(query);
            return query;
        }

    }
}
