using System;

namespace LGO.Backend.Model
{
    public interface ILeagueGameConstants
    {
        TimeSpan? InitialDragonSpawnTime { get; }

        TimeSpan? DragonRespawnTime { get; }

        TimeSpan? ElderDragonBuffDuration { get; }

        TimeSpan? InitialRiftHeraldSpawnTime { get; }

        TimeSpan? RiftHeraldRespawnTime { get; }

        TimeSpan? RiftHeraldDespawnTime { get; }

        TimeSpan? InitialBaronNashorSpawnTime { get; }

        TimeSpan? BaronNashorRespawnTime { get; }

        TimeSpan? BaronNashorBuffDuration { get; }

        TimeSpan? InhibitorRespawnTime { get; }
    }
}