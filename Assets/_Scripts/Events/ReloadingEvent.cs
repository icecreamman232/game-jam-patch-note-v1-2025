using SGGames.Scripts.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Reloading Event", menuName = "SGGames/Event/Reloading Event")]
public class ReloadingEvent : ScriptableEvent<ReloadingEventData>
{
    
}

public class ReloadingEventData
{
    public float CurrentTime;
    public float MaxTime;
}