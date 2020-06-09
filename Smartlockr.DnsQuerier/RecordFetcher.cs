using DnsClient;
using DnsClient.Protocol;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartlockr.DnsQuerier
{
    public class RecordFetcher : IRecordFetcher
    {


        public RecordFetcher()
        {
        }

        public async Task<IEnumerable<TxtRecordData>> GetTxtRecordData(string domain)
        {

            var lookupClient = new LookupClient();
            var dnsResponse = await lookupClient.QueryAsync(domain, QueryType.TXT);
            return dnsResponse.AllRecords.Where(x => x.RecordType == DnsClient.Protocol.ResourceRecordType.TXT)
                .OfType<TxtRecord>()
                .Select(x => new TxtRecordData(x.DomainName, x.Text.First()));
        }
    }
}
