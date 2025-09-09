using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private Vector2Int m_size;
        
        public Vector2Int Size => m_size;
    }
}
