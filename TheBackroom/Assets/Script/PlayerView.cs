using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Control de la vision (mouvement verticale) 
*/

public class PlayerView : MonoBehaviour{
	public Camera playerCamera;                                                                                 //Camera du joueur

	public bool canMove = true;																					//Si la camera peut bouger
	
	private Transform playerBody;                                                                               //Corp du joueur
	private float mouseSensitivity = 100f;                                                                      //Sensibilité de la vision
	private float verticalMinX = -90f;                                                                          //Vertical min
	private float verticalMaxX = 70f;                                                                           //Vertical max

	private float rotationX = 0f;																				//Rotation actuel

	void Start(){
		playerBody = this.transform;																				//Corp du joueur
	}

	void Update(){
		if(canMove) {
			float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;								//Deplacement sur l'axe horizontal
			float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;                                //Deplacement sur l'axe vertical

			rotationX -= mouseY;																						//Application de la rotation vertical
			rotationX = Mathf.Clamp(rotationX, verticalMinX, verticalMaxX);                                             //Limitation de la rotation vertical

			playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);									//Application de la rotation sur la camera
			playerBody.Rotate(Vector3.up * mouseX);																		//Deplacement horizontal sur le joueur			
		}
	}
}
