using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LGO.Backend.Server.Model.Request.WebSocket.Converter
{
    internal sealed class WebSocketRequestConverter : JsonConverter<IWebSocketRequest>
    {
        private static readonly Dictionary<WebSocketRequestType, Func<object>> PayloadFactory = new() {{WebSocketRequestType.Ping, () => new MutableWebSocketPingRequestPayload()}};

        public override IWebSocketRequest? ReadJson(JsonReader reader, Type objectType, IWebSocketRequest? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var request = new MutableWebSocketRequest();

            // taken from https://blog.mbwarez.dk/deserializing-different-types-based-on-properties-with-newtonsoft-json/
            var jsonObject = JToken.ReadFrom(reader);

            var requestTypeReader = jsonObject[nameof(IWebSocketRequest.Type)]?.CreateReader();
            if (requestTypeReader?.Read() == false)
            {
                throw new Exception($"Unable to read the {nameof(WebSocketRequestType)}.");
            }

            if (!Enum.TryParse(typeof(WebSocketRequestType), requestTypeReader?.Value as string ?? string.Empty, false, out var parseResult) || 
                parseResult is not WebSocketRequestType requestType)
            {
                throw new Exception($"Unable to convert \"{requestTypeReader?.Value as string}\" to {nameof(WebSocketRequestType)}.");
            }
            
            if (!PayloadFactory.TryGetValue(requestType, out var factory))
            {
                throw new Exception($"Unknown {nameof(WebSocketRequestType)}: {requestType}.");
            }

            request.Type = requestType;

            var payloadReader = jsonObject[nameof(IWebSocketRequest.Payload)]?.CreateReader();
            if (payloadReader?.Read() == true)
            {
                request.Payload = factory.Invoke();
                serializer.Populate(payloadReader, request.Payload);
            }

            return request;
        }

        public override void WriteJson(JsonWriter writer, IWebSocketRequest? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}