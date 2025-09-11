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
        [SerializeField] private SoulHarvestEvent m_onSoulHarvest;
        [Header("Continent Data")]
        [SerializeField] private PoliticalSystem m_politicalSystem;
        [SerializeField] private float m_population; //Million for unit
        [SerializeField] private float m_birthRate;
        [SerializeField] private float m_deathRate;
        [SerializeField] private Race m_race;
        [SerializeField] private Economy m_economy;
        
        private SoulHarvestData m_onSoulHarvestData;

        private void Awake()
        {
            m_onSoulHarvestData = new SoulHarvestData();
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
