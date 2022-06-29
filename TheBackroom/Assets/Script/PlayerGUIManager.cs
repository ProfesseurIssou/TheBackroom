using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
	Gestion de l'UI (ecran, surcouche) 
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

	public bool tabCache;
	public bool shortcut_1Cache;
	public bool shortcut_2Cache;
	public bool shortcut_3Cache;
	public bool shortcut_4Cache;
	public bool escapeCache;
	public bool mouseLeftClickCache;
}

public class PlayerGUIManager : MonoBehaviour {

	private PlayerCursorManager cursorManager;                                  //Gestion de l'affichage du curseur ou non

	/*INPUTS*/
	Inputs inputs;                                                              //Liste des inputs
	/*######*/

	private int nbOpenGUI = 0;                                                  //Nombre de couche GUI Ouvert, 0 = aucun, >0, gui ouvert
	/*GUI*/
	private bool inventoryOpen = false;                                         //Si inventaire ouvert
	private bool inventoryOpenCache = false;
	private bool objectInHand = false;                                          //Si objet dans la main
	/*###*/

	/*INTERACTION WITH ENVIRONMENT*/
	/*############################*/

	/*HAND*/
	//public GameObject Hand;                                                         //Main du joueur
	//private GameObject objectHand = null;                                           //Objet dans la main
	/*####*/

	void Start() {
		cursorManager.SetVisibleCursor(false);                                          //On bloque le curseur
		inputs = new Inputs();
	}

	void Update() {
		/*OBJET EN MAIN*/
		//if(objectInHand) objectHand.GetComponent<Item3D>().item.InHand();                //Si objet en main => Execution code de l'objet si en main
		/*#############*/

		/*EVENTS*/
		inputs.tab = Input.GetKey(KeyCode.Tab);                                         //Recuperation de l'inventaire
		inputs.shortcut_1 = Input.GetKey(KeyCode.Alpha1);                               //Recuperation de raccourci 1
		inputs.shortcut_2 = Input.GetKey(KeyCode.Alpha2);                               //Recuperation de raccourci 2
		inputs.shortcut_3 = Input.GetKey(KeyCode.Alpha3);                               //Recuperation de raccourci 3
		inputs.shortcut_4 = Input.GetKey(KeyCode.Alpha4);                               //Recuperation de raccourci 4
		inputs.escape = Input.GetKey(KeyCode.Escape);                                   //Recuperation de la mise en pause OU fermeture d'inventaire
		inputs.mouseLeftClick = Input.GetMouseButtonDown(0);                            //Recuperation du click sourie
		/*######*/

		/*LOGIC*/
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

		//if(objectInHand || nbOpenGUI>0) {                                               //Si il y a quelque chose en main OU qu'il y a un GUI ouvert
		//	InteractWithEnvironment.handActive = false;                                     //On désactive la main
		//} else {                                                                        //Sinon
		//	InteractWithEnvironment.handActive = true;                                      //On active la main
		//}


		/*#####*/

		/*RENDER*/
		//inventory.UpdateInventory();                                                    //Mise a jour de l'ui de l'inventaire
		/*######*/

		/*CACHE*/
		inventoryOpenCache = inventoryOpen;                                             //On enregistre l'etat de l'inventaire
		inputs.mouseLeftClickCache = inputs.mouseLeftClick;                             //On enregistre l'etat du clique gauche
		inputs.tabCache = inputs.tab;                                                   //On enregistre l'etat du tab
		/*#####*/
	}


	/*INVENTORY*/
	//public void OpenInventory() {
	//	inventoryOpen = true;                                                           //Ouverture de l'inventaire
	//	inventory.EnableGUI(true);                                                      //Ouverture du GUI
	//	OpenGUI();                                                                      //Ouverture du GUI
	//}
	//public void CloseInventory() {
	//	inventoryOpen = false;                                                          //Fermeture d'inventaire
	//	inventory.EnableGUI(false);                                                     //Fermeture du GUI
	//	CloseGUI();                                                                     //Fermeture du GUI
	//}
	/*#########*/

	/*INTERACTION*/
	//Check de l'interaction avec l'environment
	//private void CheckInteract() {
	//	Item3D newItem = InteractWithEnvironment.GetItem();                             //Recuperation de l'item
	//	if(newItem == null) return;                                                     //Si il n'y a pas d'item => Fin
	//	bool result = inventory.AddToInventory(newItem.item);                           //On enregistre l'item dans l'inventaire
	//	if(result) newItem.Delete();                                                    //Si mise en inventaire reussi => Destruction de l'item sur la map
	//}
	/*###########*/

	/*HAND*/
	//Utilisation de l'Objet en mains
	//public void UseItem(Inputs inputs) {
	//	objectHand.GetComponent<Item3D>().item.Use(inputs);                             //Utilisation de l'objet
	//}
	//Mettre un objet dans la main
	//public void PutInHand(Item item) {
	//	objectInHand = true;                                                            //Objet en main
	//	Item newItem = new Item(item);                                                  //Copie de l'item
	//	objectHand = Instantiate(newItem.itemPrefab,Hand.transform).gameObject;         //Creation de l'objet
	//	objectHand.GetComponent<Item3D>().LoadItem(newItem);                            //Chargement des données de l'item dans l'objet 3D
	//	Destroy(objectHand.GetComponent<Rigidbody>());                                  //On retire le physique
	//	objectHand.GetComponent<Item3D>().item.LoadUI();                                //Chargement de l'UI de l'item
	//}
	//Retirer l'objet de la main
	//public void RemoveFromHand() {
	//	objectInHand = false;                                                           //Pas d'objet en main
	//	objectHand.GetComponent<Item3D>().item.UnloadUI();                              //Dechargement de l'UI de l'item
	//	inventory.AddToInventory(objectHand.GetComponent<Item3D>().item);               //On ajoute l'item de la main dans l'inventaire
	//	Destroy(objectHand);                                                            //Destruction de l'objet en main
	//}
	/*####*/


	//Signalisation d'ouverture d'un GUI
	public void OpenGUI() {
		nbOpenGUI++;
		cursorManager.SetVisibleCursor(true);
	}

	//Signalisation de fermeture d'un GUI
	public void CloseGUI() {
		nbOpenGUI--;

		if(nbOpenGUI == 0) {                                                        //Si plus aucun GUI ouvert
			cursorManager.SetVisibleCursor(false);
		} else {
			cursorManager.SetVisibleCursor(true);
		}
	}
}
