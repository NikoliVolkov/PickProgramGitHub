using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickProgram.Models
{
    public class PickLocationRepository : IPickLocationRepository
    {
        private readonly InvoiceTrackerContext _dbConnection;
        public PickLocationRepository(InvoiceTrackerContext dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public PickLocation GetPickLocationById(int pickLocationId)
        {
            return _dbConnection.PickLocation.FirstOrDefault(p => p.LocationId == pickLocationId);
        }

        public IEnumerable<PickLocation> GetPickLocations()
        {
            var invoiceData = _dbConnection.PickLocation;
            return invoiceData;
        }
    }
}
