namespace WPBlogML.BlogML.Post
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Implementation of the "postsType" from the BlogML XSD.
    /// </summary>
    [XmlRoot("posts")]
    public class Posts
    {
        /// <summary>
        /// The posts for the blog
        /// </summary>
        [XmlElement("post")]
        public List<Post> PostList { get; set; }

        public Posts()
        {
            PostList = new List<Post>();
        }
    }
}