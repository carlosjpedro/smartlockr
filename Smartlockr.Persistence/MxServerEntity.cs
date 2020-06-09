using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smartlockr.Persistence
{

    [Table("MxServers")]
    public class MxServerEntity
    {
        [Key]
        public int Id { get; set; }
        public int NTA7516InfoEntityId { get; set; }
        public string Name { get; set; }
    }
}
