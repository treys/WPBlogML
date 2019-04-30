namespace WPBlogML.BlogML.Post
{
    using System;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using WPBlogML.BlogML.Common;

    /// <summary>
    /// Implementation of the "trackbackType" from the BlogML XSD.
    /// </summary>
    [XmlRoot("trackback")]
    public class Trackback : Node
    {
        /// <summary>
        /// The URL of the trackback
        /// </summary>
        [XmlAttribute("url")]
        public string URL { get; set; }

        public Trackback() : base() { }

        /// <summary>
        /// Constructor for a filled trackback.
        /// </summary>
        /// <param name="trackback">
        /// The WXR comment/trackback element
        /// </param>
        public Trackback(XElement trackback)
        {

            ID = trackback.Element(Util.wpNamespace + "comment_id").Value;
            Title = ((XCData)trackback.Element(Util.wpNamespace + "comment_author").FirstNode).Value;
            DateCreated = DateTime.Parse(trackback.Element(Util.wpNamespace + "comment_date_gmt").Value).ToString("s");

            URL = trackback.Element(Util.wpNamespace + "comment_author_url").Value;
        }
    }
}