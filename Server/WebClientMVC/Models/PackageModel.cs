using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClientMVC.Models
{
    public class PackageModel
    {
        public int StatusID { get; set; }
        public int SenderID { get; set; }
        public int CourierID { get; set; }
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }
}