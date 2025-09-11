using System.Collections;
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class ContinentManager : MonoBehaviour, IBootStrap, IGameService
    {
        [SerializeField] private int m_currentYear;
        [SerializeField] private int m_maxYear;
        [SerializeField] private UpdateYearEvent m_updateYearEvent;
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private Continent.Continent[] m_continents;
        
        private UpdateYearEventData m_updateYearEventData;
        
        public int CurrentYear => m_currentYear;
        
        public void Install()
        {
            m_currentYear = 1;
            ServiceLocator.RegisterService<ContinentManager>(this);
            m_gameEvent.AddListener(OnGameEventChanged);
            m_updateYearEventData = new UpdateYearEventData();
        }

        public void Uninstall()
        {
            ServiceLocator.UnregisterService<ContinentManager>();
            m_gameEvent.RemoveListener(OnGameEventChanged);
        }

        private void ProcessYearEnd()
        {
            StartCoroutine(OnYearEndCoroutine());
        }

        private IEnumerator OnYearEndCoroutine()
        {
            InputManager.SetActive(false);
            
            foreach (var continent in m_continents)
            {
                continent.EndYear();
                yield return new WaitForSeconds(1f);
            }
            
            m_currentYear++;
            m_updateYearEventData.CurrentYear = m_currentYear;
            m_updateYearEvent.Raise(m_updateYearEventData);

            if (m_currentYear >= m_maxYear)
            {
                m_gameEvent.Raise(GameEventType.GameLose);
            }
            else
            {
                m_gameEvent.Raise(GameEventType.UpdateWorld);
            }
            
            InputManager.SetActive(true);
        }
        
        
        private void OnGameEventChanged(GameEventType eventType)
        {
            switch (eventType)
            {
                case GameEventType.YearEnd:
                    ProcessYearEnd();
                    break;
            }
        }
    }
}
