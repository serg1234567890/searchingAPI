using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using submissionstorage.Entities.Searching;

namespace submissionstorage.Stories
{
    public class DefaultControls
    {
        public List<Control> Items { get; set; }
        public DefaultControls()
        {
            Items = new List<Control>();

            Items.Add(new Control(1, "field1", "text", "12345"));
            Items.Add(new Control(2, "field2", "textarea", "1"));
            Items.Add(new Control(3, "field3", "select", "2"));
            Items.Add(new Control(4, "field4", "date", DateTime.Now.ToString("ddMMyyyy")));
            Items.Add(new Control(5, "field5", "radio", "off"));
            Items.Add(new Control(6, "field6", "checkbox", "true"));
        }
    }
}
