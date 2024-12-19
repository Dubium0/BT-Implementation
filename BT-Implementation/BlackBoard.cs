using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Implementation
{
    public class Blackboard
    {
        public Dictionary<string, object> Data { get; set; }

        public Blackboard()
        {
            Data = new Dictionary<string, object>();
        }

        public T GetValue<T>(string key)
        {
            return (T)Data[key];
        }

        public void SetValue<T>(string key, T value)
        {
            if(value == null)
            {
                throw new ArgumentNullException("value");
            }
            Data[key] = value;
        }
    }

}
