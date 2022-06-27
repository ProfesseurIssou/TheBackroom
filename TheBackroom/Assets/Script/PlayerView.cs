using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Control de la vision (mouvement verticale) 
*/

public class PlayerView : MonoBehaviour{
	public float mouseSensitivity = 100f;                                                                       //Sensibilité de la vision
	public Transform playerBody;                                                                                //Corp du joueur
	public Camera playerCamera;																					//Camera du joueur
	public float verticalMinX = -90f;																			//Vertical min
	public float verticalMaxX = 90f;                                                                            //Vertical max

	private float rotationX = 0f;																				//Rotation actuel

	void Start(){
		
	}

	void Update(){
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;								//Deplacement sur l'axe horizontal
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;                                //Deplacement sur l'axe vertical

		rotationX -= mouseY;																						//Application de la rotation vertical
		rotationX = Mathf.Clamp(rotationX, verticalMinX, verticalMaxX);                                             //Limitation de la rotation vertical

		playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);									//Application de la rotation sur la camera
		playerBody.Rotate(Vector3.up * mouseX);																		//Deplacement horizontal sur le joueur
	}
}
