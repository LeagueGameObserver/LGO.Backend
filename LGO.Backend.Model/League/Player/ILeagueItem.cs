using System;

namespace LGO.Backend.Model.League.Player
{
    public interface ILeagueItem
    {
        Guid Id { get; }
        
        string Name { get; }
        
        int Price { get; }
        
        string Image { get; }
    }
}