using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(fileName = "Total Soul Harvest Event", menuName = "SGGames/Event/Total Soul Harvest")]
    public class TotalSoulHarvestEvent : ScriptableEvent<TotalSoulHarvestData>
    {
    
    }

    public class TotalSoulHarvestData
    {
        public float TotalSoulHarvested;
    }
}
