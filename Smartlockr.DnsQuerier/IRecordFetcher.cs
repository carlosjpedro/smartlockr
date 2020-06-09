using Smartlockr.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Smartlockr.DnsQuerier
{
    public interface IRecordFetcher
    {
        Task<IEnumerable<TxtRecordData>> GetTxtRecordData(string domain);
    }

    public interface INTA7516InfoManager
    {
        Task<DomainInfo> AddDomain(string email);
        Task DeleteDomain(string domain);
        Task UpdateRecords(DateTime lastUpdated);
        IEnumerable<DomainInfo> GetAllActive();
    }

    public class NTA7516InfoManager : INTA7516InfoManager
    {
        private readonly IDomainFromEmailParser _emailParser;
        private readonly IRecordFetcher _recordFetcher;
        private readonly INNTA7516InfoExtractor _infoExtractor;
        private readonly INTA7516InfoRepository _repository;

        public NTA7516InfoManager(
            IDomainFromEmailParser emailParser,
            IRecordFetcher recordFetcher,
            INNTA7516InfoExtractor infoExtractor,
            INTA7516InfoRepository repository)
        {
            _emailParser = emailParser;
            _recordFetcher = recordFetcher;
            _infoExtractor = infoExtractor;
            _repository = repository;
        }

        public async Task<DomainInfo> AddDomain(string email)
        {
            var domain = _emailParser.GetDomain(email);
            return await UpdateDomain(domain);

        }

        private async Task<DomainInfo> UpdateDomain(string domain)
        {
            var txtRecords = await _recordFetcher.GetTxtRecordData(domain);
            var nTA7516Colletion = txtRecords.Select(_infoExtractor.Extract);

            var record = nTA7516Colletion.FirstOrDefault(x => x.IsCompliant);

            var entity = record == null ?
             new NTA7516InfoEntity
             {
                 Deleted = false,
                 Domain = domain,
                 LastUpdated = DateTime.UtcNow,
                 MxServers = new List<MxServerEntity>(),
                 IsComplient = false
             }
            : new NTA7516InfoEntity
            {
                Deleted = false,
                Domain = domain,
                LastUpdated = DateTime.UtcNow,
                Expires = record.Expires,
                MxServers = record.EmailServers.Select(x => new MxServerEntity
                {
                    Name = x
                }).ToList(),
                IsComplient = record.IsCompliant,
                Version = record.Version
            };

            await _repository.UpSert(entity);

            return new DomainInfo
            {
                Name = entity.Domain,
                IsComplient = entity.IsComplient,
                LastUpdated = entity.LastUpdated
            };
        }

        public Task DeleteDomain(string domain)
        {
            return _repository.Delete(domain);
        }

        public async Task UpdateRecords(DateTime lastUpdated)
        {
            var records = _repository.DueForUpdate(lastUpdated);
            foreach (var record in records)
            {
                await UpdateDomain(record.Domain);
            }
        }

        public IEnumerable<DomainInfo> GetAllActive()
        {
            return _repository.GetAll(true).Select(x => new DomainInfo
            {
                Name = x.Domain,
                IsComplient = x.IsComplient,
                LastUpdated = x.LastUpdated,
                MxServers = x.MxServers.Select(s => s.Name)
            }); 
        }
    }

    public class DomainInfo
    {
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsComplient { get; set; }
        public IEnumerable<string> MxServers { get; internal set; }
    }

    public interface IDomainFromEmailParser
    {
        string GetDomain(string email);
    }

    public class DomainFromEmailParser : IDomainFromEmailParser
    {
        public string GetDomain(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return address.Host;
            }
            catch (Exception)
            {
                throw new InvalidEmailAddress();
            }
        }
    }

    public class InvalidEmailAddress : Exception { }
}
