using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class WorldManager : MonoBehaviour, IGameService, IBootStrap
    {
        [SerializeField] private int m_worldLevel;
        [SerializeField] private float m_requireSouls;
        [SerializeField] private WorldLevelProgress m_worldLevelProgress;
        [SerializeField] private GameEvent m_gameEvent;
        
        private SoulManager m_soulManager;
        
        public void Install()
        {
            ServiceLocator.RegisterService<WorldManager>(this);
            m_soulManager = ServiceLocator.GetService<SoulManager>();
            m_gameEvent.AddListener(OnGameEventChanged);
            m_worldLevel = 1;

            m_requireSouls = m_worldLevelProgress.WorldLevels[m_worldLevel - 1].SoulHarvestedRequire;
        }

        public void Uninstall()
        {
            ServiceLocator.UnregisterService<WorldManager>();
            m_soulManager = null;
            m_gameEvent.RemoveListener(OnGameEventChanged);
        }

        private void UpgradeWorld()
        {
            
        }
        
        private void OnGameEventChanged(GameEventType gameEventType)
        {
            if (gameEventType == GameEventType.UpdateWorld)
            {
                m_requireSouls -= m_soulManager.TotalSoulHarvested;
                if (m_requireSouls <= 0)
                {
                    UpgradeWorld();
                }
            }
        }
    }
}
