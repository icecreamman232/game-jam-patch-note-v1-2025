using System;
using SGGames.Scripts.Core;
using SGGames.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SGGames.Scripts.UI
{
    public class WorldEventCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private int m_slotIndex;
        [SerializeField] private WorldEventInfoDisplayer m_worldEventInfoDisplayer;
        [SerializeField] private ButtonController m_buyButton;
        [SerializeField] private TextMeshProUGUI m_costText;
        private WorldEventData m_worldEventData;
        
        public Action<WorldEventData, int> OnBoughtEvent;
        
        private void Awake()
        {
            m_buyButton.OnClickCallback += OnPressBuyButton;
            m_worldEventInfoDisplayer.Hide();
        }

        private void OnDestroy()
        {
            m_buyButton.OnClickCallback -= OnPressBuyButton;
        }

        public void Initialize(WorldEventData data)
        {
            m_worldEventData = data;
            m_costText.text = $"{data.Cost} souls";
            m_worldEventInfoDisplayer.FillData(data);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_worldEventInfoDisplayer.Show();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_worldEventInfoDisplayer.Hide();
        }

        private void OnPressBuyButton()
        {
            OnBoughtEvent?.Invoke(m_worldEventData, m_slotIndex);
        }
    }
}
