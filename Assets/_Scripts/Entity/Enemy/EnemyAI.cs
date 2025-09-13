using SGGames.Scripts.Core;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected virtual void Start()
    {
        ServiceLocator.GetService<LevelManager>().RegisterEnemy(this.gameObject);
    }
}
