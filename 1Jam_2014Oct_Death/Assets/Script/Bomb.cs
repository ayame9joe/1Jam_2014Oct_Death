using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public GameObject enemy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Enemy") {

			Debug.Log("Enemy Killed");
			Destroy(other.gameObject);
			Destroy(this.gameObject);
			GameManager.lightFrequency++;
		//	GameManager.light--;
		//	GameObject go = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
		//	go.transform.parent = GameObject.Find("Canvas").transform;		
		}
	}
}
