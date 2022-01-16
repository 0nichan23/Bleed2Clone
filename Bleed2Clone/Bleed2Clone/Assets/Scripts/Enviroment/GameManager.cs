using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject EndGamePrefab;
    [SerializeField] private TMP_Text endGameText;
    [SerializeField] private Transform endLevelPoint;
    [SerializeField] private GameObject joystickCanvas;
    [SerializeField] private List<EnemyDatabase> enemyDatabases;

    private PlayerController player;

    bool wonTheGame;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (!player.isActiveAndEnabled)
        {
            wonTheGame = false;
            EndGame();
        }
        else
        if ((Vector2.Distance(player.transform.position, endLevelPoint.position) <= 3) || CheckIfAlllEnemiesDead())
        {
            wonTheGame = true;
            EndGame();
        }

    }
    public void EndGame()
    {
        Time.timeScale = 0;
        joystickCanvas.SetActive(false);
        EndGamePrefab.SetActive(true);

        if (wonTheGame)
            endGameText.text = "You Won!";
        else
            endGameText.text = "You Lost.";
    }
    public bool CheckIfAlllEnemiesDead()
    {
        foreach (var database in enemyDatabases)
        {
            if (database.activeEnemiesFromThisType.Count > 0)
                return false;
        }
        return true;
    }
}
