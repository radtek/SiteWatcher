using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteWatcher.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExtensionAttribute : System.Attribute
    {
        /// <summary>
        /// Creates an instance of the attribute and assigns a description.
        /// </summary>
        public ExtensionAttribute(string description, string version, string author)
        {
            _Description = description;
            _Version = version;
            _Author = author;
        }

        /// <summary>
        /// Creates an instance of the attribute and assigns a description.
        /// </summary>
        public ExtensionAttribute(string description, string version, string author, int priority)
        {
            _Description = description;
            _Version = version;
            _Author = author;
            _priority = priority;
        }

        private string _Description;
        /// <summary>
        /// Gets the description of the extension.
        /// </summary>
        public string Description
        {
            get { return _Description; }
        }

        private string _Version;

        /// <summary>
        /// Gets the version number of the extension
        /// </summary>
        public string Version
        {
            get { return _Version; }
        }

        private string _Author;

        /// <summary>
        /// Gets the author of the extension
        /// </summary>
        public string Author
        {
            get { return _Author; }
        }

        private int _priority = 999;

        /// <summary>
        /// Gets the priority of the extension
        /// This determins in what order extensions instantiated
        /// and in what order they will respond to events
        /// </summary>
        public int Priority
        {
            get { return _priority; }
        }

    }
}
