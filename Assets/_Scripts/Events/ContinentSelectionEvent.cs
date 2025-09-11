using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(fileName = "Continent Selection Event", menuName = "SGGames/Event/Continent Selection")]
    public class ContinentSelectionEvent : ScriptableEvent<ContinentSelectionData> { }

    public class ContinentSelectionData
    {
        public float Population;
        public float DeathRate;
    }
}
