using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    private static object lockThis = new object();

    public PlayerController playerController;

    public EnemyDatabase enemyDatabase;
    public FollowingEnemy followingEnemy;
    public FlyingEnemy flyingEnemy;



   

    private void Start()
    {
        static GameManager GetGameManager()
        {
            lock (lockThis)
            {
                if (gameManager == null)
                    gameManager = new GameManager();
            }
            return gameManager;
        }
        GetGameManager();
    }
    
    [SerializeField] private PlayerController player;

    [SerializeField] private int currentScene;
    [SerializeField] private int difficultyLevel;

    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject[] platforms;
    [SerializeField] private GameObject[] exitPoint;
}
