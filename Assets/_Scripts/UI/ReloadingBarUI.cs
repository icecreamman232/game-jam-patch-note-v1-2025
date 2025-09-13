using System;
using SGGames.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingBarUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private Slider m_reloadingSlider;
    [SerializeField] private ReloadingEvent m_reloadingEvent;

    private void Awake()
    {
        m_reloadingEvent.AddListener(OnUpdateReloadingTime);
        m_canvasGroup.Deactivate();
    }

    private void OnDestroy()
    {
        m_reloadingEvent.RemoveListener(OnUpdateReloadingTime);
    }

    private void OnUpdateReloadingTime(ReloadingEventData eventData)
    {
        if (eventData.CurrentTime > 0)
        {
            m_canvasGroup.alpha = 1;
        }
        m_reloadingSlider.value = MathHelpers.Remap(eventData.CurrentTime, 0, eventData.MaxTime, 0, 1);
        if (eventData.CurrentTime >= eventData.MaxTime)
        {
            m_canvasGroup.alpha = 0;
        }
    }
}
