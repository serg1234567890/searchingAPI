using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace submissionstorage.Entities.Searching
{
    public class Submission
    {
        public long Id { get; set; }
        public string Fieldname { get; set; }
        public string Fieldvalue { get; set; }
        public long Submission_typeId { get; set; }
        public Submission_type Type { get; set; }
        public Submission() { }
        public Submission(string name, string val, long foreing) { Fieldname = name; Fieldvalue = val; Submission_typeId = foreing; }
    }
}
