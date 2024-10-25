using System.Net;

namespace zIndraTechnicalChallenge.Service.Rest.Dto.SeedWork
{
    public class ResultDto
    {
        public object Data { get; set; }
        public string TraceId { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
    }
}
