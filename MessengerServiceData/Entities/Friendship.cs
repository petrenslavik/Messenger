using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerServiceData.Entities
{
    public class Friendship
    {
        public int Id { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public User FirstUser { get; set; }
        [Required]
        public User SecondUser { get; set; }
    }
}
