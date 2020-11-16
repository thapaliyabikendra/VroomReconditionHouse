using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VroomReconditionHouse.Models
{
    public class Make
    {
        public int Id { get; set; }

        [MaxLength(255), Required]
        public string Name { get; set; }
    }
}
