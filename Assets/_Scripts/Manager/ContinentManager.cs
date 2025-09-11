using System;
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class ContinentManager : MonoBehaviour, IBootStrap, IGameService
    {
        [SerializeField] private int m_currentYear;
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private Continent.Continent[] m_continents;
        
        public int CurrentYear => m_currentYear;
        
        public void Install()
        {
            m_currentYear = 1;
            ServiceLocator.RegisterService<ContinentManager>(this);
            m_gameEvent.AddListener(OnGameEventChanged);
        }

        public void Uninstall()
        {
            ServiceLocator.UnregisterService<ContinentManager>();
            m_gameEvent.RemoveListener(OnGameEventChanged);
        }

        private void ProcessYearEnd()
        {
            foreach (var continent in m_continents)
            {
                continent.EndYear();
            }

            m_currentYear++;
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
