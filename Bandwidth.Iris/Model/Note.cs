using System;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class Note
    {
        public string Id { get; set; }
        [DefaultDateTimeAttribute]
        public DateTime LastDateModifier { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
    }

    public class Notes
    {
        [XmlElementAttribute("Note")]
        public Note[] List { get; set; }
    }
}
