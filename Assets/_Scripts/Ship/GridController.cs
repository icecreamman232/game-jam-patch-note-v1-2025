using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class GridController : MonoBehaviour
    {
        public Grid grid;
        public Vector2Int GridSize;

        private int m_halfGridSizeX;
        private int m_halfGridSizeY;
        private Vector3 m_bottomLeft;
        private Vector3 m_topRight;

        private void Awake()
        {
            m_halfGridSizeX = GridSize.x / 2;
            m_halfGridSizeY = GridSize.y / 2;

            m_bottomLeft = grid.CellToWorld(new Vector3Int(-m_halfGridSizeX, -m_halfGridSizeY, 0));
            m_topRight =
                grid.CellToWorld(new Vector3Int(GridSize.x - m_halfGridSizeX, GridSize.y - m_halfGridSizeX, 0));
        }

        /// <summary>
        /// Checks if a world position is within the grid bounds
        /// </summary>
        public bool IsValidWorldPosition(Vector3 worldPosition)
        {
            return worldPosition.x >= m_bottomLeft.x && worldPosition.x < m_topRight.x &&
                   worldPosition.y >= m_bottomLeft.y && worldPosition.y < m_topRight.y;
        }

        public Vector3 GetSnapPositionToGrid(Vector3 worldPosition)
        {
            var snapPos = grid.CellToWorld(grid.WorldToCell(worldPosition));
            snapPos.x += 0.25f/2;
            snapPos.y += 0.25f/2;
            return snapPos;
        }
        
        private void OnDrawGizmos()
        {
            var pos = InputManager.GetWorldMousePosition();
            var posByCell = grid.CellToWorld(grid.WorldToCell(pos));
            posByCell.z = 0;
            posByCell.x += 0.25f/2;
            posByCell.y += 0.25f/2;
            Gizmos.color = Color.red;
            Gizmos.DrawCube( posByCell, Vector3.one * 0.25f);

            DrawGridBounds();
        }
        
        private void DrawGridBounds()
        {
            m_halfGridSizeX = GridSize.x / 2;
            m_halfGridSizeY = GridSize.y / 2;
            Gizmos.color = Color.yellow;
            
            // Calculate grid bounds in world space
            Vector3 bottomLeft = grid.CellToWorld(new Vector3Int(-m_halfGridSizeX, -m_halfGridSizeY, 0));
            Vector3 topRight = grid.CellToWorld(new Vector3Int(GridSize.x - m_halfGridSizeX, GridSize.y - m_halfGridSizeY, 0));
            
            Vector3 center = (bottomLeft + topRight) * 0.5f;
            Vector3 size = topRight - bottomLeft;
            
            Gizmos.DrawWireCube(center, size);
        }

    }
}
