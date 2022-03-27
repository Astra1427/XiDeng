using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace XiDeng.Models
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; } = default;
        public string Message { get; set; }
        public bool IsSuccessStatusCode { get; set; }

        public ResponseModel(HttpStatusCode statusCode, string content, string message,bool isSuccessStatusCode)
        {
            StatusCode = statusCode;
            Content = content;
            Message = message;
            this.IsSuccessStatusCode = isSuccessStatusCode;
        }


        public ResponseModel()
        {

        }
    }

    public class HttpReponseMessageModel
    {
        public string Message { get; set; }
        public override string ToString()
        {
            return Message;
        }
    }
}
