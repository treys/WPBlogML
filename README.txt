WordPress BlogML Export v0.7
Project URL: http://wpblogml.codeplex.com

This project is licensed under the Microsoft Reciprocal License (Ms-RL); a
copy of this license may be found in the "license.txt".

ABOUT
-----
This program will take a WordPress eXtended RSS (WXR) file and convert it to
BlogML.  This will then allow the content to be imported into any content
management system that support BlogML.

NEW THIS RELEASE
----------------
This release contains two fixes, both related to changes in the WXR spec.
 - The program will now derive the WXR version from the "xmlns:wp" URL, and
   use it for the namespaces that are part of the WXR API.
 - Some version of the WXR spec made the excerpt optional; this version does
   not fail when processing a file that does not have an excerpt for every
   post.

REQUIREMENTS
------------
This program requires the .NET framework version 4 (either Windows or Mono).

INSTALLATION
------------
No installation is required - simply uncompress the distribution archive.

HOW TO USE THIS PROGRAM
-----------------------
1.  Obtain a WXR file from the WordPress blog.  From within WordPress, navigate
    to the Admin area, then go to Tools -> Export.  Save the resulting file.

2.  Run WPBlogML to convert the file.

    WPBlogML.exe [name_of_wxr_file]

    (In Linux, you'll need to enter "./" before the executable name.)

3.  The program will run, converting the blog into BlogML.  When the program is
    done, it will tell you how many categories, authors, posts, comments, and
    trackbacks were written to the BlogML export.


KNOWN ISSUES WITH THIS RELEASE
------------------------------
This release does not do anything with attachments in blog posts.  The BlogML
specification provides for either URL-linked attachments or base-64-encoded
attachments.  As such, if you are using this to migrate to a different location,
you will need to move images yourself.

RELEASE HISTORY
---------------
v0.7 - WXR version fixes
v0.5 - Initial release

CONTACT INFORMATION
-------------------
Documentation, a discussion forum, and the issue tracker for this project is
located at http://wpblogml.codeplex.com.
