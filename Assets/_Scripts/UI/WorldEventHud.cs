using System;
using SGGames.Scripts.Core;
using SGGames.Scripts.Managers;
using SGGames.Scripts.World;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class WorldEventHud : MonoBehaviour
    {
        [SerializeField] private WorldEventCard[] m_worldEventCard;

        private void Awake()
        {
            foreach (var card in m_worldEventCard)
            {
                card.OnBoughtEvent = OnBoughtWorldEvent;
            }
        }

        public void InitializeCard(int index, WorldEventData data)
        {
            m_worldEventCard[index].Initialize(data);
        }
        
        private void OnBoughtWorldEvent(WorldEventData data, int slotIndex)
        {
            ServiceLocator.GetService<SoulManager>().UseSoul(data.Cost);
            var newEvent = ServiceLocator.GetService<WorldEventController>().GetRandomWorldEvent();
            m_worldEventCard[slotIndex].Initialize(newEvent);
        }
    }
}
