using Com.Pax.OpenApi.Sdk.Base.Dto;

namespace Com.Pax.OpenApi.Sdk.Base.Dto{
    public class Response<T> : BaseDTO{
        public T Data{get; set;}
    }
}