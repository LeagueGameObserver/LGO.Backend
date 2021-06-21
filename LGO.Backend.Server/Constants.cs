using System;

namespace LGO.Backend.Server
{
    internal static class Constants
    {
        public static TimeSpan WebSocketConnectionTimeout { get; } = TimeSpan.FromMinutes(2);
    }
}