using SGGames.Scripts.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Announcer Event", menuName = "SGGames/Event/Announcer Event")]
public class AnnouncerEvent : ScriptableEvent<AnnouncerEventData>
{
    
}

public class AnnouncerEventData
{
    public string Message;
    public Color Color;
    public float Duration = 0.3f;
}
