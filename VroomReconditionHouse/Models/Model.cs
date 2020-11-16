using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VroomReconditionHouse.Models
{
    public class Model
    {
        public int Id { get; set; }

        [MaxLength(255), Required]
        public string Name { get; set; }
        public Make Make { get; set; }
        [ForeignKey("Make")]
        public int MakeId { get; set; }

    }
}
