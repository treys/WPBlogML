namespace WPBlogML.BlogML.Post
{
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Implementation of the "categoryRefType" from the BlogML XSD.
    /// </summary>
    [XmlRoot("category")]
    public class CategoryReference
    {
        /// <summary>
        /// The ID of the category
        /// </summary>
        [XmlAttribute("ref")]
        public string ID { get; set; }

        public CategoryReference() { }

        /// <summary>
        /// Constructor for a filled category reference.
        /// </summary>
        /// <param name="reference">
        /// The WXR category element
        /// </param>
        public CategoryReference(XElement reference)
        {
            ID = reference.Attribute("nicename").Value;
        }
    }
}