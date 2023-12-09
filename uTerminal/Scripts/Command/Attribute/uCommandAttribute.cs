using System;
using UnityEngine;

namespace uTerminal
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class uCommandAttribute : Attribute
    {
        public readonly string name; 
        public readonly string description;
        public readonly MonoBehaviour obj; 

        public uCommandAttribute(string name)
        {
            this.name = name;
        }

        public uCommandAttribute(string name, string description)
        {
            this.name = name; 
            this.description = description;
        }  
    }
}