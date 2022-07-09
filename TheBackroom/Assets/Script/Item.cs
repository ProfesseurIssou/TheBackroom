using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Gestion des données d'items 
*/
public enum ItemType {
		None,
		UgandaMap
}

public class Item{
	public ItemType type = ItemType.None;												//Type de l'item
	
	public string name;																	//Nom de l'item
	public string description;															//Description de l'item
	public string uiName;                                                               //Nom de l'UI associer a l'item (laisser "" pour rien)
	public int durability;																//Durabilité de l'objet
	public List<Effect> effectsOnHand = new List<Effect>();								//Liste des effets en main

	public GameObject itemPrefab;														//Prefab de l'item
	public Sprite miniature;                                                            //Miniature de l'item

	/*----INIT----*/
	public Item(ItemType itemType) {
		type = itemType;																	//Type d'item
		switch(type) {																		//Pour chaque type d'item
			case ItemType.UgandaMap:                                                            //UgandaMap
				name = "Uganda Map";
				description = "He always know da wae";
				uiName = "UgandaMap";
				durability = 0;
				effectsOnHand.Add(new Effect("UgandaMapHealth", "Now u know da wae to the heal", EffectType.Bonus, StatType.Health, 99, -1));
				effectsOnHand.Add(new Effect("UgandaMapStamina", "Now u know da wae and u can reach it", EffectType.Bonus, StatType.Stamina, 99, -1));
				effectsOnHand.Add(new Effect("UgandaMapHunger", "Now u know da wae to infinite food", EffectType.Bonus, StatType.Hunger, 99, -1));
				effectsOnHand.Add(new Effect("UgandaMapThirst", "Now u know da wae to the water source", EffectType.Bonus, StatType.Thirst, 99, -1));
				effectsOnHand.Add(new Effect("UgandaMapMental", "Now u know da wae and it fills u with determination", EffectType.Bonus, StatType.Mental, 99, -1));
				effectsOnHand.Add(new Effect("UgandaMapSpeed", "RUN MA BRUDDAH", EffectType.Bonus, StatType.Speed, 20, -1));
				miniature = Resources.Load<Sprite>("Items/Tools/UgandaMap/Miniature/UgandaMap");
				itemPrefab = Resources.Load<GameObject>("Items/Tools/UgandaMap/PrefabUgandaMap");
				break;
		}
	}
	public Item(Item oldItem) {
		type = oldItem.type;
		name = oldItem.name;
		description = oldItem.description;
		uiName = oldItem.uiName;
		durability = oldItem.durability;
		effectsOnHand = oldItem.effectsOnHand;
		miniature = oldItem.miniature;
		itemPrefab = oldItem.itemPrefab;
	}
	public void LoadItem(Item oldItem) {
		type = oldItem.type;
		name = oldItem.name;
		description = oldItem.description;
		uiName = oldItem.uiName;
		durability = oldItem.durability;
		effectsOnHand = oldItem.effectsOnHand;
		miniature = oldItem.miniature;
		itemPrefab = oldItem.itemPrefab;
	}
	/*------------*/
		

	/*----HAND----*/
	//Mettre l'item dans la main (activé les effets, activé l'UI)
	public void PutInHand() {
		if(uiName != "") GameObject.Find("Player").transform.Find("GUI/" + uiName).gameObject.SetActive(true);//Si l'item a un UI -> Activation de l'UI de l'item
		foreach(Effect effect in effectsOnHand) {                                           //Pour chaque effet de l'item
			GameObject.Find("Player").GetComponent<PlayerStats>().AddEffect(effect);			//Activation de l'effet
		}
	}
	//Retirer de la main (supprimer les effets, désactivé l'UI)
	public void RemoveFromHand() {
		if(uiName != "") GameObject.Find("Player").transform.Find("GUI/" + uiName).gameObject.SetActive(false);//Si l'item a un UI -> Désactivation de l'UI de l'item
		foreach(Effect effect in effectsOnHand) {                                           //Pour chaque effet de l'item
			GameObject.Find("Player").GetComponent<PlayerStats>().RemoveEffect(effect.name);	//Désactivation de l'effet
		}
	}
	/*------------*/

	/*----USE----*/
	//Utilisation de l'item
	public void Use(Inputs inputs) {
		switch(type) {                                                              //Pour chaque type d'objet
			case ItemType.UgandaMap:                                                    //Si UgandaMap
				if(inputs.mouseLeftClick) {                                                 //Si clique gauche
					GameObject UI = GameObject.Find("Player").transform.Find("GUI/"+ uiName + "/Buttons").gameObject;//Recherche de l'UI des boutons
					if(!UI.activeSelf) {                                                        //Si l'UI n'est pas actif
						UI.SetActive(true);                                                         //Affichage des boutons
						GameObject.Find("Player").GetComponent<PlayerGUIManager>().OpenGUI();       //Ouverture du GUI
					}
				}
				if(inputs.tab || inputs.escape) {                                           //Si Tab OU Echape
					GameObject UI = GameObject.Find("Player").transform.Find("GUI/" + uiName + "/Buttons").gameObject;//Recherche de l'UI des boutons
					if(UI.activeSelf) {                                                         //Si l'UI est actif
						UI.SetActive(false);                                                        //Cacher les boutons
						GameObject.Find("Player").GetComponent<PlayerGUIManager>().CloseGUI();      //Ouverture du GUI
					}
				}
				break;
		}
	}
	/*-----------*/

	/*----UI----*/
	//Mise en place de l'UI
	public void LoadUI() {
		if(uiName != null) GameObject.Find("Player").transform.Find("GUI/" + uiName).gameObject.SetActive(true); //Si l'item a une UI -> Activation de l'UI de l'item
	}
	//Déchargement de l'UI
	public void UnloadUI() {
		if(uiName != null) GameObject.Find("Player").transform.Find("GUI/" + uiName).gameObject.SetActive(false); //Si l'item a une UI -> Désactivation de l'UI de l'item
	}
	/*----------*/

	/*----GETTER----*/
	//Recuperation du nom (par exemple pour les batterie il y aura le nom plus le pourcentage)
	public string GetName() {
		return name;
	}
	/*--------------*/
}
