using System;
using System.Linq;
using Xunit;

namespace Smartlockr.DnsQuerier.Tests
{
    public class NNTA7516InfoExtractorUnitTests
    {
        [Fact]
        public void ExtractInfo_Will_ReturnAllNTA7516Fields()
        {
            var expectedText = "v=NTA7516;version:2019-06;expire:2019-10;provider:smartlockr;ntamx:mx.mail.de;ntamx:mx2.mail.de";
            var domain = "smartlockr.co.";

            var txtRecord = new TxtRecordData(domain, expectedText);

            INNTA7516InfoExtractor extractor = new NTA7516InfoExtractor();
            NTA7516Info info = extractor.Extract(txtRecord);

            Assert.Equal(domain, info.Domain);
            Assert.Equal(new DateTime(2019, 6, 1), info.Version);
            Assert.Equal(new DateTime(2019, 10, 1), info.Expires);
            Assert.Equal("smartlockr", info.Provider);
            Assert.Equal(2, info.EmailServers.Count());
            Assert.Contains(info.EmailServers, x => x == "mx.mail.de");
            Assert.Contains(info.EmailServers, x => x == "mx2.mail.de");
        }

        [Fact]
        public void ExtractInfo_Will_ReturnEmptyDataIfIsNotNNTA7516()
        {
            var expectedText = "this is not a nta7516 record";
            var domain = "mydomain.co.";

            var txtRecord = new TxtRecordData(domain, expectedText);
            INNTA7516InfoExtractor extractor = new NTA7516InfoExtractor();
            NTA7516Info info = extractor.Extract(txtRecord);
            Assert.False(info.IsCompliant);
            Assert.Equal(domain, info.Domain);
            Assert.Null(info.Version);
            Assert.Null(info.Expires);
            Assert.Null(info.Provider);
            Assert.Empty(info.EmailServers);
        }
    }
}
