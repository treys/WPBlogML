namespace WPBlogML
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    /// <summary>
    /// Utility methods.
    /// </summary>
    public static class Util
    {
        private static AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();

        /// <summary>
        /// The current version of WXR we're processing
        /// </summary>
        public static string WxrVersion { get; set; }

        /// <summary>
        /// Namespace for BlogML 2.0
        /// </summary>
        public static XNamespace blogML2point0Namespace = "http://www.blogml.com/2006/09/BlogML";

        /// <summary>
        /// Namespace for BlogML 2.5
        /// </summary>
        public static XNamespace blogML2point5Namespace = "http://blogml.org/2008/01/BlogML";

        /// <summary>
        /// The "wp:" namespace in the WXR file
        /// </summary>
        public static XNamespace wpNamespace
        {
            get
            {
                if (String.IsNullOrEmpty(WxrVersion))
                    throw new ArgumentException("Cannot retrieve wp: namespace before WXR version is set");

                return String.Format("http://wordpress.org/export/{0}/", WxrVersion);
            }
        }

        /// <summary>
        /// The "dc:" namespace in the WXR file
        /// </summary>
        public static XNamespace dcNamespace = "http://purl.org/dc/elements/1.1/";

        /// <summary>
        /// The "excerpt:" namespace in the WXR file
        /// </summary>
        public static XNamespace excerptNamespace
        {
            get
            {
                if (String.IsNullOrEmpty(WxrVersion))
                    throw new ArgumentException("Cannot retrieve exceprt: namespace before WXR version is set");

                return String.Format("http://wordpress.org/export/{0}/excerpt/", WxrVersion);
            }
        }

        /// <summary>
        /// The "content:" namespace in the WXR file
        /// </summary>
        public static XNamespace contentNamespace = "http://purl.org/rss/1.0/modules/content/";

        /// <summary>
        /// Parse an RSS format date/time to a C# DateTime.
        /// </summary>
        /// <param name="date">
        /// The date/time from the RSS feed.
        /// </param>
        /// <returns>
        /// The RSS time as a <see cref="System.DateTime"/>
        /// </returns>
        public static string ParseRSSDate(string date)
        {
            return DateTime.ParseExact(date, "ddd, dd MMM yyyy HH:mm:ss zz00",
                                       (new CultureInfo("en-US")).DateTimeFormat).ToString("s");
        }

        /// <summary>
        /// Create a "slug" - all lower case, no spaces, no special characters.
        /// </summary>
        /// <param name="text">
        /// The text to turn into a slug
        /// </param>
        /// <returns>
        /// The slug
        /// </returns>
        public static string Slug(string text)
        {
            return Regex.Replace(text, @"[^\w\.-]", "-").ToLower();
        }

        /// <summary>
        /// Get the major / minor version of this application
        /// </summary>
        /// <returns>
        /// The major/minor version of the application
        /// </returns>
        public static string MajorMinorVersion()
        {
            return String.Format("{0}.{1}", assembly.Version.Major, assembly.Version.Minor);
        }
    }
}