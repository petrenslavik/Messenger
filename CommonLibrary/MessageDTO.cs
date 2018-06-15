using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class MessageDTO
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public bool IsAuthor { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public DateTime SendDate { get; set; }
    }
}
