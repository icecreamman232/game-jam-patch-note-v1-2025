using SGGames.Scripts.Core;
using SGGames.Scripts.Managers;
using SGGames.Scripts.World;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class WorldEventHud : MonoBehaviour, IBootStrap
    {
        [SerializeField] private WorldEventCard[] m_worldEventCard;

        private SoulManager m_soulManager;
        private WorldEventController m_worldEventController;
        private ContinentManager m_continentManager;
        
        public void Install()
        {
            m_soulManager = ServiceLocator.GetService<SoulManager>();
            m_worldEventController = ServiceLocator.GetService<WorldEventController>();
            m_continentManager = ServiceLocator.GetService<ContinentManager>();
            
            foreach (var card in m_worldEventCard)
            {
                card.OnBoughtEvent = OnBoughtWorldEvent;
            }
        }

        public void Uninstall()
        {
            
        }
        

        public void InitializeCard(int index, WorldEventData data)
        {
            m_worldEventCard[index].Initialize(data, m_continentManager);
        }
        
        private void OnBoughtWorldEvent(WorldEventData data, int slotIndex)
        {
            m_soulManager.UseSoul(data.Cost);
            var newEvent = m_worldEventController.GetRandomWorldEvent();
            m_worldEventCard[slotIndex].Initialize(newEvent, m_continentManager);
        }
    }
}
