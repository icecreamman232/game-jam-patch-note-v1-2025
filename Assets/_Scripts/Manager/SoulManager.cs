using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class SoulManager : MonoBehaviour, IGameService, IBootStrap
    {
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private SoulHarvestEvent m_soulHarvestEvent;
        [SerializeField] private float m_totalSoulHarvested;
        [SerializeField] private float m_exchangeRateForPower;
        
        public bool CanUseSoul(int amount) => m_totalSoulHarvested >= amount;
        public float TotalSoulHarvested => m_totalSoulHarvested;
        
        public void Install()
        {
            ServiceLocator.RegisterService<SoulManager>(this);
            m_soulHarvestEvent.AddListener(OnSoulHarvested);
            m_gameEvent.AddListener(OnGameEventChanged);
        }

        public void Uninstall()
        {
            m_soulHarvestEvent.RemoveListener(OnSoulHarvested);
            m_gameEvent.RemoveListener(OnGameEventChanged);
            ServiceLocator.UnregisterService<SoulManager>();
        }

        public void UseSoul(int amount)
        {
            m_totalSoulHarvested -= amount;
            if (m_totalSoulHarvested < 0)
            {
                m_totalSoulHarvested = 0;
            }
        }
        
        private void OnGameEventChanged(GameEventType gameEventType)
        {
            
        }

        private void OnSoulHarvested(SoulHarvestData soulHarvestData)
        {
            m_totalSoulHarvested += soulHarvestData.Souls;
        }
    }
}
