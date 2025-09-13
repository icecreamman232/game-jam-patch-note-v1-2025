using SGGames.Scripts.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Event", menuName = "SGGames/Event/Game Event")]
public class GameEvent : ScriptableEvent<GameEventType>
{
    
}

public enum GameEventType
{
    GameStart,
}
