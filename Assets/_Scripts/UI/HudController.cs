using System;
using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private ButtonController m_endYearButton;


        private void Awake()
        {
            m_endYearButton.OnClickCallback += OnPressEndYearButton;
        }

        private void OnDestroy()
        {
            m_endYearButton.OnClickCallback -= OnPressEndYearButton;
        }

        private void OnPressEndYearButton()
        {
            InputManager.SetActive(false);
            m_gameEvent.Raise(GameEventType.YearEnd);
        }
    }
}
