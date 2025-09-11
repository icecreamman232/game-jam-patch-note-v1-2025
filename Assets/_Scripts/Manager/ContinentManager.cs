using System;
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class ContinentManager : MonoBehaviour, IBootStrap, IGameService
    {
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private Continent.Continent[] m_continents;
        
        public void Install()
        {
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
