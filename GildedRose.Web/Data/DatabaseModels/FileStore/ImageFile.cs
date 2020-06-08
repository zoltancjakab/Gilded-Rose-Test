using System;

namespace GildedRose.Web.Data.DatabaseModels.FileStore
{
    public class ImageFile
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Typically files under 256KB we would store in a byte array, otherwise we store it directly in the file system. 
        /// </summary>
        public string Base64ImageContent { get; set; }
        /*
        Alternate Code we would use for image handling. But for simplicity's sake, we will use the above property.

        /// <summary>
        /// File path, if the image size > 256KB
        /// </summary>
        public string ImageFilePath { get; set; }

        /// <summary>
        /// The URL of the image. Could be a hyperlink or a data uri.
        /// </summary>
        public string ImageUri { get; set; }

        /// <summary>
        /// Alt source / text of the image.
        /// </summary>
        public string ImageAltText { get; set; }

        */
    }
}