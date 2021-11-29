using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour {

	//if mouse is clicked on this object on which this script is attached
	void OnMouseDown()
	{
		//load last loaded level. in logic it's restart
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
