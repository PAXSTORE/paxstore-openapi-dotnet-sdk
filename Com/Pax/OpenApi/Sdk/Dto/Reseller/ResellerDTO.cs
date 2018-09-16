using System;
using System.Collections.Generic;
using Com.Pax.OpenApi.Sdk.Dto.Reseller;

namespace Com.Pax.OpenApi.Sdk.Dto.Reseller{
    public class ResellerDTO : ResellerPageDTO{
        private Dictionary<string, string> EntityAttributeValues{get; set;}
	
	    public SimpleResellerDTO Parent{get; set;}

        public override string ToString(){
            return base.ToString()+", "+ string.Format("EntityAttributeValues={0}, Parent={1}", EntityAttributeValues, Parent.ToString());
        }
    }
}