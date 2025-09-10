using System;
using SGGames.Scripts.Events;
using UnityEngine;
using UnityEngine.Rendering.Universal;


namespace SGGames.Scripts.Ship
{
    public class ShipBuildingHandler : MonoBehaviour
    {
        [SerializeField] private BuildingPlacementEvent m_buildingPlacementEvent;
        [SerializeField] private Vector2Int m_buildAreaSize;
        [SerializeField] private GridController m_gridController;
        [SerializeField] private Transform[] m_pivots;

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
            var building = Instantiate(eventData.BuildingPrefab, transform, false);
            Debug.Log($"Index {eventData.Index}");
            building.transform.position = (Vector2)m_pivots[eventData.Index].position + eventData.OffsetFromBottomLeft;
            building.transform.localRotation = Quaternion.identity;
        }
    }
}
