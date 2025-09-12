using UnityEngine;

[CreateAssetMenu(fileName = "World Event Data", menuName = "SGGames/World Event Data")]
public class WorldEventData : ScriptableObject
{
    public string EventName;
    [TextArea(3, 10)]
    public string EventDescription;
    public Sprite Icon;
    public WorldEventType EventType;
    public float MinDuration;
    public float MaxDuration;
    public float Cost;
    public WorldEventData[] EventSpawnedOnCompleted;
}

public enum WorldEventID
{
    //Natural Cause    
    Flood,
    HeavyRain,
    Earthquake,
    Tornado,
    
    
    //Human Cause start at 100
    Invasion = 100,
    
    
    
    //Invention start at 200
    
}

public enum WorldEventType
{
    NaturalCause,
    HumanCause,
    Invention,
}