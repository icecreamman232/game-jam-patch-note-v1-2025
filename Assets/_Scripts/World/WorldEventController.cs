using SGGames.Scripts.Core;
using SGGames.Scripts.Data;
using SGGames.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.World
{
    public class WorldEventController : MonoBehaviour, IGameService, IBootStrap
    {
        [SerializeField] private WorldEventDataContainer m_worldEventDataContainer;
        [SerializeField] private WorldEventHud m_worldEventHud;

        
        public void Install()
        {
            ServiceLocator.RegisterService<WorldEventController>(this);
            GetFirstEvents();
        }

        public void Uninstall()
        {
            ServiceLocator.UnregisterService<WorldEventController>();
        }

        public void GetFirstEvents()
        {
            for (int i = 0; i < 3; i++)
            {
                var eventData = GetRandomWorldEvent();
                m_worldEventHud.InitializeCard(i, eventData);
            }
        }

        public WorldEventData GetRandomWorldEvent()
        {
            var index = Random.Range(0, m_worldEventDataContainer.WorldEvents.Length);
            return m_worldEventDataContainer.WorldEvents[index];
        }
        
    }
}
