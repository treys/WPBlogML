namespace WPBlogML.BlogML.Category
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using WPBlogML;
    using WPBlogML.BlogML.Common;

    /// <summary>
    /// Implementation of "categoryType" from the BlogML XSD.
    /// </summary>
    [XmlRoot("category")]
    public class Category : Node
    {
        /// <summary>
        /// A reference to the parent category
        /// </summary>
        [XmlAttribute("parentref")]
        public string ParentCategory { get; set; }

        /// <summary>
        /// The description of the category
        /// </summary>
        [XmlAttribute("description")]
        public string Description { get; set; }

        public Category() : base() { }

        public Category(XElement category)
        {
            ID = category.Element(Util.wpNamespace + "category_nicename").Value;

            if (0 < category.Elements(Util.wpNamespace + "category_description").Count())
                Description = ((XCData)category.Element(Util.wpNamespace + "category_description").FirstNode).Value;

            Title = ((XCData)category.Element(Util.wpNamespace + "cat_name").FirstNode).Value;

            var parentCategory = category.Element(Util.wpNamespace + "category_parent").Value;

            if (String.Empty != parentCategory)
                ParentCategory = parentCategory;
        }
    }
}