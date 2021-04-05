using System;
using System.Collections.Generic;
using LGO.LeagueOfLegends.ClientApi.Model.GameEvent;
using LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LGO.LeagueOfLegends.ClientApi.Model.Converter
{
    internal sealed class GameEventConverter : JsonConverter<ILolClientGameEvent>
    {
        private static GameEventTypeConverter GameEventTypeConverter { get; } = new();

        private static Dictionary<LolClientGameEventType, Func<ILolClientGameEvent>> GameEventFactories { get; } = new()
            {
                {LolClientGameEventType.GameStarted, () => new MutableGameStartedEvent()},
                {LolClientGameEventType.FirstMinionWaveSpawned, () => new MutableFirstMinionWaveSpawnedEvent()},
                {LolClientGameEventType.FirstTurretDestroyed, () => new MutableFirstTurretDestroyedEvent()},
                {LolClientGameEventType.TurretDestroyed, () => new MutableTurretDestroyedEvent()},
                {LolClientGameEventType.InhibitorDestroyed, () => new MutableInhibitorDestroyedEvent()},
                {LolClientGameEventType.InhibitorAboutToBeReconstructed, () => new MutableInhibitorAboutToBeReconstructedEvent()},
                {LolClientGameEventType.InhibitorReconstructed, () => new MutableInhibitorReconstructedEvent()},
                {LolClientGameEventType.DragonKilled, () => new MutableDragonKilledEvent()},
                {LolClientGameEventType.RiftHeraldKilled, () => new MutableRiftHeraldKilledEvent()},
                {LolClientGameEventType.BaronNashorKilled, () => new MutableBaronNashorKilledEvent()},
                {LolClientGameEventType.FirstChampionKilled, () => new MutableFirstChampionKilledEvent()},
                {LolClientGameEventType.ChampionKilled, () => new MutableChampionKilledEvent()},
                {LolClientGameEventType.MultipleChampionsKilled, () => new MutableMultipleChampionsKilledEvent()},
                {LolClientGameEventType.EntireTeamKilled, () => new MutableEntireTeamKilledEvent()},
                {LolClientGameEventType.GameEnded, () => new MutableGameEndedEvent()},
            };

        public override void WriteJson(JsonWriter writer, ILolClientGameEvent? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override ILolClientGameEvent? ReadJson(JsonReader reader, Type objectType, ILolClientGameEvent? existingValue, bool hasExistingValue,
                                                      JsonSerializer serializer)
        {
            // taken from https://blog.mbwarez.dk/deserializing-different-types-based-on-properties-with-newtonsoft-json/
            var jsonObject = JToken.ReadFrom(reader);

            var eventTypeReader = jsonObject["EventName"]!.CreateReader();
            if (!eventTypeReader.Read())
            {
                throw new Exception($"Unable to read the {nameof(LolClientGameEventType)}!");
            }

            var eventType = (LolClientGameEventType) GameEventTypeConverter.ReadJson(eventTypeReader, typeof(string), null, serializer)!;

            if (!GameEventFactories.TryGetValue(eventType, out var factory))
            {
                throw new Exception($"Unknown {nameof(LolClientGameEventType)}: {eventType}!");
            }

            var result = factory.Invoke();
            serializer.Populate(jsonObject.CreateReader(), result);
            return result;
        }
    }
}