using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Grid m_grid; 
        [SerializeField] private Vector2Int m_gridSize;
        [SerializeField] private bool[] m_gridOccupied;

        private int m_halfGridSizeX;
        private int m_halfGridSizeY;
        private Vector3 m_bottomLeft;
        private Vector3 m_topRight;
        
        public void Initialize(Vector2Int gridSize)
        {
            m_gridSize = gridSize;
            m_gridOccupied = new bool[m_gridSize.x * m_gridSize.y];
            
            m_halfGridSizeX = m_gridSize.x / 2;
            m_halfGridSizeY = m_gridSize.y / 2;

            CalculateGridBounds();
        }

        public void CalculateGridBounds()
        {
            m_bottomLeft = m_grid.CellToWorld(new Vector3Int(-m_halfGridSizeX, -m_halfGridSizeY, 0));
            m_topRight = m_grid.CellToWorld(new Vector3Int(m_gridSize.x - m_halfGridSizeX, m_gridSize.y - m_halfGridSizeX, 0));
        }

        /// <summary>
        /// Checks if a world position is within the grid bounds
        /// </summary>
        public bool IsValidWorldPosition(Vector3 worldPosition)
        {
            return worldPosition.x >= m_bottomLeft.x && worldPosition.x < m_topRight.x &&
                   worldPosition.y >= m_bottomLeft.y && worldPosition.y < m_topRight.y;
        }
        
        public bool IsGridCellOccupied(int index)
        {
            return m_gridOccupied[index];
        }
        
        public void SetGridCellOccupied(int index, bool isOccupied)
        {
            m_gridOccupied[index] = isOccupied;
        }
        
        /// <summary>
        /// Converts a world position to a grid cell index
        /// Index 0 is at bottom-left, increments in X direction first, then Y
        /// </summary>
        /// <param name="worldPosition">World position to convert</param>
        /// <returns>Grid cell index, or -1 if position is outside grid bounds</returns>
        public int WorldPositionToGridIndex(Vector3 worldPosition)
        {
            // Check if position is within grid bounds
            if (!IsValidWorldPosition(worldPosition))
                return -1;

            // Get cell coordinates using Unity's Grid component
            Vector3Int cellCoords = m_grid.WorldToCell(worldPosition);
            
            // Convert cell coordinates to local grid coordinates (0-based from bottom-left)
            int localX = cellCoords.x + m_halfGridSizeX;
            int localY = cellCoords.y + m_halfGridSizeY;
            
            // Calculate index: row-major order (x increments first, then y)
            int index = localY * m_gridSize.x + localX;
            
            return index;
        }


        public Vector3 GetSnapPositionToGrid(Vector3 worldPosition)
        {
            var snapPos = m_grid.CellToWorld(m_grid.WorldToCell(worldPosition));
            snapPos.x += 0.25f/2;
            snapPos.y += 0.25f/2;
            return snapPos;
        }
        
        private void OnDrawGizmos()
        {
            if(m_grid == null) return;
            var pos = InputManager.GetWorldMousePosition();
            var posByCell = m_grid.CellToWorld(m_grid.WorldToCell(pos));
            posByCell.z = 0;
            posByCell.x += 0.25f/2;
            posByCell.y += 0.25f/2;
            Gizmos.color = Color.red;
            Gizmos.DrawCube( posByCell, Vector3.one * 0.25f);

            DrawGridBounds();
        }
        
        private void DrawGridBounds()
        {
            m_halfGridSizeX = m_gridSize.x / 2;
            m_halfGridSizeY = m_gridSize.y / 2;
            Gizmos.color = Color.yellow;
            
            // Calculate grid bounds in world space
            Vector3 bottomLeft = m_grid.CellToWorld(new Vector3Int(-m_halfGridSizeX, -m_halfGridSizeY, 0));
            Vector3 topRight = m_grid.CellToWorld(new Vector3Int(m_gridSize.x - m_halfGridSizeX, m_gridSize.y - m_halfGridSizeY, 0));
            
            Vector3 center = (bottomLeft + topRight) * 0.5f;
            Vector3 size = topRight - bottomLeft;
            
            Gizmos.DrawWireCube(center, size);
        }

    }
}
