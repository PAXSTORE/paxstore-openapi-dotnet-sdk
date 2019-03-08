using Newtonsoft.Json;

namespace Paxstore.OpenApi.Model
{
    public class MerchantCreateRequest: MerchantUpdateRequest
    {
        [JsonProperty("status")]
        private string Status;

        private bool ActivateWhenCreate;

        public void setActivateWhenCreate(bool activate)
        {
            this.ActivateWhenCreate = activate;
            if (activate)
            {
                this.Status = "A";
            }
        }
    }
}