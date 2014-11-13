using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandwidth.Iris
{
    public sealed class DefaultDateTimeAttribute : DefaultValueAttribute
    {
        public DefaultDateTimeAttribute()
            : base(default(DateTime)) { }

        public DefaultDateTimeAttribute(string dateTime)
            : base(DateTime.Parse(dateTime)) { }
    }
}
