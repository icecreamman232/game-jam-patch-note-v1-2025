using System;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private BuildingPlacementEvent m_buildingPlacementEvent;
        [SerializeField] private GameObject m_buildingPrefab;
        [SerializeField] private Vector2Int m_size;
        [SerializeField] private BuildingInputHandler m_inputHandler;
        
        private Vector2 m_initialPosition;
        
        public BuildingInputHandler InputHandler => m_inputHandler;
        public Vector2Int Size => m_size;
        public GameObject Prefab => m_buildingPrefab;
        

        private void Awake()
        {
            m_inputHandler.OnBuildingPlaced = OnBuildingPlaced;
            m_initialPosition = transform.position;
        }

        private void OnBuildingPlaced()
        {
            m_buildingPlacementEvent.Raise(new BuildingPlacementEventData
            {
                BuildingPrefab = m_buildingPrefab,
                BuildingPosition = transform.position
            });
        }

        public void ResetPosition()
        {
            transform.position = m_initialPosition;
        }
    }
}
