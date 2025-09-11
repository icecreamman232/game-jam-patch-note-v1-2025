using System;
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
        [SerializeField] private PoliticalSystem m_politicalSystem;
        [SerializeField] private float m_population; //Million for unit
        [SerializeField] private float m_birthRate;
        [SerializeField] private float m_deathRate;
        [SerializeField] private Race m_race;
        [SerializeField] private Economy m_economy;

        
        private void OnYearEnd()
        {
            var newPopulation = m_birthRate + m_deathRate;
            m_population += newPopulation;
        }
    }
}
