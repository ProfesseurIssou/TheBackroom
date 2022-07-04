using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Gestion des items en 3D afin que l'on puisse faire 'new' dans l'inventaire 
*/

public class Item3D : MonoBehaviour{
	public ItemType type;											//Type de l'item
	public Item item;                                               //Données de l'item


	//Au demarrage
	void Start() {
		item = new Item(type);                                          //Creation des données de l'item
	}

	/*CONSTRUCTEUR*/
	//Chargement des données de l'ancien objet
	public void LoadItem(Item oldData) {
		item = new Item(oldData);                                       //Chargement de l'ancienne données
	}
	/*############*/

	/*OTHER*/
	//Suppression de l'item
	public void Delete() {
		Destroy(this.gameObject);                                   //Destrution de l'item sur la map
	}
	/*#####*/
}
