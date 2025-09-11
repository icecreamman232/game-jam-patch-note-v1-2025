using System;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private UIEvent m_uiEvent;
        [SerializeField] private GameObject m_model;

        private void Awake()
        {
            m_uiEvent.AddListener(OnReceiveUIEvent);
        }

        private void OnDestroy()
        {
            m_uiEvent.RemoveListener(OnReceiveUIEvent);
        }

        private void OnReceiveUIEvent(UIEventState uiEventState)
        {
            if (uiEventState == UIEventState.OpenBuildMode)
            {
                this.gameObject.SetActive(false);
            }
            else if (uiEventState == UIEventState.CloseBuildMode)
            {
                this.gameObject.SetActive(true);
            }
        }
    }
}
