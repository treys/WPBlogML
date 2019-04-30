namespace WPBlogML.BlogML.Author
{
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using WPBlogML.BlogML.Common;

    /// <summary>
    /// An author of posts within a blog.
    /// </summary>
    [XmlRoot("author")]
    public class Author : Node
    {
        /// <summary>
        /// The e-mail address for the author
        /// </summary>
        [XmlAttribute("email")]
        public string Email { get; set; }

        public Author() : base() { }

        public Author(XElement author) : base()
        {
            ID = Util.Slug(((XCData)author.Element(Util.wpNamespace + "author_login").FirstNode).Value);
            Email = ((XCData)author.Element(Util.wpNamespace + "author_email").FirstNode).Value;
            Title = ((XCData)author.Element(Util.wpNamespace + "author_display_name").FirstNode).Value;
        }
    }
}