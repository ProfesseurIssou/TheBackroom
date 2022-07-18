using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Gestion de la main du joueur pour tenir des objets 
*/

public class PlayerHand : MonoBehaviour{
	private GameObject hand;                                                                            //Main du joueur
	private PlayerStats playerStats;																	//Stats du joueur
	private PlayerGUIManager GUIManager;																//Gestionnaire d'UI
	private PlayerInventory inventory;																	//Inventaire du joueur
	private int itemIndex;																				//Index de l'item depuis l'inventaire (-1 si aucun item selectionné)
	
	void Start(){
		hand = transform.Find("Hand").gameObject;                                                           //Recherche de la main du joueur
		playerStats = transform.GetComponent<PlayerStats>();												//Stats du joueur
		GUIManager = transform.GetComponent<PlayerGUIManager>();                                            //Recuperation du gestionnaire d'UI
		inventory = transform.GetComponent<PlayerInventory>();                                              //Recuperation de l'inventaire du joueur
		itemIndex = -1;																						//Aucun item selectionné
	}

	//Mettre l'objet dans la main du joueur
	public void PutInHand(int itemInventoryIndex) {
		if(itemIndex != -1) RemoveFromHand();                                                               //Si il y a déjà un objet dans la main -> On retire l'objet de la main
		
		GUIManager.SetObjectInHand(true);																	//On a un objet en main
		itemIndex = itemInventoryIndex;                                                                     //Definition de l'item dans la main
		GameObject newObjet = Instantiate(inventory.GetItem(itemIndex).itemPrefab, hand.transform).gameObject;//Instantiation de l'objet
		newObjet.GetComponent<Item3D>().LoadItem(inventory.GetItem(itemIndex));								//Chargement des données de l'item
		Destroy(newObjet.GetComponent<Rigidbody>());                                                        //On retire les collision physique
		newObjet.GetComponent<Item3D>().item.LoadUI();                                                      //Chargement de l'UI de l'item

		for(int i = 0; i < inventory.GetItem(itemIndex).effectsOnHand.Count; i++) {							//Pour chaque effet de l'objet
			playerStats.AddEffect(inventory.GetItem(itemIndex).effectsOnHand[i]);								//Ajout de l'effet de l'item dans les stats du joueur
		}
	}
	//Ranger l'objet dans la main dans l'inventaire
	public void RemoveFromHand() {
		GUIManager.SetObjectInHand(false);                                                                  //On n'a plus d'objet en main
		inventory.GetItem(itemIndex).UnloadUI();                                                            //Dechargement de l'UI de l'item
		Destroy(hand.transform.GetChild(0).gameObject);														//Destruction de l'objet en main
		for(int i = 0; i < inventory.GetItem(itemIndex).effectsOnHand.Count; i++) {                         //Pour chaque effet de l'objet
			playerStats.RemoveEffect(inventory.GetItem(itemIndex).effectsOnHand[i].name);						//Retrait de l'effet de l'item dans les stats du joueur
		}
		itemIndex = -1;                                                                                     //Plus d'objet dans la main
	}
}
