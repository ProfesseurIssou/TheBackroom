using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	 Gestion de l'affichage ou non du curseur
*/

public class PlayerCursorManager : MonoBehaviour{

	public bool defaultDisplayCursor = false;															//Etat par defaut de l'affichage du curseur
	public bool defaultLockCursor = false;																//Etat par defaut du bloquage du curseur


	void Start() {
		Cursor.visible = defaultDisplayCursor;																//Récupération de si le curseur est visible
		if(defaultLockCursor) {																				//Si le curseur doit etre bloquer
			Cursor.lockState = CursorLockMode.Locked;															//On bloque le curseur
		} else {																							//Sinon
			Cursor.lockState = CursorLockMode.None;																//Pas de bloquage
		}
	}

	//Definition de la visibilité du curseur
	public void SetVisibleCursor(bool isVisible) {
		if(isVisible) {																						//Si il doit etre visible
			Cursor.visible = true;																				//On affiche
			Cursor.lockState = CursorLockMode.None;																//On ne bloque pas
		} else {																							//Sinon
			Cursor.visible = false;																				//On cache
			Cursor.lockState = CursorLockMode.Locked;															//On bloque au centre
		}
	}


}
