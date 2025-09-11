using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "SGGames/Event/Game Event")]
    public class GameEvent : ScriptableEvent<GameEventType>
    {
    
    }

    public enum GameEventType
    {
        YearEnd,
    }
}
