using BatchAPI.Data;
using BatchAPI.Models;
using BatchAPI.Models.Dtos;
using BatchAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchAPI.Repository
{
    public class BatchTepository : IBatchRepository
    {
        private readonly ApplicationDBContext _db;
        public BatchTepository(ApplicationDBContext db)
        {
            //Dependency Injection
            _db = db;
        }
        public bool BatchExists(string name)
        {
            bool value = _db.batches.Any(a => a.businessUnit.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool BatchExists(Guid batchId)
        {
            return _db.batches.Any(a => a.batchID == batchId);
        }

        public bool CreateBatch(Batch batch)
        {
            _db.batches.Add(batch);
            return Save();
        }

        public bool DeleteBatch(Batch batch)
        {
            _db.batches.Remove(batch);
            return Save();
        }

        public Batch GetBatch(Guid batchID)
        {
            return _db.batches.FirstOrDefault(a => a.batchID == batchID);
        }

        public ICollection<Batch> GetBatches()
        {
            return _db.batches.OrderBy(a => a.businessUnit).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateBatch(Batch batch)
        {
            _db.batches.Update(batch);
            return Save();
        }

        //BatchDtos IBatchRepository.GetBatch(Guid batchID)
        //{
        //    return _db.batches.OrderBy(a => a.businessUnit).ToList(); throw new NotImplementedException();
        //}

        //BatchDtos IBatchRepository.GetBatch(Guid batchID)
        //{
        //}
    }
}
