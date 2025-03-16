using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apprendi.Application.Common.Interfaces
{
    public interface IDateTimeOffset
    {
        DateTimeOffset UtcNow { get; }
    }

    public class DateTimeOffsetWrapper : IDateTimeOffset
    {
        public DateTimeOffset UtcNow => DateTimeOffset.Now;
    }
}
