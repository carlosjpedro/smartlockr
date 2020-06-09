using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartlockr.Web.Controllers
{
    [Route("api/[controller]")]
    public class DomainController : Controller
    {
        [HttpGet]
        public Task<IEnumerable<DomainInfo>> GetDomains()
        {
            return Task.FromResult(new[]
            {
                new DomainInfo{Id=1, Name="my domain", LastUpdated=DateTime.UtcNow.AddDays(-2)}
            }.AsEnumerable());
        }

        [HttpPost("{email}")]
        public async Task<DomainInfo> AddDomain(string email)
        {
            return new DomainInfo { Id = 2, Name = "nwe Domain", LastUpdated = DateTime.UtcNow, IsComplient = false };
        }
    }

    public class DomainInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsComplient { get; set; }
    }
}
