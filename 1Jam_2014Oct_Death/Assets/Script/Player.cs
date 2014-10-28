using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject bomb;


	public static float health = 100f;

	public GameObject lighting;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position = Vector3.MoveTowards (this.transform.position, Input.mousePosition, 100f * Time.deltaTime);
		//Debug.Log (this.transform.position.x + " " + this.transform.position.y);

		if (Input.GetMouseButtonDown (0)) {

			StartCoroutine(Bomb());
		}



		if (health < 0 || health == 0) {
			Debug.Log("Game Over");
			Application.LoadLevel(0);
			GameManager.light = 0;	
			health = 100f;
		}

		if (GameManager.light > 0) {
			
			//if (Input.GetMouseButtonDown(1))
			{
				//Debug.Log("Pressed right click.");
				GameManager.light--;
				
				GameObject go = Instantiate(lighting, transform.position, transform.rotation) as GameObject;
				go.transform.localPosition = new Vector3(Random.Range(0, GameManager.widthOfScreen), Random.Range(0, GameManager.heightOfScreen),0);
				go.transform.parent = GameObject.Find("Canvas").transform;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy") {

			health -= 20f;
			//Debug.Log("Game Over");
		//	Application.LoadLevel(0);
		//	GameManager.light = 0;
		}
	}

	IEnumerator Bomb()
	{
		//Debug.Log("Bomb");
		GameObject go = Instantiate(bomb, transform.position, transform.rotation) as GameObject;
		go.transform.parent = GameObject.Find("Canvas").transform;
		yield return new WaitForSeconds(1);
		Destroy (go);
		//DestroyBombs ();
		//Destroy(GameObject.FindGameObjectsWithTag("Bomb"))
	}
}
