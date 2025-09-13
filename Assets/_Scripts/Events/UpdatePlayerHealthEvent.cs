using SGGames.Scripts.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Update Player Health Event", menuName = "SGGames/Event/Update Player Health Event")]
public class UpdatePlayerHealthEvent : ScriptableEvent<UpdatePlayerHealthEventData> { }

public class UpdatePlayerHealthEventData
{
    public int CurrentHealth;
    public int MaxHealth;
}