using System.Collections.Generic;
using System;
namespace BT_Implementation
{
    public class Blackboard
    {
        public Dictionary<string, System.Object> Data;

        public Blackboard()
        {
            Data = new Dictionary<string, System.Object>();
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
