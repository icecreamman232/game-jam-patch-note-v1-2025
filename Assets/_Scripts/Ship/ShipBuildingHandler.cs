using System;
using SGGames.Scripts.Events;
using UnityEngine;


namespace SGGames.Scripts.Ship
{
    public class ShipBuildingHandler : MonoBehaviour
    {
        [SerializeField] private BuildingPlacementEvent m_buildingPlacementEvent;
        [SerializeField] private Vector2Int m_buildAreaSize;
        [SerializeField] private GridController m_gridController;

        public GridController GridController => m_gridController;
        
        private void Awake()
        {
            m_gridController.Initialize(m_buildAreaSize);
            m_buildingPlacementEvent.AddListener(CreateBuilding);
        }

        private void OnDestroy()
        {
            m_buildingPlacementEvent.RemoveListener(CreateBuilding);
        }


        public void CreateBuilding(BuildingPlacementEventData eventData)
        {
            Instantiate(eventData.BuildingPrefab, eventData.BuildingPosition, Quaternion.identity, transform);
        }
    }
}
