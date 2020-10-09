using eshop.infrastructure.KafkaLog.LogModel;
using System.Threading.Tasks;

namespace eshop.infrastructure.KafkaLog.Logger
{
    public interface IKafkaLogger
    {
        Task LogAsync(BaseLogMessage message);
    }
}
