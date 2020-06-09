using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Smartlockr.DnsQuerier
{
    public class NTA7516InfoExtractor : INNTA7516InfoExtractor
    {
        Regex regex = new Regex(@"^v=NTA7516;version:(\d\d\d\d-\d\d);" +
            @"expire:(\d\d\d\d-\d\d);provider:([^;]+);");


        Regex mailServerRgx = new Regex("ntamx:([^;|:]+)");
        public NTA7516Info Extract(TxtRecordData txtRecord)
        {
            var match = regex.Match(txtRecord.Text);

            if (match.Success)
            {

                var version = ParseDate(match.Groups[1].Value);
                var expires = ParseDate(match.Groups[2].Value);
                var provider = match.Groups[3].Value;
                var mailServers = MailServerFromTxtRecord(txtRecord.Text);


                return new NTA7516Info(txtRecord.Domain, version, expires, provider, mailServers);
            }
            return new NTA7516Info(txtRecord.Domain);
        }

        private static DateTime ParseDate(string match)
        {
            var year = int.Parse(match.Split('-')[0]);
            var month = int.Parse(match.Split('-')[1]);

            return new DateTime(year, month, 1);
        }

        public IEnumerable<string> MailServerFromTxtRecord(string txtRecord)
        {
            foreach (Match match in mailServerRgx.Matches(txtRecord))
            {
                yield return match.Groups[1].Value;
            }
        }
    }
}
