using System.Threading.Tasks;

namespace eshop.infrastructure.KafkaLog.Logger
{
    public interface IKafkaLogger
    {
        Task WriteLogAsync(params string[] fields);
    }
}
