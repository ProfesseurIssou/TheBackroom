using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	Gestion des stats du joueur (vie, stamina, ...) 
*/



/*EFFECT*/
public enum EffectType {
	Malus,
	Bonus
}
public enum StatType {
	Health,
	Stamina,
	Hunger,
	Thirst,
	Mental,
	Speed
}
public class Effect {
	public string name;
	public string description;
	public EffectType type;
	public StatType stat;
	public float power;
	public float duration;

	public Effect(string effectName, string effectDescription, EffectType effectType, StatType statType, float effectPower, float durationNbSeconde) {
		name = effectName;
		description = effectDescription;
		type = effectType;
		stat = statType;
		power = effectPower;
		duration = durationNbSeconde;
	}
	//Si le nom est le même
	public static bool operator ==(Effect currentEffect, string effectName) {
		return currentEffect.name == effectName;                                                    //Si le nom est le même
	}
	//Si le nom n'est pas le même
	public static bool operator !=(Effect currentEffect, string effectName) {
		return currentEffect.name != effectName;                                                    //Si le nom n'est pas le même
	}
}
//Faire une liste d'effet a appliquer ET pour supprimé, on fait une recherche par nom "on recréé le == de la classe"
/*######*/

public class PlayerStats : MonoBehaviour{
	/*HEALTH*/
	public Image healthBar;
	private float minHealth = 0;
	private float maxHealth = 100;
	public float currentHealth = 100;
	/*######*/

	/*STAMINA*/
	public Image staminaBar;
	private float minStamina = 0;
	private float maxStamina = 100;
	public float currentStamina = 0;
	private float baseStaminaRegenPower = 10;
	public float runningStaminaConsumption = 40f;											//Consommation de la stamina lors du sprint
	/*#######*/

	/*HUNGER*/
	public Image hungerBar;
	private float minHunger = 0;
	private float maxHunger = 100;
	public float currentHunger = 100;
	private float baseHungerDecreasePower = 0.01f;
	/*######*/

	/*THIRST*/
	public Image thirstBar;
	private float minThirst = 0;
	private float maxThirst = 100;
	public float currentThirst = 100;
	private float baseThirstDecreasePower = 0.015f;
	/*######*/

	/*MENTAL*/
	public Image mentalBar;
	private float minMental = 0;
	private float maxMental = 100;
	public float currentMental = 100;
	/*#####*/

	/*SPEED*/
	private float minSpeed = 0;
	private float maxSpeed = 100;
	public float currentSpeed = 1;                                                          //Vitesse actuel
	private float baseSpeed = 5f;															//Vitesse de base
	/*#####*/

	/*EFFECT*/
	private List<Effect> currentEffectList = new List<Effect>();                                //Liste des effects sur les stats du joueur
	private struct StatsModifiers {                                                             //Modificateur de stats
		public float health;
		public float stamina;
		public float hunger;
		public float thirst;
		public float mental;
		public float speed;
	}
	/*######*/

	void Start(){
		AddEffect(new Effect("baseStaminaRegen", "Regeneration de base de la stamina", EffectType.Bonus, StatType.Stamina, baseStaminaRegenPower, -1));//Regeneration de la stamina
		AddEffect(new Effect("baseHunger", "Baisse de base de la faim", EffectType.Malus, StatType.Hunger, baseHungerDecreasePower, -1));//Baisse de la faim
		AddEffect(new Effect("baseThirst", "Baisse de base de la soif", EffectType.Malus, StatType.Thirst, baseThirstDecreasePower, -1));//Baisse de la soif
		AddEffect(new Effect("baseSpeed", "Vitesse de base", EffectType.Bonus, StatType.Speed, baseSpeed, -1));//Vitesse
	}

	void Update(){
		StatsModifiers statsModifiers = GetStatsModifiers();                                    //Calcul des modifications d'état selon les effets

		/*HEALTH*/
		currentHealth += statsModifiers.health * Time.deltaTime;									//Application du changement au cour du temps
		if(currentHealth > maxHealth) currentHealth = maxHealth;									//Si maximum atteint
		if(currentHealth < minHealth) currentHealth = minHealth;                                    //Si mimimum atteint
		healthBar.fillAmount = currentHealth / maxHealth;                                           //Mise a jour de la bar
		/*STAMINA*/
		//Debug.Log(currentStamina);
		currentStamina += statsModifiers.stamina * Time.deltaTime;									//Application du changement au cour du temps
		if(currentStamina > maxStamina) currentStamina = maxStamina;                                //Si maximum atteint
		if(currentStamina < minStamina) currentStamina = minStamina;                                //Si mimimum atteint
		staminaBar.fillAmount = currentStamina / maxStamina;										//Mise a jour de la bar
		/*HUNGER*/
		currentHunger += statsModifiers.hunger * Time.deltaTime;									//Application du changement au cour du temps
		if(currentHunger > maxHunger) currentHunger = maxHunger;                                    //Si maximum atteint
		if(currentHunger < minHunger) currentHunger = minHunger;                                    //Si mimimum atteint
		hungerBar.fillAmount = currentHunger / maxHunger;											//Mise a jour de la bar
		/*THIRST*/
		currentThirst += statsModifiers.thirst * Time.deltaTime;									//Application du changement au cour du temps
		if(currentThirst > maxThirst) currentThirst = maxThirst;                                    //Si maximum atteint
		if(currentThirst < minThirst) currentThirst = minThirst;                                    //Si mimimum atteint
		thirstBar.fillAmount = currentThirst / maxThirst;											//Mise a jour de la bar
		/*MENTAL*/
		currentMental += statsModifiers.mental * Time.deltaTime;									//Application du changement au cour du temps
		if(currentMental > maxMental) currentMental = maxMental;                                    //Si maximum atteint
		if(currentMental < minMental) currentMental = minMental;                                    //Si mimimum atteint
		mentalBar.fillAmount = currentMental / maxMental;											//Mise a jour de la bar
		/*SPEED*/
		currentSpeed = statsModifiers.speed;                                                        //Application de la nouvelle vitesse
		if(currentSpeed > maxSpeed) currentSpeed = maxSpeed;                                        //Si maximum atteint
		if(currentSpeed < minSpeed) currentSpeed = minSpeed;                                        //Si mimimum atteint
		if(currentStamina < 5) {                                                                    //Si pas assez de stamina
			if(!IsEffectPresent("NoStamina"))															//Si l'effet n'est pas encore appliqué
				AddEffect(new Effect("NoStamina", "Tu es épuisé", EffectType.Malus, StatType.Speed, 2.5f, 1));//Baisse de la vitesse
		} else {                                                                                    //Sinon assez de stamina
			RemoveEffect("NoStamina");																	//Suppression de l'effet
		}
	}


	//Ajout d'un nouvel effet
	public void AddEffect(Effect newEffect) {
		currentEffectList.Add(newEffect);                                                       //Ajout du nouvel effet
	}
	//Suppression d'un effet
	public void RemoveEffect(string effectName) {
		for(int i = 0; i < currentEffectList.Count; i++) {                                           //Pour chaque effet de la liste
			if(currentEffectList[i] == effectName) {                                                  //Si le nom est le même
				currentEffectList.RemoveAt(i);                                                          //Suppression de l'effet
				return;                                                                                 //Fin
			}
		}
	}

	//Check si un effet est présent
	public bool IsEffectPresent(string effectName) {
		for(int i = 0; i < currentEffectList.Count; i++) {                                           //Pour chaque effet de la liste
			if(currentEffectList[i].name == effectName) {												//Si le nom est le même
				return true;																				//Retourne true
			}
		}
		return false;                                                                                //Retourne false
	}

	//Calcul de modification des stats due au effets
	private StatsModifiers GetStatsModifiers() {
		StatsModifiers statsModifiers = default(StatsModifiers);                                //Creation d'un nouveau modifiers
		int effectType;                                                                         //Pour un calcul plus rapide (*-1 ou *1)
		for(int i = 0; i < currentEffectList.Count; i++) {                                      //Pour chaque effet
			effectType = ((int)currentEffectList[i].type);                                          //Récupération du type d'effet (bonus ou malus)
			if(effectType == 0) effectType--;                                                         //Si c'est un malus => -1
			switch(currentEffectList[i].stat) {                                                     //Pour chaque type de stat
				case StatType.Health:
					statsModifiers.health += effectType * currentEffectList[i].power;                         //Application de l'effet
					break;
				case StatType.Stamina:
					statsModifiers.stamina += effectType * currentEffectList[i].power;                        //Application de l'effet
					break;
				case StatType.Hunger:
					statsModifiers.hunger += effectType * currentEffectList[i].power;                         //Application de l'effet
					break;
				case StatType.Thirst:
					statsModifiers.thirst += effectType * currentEffectList[i].power;                         //Application de l'effet
					break;
				case StatType.Mental:
					statsModifiers.mental += effectType * currentEffectList[i].power;                         //Application de l'effet
					break;
				case StatType.Speed:
					statsModifiers.speed += effectType * currentEffectList[i].power;                         //Application de l'effet
					break;
			}

			//Gestion de la duration des effets
			if(currentEffectList[i].duration != -1) {                                               //Si l'effet n'est pas infinie
				currentEffectList[i].duration -= Time.deltaTime;                                        //Duration - 1
				if(currentEffectList[i].duration <= 0) {                                                //Si l'effet est terminé
					currentEffectList.RemoveAt(i);                                                          //Suppression de l'effet
					i--;                                                                                    //Pour palier au décallage des effets suivant
				}
			}
		}
		return statsModifiers;                                                                  //Retour du modifier
	}
}
