using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageService
{
    public class Image
    {
        public long Id { get; set; }

        public string Url { get; set; }

        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        
        [NotMapped]
        public long? Product_Id { get; set; }
    }

}
