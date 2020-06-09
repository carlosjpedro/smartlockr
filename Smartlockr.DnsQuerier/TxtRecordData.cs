
namespace Smartlockr.DnsQuerier
{
    public class TxtRecordData
    {
        public TxtRecordData(string domain, string text)
        {
            this.Domain = domain;
            this.Text = text;
        }

        public string Domain { get; }
        public string Text { get; }
    }
}
