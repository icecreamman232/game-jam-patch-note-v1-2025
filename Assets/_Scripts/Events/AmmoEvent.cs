using SGGames.Scripts.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo Event", menuName = "SGGames/Event/Ammo Event")]
public class AmmoEvent : ScriptableEvent<AmmoEventData>
{
    
}

public class AmmoEventData
{
    public int Ammo;
}