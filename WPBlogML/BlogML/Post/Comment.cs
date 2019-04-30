namespace WPBlogML.BlogML.Post
{
    using System;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using WPBlogML.BlogML.Common;

    /// <summary>
    /// Implmentation of the "commentType" from the BlogML XSD.
    /// </summary>
    [XmlRoot("comment")]
    public class Comment : Node
    {
        /// <summary>
        /// The content of the comment
        /// </summary>
        [XmlElement("content")]
        public Content Content { get; set; }

        /// <summary>
        /// The name of the user who left the comment
        /// </summary>
        [XmlAttribute("user-name")]
        public string UserName { get; set; }

        /// <summary>
        /// The e-mail address of the user who left the comment
        /// </summary>
        [XmlAttribute("user-email")]
        public string UserEmail { get; set; }

        /// <summary>
        /// The URL of the user who left the comment
        /// </summary>
        [XmlAttribute("user-url")]
        public string UserURL { get; set; }

        public Comment()
            : base()
        {
            Content = new Content();
        }

        /// <summary>
        /// Constructor for a filled comment.
        /// </summary>
        /// <param name="comment">
        /// The WXR comment element
        /// </param>
        public Comment(XElement comment)
        {
            // Node (parent) properties.
            ID = comment.Element(Util.wpNamespace + "comment_id").Value;
            Title = ID;
            DateCreated = DateTime.Parse(comment.Element(Util.wpNamespace + "comment_date_gmt").Value).ToString("s");

            Content = new Content();
            Content.Type = Content.TypeHTML;
            Content.Value = ((XCData)comment.Element(Util.wpNamespace + "comment_content").FirstNode).Value;

            UserName = ((XCData)comment.Element(Util.wpNamespace + "comment_author").FirstNode).Value;

            var email = comment.Element(Util.wpNamespace + "comment_author_email").Value;
            if (String.Empty != email)
                UserEmail = email;

            var url = comment.Element(Util.wpNamespace + "comment_author_url").Value;
            if (String.Empty != url)
                UserURL = url;
        }
    }
}