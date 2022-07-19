using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
	Gestion de la teleportation du joueur 
*/

public enum Spawn {
	level_0,
	level_1
}

public class PlayerSceneManage : MonoBehaviour{
	private float nbSecondeTransition = 3;
	private GameObject player;                                                                              //Joueur
	private PlayerBlackscreen playerBlackscreen;                                                            //Ecran noir de transition
	private Spawn ?nextLevel;																				//Niveau a teleporter apres transition (null si aucune teleportation en cour)


	void Start(){
		player = this.gameObject;
		playerBlackscreen = this.gameObject.GetComponent<PlayerBlackscreen>();
	}
	void Update(){
		if(!playerBlackscreen.InTransition() && nextLevel != null) TeleportNextLevel();							//Si transition fini ET qu'une teleportation est attendue
	}

	//Demande pour changer de scene
	public void ChangeScene(Spawn levelSpawn) {
		nextLevel = levelSpawn;                                                                                 //Definition du prochain niveau
	}


	//Teleportation vers la prochaine scene
	private void TeleportNextLevel() {
		//Enlever les effets du niveau
		string currentLevel = SceneManager.GetActiveScene().name;												//Nom de la scene actuel
		if(currentLevel == "level_0") gameObject.GetComponent<PlayerStats>().RemoveEffect("level_0");			//On enleve le malus mental

		//Teleportation
		switch(nextLevel) {																						//Pour chaque scene
			case Spawn.level_0:																						//Si level_0
				gameObject.GetComponent<PlayerStats>().AddEffect(new Effect("level_0", "level 0", EffectType.Malus, StatType.Mental, 0.2f, -1));//On ajoute le malus de mentale
				StartCoroutine(Teleport("level_0", new Vector3(5, 1, 5)));												//Teleportation
				break;
			case Spawn.level_1:																						//Si level_1
				StartCoroutine(Teleport("level_1", new Vector3(0, 2.5f, 0)));											//Teleportation
				break;
		}

		playerBlackscreen.StartTransition(nbSecondeTransition / 2, TransitionType.ToClear);						//Démarrage de la transition de sortie
		nextLevel = null;																						//Fin de teleportation
	}

	//Lancement de la teleportation
	IEnumerator Teleport(string sceneName, Vector3 position) {
		yield return new WaitForEndOfFrame();                                                               //Attendre la fin de la frame
		ChangeScene(sceneName);                                                                             //On passe au niveau suivant		
		player.transform.position = position;                                                               //Definition de la position pour apparaitre
	}

	//Execute le changement de scene
	private void ChangeScene(string sceneName) {
		DontDestroyOnLoad(this.gameObject);
		SceneManager.LoadScene(sceneName);
	}
	//Arret de l'application
	public void Exit() {
		Application.Quit();
	}
}
