using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Verification de si le joueur tombe ou non 
*/

public class PlayerFallingChecker : MonoBehaviour{
	public bool isFalling;                                                      //Si le joueur tombe

	void Start() {
	}

	void Update(){
		float distToGround = this.GetComponent<CharacterController>().bounds.extents.y;
		isFalling = !Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}
}
