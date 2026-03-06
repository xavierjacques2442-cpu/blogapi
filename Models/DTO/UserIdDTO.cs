using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogapi.Models.DTO
{
    public class UserIdDTO
    {
        internal object id;

        public int UserId { get; set; }

        public string? PublisherName {get; set;}
        public string? Username { get; internal set; }
    }
}