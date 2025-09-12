using SGGames.Scripts.Core;
using UnityEngine;

public class LevelManager : MonoBehaviour, IGameService, IBootStrap
{
    [SerializeField] private GameObject m_player;
    
    public GameObject CurrentPlayer => m_player;
    public void Install()
    {
        ServiceLocator.RegisterService<LevelManager>(this);
    }

    public void Uninstall()
    {
        ServiceLocator.UnregisterService<LevelManager>();
    }
}
