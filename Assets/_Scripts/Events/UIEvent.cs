using UnityEngine;

namespace SGGames.Scripts.Events
{
    [CreateAssetMenu(fileName = "UI Event", menuName = "SGGames/Event/UI Event")]
    public class UIEvent : ScriptableEvent<UIEventState> { }

    public enum UIEventState
    {
        OpenBuildMode,
        CloseBuildMode,
    }
}
