using System;
using System.Linq;
using UnityEngine;
using SGGames.Scripts.Events;

namespace SGGames.Scripts.Ship
{
    public class ShipGrid : MonoBehaviour
    {
        [SerializeField] private BuildingPlacementEvent m_buildingPlacementEvent;
        [SerializeField] private Vector2Int m_gridSize;
        [SerializeField] private BuildSlot m_buildSlotPrefab;
        [SerializeField] private BuildSlot[] m_buildSlot;

        private BuildSlot[,] m_grid;
        private int m_halfWidthGridSize;
        private int m_halfHeightGridSize;

        private void Awake()
        {
            m_grid = new BuildSlot[m_gridSize.x, m_gridSize.y];
            m_halfWidthGridSize = m_gridSize.x/ 2;
            m_halfHeightGridSize = m_gridSize.y/ 2;
            for (int i = 0; i < m_gridSize.x * m_gridSize.y; i++)
            {
                int x = i % m_gridSize.x;
                int y = i / m_gridSize.x;
                var slot = Instantiate(m_buildSlotPrefab, transform);
                var worldPosition = new Vector3(x - m_halfWidthGridSize + 0.5f, y - m_halfHeightGridSize - 0.5f, 0) * 0.25f;
                slot.transform.position = worldPosition;
                m_grid[x, y] = slot;
            }
        }

        public bool CanPlaceBuilding(Vector2[] buildingOccupiedPositions)
        {
            // int count = 0;
            // foreach (var buildingPos in buildingOccupiedPositions)
            // {
            //     foreach (var slot in m_grid)
            //     {
            //         if (Vector2.Distance(buildingPos, slot.transform.position) <= 0.125f)
            //         {
            //             count++;
            //         }
            //     }
            // }
            //
            //
            // return count == buildingOccupiedPositions.Length;
            
            int count = 0;
    
            foreach (var buildingPos in buildingOccupiedPositions)
            {
                Vector2Int gridCoord = WorldToGridCoordinate(buildingPos);
        
                if (IsValidGridCoordinate(gridCoord))
                {
                    count++;
                }
            }
    
            return count == buildingOccupiedPositions.Length;
        }
        
        private Vector2Int WorldToGridCoordinate(Vector2 worldPosition)
        {
            // Convert to grid coordinates
            int gridX = Mathf.RoundToInt((worldPosition.x - m_halfWidthGridSize + 0.5f) * 0.25f);
            int gridY = Mathf.RoundToInt((worldPosition.y - m_halfHeightGridSize - 0.5f) * 0.25f);
    
            return new Vector2Int(gridX, gridY);
        }

        private bool IsValidGridCoordinate(Vector2Int gridCoord)
        {
            return gridCoord.x >= 0 && gridCoord.x < m_gridSize.x &&
                   gridCoord.y >= 0 && gridCoord.y < m_gridSize.y;
        }


        // [SerializeField] private Vector2Int m_gridSize = new Vector2Int(10, 10);
        // [SerializeField] private float m_slotSize = 1f;
        //
        // private int m_halfWidthGridSize;
        // private int m_halfHeightGridSize;
        // private BuildSlot[,] m_gridSlots;
        //
        // public Vector2Int GridSize => m_gridSize;
        // public float SlotSize => m_slotSize;
        //
        // private void Awake()
        // {
        //     InitializeGrid();
        //     m_buildingPlacementEvent.AddListener(OnReceiveBuildingPlacementEvent);
        // }
        //
        // private void OnDestroy()
        // {
        //     m_buildingPlacementEvent.RemoveListener(OnReceiveBuildingPlacementEvent);
        // }
        //
        // private void InitializeGrid()
        // {
        //     m_gridSlots = new BuildSlot[m_gridSize.x, m_gridSize.y];
        //     
        //     // If build slots are manually assigned, map them to the grid
        //     if (m_buildSlot != null && m_buildSlot.Length > 0)
        //     {
        //         for (int i = 0; i < m_buildSlot.Length && i < m_gridSize.x * m_gridSize.y; i++)
        //         {
        //             int x = i % m_gridSize.x;
        //             int y = i / m_gridSize.x;
        //             m_gridSlots[x, y] = m_buildSlot[i];
        //         }
        //     }
        //     m_halfWidthGridSize = m_gridSize.x / 2;
        //     m_halfHeightGridSize = m_gridSize.y / 2;
        //     
        // }
        //
        // private void OnReceiveBuildingPlacementEvent(BuildingPlacementEventData eventData)
        // {
        //     
        // }
        //
        // /// <summary>
        // /// Converts world position to grid coordinates
        // /// </summary>
        // public Vector2Int WorldToGridPosition(Vector3 worldPosition)
        // {
        //     Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
        //     
        //     int gridX = Mathf.FloorToInt(localPosition.x / m_slotSize - m_halfWidthGridSize  + 0.5f);
        //     int gridY = Mathf.FloorToInt(localPosition.y / m_slotSize - m_halfHeightGridSize + 0.5f);
        //     
        //     return new Vector2Int(gridX, gridY);
        // }
        //
        // /// <summary>
        // /// Converts grid coordinates to world position
        // /// </summary>
        // public Vector3 GridToWorldPosition(Vector2Int gridPosition)
        // {
        //     Vector3 localPosition = new Vector3((gridPosition.x + m_halfWidthGridSize) * m_slotSize, (gridPosition.y + m_halfHeightGridSize) * m_slotSize, 0);
        //     return transform.TransformPoint(localPosition);
        // }
        //
        // /// <summary>
        // /// Checks if a building can be placed at the specified grid position
        // /// </summary>
        // public bool CanPlaceBuilding(Building building, Vector2Int gridPosition)
        // {
        //     var occupiedPositions = building.GetOccupiedGridPositions(gridPosition);
        //     
        //     foreach (var pos in occupiedPositions)
        //     {
        //         if (!IsValidGridPosition(pos) || IsGridPositionOccupied(pos))
        //         {
        //             return false;
        //         }
        //     }
        //     
        //     return true;
        // }
        //
        // /// <summary>
        // /// Places a building at the specified grid position
        // /// </summary>
        // public bool PlaceBuilding(Building building, Vector2Int gridPosition)
        // {
        //     if (!CanPlaceBuilding(building, gridPosition))
        //     {
        //         return false;
        //     }
        //     
        //     var occupiedPositions = building.GetOccupiedGridPositions(gridPosition);
        //     
        //     // Mark all occupied positions
        //     foreach (var pos in occupiedPositions)
        //     {
        //         if (IsValidGridPosition(pos))
        //         {
        //             var slot = GetBuildSlot(pos);
        //             if (slot != null)
        //             {
        //                 slot.SetOccupied(true);
        //             }
        //         }
        //     }
        //     
        //     // Position the building in the world
        //     Vector3 worldPosition = GridToWorldPosition(gridPosition);
        //     worldPosition += new Vector3(building.PivotOffset.x, building.PivotOffset.y, 0);
        //     building.transform.position = worldPosition;
        //     
        //     return true;
        // }
        //
        // /// <summary>
        // /// Removes a building from the grid
        // /// </summary>
        // public void RemoveBuilding(Building building, Vector2Int gridPosition)
        // {
        //     var occupiedPositions = building.GetOccupiedGridPositions(gridPosition);
        //     
        //     foreach (var pos in occupiedPositions)
        //     {
        //         if (IsValidGridPosition(pos))
        //         {
        //             var slot = GetBuildSlot(pos);
        //             if (slot != null)
        //             {
        //                 slot.SetOccupied(false);
        //             }
        //         }
        //     }
        // }
        //
        // /// <summary>
        // /// Gets the closest valid grid position for a building at the given world position
        // /// </summary>
        // public Vector2Int GetClosestValidGridPosition(Building building, Vector3 worldPosition)
        // {
        //     Vector2Int gridPosition = WorldToGridPosition(worldPosition);
        //     
        //     // Try to find a valid position starting from the closest one
        //     for (int range = 0; range <= Mathf.Max(m_gridSize.x, m_gridSize.y); range++)
        //     {
        //         for (int x = -range; x <= range; x++)
        //         {
        //             for (int y = -range; y <= range; y++)
        //             {
        //                 if (Mathf.Abs(x) == range || Mathf.Abs(y) == range)
        //                 {
        //                     Vector2Int testPosition = gridPosition + new Vector2Int(x, y);
        //                     if (CanPlaceBuilding(building, testPosition))
        //                     {
        //                         return testPosition;
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     
        //     return gridPosition; // Return original position if no valid position found
        // }
        //
        // private bool IsValidGridPosition(Vector2Int gridPosition)
        // {
        //     return gridPosition.x >= 0 && gridPosition.x < m_gridSize.x &&
        //            gridPosition.y >= 0 && gridPosition.y < m_gridSize.y;
        // }
        //
        // private bool IsGridPositionOccupied(Vector2Int gridPosition)
        // {
        //     var slot = GetBuildSlot(gridPosition);
        //     return slot != null && slot.IsOccupied;
        // }
        //
        // private BuildSlot GetBuildSlot(Vector2Int gridPosition)
        // {
        //     if (IsValidGridPosition(gridPosition))
        //     {
        //         return m_gridSlots[gridPosition.x, gridPosition.y];
        //     }
        //     return null;
        // }
        //
        // private void OnDrawGizmos()
        // {
        //     // Draw grid outline
        //     Gizmos.color = Color.red;
        //     Vector3 size = new Vector3(m_gridSize.x * m_slotSize, m_gridSize.y * m_slotSize, 0.1f);
        //     Gizmos.DrawCube(transform.position + size * 0.5f, size);
        // }
    }
}
