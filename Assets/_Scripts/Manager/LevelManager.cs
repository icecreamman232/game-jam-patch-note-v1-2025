using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SGGames.Scripts.Core;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
}

public class LevelManager : MonoBehaviour, IGameService, IBootStrap
{
    [SerializeField] private Difficulty m_currentDifficulty;
    [SerializeField] private GameObject m_player;
    [SerializeField] private LevelContainer m_levelContainer;
    [SerializeField] private Transform m_levelSpawnPoint;

    private const int k_MaxEasyLevel = 2;
    private const int k_MaxMediumLevel = 3;
    private int m_levelIndex;
    private List<GameObject> m_easyList;
    private List<GameObject> m_mediumList;
    
    public GameObject CurrentPlayer => m_player;
    public void Install()
    {
        ServiceLocator.RegisterService<LevelManager>(this);
        m_easyList = new List<GameObject>();
        m_mediumList = new List<GameObject>();
        StartCoroutine(LoadFirstLevel());
    }

    public void Uninstall()
    {
        ServiceLocator.UnregisterService<LevelManager>();
    }

    private IEnumerator LoadFirstLevel()
    {
        //Spawn player
        
        //Get random level
        GetRandomLevels();
        m_currentDifficulty = Difficulty.Easy;
        var level = GetLevel(m_currentDifficulty, m_levelIndex);
        Instantiate(level, Vector3.zero, Quaternion.identity, m_levelSpawnPoint);
        yield return new WaitForSeconds(1f);

    }

    private void GetLevelsFromContainer(GameObject[] inputList, List<GameObject> outputList, int levelCount)
    {
        outputList.Clear();
        var levels = inputList.ToList();
        GameHelper.Shuffle(levels);
        for (int i = 0; i < levelCount; i++)
        {
            outputList.Add(levels[i]);
        }
    }
    
    private void GetRandomLevels()
    {
        GetLevelsFromContainer(m_levelContainer.EasyLevels, m_easyList, k_MaxEasyLevel);
        GetLevelsFromContainer(m_levelContainer.MediumLevels, m_mediumList, k_MaxMediumLevel);
    }

    private GameObject GetLevel(Difficulty difficulty, int levelIndex)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                levelIndex++;
                if (levelIndex > k_MaxEasyLevel - 1)
                {
                    m_currentDifficulty = Difficulty.Medium;
                }
                return m_easyList[levelIndex - 1];
            case Difficulty.Medium:
                levelIndex++;
                if (levelIndex > k_MaxMediumLevel - 1)
                {
                    m_currentDifficulty = Difficulty.Medium;
                }
                return m_mediumList[levelIndex - 1];
        }
        
        return null;
    }
}
