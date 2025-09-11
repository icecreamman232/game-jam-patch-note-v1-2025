using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(fileName = "Soul Harvest Event", menuName = "SGGames/Event/Soul Harvest")]
    public class SoulHarvestEvent : ScriptableEvent<SoulHarvestData>
    {
    
    }

    public class SoulHarvestData
    {
        public float Souls;    
    }
}
