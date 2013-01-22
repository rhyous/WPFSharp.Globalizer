namespace WPFSharp.Globalizer
{
    public class GlobalizationResourceDictionary : EnhancedResourceDictionary
    {
        /// <summary>
        /// A Style to load. This allows changing the language to also
        /// change the Style. This way colors and FlowDirection can be
        /// changed the same time as a language change.
        /// </summary>
        public string LinkedStyle { get; set; }
    }
}
