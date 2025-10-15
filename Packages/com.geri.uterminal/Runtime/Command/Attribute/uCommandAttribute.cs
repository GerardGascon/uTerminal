using System;
using JetBrains.Annotations;
using UnityEngine;

namespace uTerminal
{
    /// <summary>
    /// Attribute for defining uTerminal commands on methods.
    /// Allows specifying the command name, description, and associated MonoBehaviour.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    [MeansImplicitUse]
    public class uCommandAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the uTerminal command.
        /// </summary>
        public readonly string name;

        /// <summary>
        /// Gets the description of the uTerminal command.
        /// </summary>
        public readonly string description;

        /// <summary>
        /// Gets the associated MonoBehaviour for the uTerminal command.
        /// </summary>
        public readonly MonoBehaviour obj;

        /// <summary>
        /// Initializes a new instance of the uCommandAttribute with the specified name.
        /// </summary>
        /// <param name="name">The name of the uTerminal command.</param>
        public uCommandAttribute(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of the uCommandAttribute with the specified name and description.
        /// </summary>
        /// <param name="name">The name of the uTerminal command.</param>
        /// <param name="description">The description of the uTerminal command.</param>
        public uCommandAttribute(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}
