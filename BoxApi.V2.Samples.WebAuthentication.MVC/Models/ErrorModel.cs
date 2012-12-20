using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoxApi.V2.Samples.WebAuthentication.MVC.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
    }
}