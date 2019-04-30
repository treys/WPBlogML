namespace WPBlogML.BlogML.Author
{
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

        public Author()
            : base()
        {
        }
    }
}