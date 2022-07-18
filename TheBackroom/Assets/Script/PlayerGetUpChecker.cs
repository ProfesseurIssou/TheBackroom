using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Vérification de si le joueur peut ce lever ou non 
*/

public class PlayerGetUpChecker : MonoBehaviour{
	public bool canGetUp;																	//Si le joueur tombe

	void Start() {
	}
	
	void Update() {
		float distAboveThePlayer = this.GetComponent<CharacterController>().bounds.extents.y;	//Distance au dessus de la tete du joueur
		canGetUp = !Physics.Raycast(transform.position, Vector3.up, distAboveThePlayer + 0.5f);	//Si rien n'est au dessus du joueur
	}
}
