using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(fileName = "Building Placement Event", menuName = "SGGames/Event/Building Placement")]
    public class BuildingPlacementEvent : ScriptableEvent<BuildingPlacementEventData> { }

    public class BuildingPlacementEventData
    {
        public Transform BuildingSlot;
    }
}
