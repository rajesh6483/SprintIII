using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int Status { get; set; }
        public int AssignedToUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Detail { get; set; }
    }
}
