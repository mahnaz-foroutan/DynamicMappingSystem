using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DynamicMapper: IDynamicMapper
    {
        public TTarget Map<TSource, TTarget>(TSource source, Dictionary<string, string> mappingRules)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var target = Activator.CreateInstance<TTarget>();
            PerformMapping(source, target, mappingRules,false);
            return target;
        }

        public TSource MapBack<TSource, TTarget>(TTarget target, Dictionary<string, string> mappingRules)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            var source = Activator.CreateInstance<TSource>();
            PerformMapping(target, source, mappingRules,true);
            return source;
        }


        private void PerformMapping<TSource, TTarget>(TSource source, TTarget target, Dictionary<string, string> mappingRules, bool isMapBack)
        {
            foreach (var rule in mappingRules)
            {
                var sourceProperty = isMapBack==true? typeof(TSource).GetProperty(rule.Value):typeof(TSource).GetProperty(rule.Key);
                var targetProperty =isMapBack==true? typeof(TTarget).GetProperty(rule.Key): typeof(TTarget).GetProperty(rule.Value);
              
                
                if (sourceProperty != null && targetProperty != null)
                {
                    var value = sourceProperty.GetValue(source);
                    targetProperty.SetValue(target, value);
                }
            }
        }
    }
}
