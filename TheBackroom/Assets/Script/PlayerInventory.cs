using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	Gestion de l'inventaire 
*/

/*TYPE DE SAC*/
public enum BagType {
	None,
	Tier_1,
	Tier_2
}
/*###########*/

/*INPUT TYPE*/
public enum ClickType {
	Left,
	Right,
	Middle
}
/*##########*/

public class PlayerInventory : MonoBehaviour{

	/*GUI*/
	public GameObject InventoryGUI;                                                                         //GUI de l'inventaire
	public List<Image> ItemsGUI;																			//Liste des GUI d'items dans l'inventaire
	/*###*/

	/*SPRITE*/
	public Sprite Inventory_1_Sprite;																		//Image de l'inventaire par defaut
	public Sprite Inventory_2_Sprite;																		//Image de l'inventaire avec backpack tier 1
	public Sprite Inventory_3_Sprite;                                                                       //Image de l'inventaire avec backpack tier 2
	/*######*/
	
	private BagType backpackType = BagType.None;															//Par defaut pas de sac
	private List<Item> inventory = new List<Item>() { null, null, null, null, null, null, null, null, null, null, null, null };//Inventaire


	//Affichage de l'inventaire
	public void DisplayInventory(bool displayed) {
		InventoryGUI.SetActive(displayed);																	//Definition de l'affichage de l'inventaire
		for(int i=0; i<12; i++) {																			//Pour chaque slot de l'inventaire
			if(inventory[i] != null) {                                                                          //Si il y a un item
				ItemsGUI[i].gameObject.SetActive(true);																//Activation du slot 
				ItemsGUI[i].sprite = inventory[i].miniature;														//Definition de la miniature
			} else {																							//Sinon
				ItemsGUI[i].gameObject.SetActive(false);                                                            //Desactivation du slot 
				ItemsGUI[i].sprite = null;                                                                          //Suppression de la miniature
			}
		}
	}


	//Modification du sac
	public void ChangeBackpack(BagType newBag) {
		backpackType = newBag;                                                                                  //Definition du nouveau sac
		if(backpackType == BagType.None) {                                                                      //Si le sac est vide
			InventoryGUI.GetComponent<Image>().sprite = Inventory_1_Sprite;                                         //On change l'image de l'inventaire
		}
		if(backpackType == BagType.Tier_1) {                                                                    //Si le sac est tier 1
			InventoryGUI.GetComponent<Image>().sprite = Inventory_2_Sprite;                                         //On change l'image de l'inventaire
		}
		if(backpackType == BagType.Tier_2) {                                                                    //Si le sac est tier 2
			InventoryGUI.GetComponent<Image>().sprite = Inventory_3_Sprite;                                         //On change l'image de l'inventaire
		}
		CheckNbItem();																							//Vérification du nombre d'item dans l'inventaire par rapport a la capacité

	}
	//Vérification du nombre d'item dans l'inventaire (si il y en a trop, le surplus sera jeté)
	private void CheckNbItem() {
		switch(backpackType) {																					//Pour chaque type de sac
			case BagType.None:																						//Si il n'y a aucun sac
				if(inventory.Count > 4) {																				//Si il y a trop d'objet dans l'inventaire
					for(int i = 5; i < inventory.Count; i++) {																//Pour chaque objet en surplus
						DropFromInventory(i);																					//Drop de l'objet
					}
				}
				break;
			case BagType.Tier_1:                                                                                    //Si il y a un sac tier 1
				if(inventory.Count > 8) {                                                                               //Si il y a trop d'objet dans l'inventaire
					for(int i = 9; i < inventory.Count; i++) {																//Pour chaque objet en surplus
						DropFromInventory(i);                                                                                   //Drop de l'objet
					}
				}
				break;
			case BagType.Tier_2:                                                                                    //Si il y a un sac tier 2
				break;
		}
	}

	
	//Ajouter dans l'inventaire
	public void AddToInventory(GameObject newItem) {
		int nbCase = 0;                                                                 //Nombre de case dans l'inventaire

		if(backpackType == BagType.None) nbCase = 4;                                    //nbCase disponible
		if(backpackType == BagType.Tier_1) nbCase = 8;                                  //nbCase disponible
		if(backpackType == BagType.Tier_2) nbCase = 12;                                 //nbCase disponible

		for(int i = 0; i < nbCase; i++) {                                                   //Pour chaque position de l'inventaire
			if(inventory[i] == null) {                                                      //Si la case actuel est libre
				inventory[i] = new Item(newItem.GetComponent<Item3D>().item);					//Ajout d'un item et chargement des données de l'item
				Destroy(newItem);																//Destruction de l'objet
				break;
			}
		}
		return;
	}
	//Lacher l'objet
	public void DropFromInventory(int itemPos) {
		GameObject newObjet = Instantiate(inventory[itemPos].itemPrefab, transform.position, Quaternion.identity).gameObject;//Instantiation de l'objet
		newObjet.GetComponent<Item3D>().LoadItem(inventory[itemPos]);												//Chargement des données de l'item
		inventory[itemPos] = null;																					//Suppression de l'inventaire
	}


	//Clique sur un objet dans l'inventaire
	public void ClickOnInventoryItem(ClickType clickType, int itemPos) {
		if(inventory[itemPos] != null) {																//Si l'item existe
			switch(clickType) {																				//Pour chaque type de clique
				case ClickType.Left:																			//Si clique gauche
					//guiManager.PutInHand(inventory[itemPos]);														//Mettre l'objet dans la main
					//inventory[itemPos] = null;																		//On retire l'objet de l'inventaire
					//guiManager.CloseInventory();																	//Fermeture de l'inventaire
					break;
				case ClickType.Right:																			//Si clique droit
					DropFromInventory(itemPos);																		//On jete l'item
					break;
				case ClickType.Middle:																			//Si clique central
					//Selection d'un objet pour le craft
					break;
			}
			DisplayInventory(true);																			//Pour mettre a jour l'inventaire
		}
	}
}
