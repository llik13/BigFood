using Deliver.DAL.Entities;
using Deliver.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.DAL.Repository
{
    public class DeliverRepository : GenericRepository<DeliverModel>, IDeliverRepository
    {
        public DeliverRepository(DeliverContext databaseContext) : base(databaseContext)
        {
        }
    }
}
