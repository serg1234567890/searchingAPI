using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace submissionstorage.Entities.Searching
{
    public class Control
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Control(long id_, string name_, string type_, string value_) { Id = id_; Name = name_; Type = type_; Value = value_; }
        public Control() { }
    }
}
