using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ZedSharp.Utils
{
    public static class DictionaryHelper
    {
        public static Dictionary<string, string> ToDictionary(this object myObj)
        {
            return myObj.GetType()
                .GetProperties()
                .Select(pi => new {pi.Name, Value = pi.GetValue(myObj, null)})
                .Union(
                    myObj.GetType()
                        .GetFields()
                        .Select(fi => new {fi.Name, Value = fi.GetValue(myObj)})
                )
                .ToDictionary(ks => ks.Name, vs => vs.Value.ToString());
        }
    }
}