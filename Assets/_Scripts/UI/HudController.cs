using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Image m_worldSoulBar;
        [SerializeField] private ButtonController m_endYearButton;


        private void Awake()
        {
            m_endYearButton.OnClickCallback += OnPressEndYearButton;
            m_updateYearEvent.AddListener(OnUpdateYear);
            m_totalSoulHarvestEvent.AddListener(OnUpdateSoulHarvest);
            m_worldSoulBar.fillAmount = 0;
            m_totalSoulTextOnBar.text = "0/100";
        }

        private void OnDestroy()
        {
            m_endYearButton.OnClickCallback -= OnPressEndYearButton;
            m_updateYearEvent.RemoveListener(OnUpdateYear);
            m_totalSoulHarvestEvent.RemoveListener(OnUpdateSoulHarvest);
        }
        
        private void OnUpdateSoulHarvest(TotalSoulHarvestData soulHarvestData)
        {
            var requireSoul = ServiceLocator.GetService<WorldManager>().RequireSouls;
            var totalSoul = soulHarvestData.TotalSoulHarvested;
            m_totalSoulText.text = $"Souls: {totalSoul}";
            m_totalSoulTextOnBar.text = $"{totalSoul}/{requireSoul}";
            m_worldSoulBar.fillAmount = MathHelpers.Remap(totalSoul, 0, requireSoul, 0, 1);
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
