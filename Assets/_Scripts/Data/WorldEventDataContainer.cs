using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(fileName = "World Event Data Container", menuName = "SGGames/World Event Data Container")]
    public class WorldEventDataContainer : ScriptableObject
    {
        public WorldEventData[] WorldEvents;
    }
}