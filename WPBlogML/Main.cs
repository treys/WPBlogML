namespace WPBlogML
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using WPBlogML.BlogML;

    /// <summary>
    /// Control the flow of execution for the WordPress to BlogML Export program.
    /// </summary>
    class MainClass
    {
        /// <summary>
        /// Entry point for program execution.
        /// </summary>
        /// <param name="args">
        /// The arguments passed on the command line.
        /// </param>
        /// <returns>
        /// 0 if the execution was successful, -1 if not.
        /// </returns>
        public static int Main(string[] args)
        {
            if (0 == args.Length)
            {
                Console.Error.WriteLine("Usage: WPBlogML.exe [wxr_filename]");
                return -1;
            }

            Console.Out.WriteLine("\nWordPress BlogML Export v{0}", Util.MajorMinorVersion());
            Console.Out.WriteLine("Exporting from WXR file {0}", args[0]);

            var blog = ParseWXR(args[0]);
            TransformBlog(blog);
            WriteBlogML(blog);

            if (!ValidateBlogML(blog.FileName()))
                return -1;

            PostProcessing(blog);
            DisplayStats(blog);

            Console.Out.WriteLine("\nWordPress BlogML Export completed successfully!");

            return 0;
        }

        /// <summary>
        /// Parse the WXR file and create a Blog composite object
        /// </summary>
        /// <param name="wxr">
        /// The name of the WXR file to parse
        /// </param>
        /// <returns>
        /// A populated <see cref="WPBlogML.BlogML.Blog"/> with the information from the WXR file
        /// </returns>
        private static Blog ParseWXR(string wxr)
        {
            Console.Out.Write("\n[1/3] Parsing ");

            var blog = new WXRParser().ParseWXR(wxr);

            Console.Out.WriteLine("done.");

            return blog;
        }

        /// <summary>
        /// Perform transformations on the blog content
        /// </summary>
        /// <param name="blog">
        /// The <see cref="WPBlogML.BlogML.Blog"/> which should be transformed
        /// </param>
        private static void TransformBlog(Blog blog)
        {
            // TODO: implement transformations
        }

        /// <summary>
        /// Create the BlogML output.  The file name is the name of the blog and the current date/time.
        /// </summary>
        /// <param name="blog">
        /// The <see cref="WPBlogML.BlogML.Blog"/> to be written
        /// </param>
        private static void WriteBlogML(Blog blog)
        {
            Console.Out.Write("[2/3] Writing BlogML... ");

            var file = new StreamWriter(blog.FileName());
            new XmlSerializer(typeof(Blog)).Serialize(file, blog);

            file.Close();

            Console.Out.WriteLine("done.");
        }

        /// <summary>
        /// Validate the output as BlogML.
        /// </summary>
        /// <param name="blogml">
        /// The name of the file that was exported.
        /// </param>
        private static bool ValidateBlogML(string blogml)
        {
            Console.Out.Write("[3/3] Validating BlogML... ");

            bool valid = true;
            var errors = new StringBuilder();

            var settings = new XmlReaderSettings();
            settings.Schemas.Add(Schemas.BlogML2_0_With_Tags);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += (object sender, ValidationEventArgs e) =>
            {
                valid = false;
                errors.Append(e.Message);
                errors.Append("\n");
            };

            var document = new XmlDocument();
            document.Load(blogml);
            var reader = XmlReader.Create(new StringReader(document.InnerXml), settings);

            while (reader.Read()) { }

            if (valid)
            {
                Console.Out.WriteLine("done.");
            }
            else
            {
                Console.Out.WriteLine("Failed!\n");
                Console.Error.Write(errors.ToString());
            }

            return valid;
        }

        /// <summary>
        /// Perform post-processing tasks on the blog
        /// </summary>
        /// <param name="blog">
        /// The <see cref="WPBlogML.BlogML.Blog"/> being processed
        /// </param>
        private static void PostProcessing(Blog blog)
        {
            // TODO: implement post-processing
        }

        /// <summary>
        /// Display statistics about the run
        /// </summary>
        /// <param name="blog">
        /// The blog created and serialized to XML
        /// </param>
        private static void DisplayStats(Blog blog)
        {
            Console.Out.WriteLine("\nExport {0} posts (including {1} comments and {2} trackbacks),", blog.PostCount,
                blog.CommentCount, blog.TrackbackCount);
            Console.Out.WriteLine("  from {0} authors in {1} categories", blog.Authors.AuthorList.Count,
                blog.Categories.CategoryList.Count);
        }
    }
}