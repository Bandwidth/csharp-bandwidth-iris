using System;
using System.ComponentModel;

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
