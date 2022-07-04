using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	Gestion de l'interaction avec l'environement (item a ramaser et bouton)  
*/

public enum InteractType {
	None,
	Item,
	Button
}

public class PlayerInteraction : MonoBehaviour{
	public InteractType interactionType;                                                                //Si une interaction peu ce faire et quel type d'interaction
	public bool canTakeItem = true;																		//On peut prendre des objets
	
	private PlayerInventory inventory;																	//Invenatire du joueur

	public GameObject GUIInteract;                                                                      //GUI de l'interaction
	public Text GUIInteractName;																		//GUI du nom de l'interaction

	public Camera camera;																				//Camera principale
	private float interactRange = 2f;																	//Distance d'interaction max
	private uint numberFramePerUpdate = 10;                                                             //Nombre de frame entre chaque check
	private int frameCount = 0;                                                                         //Nb frame avant de check (pour les fps)

	private GameObject interactObject;																	//Objet visé par l'interaction


	void Start(){
		interactionType = InteractType.None;                                                                //Aucune interaction
		inventory = transform.GetComponent<PlayerInventory>();													//Inventaire du joueur
	}

	void Update(){
		bool isInteracting = Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0);                    //Si on appuie sur E ou Click gauche pour interagir

		if(Time.frameCount >= frameCount + numberFramePerUpdate) {                                          //Si nombre de frame dépasse
			frameCount = Time.frameCount;																		//On recupere le nombre de frame actuel
			CheckInteract();                                                                                    //Check des interactions
		}

		if(isInteracting && interactionType != InteractType.None) {                                         //Si interaction ET qu'une interaction est possible
			switch(interactionType) {																			//Pour chaque type d'interaction
				case InteractType.Item:                                                                             //Si c'est un item
					inventory.AddToInventory(interactObject);															//Tentative d'ajout dans l'inventaire (si l'inventaire n'est pas plein)
					break;
				case InteractType.Button:																			//Si c'est un actionneur
					
					break;
			}
		}
	}

	/*INTERACT*/
	//Vérifier la présence d'interaction
	private void CheckInteract() {
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);                                             //Rayons du Hit
		RaycastHit hit;                                                                                     //Hit

		if(Physics.Raycast(ray, out hit, interactRange)) {													//Si il y a un Hit
			Debug.DrawLine(transform.position, hit.point, Color.magenta, Time.deltaTime);                       //DEBUG

			if(hit.transform.tag == "Item" && canTakeItem) {													//Si le hit est un item ET qu'on peut prendre un item
				interactionType = InteractType.Item;																//On met le type d'interaction à item
				interactObject = hit.transform.gameObject;															//On met l'objet visé par l'interaction
				GUIInteract.SetActive(true);																		//On active le GUI de l'interaction
				GUIInteractName.text = interactObject.GetComponent<Item3D>().item.GetName();						//On met le nom de l'item dans le GUI
			} else {                                                                                            //Sinon (Faire si c'est un bouton ou autre)
				interactionType = InteractType.None;																//On met le type d'interaction à none
				interactObject = null;																				//On met l'objet visé par l'interaction à null
				GUIInteract.SetActive(false);																		//On désactive le GUI de l'interaction
			}
			
		} else {                                                                                            //Sinon
			interactionType = InteractType.None;																//On met le type d'interaction à none
			interactObject = null;																				//On met l'objet visé par l'interaction à null
			GUIInteract.SetActive(false);																		//Desactivation du GUI de l'interaction
		}
	}
	/*########*/
}
