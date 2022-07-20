using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSmoothCameraMove : MonoBehaviour{
	public float smoothXMinOffset;
	public float smoothXMaxOffset;
	public float smoothYMinOffset;
	public float smoothYMaxOffset;
	public float smoothSpeedX;
	public float smoothSpeedY;

	public float changeSceneZAxis;                                                                                          //Axis avant de changer de scene
	private bool isChangingScene = false;                                                                                   //Si on change de scene (pour appliquer un mouvement sur l'axe Z de la camera
	public float initialZForce;                                                                                             //Force initial vers l'arriere lors du début de changement de scene
	public float zForce;                                                                                                    //Force appliqué a l'axe Z chaque frame

	public Spawn outputScene;                                                                                              //Scene de sortie

	private Vector3 cameraMove = new Vector3(0, 0, 0);
	private Vector3 originalPos = new Vector3(0, 0, 0);
	public GameObject playerPefab;                                                                                          //Prefab du joueur

	public AudioSource windSound;
	public AudioSource powerDownSound;

	public GameObject UIBlackScreen;


	void Start() {
		originalPos = gameObject.transform.position;
		cameraMove.x = smoothSpeedX;
		cameraMove.y = smoothSpeedY;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.None;
	}
	void Update() {
		if(Input.GetButtonDown("Submit")) StartCoroutine("StartTheGame");
		gameObject.transform.position = gameObject.transform.position + (cameraMove * Time.deltaTime);
		if(gameObject.transform.position.x < originalPos.x - smoothXMinOffset || gameObject.transform.position.x > originalPos.x + smoothXMaxOffset) cameraMove.x *= -1;//Si dépassement des limites, on inverse le mouvement
		if(gameObject.transform.position.y < originalPos.y - smoothYMinOffset || gameObject.transform.position.y > originalPos.y + smoothYMaxOffset) cameraMove.y *= -1;//Si dépassement des limites, on inverse le mouvement

		if(isChangingScene) cameraMove.z += zForce * Time.deltaTime;                                          //On applique la force de la camera sur l'axe Z
		if(gameObject.transform.position.z > changeSceneZAxis && isChangingScene) {                         //Si on trigger le mur ET qu'on est en train de changer de scene
			isChangingScene = false;                                                                            //Pour éviter les appel multiple
			StartCoroutine("ChangeScene");                                                                      //Démarrage du changement de scene
		}
	}

	IEnumerator StartTheGame() {
		yield return new WaitForSeconds(1);                                                                 //Attendre une sedonde
		cameraMove.z = initialZForce;                                                                       //Force initial de la camera sur l'axe Z
		isChangingScene = true;                                                                             //On change de scene
		windSound.Play();                                                                                   //Lancement de l'audio de vent
	}

	IEnumerator ChangeScene() {
		windSound.Stop();                                                                                   //Arret de l'audio de vent
		UIBlackScreen.SetActive(true);
		powerDownSound.Play();
		yield return new WaitForSeconds(2f);
		GameObject player = Instantiate(playerPefab, new Vector3(0, 0, 0), Quaternion.identity);            //Instantiation du joueur
		player.name = "Player";                                                                             //On change le nom de l'objet
		player.GetComponent<PlayerSceneManage>().ChangeScene(outputScene);                                  //On change de scene
	}
}
