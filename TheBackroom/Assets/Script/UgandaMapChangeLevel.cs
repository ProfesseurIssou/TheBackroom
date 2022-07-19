using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UgandaMapChangeLevel : MonoBehaviour{
	private Button buttonLevel_0;
	private Button buttonLevel_1;

	void Start() {
		buttonLevel_0 = GameObject.Find("Player").transform.Find("GUI/UgandaMap/Buttons/ButtonLevel0").GetComponent<Button>();
		buttonLevel_1 = GameObject.Find("Player").transform.Find("GUI/UgandaMap/Buttons/ButtonLevel1").GetComponent<Button>();
		buttonLevel_0.onClick.AddListener(GotoLevel_0);
		buttonLevel_1.onClick.AddListener(GotoLevel_1);
	}

	public void GotoLevel_0() {
		GameObject player = GameObject.Find("Player");                                                  //On recupere le joueur
		player.GetComponent<PlayerSceneManage>().ChangeScene(Spawn.level_0);							//Changement du niveau
	}

	public void GotoLevel_1() {
		GameObject player = GameObject.Find("Player");                                                  //On recupere le joueur
		player.GetComponent<PlayerSceneManage>().ChangeScene(Spawn.level_1);							//Changement du niveau
	}
}
