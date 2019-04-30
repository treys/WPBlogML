namespace WPBlogML.BlogML.Post
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Implementation of the "commentsType" from the BlogML XSD.
    /// </summary>
    [XmlRoot("comments")]
    public class Comments
    {
        /// <summary>
        /// The comments on the post
        /// </summary>
        [XmlElement("comment")]
        public List<Comment> CommentList { get; set; }

        public Comments()
        {
            CommentList = new List<Comment>();
        }
    }
}