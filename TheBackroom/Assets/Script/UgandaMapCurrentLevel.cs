using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
	Affichage du niveau actuel 
*/

public class UgandaMapCurrentLevel : MonoBehaviour {
	private Text LevelText;
	//private GameObject Level_0_GUI;

	void Start() {
		LevelText = transform.GetComponent<Text>();
		//Level_0_GUI = GameObject.Find("Level_0_GUI");
	}

	void Update() {
		string levelName = SceneManager.GetActiveScene().name;
		LevelText.text = "Level " + levelName;
		//if(levelName == "level_0") {
		//	Level_0_GUI.SetActive(true);
		//} else {
		//	Level_0_GUI.SetActive(false);
		//}
	}
}
