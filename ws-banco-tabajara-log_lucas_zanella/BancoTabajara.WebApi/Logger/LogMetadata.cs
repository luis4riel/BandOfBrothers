using BancoTabajara.WebApi.Excessoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancoTabajara.WebApi.Logger
{
    public class LogMetadata
    {
        public string RequestUri { get; set; }
        
        public string RequestMethod { get; set; }
        
        public DateTime RequestTimestamp { get; set; }
        
        public int? ResponseStatusCode { get; set; }
        
        public DateTime ResponseTimestamp { get; set; }
       
        public ExceptionPayload ResponseExceptionPayLoad { get; set; }
    }
}