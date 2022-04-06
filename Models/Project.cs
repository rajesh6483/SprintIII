using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class Project
    {
        public Project()
        {
            this.Tasks = new HashSet<Task>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
