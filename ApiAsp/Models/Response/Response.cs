using System.Net;

namespace ApiAsp.Models.Response
{
    public class Response<T>
    {
        public Response(HttpStatusCode status = HttpStatusCode.OK, string message = "", int count = 0, T data = default)
        {
            this.Status = (int)status;   
            this.Message = message; 
            this.Count = count; 
            this.Data = data;
        }

        public int Count { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public T Data { set; get; }
    }
}
