
using Newtonsoft.Json;

namespace Paxstore.OpenApi.Model
{
    public class ResellerCreateRequest: ResellerUpdateRequest
    {
        //        [JsonProperty("parentResellerName")]
        //        public string ParentResellerName { get; set; }

        [JsonProperty("status")]
        private string Status;

        private bool ActivateWhenCreate;

        public void setActivateWhenCreate(bool activate) {
            this.ActivateWhenCreate = activate;
            if (activate) {
                this.Status = "A";
            }
        }
    }
}