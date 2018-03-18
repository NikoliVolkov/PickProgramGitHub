using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickProgram.Models
{
    public class InvoiceStatusRepository : IInvoiceStatusRepository
    {
        private readonly InvoiceTrackerContext _dbConnection;
        public InvoiceStatusRepository(InvoiceTrackerContext dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public IQueryable<InvoiceStatus> GetInvoiceStatuses()
        {
            return _dbConnection.InvoiceStatus.AsQueryable();
        }
    }
}
