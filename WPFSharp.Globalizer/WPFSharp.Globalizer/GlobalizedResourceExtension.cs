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
using System.Windows;
using System.Windows.Markup;
using WPFSharp.Globalizer.Base;

namespace WPFSharp.Globalizer
{
    //[TypeConverter(typeof(GlobalizedDynamicResourceExtensionConverter))]
    [MarkupExtensionReturnType(typeof(object))]
    public class GlobalizedResourceExtension : DynamicResourceExtension
    {
        #region Constructors

        /// <summary> 
        ///  Default Contructor
        /// </summary>
        public GlobalizedResourceExtension()
        {
            FallbackValue = DependencyProperty.UnsetValue;
        }

        /// <summary> 
        ///  Constructor that takes the resource key that this is a static reference to.
        /// </summary> 
        public GlobalizedResourceExtension(object inResourceKey)
        {
            ResourceKey = inResourceKey;
            FallbackValue = DependencyProperty.UnsetValue;
        }

        /// <summary> 
        ///  Constructor that takes the resource key that this is a static reference to.
        /// </summary> 
        public GlobalizedResourceExtension(object inResourceKey, object inFallBackValue)
        {
            ResourceKey = inResourceKey;
            FallbackValue = inFallBackValue ?? DependencyProperty.UnsetValue;
        }

        #endregion

        #region Properties
        
        public Object FallbackValue { get; set; }

        #endregion

        /// <summary>
        ///  Return an object that should be set on the targetObject's targetProperty 
        ///  for this markup extension.  For DynamicResourceExtension, this is the object found in
        ///  a resource dictionary in the current parent chain that is keyed by ResourceKey 
        /// </summary> 
        /// <returns>
        ///  The object to set on this property. 
        /// </returns>
        public override object ProvideValue(IServiceProvider inServiceProvider)
        {
            if (ResourceKey == null || String.IsNullOrWhiteSpace(ResourceKey.ToString()))
                throw new InvalidOperationException("ResourceKey cannot be null or empty.");

            if (GlobalizedApplication.Instance == null)
                return FallbackValue;

            if (GlobalizedApplication.Instance.GetResource(ResourceKey.ToString()) == null)
            {
                // Add the fallback value to one of the Globalization EnhancedResourceDictionary objects
                foreach (EnhancedResourceDictionary erd in GlobalizedApplication.Instance.Resources.MergedDictionaries)
                {
                    if (erd.Type == ResourceDictionaryType.Fallback)
                        erd.Add(ResourceKey, FallbackValue);
                }
            }

            return base.ProvideValue(inServiceProvider);
        }

    }
}
