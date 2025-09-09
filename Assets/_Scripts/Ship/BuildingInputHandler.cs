using System;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class BuildingInputHandler : MonoBehaviour
    {
        [SerializeField] private GridController m_gridController;
        [SerializeField] private Vector2 m_offsetBottomLeft;
        [SerializeField] private Transform[] m_slotPivot;
        
        private Vector3 m_startDragPosition;
        public Action<int[]> OnBuildingPlaced; 
        
        public void Initialize(GridController gridController)
        {
            m_gridController = gridController;
        }
        
        private void OnMouseDown()
        {
            m_startDragPosition = transform.position;
        }
        
        private void OnMouseDrag()
        {
            transform.position = InputManager.GetWorldMousePosition();
        }

        private void OnMouseUp()
        {
            var count = 0;
            int[] occupiedIndexArray = new int[m_slotPivot.Length]; 
            foreach (var pivot in m_slotPivot)
            {
                if (m_gridController.IsValidWorldPosition(pivot.position))
                {
                    var index = m_gridController.WorldPositionToGridIndex(pivot.position);
                    occupiedIndexArray[count] = index;
                    count++;
                }
            }

            if (count == m_slotPivot.Length)
            {
                //Snap building to grid
                var bottomLeftByGrid = m_gridController.GetSnapPositionToGrid(m_slotPivot[0].position);
                var snapPos = (Vector2)bottomLeftByGrid + m_offsetBottomLeft;
                transform.position = snapPos;
                
                OnBuildingPlaced?.Invoke(occupiedIndexArray);
            }
            else
            {
                transform.position = m_startDragPosition;
            }
        }
    }
}
