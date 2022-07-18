using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Mouvement du joueur 
*/

public class PlayerMovement : MonoBehaviour{
	public bool canMove = true;                                                                 //Si le joueur peut bouger

	private Transform playerBody;                                                               //Corp du joueur
	private CharacterController playerConstroller;                                              //Controlleur du joueur
	private PlayerMovementStatus playerMovementStatus;                                          //Status du joueur
	private PlayerStats playerStats;                                                            //Stats du joueur (pour la vitesse actuel et la stamina du jump)

	private float crouchCoef = 0.5f;															//Coef de vitesse de l'accroupissement
	private float sprintCoef = 2f;                                                              //Coef de vitesse du sprint
	private float jumpForce = 5f;                                                               //Force du saut
	private float gravity = -9f;                                                                //Gravité

	private float normalHeight = 2;                                                             //Hauteur du joueur normal
	private float crouchHeight = 1;                                                             //Hauteur du joueur accroupis

	private float jumpStaminaConsumption = 5f;													//Consomation de stamina par le jump

	private Vector3 velocity;																	//Vélocité du joueur

	void Start(){
		playerBody = this.transform;
		playerConstroller = GetComponent<CharacterController>();
		playerMovementStatus = GetComponent<PlayerMovementStatus>();
		playerStats = GetComponent<PlayerStats>();
	}

	void Update(){
		if(canMove) {																				//Si le joueur peut bouger
			float x = Input.GetAxis("Horizontal");														//Mouvement axe X
			float z = Input.GetAxis("Vertical");                                                        //Mouvement axe Y
			bool jump = Input.GetButton("Jump");                                                        //Si on saute

			if(playerMovementStatus.isCrouch) {                                                         //Si le joueur s'accroupis
				playerConstroller.height = crouchHeight;
			} else {																					//Sinon
				playerConstroller.height = normalHeight;
				if(jump && !playerMovementStatus.isFalling && playerStats.currentStamina > jumpStaminaConsumption) {//Si on veux sauté ET que le joueur ne tombe pas ET que le joueur a assez de stamina
					velocity.y = jumpForce;                                                                     //On lui donne une force de saut
					playerStats.currentStamina -= jumpStaminaConsumption;                                       //On enlève la stamina
				}
			}


			velocity = playerBody.transform.right * x + playerBody.transform.forward * z + playerBody.transform.up * (velocity.y + gravity * Time.deltaTime);//Calcul du mouvement

			if(playerMovementStatus.isCrouch) playerConstroller.Move(new Vector3(velocity.x * playerStats.currentSpeed * crouchCoef, velocity.y, velocity.z * playerStats.currentSpeed * crouchCoef) * Time.deltaTime);//Si accroupis => Application du mouvement avec accroupissement (pour que le saut ne soit pas affecté par le gain en vitesse)
			else if(playerMovementStatus.isRunning)	playerConstroller.Move(new Vector3(velocity.x * playerStats.currentSpeed * sprintCoef, velocity.y, velocity.z * playerStats.currentSpeed * sprintCoef) * Time.deltaTime);//Si sprint => Application du mouvement avec sprint
			else playerConstroller.Move(new Vector3(velocity.x * playerStats.currentSpeed, velocity.y, velocity.z * playerStats.currentSpeed) * Time.deltaTime);//Sinon => Application du mouvement
		}

	}
}
