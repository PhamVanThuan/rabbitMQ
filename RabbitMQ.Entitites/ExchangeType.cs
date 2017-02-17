using System;

namespace RabbitMQ.Entitites
{
    public static class ExchangeType
    {
        public const String Direct = "direct";
        public const String Topic = "topic";
        public const String Headers = "headers";
        public const String Fanout = "fanout";
    }
}
