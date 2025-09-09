using System;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class BuildingInputHandler : MonoBehaviour
    {
        public GridController GridController;
        public Vector2 OffsetBottomLeft;
        public Transform[] SlotPivot;
        [SerializeField] private Building m_building;
        private Vector3 m_startDragPosition;
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
            foreach (var pivot in SlotPivot)
            {
                if (GridController.IsValidWorldPosition(pivot.position))
                {
                    count++;
                }
            }

            if (count == SlotPivot.Length)
            {
                var bottomLeftByGrid = GridController.GetSnapPositionToGrid(SlotPivot[0].position);
                //Bottom Left Pivot
                var snapPos = (Vector2)bottomLeftByGrid + OffsetBottomLeft;
                transform.position = snapPos;
                Debug.Log("Building Placed");
            }
            
            
            // if (IsCollideWithSlot())
            // {
            //     var newPos = transform.position;
            //     newPos.x = Mathf.RoundToInt(newPos.x / 0.125f) * 0.125f;
            //     newPos.y = Mathf.RoundToInt(newPos.x / 0.125f) * 0.125f;
            //     m_startDragPosition = newPos;
            // }
            // else
            // {
            //     transform.position = m_startDragPosition;
            // }
        }
    }
}
