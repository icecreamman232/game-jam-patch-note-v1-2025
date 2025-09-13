using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Accuracy Data", menuName = "SGGames/AccuracyData")]
public class AccuracyData : ScriptableObject
{
    public AccuracyLevel[] AccuracyLevels;
    
    public float GetAccuracy(int level)
    {
        return AccuracyLevels[level - 1].DifferentAngle;
    }
}

[Serializable]
public class AccuracyLevel
{
    public int Level;
    public float DifferentAngle;
}
