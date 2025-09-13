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
    [SerializeField] private GameEvent m_gameEvent;

    private GameObject m_currentLevel;
    private const int k_MaxEasyLevel = 2;
    private const int k_MaxMediumLevel = 3;
    private int m_levelIndex;
    private List<GameObject> m_easyList;
    private List<GameObject> m_mediumList;
    private HashSet<GameObject> m_enemyGroup;
    
    public GameObject CurrentPlayer => m_player;
    public void Install()
    {
        ServiceLocator.RegisterService<LevelManager>(this);
        m_easyList = new List<GameObject>();
        m_mediumList = new List<GameObject>();
        m_enemyGroup = new HashSet<GameObject>();
        StartCoroutine(LoadFirstLevel());
    }

    public void Uninstall()
    {
        ServiceLocator.UnregisterService<LevelManager>();
    }

    public void RegisterEnemy(GameObject newEnemy)
    {
        m_enemyGroup.Add(newEnemy);
    }
    
    public void NotifyEnemyDeath(GameObject deathEnemy)
    {
        if (m_enemyGroup.Remove(deathEnemy))
        {
            if (m_enemyGroup.Count == 0)
            {
                m_enemyGroup.Clear();
                m_gameEvent.Raise(GameEventType.NextLevel);
                StartCoroutine(LoadNextLevel());
                Debug.Log("NEXT LEVEL");
            }
        }
    }

    private IEnumerator LoadFirstLevel()
    {
        //Spawn player
        
        //Get random level
       
        GetRandomLevels();
        m_currentDifficulty = Difficulty.Easy;
        var level = GetLevel();
        m_currentLevel = Instantiate(level, Vector3.zero, Quaternion.identity, m_levelSpawnPoint);
        CheckDifficulty();
        yield return new WaitForSeconds(1f);
        m_gameEvent.Raise(GameEventType.GameStart);
    }

    private IEnumerator LoadNextLevel()
    {
        if (m_currentLevel != null)
        {
            Destroy(m_currentLevel);
            yield return new WaitForEndOfFrame();
        }
        var level = GetLevel();
        m_currentLevel = Instantiate(level, Vector3.zero, Quaternion.identity, m_levelSpawnPoint);
        CheckDifficulty();
        yield return new WaitForSeconds(1f);
        m_gameEvent.Raise(GameEventType.GameStart);
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

    private GameObject GetLevel()
    {
        switch (m_currentDifficulty)
        {
            case Difficulty.Easy:
                return m_easyList[m_levelIndex];
            case Difficulty.Medium:
                return m_mediumList[m_levelIndex];
        }
        
        return null;
    }
    
    private void CheckDifficulty()
    {
        switch (m_currentDifficulty)
        {
            case Difficulty.Easy:
                m_levelIndex++;
                if (m_levelIndex > k_MaxEasyLevel - 1)
                {
                    m_currentDifficulty = Difficulty.Medium;
                }

                break;
            case Difficulty.Medium:
                m_levelIndex++;
                if (m_levelIndex > k_MaxMediumLevel - 1)
                {
                    m_currentDifficulty = Difficulty.Medium;
                    m_levelIndex = k_MaxMediumLevel - 1;
                }

                break;
        }
    }
}
