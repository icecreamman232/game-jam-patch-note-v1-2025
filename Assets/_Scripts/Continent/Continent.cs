using System;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Continent
{
    [Serializable]
    public class Race
    {
        //All values are percentage.
        
        public float White;
        public float Black;
        public float Asian;
        public float Hispanic;
    }

    [Serializable]
    public class Economy
    {
        //All values are percentage.

        public float Rich;
        public float MiddleClass;
        public float Poor;
    }

    public enum PoliticalSystem
    {
        Capitalism,
        Communism,
        Monarchy,
    }
    
    public class Continent : MonoBehaviour
    {
        [SerializeField] private string m_continentName;
        [SerializeField] private ContinentInputHandler m_continentInputHandler;
        [SerializeField] private SoulHarvestEvent m_onSoulHarvest;
        [Header("Continent Data")]
        [SerializeField] private PoliticalSystem m_politicalSystem;
        [SerializeField] private float m_population; //Million for unit
        [SerializeField] private float m_birthRate;
        [SerializeField] private float m_deathRate;
        [SerializeField] private Race m_race;
        [SerializeField] private Economy m_economy;
        
        private SoulHarvestData m_onSoulHarvestData;

        public string ContinentName => m_continentName;
        public PoliticalSystem PoliticalSystem => m_politicalSystem;
        public float Population => m_population;
        public float BirthRate => m_birthRate;
        public float DeathRate => m_deathRate;
        public Race Race => m_race;
        public Economy Economy => m_economy;
        public Action<Continent> OnSelect;

        private void Awake()
        {
            m_onSoulHarvestData = new SoulHarvestData();
            m_continentInputHandler.OnSelectedChanged = OnSelectedChanged;
        }

        private void OnSelectedChanged(bool isSelected)
        {
            OnSelect?.Invoke(isSelected ? this : null);
        }

        public void Deselect()
        {
            m_continentInputHandler.Deselect();
        }

        public void EndYear()
        {
            var newPopulation = m_birthRate + m_deathRate;
            m_onSoulHarvestData.Souls = m_deathRate;
            m_onSoulHarvest.Raise(m_onSoulHarvestData);
            m_population += newPopulation;
        }
    }
}
