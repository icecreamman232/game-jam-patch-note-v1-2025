using System;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private WinResultScreen m_winResultScreen;
        [SerializeField] private LoseResultScreen m_loseResultScreen;

        private void Awake()
        {
            m_gameEvent.AddListener(OnGameEventChanged);
            m_winResultScreen.Hide();
            m_loseResultScreen.Hide();
        }

        private void OnDestroy()
        {
            m_gameEvent.RemoveListener(OnGameEventChanged);
        }

        private void OnGameEventChanged(GameEventType gameEventType)
        {
            if (gameEventType == GameEventType.GameWin)
            {
                m_winResultScreen.Show();
            }
            else if (gameEventType == GameEventType.GameLose)
            {
                m_loseResultScreen.Show();
            }
        }
    }
}
