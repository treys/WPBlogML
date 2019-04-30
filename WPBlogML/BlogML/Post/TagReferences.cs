namespace WPBlogML.BlogML.Post
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// BlogML XSD doesn't officially support tags, but Articulate BlogML importer
    /// will import tags for each post. Use the full tag name in the ref attribute
    /// instead of a reference to an external list of defined tags.
    /// </summary>
    [XmlRoot("tags")]
    public class TagReferences
    {
        /// <summary>
        /// The tags for a post
        /// </summary>
        [XmlElement("tag")]
        public List<TagReference> TagReferenceList { get; set; }

        public TagReferences()
        {
            TagReferenceList = new List<TagReference>();
        }
    }
}