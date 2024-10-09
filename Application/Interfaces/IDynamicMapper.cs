using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDynamicMapper
    {
        TTarget Map<TSource, TTarget>(TSource source, Dictionary<string, string> mappingRules);
        TSource MapBack<TSource, TTarget>(TTarget source, Dictionary<string, string> mappingRules);
    }
}
