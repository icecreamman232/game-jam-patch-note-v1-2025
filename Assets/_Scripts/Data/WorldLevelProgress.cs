using System;
using UnityEngine;

[CreateAssetMenu(fileName = "World Level Data", menuName = "SGGames/World Level")]
public class WorldLevelProgress : ScriptableObject
{
    public WorldLevelData[] WorldLevels;
}

[Serializable]
public class WorldLevelData
{
    public int Level;
    public float SoulHarvestedRequire;
    public float BonusSoulHarvested;
}
