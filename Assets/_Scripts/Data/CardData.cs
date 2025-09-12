using UnityEngine;

[CreateAssetMenu(fileName = "Card Data", menuName = "SGGames/Card Data")]
public class CardData : ScriptableObject
{
    public CardCategory Category;
    public string CardName;
    public Sprite Icon;
    public float GenerateDeathSpeed;
    public float DeathCount;
}

public enum CardCategory
{
    NatureCause,
    HumanCause,
    Science,
}
