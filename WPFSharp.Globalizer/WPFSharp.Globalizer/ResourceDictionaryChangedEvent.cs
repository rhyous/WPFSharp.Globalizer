using System;
using System.Collections.Generic;

namespace WPFSharp.Globalizer
{
    public delegate void ResourceDictionaryChangedEventHandler(object source, ResourceDictionaryChangedEventArgs e);

    public class ResourceDictionaryChangedEventArgs : EventArgs
    {
        public List<string> ResourceDictionaryNames
        {
            get { return _ResourceDictionaryNames ?? (_ResourceDictionaryNames = new List<string>()); }
            set { _ResourceDictionaryNames = value; }
        } private List<string> _ResourceDictionaryNames;

        public List<string> ResourceDictionaryPaths
        {
            get { return _ResourceDictionaryPaths ?? (_ResourceDictionaryPaths = new List<string>()); }
            set { _ResourceDictionaryPaths = value; }
        } private List<string> _ResourceDictionaryPaths;
    }
}
