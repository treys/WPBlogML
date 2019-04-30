namespace WPBlogML.BlogML.Post
{
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// BlogML XSD doesn't officially support tags, but Articulate BlogML importer
    /// will import tags for each post. Use the full tag name in the ref attribute
    /// instead of a reference to an external list of defined tags.
    /// </summary>
    [XmlRoot("tag")]
    public class TagReference
    {
        /// <summary>
        /// The Name of the tag
        /// </summary>
        [XmlAttribute("ref")]
        public string Name { get; set; }

        public TagReference() { }

        /// <summary>
        /// Constructor for a filled tag reference.
        /// </summary>
        /// <param name="reference">
        /// The WXR tag element
        /// </param>
        public TagReference(XElement reference)
        {
            Name = reference.Value;
        }
    }
}
