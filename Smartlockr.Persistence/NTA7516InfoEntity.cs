using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smartlockr.Persistence
{
    [Table("NTA7516Info")]
    public class NTA7516InfoEntity
    { 
        [Key]
        public int Id { get; set; }
        public string Domain { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime? Version { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<MxServerEntity> MxServers { get; set; }
        public bool Deleted { get; set; }
        public bool IsComplient { get; set; }
    }
}
