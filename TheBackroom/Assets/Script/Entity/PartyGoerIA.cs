using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	IA qui va gérer le partygoer 
*/

//Gestion de l'IA en mode MENU
public class PartyGoerMenuAI {
	public float posXMax = 20;													//Position x max du partygoer
	public float posXMin = -20;													//Position x min du partygoer
	public float speed = 2;														//Vitesse de deplacement
	public bool direction = false;												//Direction du deplacement (true = +x    false = -x)
}

public class PartyGoerIA : MonoBehaviour{
	private Animator animatorController;                                        //Controlleur d'animation
	
	public enum AIMode {														//Mode de l'IA
		menu,																		//Dans le menu
		hunt,																		//Recherche de joueur
	}
	public AIMode mode;															//Mode de l'IA  
	
	public enum AnimationType {
		standing,
		walking
	}

	/*MODE*/
	private PartyGoerMenuAI partyGoerMenuAI;									//Gestion des variables du mode menu
	/*####*/

	void Start(){
		animatorController = GetComponent<Animator>();
		partyGoerMenuAI = new PartyGoerMenuAI();
	}

	void Update(){
		switch(mode) {																//Pour chaque mode de l'IA
			case AIMode.menu:                                                           //Si dans menu
				SetAnimation(AnimationType.walking);                                        //Lancement de la marche
				if(partyGoerMenuAI.direction) {												//Si vers +x
					transform.rotation = Quaternion.Euler(0, 90, 0);							//On tourne le partygoer
					transform.position = new Vector3(transform.position.x + partyGoerMenuAI.speed * Time.deltaTime, transform.position.y, transform.position.z);//On le deplace
					if(transform.position.x > partyGoerMenuAI.posXMax) partyGoerMenuAI.direction = false;//Si il arrive a la fin, il passe de l'autre sens
				} else {
					transform.rotation = Quaternion.Euler(0, 270, 0);							//On tourne le partygoer
					transform.position = new Vector3(transform.position.x - partyGoerMenuAI.speed * Time.deltaTime, transform.position.y, transform.position.z);//On le deplace
					if(transform.position.x < partyGoerMenuAI.posXMin) partyGoerMenuAI.direction = true;//Si il arrive a la fin, il passe de l'autre sens
				}
				break;
			case AIMode.hunt:															//Si chasse le joueur
				break;
		}
	}

	//Changement de l'animation
	private void SetAnimation(AnimationType animationType) {
		switch(animationType) {														//Pour chaque type d'animation
			case AnimationType.standing:												//Si il doit rester sur place
				break;
			case AnimationType.walking:                                                 //Si il doit marché
				animatorController.SetBool("walk", true);                                   //L'entité marche
				break;
		}
	}
}
