using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
	Gestion de l'UI (ecran, surcouche) 
	Gestion des touche pour acceder a l'inventaire ou autre
	Gestion de si le joueur peut bouger la camera ou faire un mouvement
	Gestion de si le joueur peut prendre des objets, peut utiliser les objets, etc.
*/

public class Inputs {
	public Inputs() { }
	public bool tab;
	public bool shortcut_1;
	public bool shortcut_2;
	public bool shortcut_3;
	public bool shortcut_4;
	public bool escape;
	public bool mouseLeftClick;
}

public class PlayerGUIManager : MonoBehaviour {

	private PlayerCursorManager cursorManager;                                  //Gestion de l'affichage du curseur ou non
	private PlayerInventory inventory;											//Inventaire du joueur

	/*INPUTS*/
	Inputs inputs;                                                              //Liste des inputs
	/*######*/

	private int nbOpenGUI = 0;                                                  //Nombre de couche GUI Ouvert, 0 = aucun, >0, gui ouvert
	/*GUI*/
	private bool inventoryOpen = false;                                         //Si inventaire ouvert
	private bool objectInHand = false;                                          //Si objet dans la main
	/*###*/

	/*MOVEMENT*/
	private PlayerMovement playerMovement;                                      //Gestion du mouvement du joueur
	private PlayerView playerView;                                              //Gestion du mouvement de camera du joueur
	private PlayerInteraction playerInteraction;                                //Gestion des interactions du joueur
	private PlayerHand hand;													//Main du joueur
	/*########*/

	void Start() {
		cursorManager = this.GetComponent<PlayerCursorManager>();                       //Recuperation du gestionnaire de curseur
		inventory = this.GetComponent<PlayerInventory>();                               //Inventaire du joueur
		playerMovement = this.GetComponent<PlayerMovement>();                           //Gestion du mouvement du joueur
		playerView = this.GetComponent<PlayerView>();                                   //Gestion de la camera du joueur
		playerInteraction = this.GetComponent<PlayerInteraction>();                     //Gestion des interactions du joueur
		hand = this.GetComponent<PlayerHand>();											//Main du joueur
		inputs = new Inputs();
	}

	void Update() {
		/*OBJET EN MAIN*/
		//if(objectInHand) objectHand.GetComponent<Item3D>().item.InHand();                //Si objet en main => Execution code de l'objet si en main
		/*#############*/

		/*EVENTS*/
		inputs.tab = Input.GetKeyDown(KeyCode.Tab);                                     //Recuperation de l'inventaire
		inputs.shortcut_1 = Input.GetKeyDown(KeyCode.Alpha1);                           //Recuperation de raccourci 1
		inputs.shortcut_2 = Input.GetKeyDown(KeyCode.Alpha2);                           //Recuperation de raccourci 2
		inputs.shortcut_3 = Input.GetKeyDown(KeyCode.Alpha3);                           //Recuperation de raccourci 3
		inputs.shortcut_4 = Input.GetKeyDown(KeyCode.Alpha4);                           //Recuperation de raccourci 4
		inputs.escape = Input.GetKeyDown(KeyCode.Escape);                               //Recuperation de la mise en pause OU fermeture d'inventaire
		inputs.mouseLeftClick = Input.GetMouseButtonDown(0);                            //Recuperation du click sourie
		/*######*/

		/*LOGIC*/
		//Ouverture / fermeture de l'inventaire, mettre l'objet de la main dans l'inventaire
		if(inputs.tab) {                                                                //Si on appuie sur tab
			if(objectInHand) {                                                              //Si il y a un objet dans la main
				if(nbOpenGUI == 0) {                                                            //Si l'interface de l'objet n'est pas ouvert
					objectInHand = false;															//Aucun objet dans la main
					hand.RemoveFromHand();                                                          //On range l'objet de la main
					UpdatePlayerAction(true, true, true);                                           //Modification des actions disponible du joueur (camera, interaction, mouvement)
				}
			} else {																		//Sinon pas d'objet dans la main
				if(!inventoryOpen) {                                                            //Si inventaire pas ouvert
					inventory.OpenInventory();														//On ouvre l'inventaire
					UpdatePlayerAction(false, false, true);											//Modification des actions disponible du joueur (camera, interaction, mouvement)
				} else {                                                                        //Sinon inventaire ouvert
					inventory.CloseInventory();                                                     //On ferme l'inventaire
					UpdatePlayerAction(true, true, true);                                           //Modification des actions disponible du joueur (camera, interaction, mouvement)
				}				
			}
		}

		//Utilisation de l'objet
		if(objectInHand) {																//Si il y a un objet dans la main
			hand.UseItem(inputs);															//Utilisation de l'item de la main selon les inputs (peut ne pas avoir d'input mais permet aussi d'executé quand objet en main)
		}

		//Mise a jour des actions possible du joueur
		if(objectInHand && nbOpenGUI == 0) {													//Si il y a un objet dans la main ET qu'il n'y a pas d'interface ouverte
			UpdatePlayerAction(true, false, true);													//Le joueur peut voir, Ne peut pas prendre d'objet, Peut bouger
			cursorManager.SetVisibleCursor(false);													//On cache le curseur
		}

		//if(objectInHand) {                                                              //Si objet dans la main
		//	if(inputs.tab && !inputs.tabCache && nbOpenGUI == 0) {                          //Si tab pressé ET qu'il n'y a pas de GUI
		//		RemoveFromHand();                                                               //On retire l'objet en main
		//	} else {                                                                        //Sinon
		//		UseItem(inputs);                                                                //Utilisation de l'item
		//	}
		//} else if(inventoryOpen) {                                                      //Sinon Si inventaire ouvert
		//	if(inputs.tab && !inputs.tabCache) {                                            //Si tab pressé
		//		CloseInventory();                                                               //Fermeture de l'inventaire
		//	}
		//} else {                                                                        //Sinon
		//	if(inputs.tab && !inputs.tabCache) {                                            //Si tab pressé
		//		OpenInventory();                                                                //Ouverture de l'inventaire
		//	}
		//	if(inputs.mouseLeftClick) {                                                     //Si clique gauche
		//		CheckInteract();                                                                //Check des interactions avec l'environement
		//	}
		//}

		//if(objectInHand || nbOpenGUI > 0) {                                               //Si il y a quelque chose en main OU qu'il y a un GUI ouvert
		//	InteractWithEnvironment.handActive = false;                                     //On désactive la main
		//} else {                                                                        //Sinon
		//	InteractWithEnvironment.handActive = true;                                      //On active la main
		//}


		/*#####*/

	}

	//Mise a jour des actions du joueur
	public void UpdatePlayerAction(bool view, bool takeObject, bool movement) {
		playerMovement.canMove = movement;
		playerInteraction.canTakeItem = takeObject;
		playerView.canMove = view;
	}

	//Definition de si il y a un objet en main
	public void SetObjectInHand(bool value) {
		objectInHand = value;
	}	
	//Definition de si l'inventaire est ouvert
	public void SetInventoryOpen(bool value) {
		inventoryOpen = value;
	}


	//Signalisation d'ouverture d'un GUI
	public void OpenGUI() {
		nbOpenGUI++;
		cursorManager.SetVisibleCursor(true);
	}

	//Signalisation de fermeture d'un GUI
	public void CloseGUI() {
		nbOpenGUI--;

		if(nbOpenGUI == 0) {                                                            //Si plus aucun GUI ouvert
			cursorManager.SetVisibleCursor(false);											//On cache le curseur
		} else {
			cursorManager.SetVisibleCursor(true);											//On affiche le curseur
		}
	}
}
