using System;

namespace eshop.infrastructure.KafkaLog.LogModel
{
    public abstract class BaseLogMessage
    {
        public string IP { get; set; }
        public DateTime DateTime { get; set; }
        public abstract string Serialize();
    }
}
