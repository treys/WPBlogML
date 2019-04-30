namespace WPBlogML.BlogML.Post
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Implementation of the "categoriesRefType" from the BlogML XSD.
    /// </summary>
    [XmlRoot("categories")]
    public class CategoryReferences
    {
        /// <summary>
        /// The categories for a post
        /// </summary>
        [XmlElement("category")]
        public List<CategoryReference> CategoryReferenceList { get; set; }

        public CategoryReferences()
        {
            CategoryReferenceList = new List<CategoryReference>();
        }
    }
}