using System;
using SGGames.Scripts.Core;
using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class ContinentInfoUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private TextMeshProUGUI m_continentNameText;
        [SerializeField] private TextMeshProUGUI m_politicalSystemText;
        [SerializeField] private TextMeshProUGUI m_populationText;
        [SerializeField] private TextMeshProUGUI m_birthRateText;
        [SerializeField] private TextMeshProUGUI m_deathRateText;

        private void Awake()
        {
            Hide();
        }

        public void Show(Continent.Continent continent)
        {
            m_continentNameText.text = continent.ContinentName;
            m_politicalSystemText.text = continent.PoliticalSystem.ToString();
            m_populationText.text = $"Population: {continent.Population}";
            m_birthRateText.text = $"Birth Rate: {continent.BirthRate}";
            m_deathRateText.text = $"Death Rate: {continent.DeathRate}";
            m_canvasGroup.Activate();
        }

        public void Hide()
        {
            m_canvasGroup.Deactivate();
        }
    }
}
