using LGO.Backend.Model.Event.Enum;

namespace LGO.Backend.Model.Event
{
    public interface ILgoEvent
    {
        LgoEventType Type { get; }
    }
}