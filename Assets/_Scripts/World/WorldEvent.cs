
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.World
{
    [Serializable]
    public class WorldEvent
    {
        [SerializeField] private WorldEventData m_data;
        private float m_duration;
        
        public float Duration => m_duration;
        
        public WorldEvent(WorldEventData data)
        {
            m_data = data;
        }
        
        public void StartEvent()
        {
            m_duration = Random.Range(m_data.MinDuration, m_data.MaxDuration);
        }
        
        public void UpdateEvent()
        {
            m_duration -= Time.deltaTime;
            if (m_duration <= 0)
            {
                CompleteEvent();
            }
        }

        protected virtual void CompleteEvent()
        {
            
        }
    }
}
