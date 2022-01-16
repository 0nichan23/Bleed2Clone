using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    [SerializeField] List<Transform> positions;
    int i = 0;
    Transform player;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    public void GoToNextPos()
    {
        player.position = positions[i].position;
        i++;
        if (i >= positions.Count) i = 0;
    }
}
