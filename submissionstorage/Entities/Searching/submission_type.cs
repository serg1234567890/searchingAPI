using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace submissionstorage.Entities.Searching
{
    public class Submission_type
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Submission_type() { }
        public Submission_type(string name) { Name = name; }
    }
}
