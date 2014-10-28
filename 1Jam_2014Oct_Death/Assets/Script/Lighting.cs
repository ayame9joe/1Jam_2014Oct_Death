using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lighting : MonoBehaviour {

	private Vector3 positionOfLighting;
	private int lightingTime = 5;

	float distanceToPlayer;
	float distanceToEnemy;
	
	public Vector3 PositionOfLighting
	{
		get
		{
			//Some other code
			return positionOfLighting;
		}
		set
		{
			//Some other code
			positionOfLighting = value;
		}
	}
//	float distanceToEnemies;
	// Use this for initialization
	void Start () {
		StartCoroutine (DieOut ());
	}
	
	// Update is called once per frame
	void Update () {

		positionOfLighting = this.transform.position;

//		distance = Vector3.Distance (this.transform.position, GameObject.Find("Player").transform.position);

	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Enemy") {
			Debug.Log("Lighting Enemies");
			other.GetComponent<Enemy>().Health -= 10f;
		}
		if (other.tag == "Player") {
				
			lightingTime++;
		}
	}

	IEnumerator DieOut() {
		//Debug.Log("Before Waiting 2 seconds");
		yield return new WaitForSeconds(lightingTime);
		//Debug.Log("After Waiting 2 Seconds");
		Destroy (this.gameObject);
	}
}
