using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
	A mettre sur les slot de l'inventaire, quand cliqué, envoie un appel a l'inventaire afin de traiter le click 
*/

public class PlayerInventoryClickItem : MonoBehaviour, IPointerClickHandler {

	public PlayerInventory playerInventory;                                                                         //Inventaire du joueur
	public int ItemPos;                                                                                             //Position de l'item dans l'inventaire

	//Quand objet cliqué
	public void OnPointerClick(PointerEventData eventData) {
		if(eventData.button == PointerEventData.InputButton.Left) playerInventory.ClickOnInventoryItem(ClickType.Left, ItemPos);
		else if(eventData.button == PointerEventData.InputButton.Middle) playerInventory.ClickOnInventoryItem(ClickType.Middle, ItemPos);
		else if(eventData.button == PointerEventData.InputButton.Right) playerInventory.ClickOnInventoryItem(ClickType.Right, ItemPos);
	}
}
