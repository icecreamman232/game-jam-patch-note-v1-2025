using UnityEngine;

[CreateAssetMenu(fileName = "Level Container", menuName = "SGGames/Level Container")]
public class LevelContainer : ScriptableObject
{
    public GameObject[] EasyLevels;
    public GameObject[] MediumLevels;
    public GameObject[] HardLevels;
}
