using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(fileName = "Update Year Event", menuName = "SGGames/Event/Update Year")]
    public class UpdateYearEvent : ScriptableEvent<UpdateYearEventData> { }

    public class UpdateYearEventData
    {
        public int CurrentYear;
    }
}
