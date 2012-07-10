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
3. Use of the source code or binaries for a competing project, whether open
   source or commercial, is prohibited unless permission is specifically
   granted under a separate license by Rhyous.com.
4. Source code enhancements or additions are the property of the author until
   the source code is contributed to this project. By contributing the source
   code to this project, the author immediately grants all rights to
   the contributed source code to Rhyous.com.
 
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

using System;
using System.IO;
using System.Threading;
using System.Windows;
using WPFSharp.Globalizer.Base;

namespace WPFSharp.Globalizer
{
    public class GlobalizedApplication : Application
    {
        public static GlobalizedApplication Instance;

        public String Directory;
        public ResourceDictionaryLoader DictionaryLoader;
        public GlobalizationManager GlobalizationManager;
        public StyleManager StyleManager;

        public GlobalizedApplication()
        {
            // Make App a singleton
            Instance = this;
            DictionaryLoader = new ResourceDictionaryLoader(Resources.MergedDictionaries);
            GlobalizationManager = new GlobalizationManager(DictionaryLoader);
            StyleManager = new StyleManager(DictionaryLoader);

            // Create and populate the SupportedLanguages singleton
            SupportedLanguages.CreateInstance();
            Directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            GlobalizationManager.GlobalizationPath = Path.Combine(Directory, "Globalization");
            SupportedLanguages.Instance.AddListFromSubDirectories(GlobalizationManager.GlobalizationPath);

            // Get current 5 character language and load the appropriate Globalization file
            GlobalizationManager.SwitchLanguage(Thread.CurrentThread.CurrentCulture.Name, true);
        }

        public EnhancedResourceDictionary FallBackResourceDictionary
        {
            get
            {
                if (_FallBackResourceDictionary == null)
                {
                    _FallBackResourceDictionary = new EnhancedResourceDictionary() { Name = "Fallback", Type = ResourceDictionaryType.Fallback };
                    
                }
                return _FallBackResourceDictionary;
            }
        }
        private EnhancedResourceDictionary _FallBackResourceDictionary;


        public object GetResource(string inKey)
        {
            if (string.IsNullOrWhiteSpace(inKey))
                throw new ArgumentNullException(inKey, "parameter cannot be null.");
            return (Instance.Resources.Contains(inKey))
                ? Instance.Resources[inKey]
                : null;
        }
    }
}
