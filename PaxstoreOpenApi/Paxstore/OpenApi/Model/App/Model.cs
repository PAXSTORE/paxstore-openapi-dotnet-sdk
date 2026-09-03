using System;

namespace Paxstore.OpenApi.Model
{
    public class Model{
        
        public long Id { get; set; }
        public string Name { get; set; }

        public Factory Factory {get; set;}
    }
}
