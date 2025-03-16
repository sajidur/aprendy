using System.Net;
using System.Text.Json.Serialization;

namespace Apprendi.Application.Common.Base
{
    public abstract class Response
    {
        public bool IsSuccess { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string ErrorMessage { get; set; }
        
        public HttpStatusCode StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<ValidationError> ValidationErrors { get; set; } = new();
    }
}
