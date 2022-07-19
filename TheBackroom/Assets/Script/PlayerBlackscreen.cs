using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	Gestion du fondu au noir 
*/


public enum TransitionType {
	ToBlack,
	ToClear
}

public class PlayerBlackscreen : MonoBehaviour{
	private float defaultTotalTransitionSeconde = 3;

	public Image blackImage;
	private float nbTotalSeconde;                                                       //Nombre de seconde pour le fondu (pour arrivé au noir)
	private float nbCurrentSeconde;                                                     //Nombre actuel de seconde
	private bool inTransition;                                                          //Si en transition
	private TransitionType transitionType;                                              //Type de transition


	void Start() {
		//Au démarrage, le joueur ce reveil
		nbTotalSeconde = defaultTotalTransitionSeconde;
		nbCurrentSeconde = 0;
		inTransition = true;
		transitionType = TransitionType.ToClear;
	}

	void Update() {
		if(inTransition) {
			blackImage.gameObject.SetActive(true);											//On active le calque de transition
			switch(transitionType) {
				case TransitionType.ToBlack:
					SetAlpha(nbCurrentSeconde / nbTotalSeconde);
					break;
				case TransitionType.ToClear:
					SetAlpha(1 - (nbCurrentSeconde / nbTotalSeconde));
					break;
			}
			if(nbCurrentSeconde >= nbTotalSeconde) inTransition = false;
			nbCurrentSeconde += Time.deltaTime;

			if(!inTransition && transitionType == TransitionType.ToClear) blackImage.gameObject.SetActive(false);//Si la transition ce termine => On désactive le calque (pour que ça ne bloque pas le curseur)
		}
	}

	//Démarrage de la transition
	public void StartTransition(float nbSeconde, TransitionType prmTransitionType) {
		inTransition = true;
		nbTotalSeconde = nbSeconde;
		nbCurrentSeconde = 0;
		transitionType = prmTransitionType;
	}
	//Suppression de l'ecran noir
	public void ResetScreen() {
		inTransition = false;
		nbTotalSeconde = 0;
		nbCurrentSeconde = 0;
		SetAlpha(0);
	}
	//Si on est en transition
	public bool InTransition() {
		return inTransition;
	}

	//Modification de la transparence de l'image
	private void SetAlpha(float alpha) {
		Color color = blackImage.color;
		color.a = alpha;
		blackImage.color = color;
	}
}
