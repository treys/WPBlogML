namespace WPBlogML.BlogML.Common
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Implementation of "contentType" from the BlogML XSD.
    /// </summary>
    [XmlRoot("content")]
    public class Content
    {
        public static string TypeHTML = "html";
        public static string TypeXHTML = "xhtml";
        public static string TypeText = "text";
        public static string TypeBase64 = "base64";

        /// <summary>
        /// The type of content (use the "Type*" strings defined in this class).
        /// </summary>
        [XmlAttribute("type")]
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (!new List<string>(new string[] { TypeHTML, TypeXHTML, TypeText, TypeBase64 }).Contains(value))
                    throw new NotSupportedException(String.Format("Content type {0} is not supported in BlogML", value));

                type = value;
            }
        }

        // Actual type variable.
        private string type;

        /// <summary>
        /// The content.
        /// </summary>
        [XmlText(typeof(string))]
        public string Value { get; set; }

        public Content() { }
    }
}