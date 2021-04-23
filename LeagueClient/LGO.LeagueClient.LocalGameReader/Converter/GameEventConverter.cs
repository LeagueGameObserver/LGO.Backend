using System;
using System.Collections.Generic;
using LGO.LeagueClient.LocalGameReader.Model.GameEvent;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal sealed class GameEventConverter : JsonConverter<ILeagueClientGameEvent>
    {
        private static GameEventTypeConverter GameEventTypeConverter { get; } = new();

        private static Dictionary<LeagueClientGameEventType, Func<ILeagueClientGameEvent>> GameEventFactories { get; } = new()
                                                                                                                         {
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.GameStarted,
                                                                                                                                 () => new MutableLeagueClientGameStartedEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.FirstMinionWaveSpawned,
                                                                                                                                 () => new MutableLeagueClientFirstMinionWaveSpawnedEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.FirstTurretDestroyed,
                                                                                                                                 () => new MutableLeagueClientFirstTurretDestroyedEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.TurretDestroyed,
                                                                                                                                 () => new MutableLeagueClientTurretDestroyedEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.InhibitorDestroyed,
                                                                                                                                 () => new MutableLeagueClientInhibitorDestroyedEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.InhibitorAboutToBeReconstructed,
                                                                                                                                 () => new MutableLeagueClientInhibitorAboutToBeReconstructedEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.InhibitorReconstructed,
                                                                                                                                 () => new MutableLeagueClientInhibitorReconstructedEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.DragonKilled,
                                                                                                                                 () => new MutableLeagueClientDragonKilledEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.RiftHeraldKilled,
                                                                                                                                 () => new MutableLeagueClientRiftHeraldKilledEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.BaronNashorKilled,
                                                                                                                                 () => new MutableLeagueClientBaronNashorKilledEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.FirstChampionKilled,
                                                                                                                                 () => new MutableLeagueClientFirstChampionKilledEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.ChampionKilled,
                                                                                                                                 () => new MutableLeagueClientChampionKilledEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.MultipleChampionsKilled,
                                                                                                                                 () => new MutableLeagueClientMultipleChampionsKilledEvent()
                                                                                                                             },
                                                                                                                             {
                                                                                                                                 LeagueClientGameEventType.EntireTeamKilled,
                                                                                                                                 () => new MutableLeagueClientEntireTeamKilledEvent()
                                                                                                                             },
                                                                                                                             {LeagueClientGameEventType.GameEnded, () => new
                                                                                                                                                                       MutableLeagueClientGameEndedEvent()},
                                                                                                                         };

        public override void WriteJson(JsonWriter writer, ILeagueClientGameEvent? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override ILeagueClientGameEvent? ReadJson(JsonReader reader, Type objectType, ILeagueClientGameEvent? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // taken from https://blog.mbwarez.dk/deserializing-different-types-based-on-properties-with-newtonsoft-json/
            var jsonObject = JToken.ReadFrom(reader);

            var eventTypeReader = jsonObject["EventName"]!.CreateReader();
            if (!eventTypeReader.Read())
            {
                throw new Exception($"Unable to read the {nameof(LeagueClientGameEventType)}!");
            }

            var eventType = (LeagueClientGameEventType) GameEventTypeConverter.ReadJson(eventTypeReader, typeof(string), null, serializer)!;

            if (!GameEventFactories.TryGetValue(eventType, out var factory))
            {
                throw new Exception($"Unknown {nameof(LeagueClientGameEventType)}: {eventType}!");
            }

            var result = factory.Invoke();
            serializer.Populate(jsonObject.CreateReader(), result);
            return result;
        }
    }
}