namespace Com.Pax.OpenApi.Sdk.Dto.Reseller {
    public class ResellerPageDTO {
        public long ID { get; set; }
        public string Name { get; set; } // name
        public string Phone { get; set; } // phone
        public string Country { get; set; } // country
        public string Postcode { get; set; } // postcode
        public string Address { get; set; } // address
        public string Company { get; set; } // company
        public string Contact { get; set; } // contact
        public string Email { get; set; }
        public string Status { get; set; }

        public override string ToString(){
            return string.Format("ID={0}, Name={1}, Phone={2}, Country={3}, Postcode={4}, Address={5}, Company={6}, Contact={7}, Email={8}, Status={9}",ID,
                Name, Phone, Country, Postcode, Address, Company, Contact, Email, Status);
        }
        
    }
}