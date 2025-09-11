using System;
using UnityEngine;

namespace SGGames.Scripts.Continent
{
    public class ContinentInputHandler : MonoBehaviour
    {
        [SerializeField] private bool m_isSelected;
        
        public bool IsSelected => m_isSelected;
        
        public Action<bool> OnSelectedChanged;
        
        private void OnMouseDown()
        {
            m_isSelected = !m_isSelected;
            OnSelectedChanged?.Invoke(m_isSelected);
        }
    }
}
