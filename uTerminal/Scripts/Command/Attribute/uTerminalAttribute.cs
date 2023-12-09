using System;
using UnityEngine;

namespace uTerminal
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class uTerminalAttribute : Attribute
    {
        public readonly string name; 
        public readonly string description;
        public readonly MonoBehaviour obj; 

        public uTerminalAttribute(string name)
        {
            this.name = name;
        }

        public uTerminalAttribute(string name, string description)
        {
            this.name = name; 
            this.description = description;
        }  
    }
}