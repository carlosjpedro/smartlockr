using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartlockr.Persistence
{
    public interface INTA7516InfoRepository
    {
        IEnumerable<NTA7516InfoEntity> DueForUpdate(DateTime lastupdate);
        Task UpSert(NTA7516InfoEntity entity);
        Task Delete(string domain);
        IEnumerable<NTA7516InfoEntity> GetAll(bool v);
    }

    public class NTA7516InfoRepository : INTA7516InfoRepository
    {
        private readonly DomainDataContext _dbContext;
        private const string LastestRecordsQuery = "SELECT NTA7516Info.* " +
                "FROM (SELECT Domain, Max(LastUpdated) LastUpdated from NTA7516Info GROUP BY Domain) as r " +
                "INNER JOIN NTA7516Info ON NTA7516Info.LastUpdated = r.LastUpdated AND NTA7516Info.Domain = r.Domain";

        public NTA7516InfoRepository(DomainDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task Delete(string domain)
        {
            var lastestRecord = _dbContext.NTA7516InfoCollection
                .OrderByDescending(x => x.Id)
                .FirstOrDefault(x => x.Domain == domain);

            if (lastestRecord == null) return Task.CompletedTask;

            var entity = new NTA7516InfoEntity
            {
                Domain = lastestRecord.Domain,
                Deleted = true,
                Expires = null,
                LastUpdated = DateTime.UtcNow,
                MxServers = new List<MxServerEntity>(),
                Version = null
            };

            _dbContext.Add(entity);
            return _dbContext.SaveChangesAsync();
        }

        public IEnumerable<NTA7516InfoEntity> DueForUpdate(DateTime lastupdate)
        {
            return _dbContext.NTA7516InfoCollection
                 .FromSqlRaw(LastestRecordsQuery)
                 .Where(x => x.Deleted == false && x.LastUpdated <= lastupdate);
        }

        public IEnumerable<NTA7516InfoEntity> GetAll(bool active)
        {
            var r = _dbContext.NTA7516InfoCollection
                .FromSqlRaw(LastestRecordsQuery).ToList();
            return _dbContext.NTA7516InfoCollection
               .FromSqlRaw(LastestRecordsQuery).Where(x => x.Deleted == !active)
                    .Include(x => x.MxServers);

        }

        public Task UpSert(NTA7516InfoEntity entity)
        {
            try
            {
                _dbContext.NTA7516InfoCollection.Add(entity);
                return _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
