// See license at bottom of file
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace WPFSharp.Globalizer
{
    public sealed class StyleManager : ResourceDictionaryManagerBase
    {
        #region Members

        public static String DefaultStyle = "Default Style.xaml";

        #endregion

        #region Contructor

        public StyleManager(Collection<ResourceDictionary> inMergedDictionaries)
            : base(inMergedDictionaries)
        {
            SubDirectory = "Styles";
        }

        #endregion

        #region Functions
        /// <summary>
        /// Dynamically load a Localization ResourceDictionary from a file
        /// </summary>
        public void SwitchStyle(String inFileName, string inResourceDictionaryName = null)
        {
            string path = inFileName;
            if (!File.Exists(path) && !path.Contains(@":\"))
                path = Path.Combine(GlobalizedApplication.Instance.Directory, SubDirectory, inFileName);
            if (File.Exists(path))
            {
                RemoveResourceDictionaries();
                MergedDictionaries.Add(LoadFromFile(path) as StyleResourceDictionary);
                NotifyResourceDictionaryChanged();
            }
            else
            {
                Debug.WriteLine("ResourceDictionary not found: " + inFileName);
            }
        }

        public override EnhancedResourceDictionary LoadFromFile(string inFile)
        {
            string file = inFile;
            // Determine if the path is absolute or relative
            if (!Path.IsPathRooted(inFile))
            {
                string exedir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                file = Path.Combine(exedir, inFile);
            }

            if (!File.Exists(file))
                return null;

            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // Read in ResourceDictionary File or preferably an EnhancedResourceDictionary file
                var styleResourceDictionary = XamlReader.Load(fs) as StyleResourceDictionary;

                if (styleResourceDictionary == null)
                    return null;

                styleResourceDictionary.Source = inFile;
                return styleResourceDictionary;
            }
        }

        private void RemoveResourceDictionaries()
        {
            var dictionariesToRemove = new List<ResourceDictionary>();
            foreach (ResourceDictionary srd in GlobalizedApplication.Instance.Resources.MergedDictionaries)
            {
                // Make sure to only remove Styles, but don't remove styles owned by the language
                if (srd is StyleResourceDictionary && !(srd as StyleResourceDictionary).IsLinkedToLanguage)
                    dictionariesToRemove.Add(srd);
            }
            foreach (var rd in dictionariesToRemove)
            {
                GlobalizedApplication.Instance.Resources.MergedDictionaries.Remove(rd);
            }
        }
        #endregion
    }
}

#region License
/*
WPF Sharp Globalizer - A project deisgned to make localization and styling
                       easier by decoupling both process from the build.

Copyright (c) 2012, Rhyous.com
All rights reserved.
 
Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
 
1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.
3. Use of the source code or binaries that in any way competes with this
   project, whether open source or commercial or other, is prohibited unless 
   permission is granted under a separate license by Rhyous.com.
 
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
#endregion
