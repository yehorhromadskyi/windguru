using System.Net;

namespace Windguru.Core.Models.Http
{
    public class Response 
    {
        public HttpStatusCode Status { get; set; }
        public string Result { get; set; }

        public bool IsSuccess => Status == HttpStatusCode.OK;
    }
}
