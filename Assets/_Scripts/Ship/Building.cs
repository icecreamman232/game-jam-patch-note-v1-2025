using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private Vector2Int m_size;
        [SerializeField] private Vector2[] m_buildingOccupiedPositions;

        private int m_halfWidthGridSize;
        private int m_halfHeightGridSize;
        
        private void Awake()
        {
            m_buildingOccupiedPositions = new Vector2[m_size.x * m_size.y];
            m_halfWidthGridSize = m_size.x / 2;
            m_halfHeightGridSize = m_size.y / 2;
            GetOccupiedPositions();
        }
        
        
        [ContextMenu("Test")]
        public Vector2[] GetOccupiedPositions()
        {
            for (int i = 0; i < m_buildingOccupiedPositions.Length; i++)
            {
                var x = i % m_size.x;
                var y = i / m_size.x;
                var worldPosition = new Vector2(x - 0.5f, y - 0.5f ) * 0.25f;
                worldPosition += (Vector2)transform.position;
                worldPosition.x = Mathf.Round(worldPosition.x / 0.125f) * 0.125f;
                worldPosition.y = Mathf.Round(worldPosition.y / 0.125f) * 0.125f;
                m_buildingOccupiedPositions[i] = worldPosition;
            }
            
            return m_buildingOccupiedPositions;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color= Color.yellow;
            if (m_buildingOccupiedPositions != null && m_buildingOccupiedPositions.Length > 0)
            {
                for (int i = 0; i < m_buildingOccupiedPositions.Length; i++)
                {
                    Gizmos.DrawCube(m_buildingOccupiedPositions[i], Vector3.one * 0.15f);
                }
            }
        }
    }
}
