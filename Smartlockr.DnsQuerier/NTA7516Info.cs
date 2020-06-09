using System;
using System.Collections.Generic;
using System.Linq;

namespace Smartlockr.DnsQuerier
{
    public class NTA7516Info
    {

        public NTA7516Info(string domain, DateTime? version = null, DateTime? expires = null, string provider = null, IEnumerable<string> emailServers = null)
        {
            Domain = domain;
            Version = version;
            Expires = expires;
            Provider = provider;
            EmailServers = emailServers ?? Enumerable.Empty<string>();
        }

        public DateTime? Version { get; }
        public DateTime? Expires { get; }
        public string Provider { get; }
        public IEnumerable<string> EmailServers { get; }
        public string Domain { get; }
        public bool IsCompliant => !(Version == null || Expires == null || Provider == null || !EmailServers.Any());
    }
}
