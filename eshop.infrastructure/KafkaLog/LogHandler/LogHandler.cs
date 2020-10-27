using eshop.infrastructure.KafkaLog.Logger;
using System;

namespace eshop.infrastructure.KafkaLog.LogHandler
{
    public class LogHandler : ILogHandler
    {
        private readonly IKafkaLogger _logger;
        private const string MANAGER_LOGIN = "MANAGER_LOGIN";
        private const string CUSTOMER_LOGIN = "CUSTOMER_LOGIN";
        private const string CUSTOMER_REGISTER = "CUSTOMER_REGISTER";

        public LogHandler(IKafkaLogger logger)
        {
            _logger = logger;
        }

        public void LogManagerLogin(int id, string email, string role)
        {
            _ = _logger.WriteLogAsync(MANAGER_LOGIN, DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy"), id.ToString(), email, role);
        }

        public void LogCustomerLogin(int id, string email, string country, string platform)
        {
            _ = _logger.WriteLogAsync(CUSTOMER_LOGIN, DateTime.Now.ToString("s"), id.ToString(), email, country, platform);
        }

        public void LogCustomerRegister(int id, string email, string country, string platform)
        {
            _ = _logger.WriteLogAsync(CUSTOMER_REGISTER, DateTime.Now.ToString("s"), id.ToString(), email, country, platform);
        }
    }
}
