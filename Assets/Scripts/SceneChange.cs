using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public void ButtonMoveScene(string level){
		SceneManager.LoadScene(level);
		Debug.Log("Scene changed to " + level);
	}
}
