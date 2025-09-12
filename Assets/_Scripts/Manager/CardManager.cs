using System.Collections.Generic;
using SGGames.Scripts.Core;
using UnityEngine;

public class CardManager : MonoBehaviour, IBootStrap, IGameService
{
    [SerializeField] private Card m_cardPrefab;
    [SerializeField] private ButtonController m_rerollButton;
    [SerializeField] private float m_currentRerollPrice;
    [SerializeField] private CardData[] m_cardDataContainer;
    [SerializeField] private Transform[] m_cardPivot;
    private List<Card> m_currentCards;
    private SoulManager m_soulManager;
    private const float k_rerollPriceMultiplier = 1.1f;
    
    public void Install()
    {
        ServiceLocator.RegisterService<CardManager>(this);
        m_soulManager = ServiceLocator.GetService<SoulManager>();
        m_currentCards = new List<Card>();
        GenerateNewCards(3);
        m_rerollButton.OnClickButton = Reroll;
    }

    public void Uninstall()
    {
        ServiceLocator.UnregisterService<CardManager>();
    }

    public void GenerateNewCards(int number)
    {
        for (int i = 0; i < number; i++)
        {
            var cardData = GetRandomCard();
            var card = Instantiate(m_cardPrefab, m_cardPivot[i].position, Quaternion.identity);
            card.Initialize(cardData);
            m_currentCards.Add(card);
        }
    }

    public void RemoveFromPool(Card card)
    {
        m_currentCards.Remove(card);
    }
    
    private void Reroll()
    {
        if (!m_soulManager.IsSoulEnough(m_currentRerollPrice)) return;
        
        m_soulManager.SpentSoul(m_currentRerollPrice);
        foreach (var card in m_currentCards)
        {
            if (card == null) continue;
            Destroy(card.gameObject);
        }
        m_currentCards.Clear();
        GenerateNewCards(3);
        m_currentRerollPrice *= k_rerollPriceMultiplier;
    }

    private CardData GetRandomCard()
    {
        return m_cardDataContainer[Random.Range(0, m_cardDataContainer.Length)];
    }
}
