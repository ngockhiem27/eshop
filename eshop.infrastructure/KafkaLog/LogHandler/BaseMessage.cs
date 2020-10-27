using System;
using System.Text.Json;

namespace eshop.infrastructure.KafkaLog.LogModel
{
    public abstract class BaseLogMessage
    {
        public string IP { get; set; }
        public DateTime DateTime { get; set; }
        protected string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
