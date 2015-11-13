using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Hk.Infrastructures.ElasticSearchTests
{
    class Program
    {
        static void Main(string[] args)
        {
            IElasticClient elasticClient = Hk.Infrastructures.ElasticSearch.ElasticClientManager.GetElasticClient("E234");
            var person = new Person
            {
                Id = "2",
                Firstname = "Martijn",
                Lastname = "Laarman"
            };

//            var index = elasticClient.Index(person);
//            var searchResults = elasticClient.Search<Person>(s => s
//    .From(0)
//    .Size(10)
//    .Query(q => q
//         .Term(p => p.Firstname, "martijn")
//    )
//);
        }
    }
    public class Person
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
