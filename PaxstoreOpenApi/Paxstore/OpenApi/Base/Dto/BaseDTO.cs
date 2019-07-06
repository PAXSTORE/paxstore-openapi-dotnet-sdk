namespace Paxstore.OpenApi.Base.Dto{
    public class BaseDTO{
        public int BusinessCode{get; set;}
        public string Message{get; set;}

        public string RateLimit { get; set; }
        public string RateLimitRemain { get; set; }
        public string RateLimitReset { get; set; }
    }
}
