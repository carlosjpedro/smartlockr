using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Smartlockr.DnsQuerier.Tests
{
    public class RecordFetcherUnitIntegrationTests
    {
        [Fact]
        public async Task WhenGettingTxtRecord_Will_RecordTxtRecordData()
        {
            var expectedText = "v=NTA7516;version:2019-06;expire:2019-10;provider:smartlockr;ntamx:mx.mail.de;ntamx:mx2.mail.de";
            var domain = "smartlockr.co.";

            IRecordFetcher recordFetcher = new RecordFetcher();
            var records = await recordFetcher.GetTxtRecordData(domain);
            Assert.Equal(expectedText, records.Single().Text);
            Assert.Equal(domain, records.Single().Domain);
        }

        [Fact]
        public async Task WhenRecordDoesNotHaveTxtRecords_Will_ReturnEmptyEnumerable()
        {
            var domain = "southbank.pt";

            IRecordFetcher recordFetcher = new RecordFetcher();
            var records = await recordFetcher.GetTxtRecordData(domain);
            Assert.Empty(records);
        }
    }
}
