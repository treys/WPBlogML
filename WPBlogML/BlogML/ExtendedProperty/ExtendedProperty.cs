namespace WPBlogML.BlogML.ExtendedProperty
{
    using System.Xml.Serialization;

    /// <summary>
    /// A property for the blog.
    /// </summary>
    [XmlRoot("property")]
    public class ExtendedProperty
    {
        /// <summary>
        /// The name of the property
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// The value of the property
        /// </summary>
        [XmlAttribute("value")]
        public string Value { get; set; }

        public ExtendedProperty() { }
    }
}