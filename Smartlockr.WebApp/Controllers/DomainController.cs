using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smartlockr.DnsQuerier;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smartlockr.WebApp.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class DomainController : Controller
    {
        private readonly INTA7516InfoManager _manager;

        public DomainController(INTA7516InfoManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IEnumerable<DomainInfo> GetDomains()
        {
            return _manager.GetAllActive();
        }

        [HttpPost("{email}")]
        public Task<DomainInfo> AddDomain(string email)
        {
            return _manager.AddDomain(email);
        }

        [HttpDelete("{domain}")]
        public Task Delete(string domain)
        {
            return _manager.DeleteDomain(domain);
        }
    }
}
