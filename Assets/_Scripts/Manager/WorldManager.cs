using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class WorldManager : MonoBehaviour, IGameService, IBootStrap
    {
        [SerializeField] private int m_worldLevel;
        [SerializeField] private float m_currentRequireSouls;
        [SerializeField] private WorldLevelProgress m_worldLevelProgress;
        [SerializeField] private GameEvent m_gameEvent;
        
        private SoulManager m_soulManager;
        
        public float RequireSouls => m_worldLevelProgress.WorldLevels[m_worldLevel - 1].SoulHarvestedRequire;
        
        public void Install()
        {
            ServiceLocator.RegisterService<WorldManager>(this);
            m_soulManager = ServiceLocator.GetService<SoulManager>();
            m_gameEvent.AddListener(OnGameEventChanged);
            m_worldLevel = 1;

            m_currentRequireSouls = m_worldLevelProgress.WorldLevels[m_worldLevel - 1].SoulHarvestedRequire;
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
                m_currentRequireSouls -= m_soulManager.TotalSoulHarvested;
                if (m_currentRequireSouls <= 0)
                {
                    UpgradeWorld();
                }
            }
        }
    }
}
