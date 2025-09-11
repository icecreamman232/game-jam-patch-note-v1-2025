using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class SoulManager : MonoBehaviour, IGameService, IBootStrap
    {
        [SerializeField] private SoulHarvestEvent m_soulHarvestEvent;
        [SerializeField] private float m_totalSoulHarvested;
        [SerializeField] private float m_exchangeRateForPower;
        [SerializeField] private int m_lastPowerGained;
        
        public void Install()
        {
            ServiceLocator.RegisterService<SoulManager>(this);
            m_soulHarvestEvent.AddListener(OnSoulHarvested);
        }

        public void Uninstall()
        {
            m_soulHarvestEvent.RemoveListener(OnSoulHarvested);
            ServiceLocator.UnregisterService<SoulManager>();
        }

        private void OnSoulHarvested(SoulHarvestData soulHarvestData)
        {
            m_totalSoulHarvested += soulHarvestData.Souls;
            m_lastPowerGained = (int) (soulHarvestData.Souls * m_exchangeRateForPower);
        }
    }
}
