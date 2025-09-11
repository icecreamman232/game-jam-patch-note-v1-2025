using SGGames.Scripts.Events;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class HudController : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private GameEvent m_gameEvent;
        [SerializeField] private UpdateYearEvent m_updateYearEvent;
        [SerializeField] private TotalSoulHarvestEvent m_totalSoulHarvestEvent;
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI m_yearText;
        [SerializeField] private TextMeshProUGUI m_totalSoulText;
        [SerializeField] private TextMeshProUGUI m_totalSoulTextOnBar;
        [SerializeField] private ButtonController m_endYearButton;


        private void Awake()
        {
            m_endYearButton.OnClickCallback += OnPressEndYearButton;
            m_updateYearEvent.AddListener(OnUpdateYear);
            m_totalSoulHarvestEvent.AddListener(OnUpdateSoulHarvest);
        }

        private void OnDestroy()
        {
            m_endYearButton.OnClickCallback -= OnPressEndYearButton;
            m_updateYearEvent.RemoveListener(OnUpdateYear);
            m_totalSoulHarvestEvent.RemoveListener(OnUpdateSoulHarvest);
        }
        
        private void OnUpdateSoulHarvest(TotalSoulHarvestData soulHarvestData)
        {
            m_totalSoulText.text = $"Souls: {soulHarvestData.TotalSoulHarvested}";
        }
        
        private void OnUpdateYear(UpdateYearEventData updateYearEventData)
        {
            m_yearText.text = $"Year: {updateYearEventData.CurrentYear}";
        }

        private void OnPressEndYearButton()
        {
            m_gameEvent.Raise(GameEventType.YearEnd);
        }
    }
}
