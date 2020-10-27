using Nest;
using System;

namespace LogParser.LogModel
{
    [ElasticsearchType(RelationName = "CustomerLogin")]

    public class CustomerLogin
    {
        [Text(Name = "Type", Norms = false, Similarity = "LMDirichlet")]
        public string Type { get; set; }
        [Number(Name = "Id")]
        public int Id { get; set; }
        [Text(Name = "Email")]
        public string Email { get; set; }
        [Date(Name = "DateTime", Format = "HH:mm:ss dd-MM-yyyy")]
        public DateTime DateTime { get; set; }
        [Text(Name = "Country")]
        public string Country { get; set; }
        [Text(Name = "Platform")]
        public string Platform { get; set; }
    }
}
