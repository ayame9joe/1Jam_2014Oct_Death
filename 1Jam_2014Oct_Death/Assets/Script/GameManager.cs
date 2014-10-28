using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static int time;
	int timeInt = 0;

	public static int light;

	public static float widthOfScreen = 500f;
	public static float heightOfScreen = 360f;

	int numOfEnemy = 2;


	//----UI---

	public Text txtLight;
	public Text txtDistance;
	public Text txtEnemy;
	public Text txtPlayerHealth;
	public Text txtTouchToStart;

	public GameObject enemy;

	float minDistance = 0;

	bool startTiming = true;

	public static int lightFrequency = 5;

	bool hasCreatedEnemies = false;
	//---Sound---
//	public AudioClip walkingSound;

	public enum eGameStatus
	{
		eGamePending = 0,
		eGameRunning,
		eGamePause,
		eGameShowResult,
		eGameEnd
	}

	eGameStatus gameStatus;
	// Use this for initialization
	void Start () {



	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStatus == eGameStatus.eGamePending) {
			if (Input.GetMouseButton(0))
			{
				gameStatus = eGameStatus.eGameRunning;
				txtTouchToStart.enabled = false;
				txtLight.renderer.enabled = true;
			}
		}

		else if (gameStatus == eGameStatus.eGameRunning)
		{

			if (!hasCreatedEnemies)
			{
				hasCreatedEnemies = true;
				for (int i = 0; i < numOfEnemy; i++) {
					GameObject go = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
					go.transform.parent = GameObject.Find("Canvas").transform;		
					
				}
			}
			//Debug.Log (lightFrequency);
			if (startTiming) {
				StartCoroutine(Timing());
				startTiming = false;
			}
			//---UI---
			txtLight.text = "Light:" + light;
			txtPlayerHealth.text = "Player Health:" + Player.health;

			//audio.Play ();
			//walkingSound.Play ();
			//walkingSound.pitch = minDistance / Mathf.Sqrt (widthOfScreen * widthOfScreen + heightOfScreen * heightOfScreen) * 10;
			//walkingSound.volume = minDistance / Mathf.Sqrt (widthOfScreen * widthOfScreen + heightOfScreen * heightOfScreen) * 10;
			/*time = Mathf.FloorToInt (Time.time);*/

			//Debug.Log (time);


			/*if ((time - timeInt) > 5) {
					
				light++;
				timeInt += 5;
			}*/

			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			for (int i = 0; i < enemies.Length; i++) {
				minDistance += enemies[i].GetComponent<Enemy>().Distance;
				minDistance = minDistance / enemies.Length;
				if (minDistance > enemies[i].GetComponent<Enemy>().Distance)
				{
					minDistance = enemies[i].GetComponent<Enemy>().Distance;
				}
				//Debug.Log(minDistance);

				//Debug.Log("Enemy" + i + ":" + enemies[i].GetComponent<Enemy>().Distance);
				//enemies[i].transform.localPosition = new Vector3(Random.Range(-widthOfScreen / 2, widthOfScreen / 2), Random.Range(-heightOfScreen / 2, heightOfScreen / 2),0);
				
			}
			txtDistance.text = minDistance.ToString();
			txtEnemy.text = enemies.Length.ToString ();

			if (enemies.Length == 0) {
			
				Application.LoadLevel(0);
				//time = Mathf.FloorToInt (Time.time);
				light = 0;
				Player.health = 100f;

			}
		}
		//Debug.Log ("Light:" + light);

	}

	IEnumerator Timing ()
	{
		yield return new WaitForSeconds (lightFrequency);
		light++;

		startTiming = true;
	}




}
