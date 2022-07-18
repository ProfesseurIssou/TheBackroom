using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Gestion des etats et des status du joueur (si il est en saut, ...) 
*/

public class PlayerMovementStatus: MonoBehaviour{
	private PlayerFallingChecker playerFallingChecker;                                              //Checker de si le joueur tombe
	private PlayerGetUpChecker playerGetUpChecker;													//Checker de si le joueur peut ce relevé
	private PlayerStats playerStats;                                                                //Stats du joueur
	private Transform playerBody;																	//Corp du joueur

	public bool isFalling = false;                                                                  //Si le joueur tombe
	public bool isRunning = false;                                                                  //Si le joueur cour
	public bool isCrouch = false;                                                                   //Si le joueur est accroupis

	void Start() {
		playerFallingChecker = GetComponent<PlayerFallingChecker>();
		playerGetUpChecker = GetComponent<PlayerGetUpChecker>();
		playerStats = GetComponent<PlayerStats>();
		playerBody = this.transform;
	}

	void Update(){
		bool sprintKey = Input.GetKey(KeyCode.LeftShift);												//Si le joueur veut courir
		bool crouchKey = Input.GetKey(KeyCode.LeftControl);                                             //Si le joueur veut s'accroupir

		/*FALLING*/
		SetFalling(playerFallingChecker.isFalling);														//Récupération de si le joueur tombe
		/*#######*/

		/*RUNNING*/
		SetRunning(sprintKey);                                                                          //Si le joueur veux courir
		/*#######*/

		/*CROUCH*/
		if(isCrouch && !crouchKey && playerGetUpChecker.canGetUp)                                       //Si le joueur est accroupi ET qu'il veut se relever ET qu'il peut se relever
			SetCrouch(false);                                                                               //Le joueur ce releve
		if(!isCrouch && crouchKey)                                                                      //Si le joueur n'est pas accroupi ET qu'il veut s'accroupir
			SetCrouch(true);                                                                                //Le joueur s'accroupit
		if(isCrouch) SetRunning(false);																	//Si le joueur est accroupis => Ne coure pas
		/*######*/



		/*EFFECT*/
		if(isRunning) playerStats.currentStamina -= playerStats.runningStaminaConsumption * Time.deltaTime;//Si le joueur cour => On retire la stamina
		/*######*/

	}


	public void SetFalling(bool isFalling) { this.isFalling = isFalling; }                          //Definition de si le joueur tombe
	public void SetRunning(bool isRunning) { this.isRunning = isRunning; }                          //Definition de si le joueur cour
	public void SetCrouch(bool isCrouch) { this.isCrouch = isCrouch; }								//Definition de si le joueur s'accourpis
}
