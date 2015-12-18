using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monad.EHR.Web.App.Models
{
    public class UserImageViewModel
    {
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string Base64 { get; set; }
        public string FileType { get; set; }
    }
}
