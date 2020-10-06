#region Copyright Preamble

// 
//    Copyright @ 2020 NCode Group
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

#endregion

using System.Collections.Generic;
using System.Linq;

namespace NCode.QuerySpecifications.Introspection
{
    public interface IProbeContext
    {
        IProbeContext Add(string key, object value);

        IProbeContext CreateScope(string key);
    }

    internal class ProbeContext : IProbeContext
    {
        private readonly IDictionary<string, object> _values = new Dictionary<string, object>();

        protected IDictionary<string, object> Build()
        {
            return _values.ToDictionary(kvp => kvp.Key, kvp =>
            {
                if (kvp.Value is IList<ProbeContext> scope)
                {
                    return kvp.Value;
                }
                else
                {
                    return kvp.Value;
                }
            });
        }

        public IProbeContext Add(string key, object value)
        {
            if (value == null)
            {
                _values.Remove(key);
            }
            else
            {
                IList<object> targetList;
                if (!_values.TryGetValue(key, out var obj))
                {
                    targetList = new List<object>();
                    _values[key] = targetList;
                }
                else if ((targetList = obj as IList<object>) == null)
                {
                    targetList = new List<object> { obj };
                    _values[key] = targetList;
                }

                targetList.Add(value);
            }
            return this;
        }

        public IProbeContext CreateScope(string key)
        {
            var scope = new ProbeContext();
            Add(key, scope);
            return scope;
        }

    }
}