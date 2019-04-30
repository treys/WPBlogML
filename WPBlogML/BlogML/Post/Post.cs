namespace WPBlogML.BlogML.Post
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using WPBlogML.BlogML.Common;

    /// <summary>
    /// Implementation of the "postType" from the BlogML XSD.
    /// </summary>
    [XmlRoot("post")]
    public class Post : Node
    {
        /// <summary>
        /// The content of the blog post
        /// </summary>
        [XmlElement("content")]
        public Content Content { get; set; }

        /// <summary>
        /// The name of the post
        /// </summary>
        [XmlElement("post-name")]
        public Title PostName { get; set; }

        /// <summary>
        /// The excerpt for the blog post
        /// </summary>
        [XmlElement("excerpt")]
        public Content Excerpt { get; set; }

        /// <summary>
        /// Categories for this post
        /// </summary>
        [XmlElement("categories")]
        public CategoryReferences Categories { get; set; }

        /// <summary>
        /// Comments on this post
        /// </summary>
        [XmlElement("comments")]
        public Comments Comments { get; set; }

        /// <summary>
        /// Trackbacks for this blog post
        /// </summary>
        [XmlElement("trackbacks")]
        public Trackbacks Trackbacks { get; set; }

        /// <summary>
        /// Attachments for this blog post
        /// </summary>
        [XmlElement("attachments")]
        public Attachments Attachments { get; set; }

        /// <summary>
        /// Authors for this post
        /// </summary>
        [XmlElement("authors")]
        public AuthorReferences Authors { get; set; }

        /// <summary>
        /// The URL of this post
        /// </summary>
        [XmlAttribute("post-url")]
        public string PostURL { get; set; }

        /// <summary>
        /// The type of this post
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }

        /// <summary>
        /// Whether this post has an excerpt
        /// </summary>
        [XmlAttribute("hasexcerpt")]
        public bool HasExcerpt { get; set; }

        /// <summary>
        /// The number of views of this post
        /// </summary>
        [XmlAttribute("views")]
        public string Views { get; set; }

        public Post()
            : base()
        {
            // These are the only required item.
            Content = new Content();
            Authors = new AuthorReferences();
        }

        /// <summary>
        /// Constructor for a filled post.
        /// </summary>
        /// <param name="item">
        /// The WXR post element.
        /// </param>
        public Post(XElement item)
        {
            // Node (parent) properties.
            ID = item.Element(Util.wpNamespace + "post_id").Value;
            Title = item.Element("title").Value;
            DateCreated = Util.ParseRSSDate(item.Element("pubDate").Value);
            Approved = "true";

            // Object properties.
            Content = new Content();
            Content.Type = Content.TypeHTML;
            Content.Value = Util.ReplaceCRLF(((XCData)item.Element(Util.contentNamespace + "encoded").FirstNode).Value);

            PostName = new Title();
            PostName.Type = Content.TypeHTML;
            PostName.Value = item.Element("title").Value;

            var excerptNode = item.Element(Util.excerptNamespace + "encoded");

            if (null == excerptNode)
            {
                HasExcerpt = false;
            }
            else
            {
                var excerpt = ((XCData)excerptNode.FirstNode).Value;

                if (String.Empty == excerpt)
                {
                    HasExcerpt = false;
                }
                else
                {
                    Excerpt = new Content();
                    Excerpt.Type = Content.TypeHTML;
                    Excerpt.Value = excerpt;
                    HasExcerpt = true;
                }
            }

            // Category references.
            var categories =
                from cat in item.Elements("category")
                where cat.Attribute("domain") != null && cat.Attribute("domain").Value == "category"
                select cat;

            if (0 < categories.Count())
            {
                Categories = new CategoryReferences();

                foreach (var reference in categories)
                    Categories.CategoryReferenceList.Add(new CategoryReference(reference));
            }

            // Comments on this post.
            var comments =
                from comment in item.Elements(Util.wpNamespace + "comment")
                where comment.Element(Util.wpNamespace + "comment_approved").Value == "1"
                    && comment.Element(Util.wpNamespace + "comment_type").Value == String.Empty
                select comment;

            if (0 < comments.Count())
            {
                Comments = new Comments();
                
                foreach (var comment in comments)
                    Comments.CommentList.Add(new Comment(comment));
            }

            // Trackbacks for this post.
            var trackbax =
                from tb in item.Elements(Util.wpNamespace + "comment")
                where tb.Element(Util.wpNamespace + "comment_approved").Value == "1"
                    && ((tb.Element(Util.wpNamespace + "comment_type").Value == "trackback")
                        || (tb.Element(Util.wpNamespace + "comment_type").Value == "pingback"))
                select tb;

            if (0 < trackbax.Count())
            {
                Trackbacks = new Trackbacks();
                
                foreach (var trackback in trackbax)
                    Trackbacks.TrackbackList.Add(new Trackback(trackback));
            }

            Authors = new AuthorReferences();
        }
    }
}