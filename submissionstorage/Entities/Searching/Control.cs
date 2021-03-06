﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace submissionstorage.Entities.Searching
{
    public class Control
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Error { get; set; }
        public Control(long? id_, string name_, string type_, string value_) { Id = id_; Name = name_; Type = type_; Value = value_; Error = null; }
        public Control() { }
        public Control(Submission s, string type) { Id = s.Id; Name = "field" + s.Id; Type = type; Value = s.Fieldvalue; Error = null; }
    }
}
