using System;
using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class BuildSlot : MonoBehaviour
    {
        [SerializeField] private bool m_isOccupied;
        
        public void SetOccupied(bool isOccupied) => m_isOccupied = isOccupied;
        public bool IsOccupied => m_isOccupied;

        private void OnDrawGizmos()
        {
            if (m_isOccupied)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(transform.position, Vector3.one * 0.25f);
            }
        }
    }
}
