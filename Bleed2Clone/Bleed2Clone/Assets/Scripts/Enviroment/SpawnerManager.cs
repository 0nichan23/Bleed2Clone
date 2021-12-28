using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour {

	public GameObject enemy;
	public int maxNumberOfSpawns;
	private int currentNumberOfSpawns;
	public float timeBetweenSpawning;
	public float timeOfSpawn;

	// Use this for initialization
	void Start () {
		currentNumberOfSpawns = maxNumberOfSpawns;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentNumberOfSpawns > 0) {
			if (Time.time - timeOfSpawn >= timeBetweenSpawning) {
				Instantiate (enemy, transform.position, transform.rotation);
				timeOfSpawn = Time.time;
				currentNumberOfSpawns -= 1;
			}
		}
	}
}
