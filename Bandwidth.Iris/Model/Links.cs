using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class Links
    {

        [XmlElement("first")]
        public string First { get; set; }

        [XmlElement("next")]
        public string Next { get; set; }

        [XmlElement("last")]
        public string Last { get; set; }
    }
}
