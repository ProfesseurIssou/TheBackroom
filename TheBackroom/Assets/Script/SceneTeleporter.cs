using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SceneTeleporter : MonoBehaviour{

	public Spawn destination;																									//Destination du teleporteur

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.name == "Player") {																						//Si collision avec le joueur
			switch(destination) {																										//Pour chaque destination
				case Spawn.level_0:																											//Level 0
					GameObject.Find("Player").GetComponent<PlayerSceneManage>().ChangeScene(Spawn.level_0);										//Changement de scene
					break;
				case Spawn.level_1:                                                                                                         //Level 1
					GameObject.Find("Player").GetComponent<PlayerSceneManage>().ChangeScene(Spawn.level_1);                                     //Changement de scene
					break;
			}
		}
	}

}
