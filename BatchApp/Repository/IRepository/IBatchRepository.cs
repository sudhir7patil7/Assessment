using BatchAPI.Models;
using BatchAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchAPI.Repository.IRepository
{
    public interface IBatchRepository
    {
        ICollection<Batch> GetBatches();
        Batch GetBatch(Guid batchID);
        bool BatchExists(string businessUnit);
        bool BatchExists(Guid batchId);
        bool CreateBatch(Batch batch);
        bool UpdateBatch(Batch batch);
        bool DeleteBatch(Batch batch);
        bool Save();


    }
}
