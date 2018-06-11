using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerServiceData.Entities
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        public User Holder { get; set; }

        [Required]
        public byte[] Key { get; set; }

        public string Ip { get; set; }
    }
}
