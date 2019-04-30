namespace WPBlogML
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using WPBlogML.BlogML;
    using WPBlogML.BlogML.Author;
    using WPBlogML.BlogML.Category;
    using WPBlogML.BlogML.Post;

    /// <summary>
    /// Reads a WordPress eXtended RSS (WXR) file and creates a Blog object from it.
    /// </summary>
    public class WXRParser
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public WXRParser() { }

        /// <summary>
        /// Parse the WXR file and create a blog composite object
        /// </summary>
        /// <param name="wxr">
        /// The filename of the WXR file
        /// </param>
        /// <returns>
        /// The <see cref="WPBlogML.BlogML.Blog"/> object filled with the data extracted from the WXR file.
        /// </returns>
        /// <exception cref="System.IO.FileNotFoundException">
        /// If the file does not exist
        /// </exception>
        public Blog ParseWXR(string wxr)
        {
            // Make sure the file exists.
            if (!(new FileInfo(wxr)).Exists)
                throw new FileNotFoundException(String.Format("Unable to find WXR file {0}", wxr));

            // Get the RSS channel.
            var channel = XElement.Load(wxr).Element("channel");

            // Determine what version we're importing
            var wpNamespace = channel.GetNamespaceOfPrefix("wp").ToString().Replace("http://wordpress.org/export/", "");

            if (wpNamespace.EndsWith("/"))
                wpNamespace = wpNamespace.Substring(0, wpNamespace.Length - 1);

            Util.WxrVersion = wpNamespace;

            Console.Out.Write("version {0} WXR file... ", wpNamespace);

            // Create the blog with the base information.
            var blog = new Blog(channel);

            GetCategories(blog, channel);
            GetPosts(blog, channel);

            return blog;
        }

        /// <summary>
        /// Create the category list for the blog.
        /// </summary>
        /// <param name="blog">
        /// The <see cref="WPBlogML.BlogML.Blog"/> being constructed
        /// </param>
        /// <param name="channel">
        /// The RSS channel in the WXR feed
        /// </param>
        private void GetCategories(Blog blog, XElement channel)
        {
            // Loop through the elements and build the category list.
            var categories =
                from cat in channel.Elements(Util.wpNamespace + "category")
                select cat;

            foreach (var categoryElement in categories)
                blog.Categories.CategoryList.Add(new Category(categoryElement));

            // WordPress stores the parent category as the description, but BlogML wants the ID.  Now that we have a
            // complete list, we'll go back through and fix them.
            var children =
                from a in blog.Categories.CategoryList
                where a.ParentCategory != null
                select a;

            foreach (var child in children)
            {
                var parent =
                    from a in blog.Categories.CategoryList
                    where a.Title == child.ParentCategory
                    select a;

                if (0 < parent.Count())
                    child.ParentCategory = parent.ElementAt(0).ID;
            }
        }

        /// <summary>
        /// Get all posts from the WXR.
        /// </summary>
        /// <param name="blog">
        /// The <see cref="WPBlogML.BlogML.Blog"/> being constructed
        /// </param>
        /// <param name="channel">
        /// The RSS channel in the WXR feed
        /// </param>
        private void GetPosts(Blog blog, XElement channel)
        {
            var posts =
                from item in channel.Elements("item")
                where item.Element(Util.wpNamespace + "status").Value == "publish"
                select item;

            foreach (var item in posts)
            {
                var post = new Post(item);

                // We need to get the author reference separately, as we need the AuthorList from the blog.
                var author = new AuthorReference();
                author.ID = GetAuthorReference(blog, item.Element(Util.dcNamespace + "creator").FirstNode.ToString());

                post.Authors.AuthorReferenceList.Add(author);

                blog.Posts.PostList.Add(post);
            }
        }

        /// <summary>
        /// Get a reference to an author.
        /// </summary>
        /// <remarks>
        /// This method allows us to accrue author names as we parse posts; WXR does not contain an author list.
        /// This method will convert the author name to a slug, then check to see if there is an author in the blog
        /// whose ID matches that slug.  If there is, it will return the ID for future use in the post; if it is not,
        /// the author entry will be created before the ID is returned.  Some manipulation must be done here, because
        /// the only thing WordPress exports is a friendly name; the slug will become the ID, and the e-mail address
        /// will be this ID at the domain of the blog's root URL.
        /// </remarks>
        /// <param name="blog">
        /// The <see cref="WPBlogML.BlogML.Blog"/> being constructed
        /// </param>
        /// <param name="author">
        /// The name of the author
        /// </param>
        private string GetAuthorReference(Blog blog, string author)
        {
            var id = Util.Slug(author);

            var authors =
                from a in blog.Authors.AuthorList
                where a.ID == id
                select a;

            if (0 == authors.Count())
            {
                // Author not found - let's create them!
                var newAuthor = new Author();
                newAuthor.ID = id;
                newAuthor.Email = id + "@" + (new Uri(blog.RootURL)).Host;
                newAuthor.Title = author;
                blog.Authors.AuthorList.Add(newAuthor);
            }

            return id;
        }
    }
}