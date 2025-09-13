using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Announcer : MonoBehaviour
{
    [SerializeField] private AnnouncerEvent m_announcerEvent;
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private TextMeshProUGUI m_announcerText;

    private void Awake()
    {
        m_announcerEvent.AddListener(PlayMessage);
        m_canvasGroup.alpha = 0;
    }

    private void OnDestroy()
    {
        m_announcerEvent.RemoveListener(PlayMessage);
    }

    private void PlayMessage(AnnouncerEventData eventData)
    {
        m_canvasGroup.alpha = 1;
        m_announcerText.color = eventData.Color;
        m_announcerText.text = eventData.Message;
        ((RectTransform)m_announcerText.transform).rotation = Quaternion.AngleAxis(Random.Range(-15,15),Vector3.forward);
        ((RectTransform)m_announcerText.transform).LeanScale(Vector3.one * 2f, eventData.Duration)
            .setEase(LeanTweenType.punch)
            .setOnComplete(() =>
            {
                m_canvasGroup.alpha = 0;
                ((RectTransform)m_announcerText.transform).localScale = Vector3.one;
                ((RectTransform)m_announcerText.transform).rotation = Quaternion.identity;
            });
    }
}
