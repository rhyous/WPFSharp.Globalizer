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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using WPFSharp.Globalizer.Base;

namespace WPFSharp.Globalizer
{
    public class ResourceDictionaryLoader : ILoadResourceDictionaries
    {
        #region Constructor
        public ResourceDictionaryLoader(Collection<ResourceDictionary> inMergedDictionaries)
        {
            // Initialize variables
            MergedDictionaries = inMergedDictionaries;
        }
        #endregion

        #region Properties
        public Collection<ResourceDictionary> MergedDictionaries { get; set; }
        #endregion

        #region Functions
        /// <summary>
        /// Dynamically load a ResourceDictionary from a file
        /// </summary>
        public void LoadDictionariesFromFiles(List<string> inList, bool inClearPreviousDictionaries = false)
        {
            foreach (var filePath in inList)
            {
                LoadDictionaryFromFile(filePath, inClearPreviousDictionaries);
            }
        }

        /// <summary>
        /// Dynamically load a ResourceDictionary from a file
        /// </summary>
        public void LoadDictionariesFromFiles(string[] inList, bool inClearPreviousDictionaries = false)
        {
            foreach (var filePath in inList)
            {
                LoadDictionaryFromFile(filePath, inClearPreviousDictionaries);
            }
        }

        /// <summary>
        /// Dynamically load a ResourceDictionary from a file
        /// </summary>
        public void LoadDictionaryFromFile(string inFileName, bool inClearPreviousDictionaries = false)
        {
            var resourceDictionary = GetResourceDictionaryFromFile(inFileName) as EnhancedResourceDictionary;


            if (resourceDictionary != null && File.Exists(resourceDictionary.Source))
            {
                if (inClearPreviousDictionaries)
                    // Clear any previous dictionaries loaded
                    MergedDictionaries.Clear();

                // Add in newly loaded Resource Dictionary
                MergedDictionaries.Add(resourceDictionary);

                Debug.Write(false, "Successfully loaded Resource Dictionary file: " + resourceDictionary.Source);
            }
            else
            {
                Debug.Write(true, "Failed to find Resource Dictionary file: " + resourceDictionary.Source);
            }
        }


        /// <summary>
        /// Sets or replaces the ResourceDictionary by dynamically loading
        /// a ResourceDictionary from the file path passed in. This function
        /// Assumes the ResourceDictionary has a string resource named 
        /// ResourceDictionaryName and replaces the first ResourceDictionary
        /// with the same name.
        /// </summary>
        /// <param name="inNewFile"></param>
        public void ReplaceResourceDictionary(String inNewFile, string inResourceDictionaryName = null)
        {
            var newDictionary = GetResourceDictionaryFromFile(inNewFile) as EnhancedResourceDictionary;
            if (newDictionary != null && File.Exists(newDictionary.Source))
            {
                string name = inResourceDictionaryName;
                // Get the Resource Dictionary's name from the file if one is not passed in
                if (string.IsNullOrWhiteSpace(name))
                {
                    var enhancedRD = newDictionary as EnhancedResourceDictionary;
                    if (enhancedRD != null)
                    {
                        name = (enhancedRD).Name;
                    }
                    else if (newDictionary.Contains("ResourceDictionaryName"))
                    {
                        name = newDictionary["ResourceDictionaryName"].ToString();
                    }
                }
                ReplaceResourceDictionary(name, newDictionary);
            }
        }

        public void ReplaceResourceDictionary(String inResourceDictionaryName, ResourceDictionary inDictionary)
        {
            // Find the previous ResourceDictionary with the same name if loaded
            int dictId = -1;
            for (int i = 0; i < MergedDictionaries.Count; i++)
            {
                var nrd = MergedDictionaries[i] as EnhancedResourceDictionary;
                // If you used a normal ResourceDictionary, have a ResourceDictionaryName resource
                if (nrd == null)
                {
                    if (MergedDictionaries[i].Contains("ResourceDictionaryName"))
                    {
                        if (MergedDictionaries[i]["ResourceDictionaryName"].ToString().Equals(inResourceDictionaryName))
                        {
                            dictId = i;
                            break;
                        }
                    }
                }
                else // If you used a NamedResourceDictionary, just use the name
                {
                    if (String.IsNullOrWhiteSpace(nrd.Name))
                    {
                        Debug.Write(true, "NamedResourceDictionary requires a Name property.");
                        //Environment.Exit(-2);
                    }
                    else if (nrd.Name.Equals(inResourceDictionaryName))
                    {
                        dictId = i;
                        break;
                    }
                }
            }

            if (dictId == -1) // If Resource dictionary isn't already loaded
            {
                // Add the Resource Dictionary
                MergedDictionaries.Add(inDictionary);
            }
            else
            {
                // Replace the current langage dictionary with the new one
                MergedDictionaries[dictId] = inDictionary;
            }
        }

        public IEnhanceResourceDictionary GetResourceDictionaryFromFile(string inFileName)
        {
            String file = inFileName;
            // Determine if the path is absolute or relative
            if (!file.Substring(1, 2).Equals(@":\") || !file.Substring(0, 2).Equals(@"\\"))
            {
                string exedir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                file = Path.Combine(exedir, inFileName);
            }

            if (!File.Exists(file))
                return null;

            // Read in ResourceDictionary File
            EnhancedResourceDictionary resourceDictionary = null;
            try
            {
                resourceDictionary = new EnhancedResourceDictionary { Source = file };
            }
            catch
            {
                Debug.Write(true, "Failed to load resource dictionary: " + file);
            }
            return resourceDictionary;
        }
        #endregion
    }
}
