namespace WPBlogML.BlogML.Post
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Implementation of the "trackbacksType" from the BlogML XSD.
    /// </summary>
    [XmlRoot("trackbacks")]
    public class Trackbacks
    {
        /// <summary>
        /// The trackbacks for the post
        /// </summary>
        [XmlElement("trackback")]
        public List<Trackback> TrackbackList { get; set; }

        public Trackbacks()
        {
            TrackbackList = new List<Trackback>();
        }
    }
}