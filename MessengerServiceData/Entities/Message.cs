using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerServiceData.Entities
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string MessageText { get; set; }
        [Required]
        public DateTime SendDate { get; set; }
        [Required]
        public User Sender { get; set; }
        [Required]
        public User Receiver { get; set; }
    }
}
