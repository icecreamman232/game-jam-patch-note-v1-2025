using SGGames.Scripts.Events;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private TextMeshProUGUI m_yearText;
        [SerializeField] private UpdateYearEvent m_updateYearEvent;
        [SerializeField] private ButtonController m_endYearButton;


        private void Awake()
        {
            m_endYearButton.OnClickCallback += OnPressEndYearButton;
            m_updateYearEvent.AddListener(OnUpdateYear);
        }

        private void OnUpdateYear(UpdateYearEventData updateYearEventData)
        {
            m_yearText.text = $"Year: {updateYearEventData.CurrentYear}";
        }

        private void OnDestroy()
        {
            m_endYearButton.OnClickCallback -= OnPressEndYearButton;
            m_updateYearEvent.RemoveListener(OnUpdateYear);
        }

        private void OnPressEndYearButton()
        {
            m_gameEvent.Raise(GameEventType.YearEnd);
        }
    }
}
