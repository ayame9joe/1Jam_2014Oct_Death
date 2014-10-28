using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy: MonoBehaviour{
	
	float speedX;
	float speedY;
	
	private bool right;
	private bool up;

	float distance;
	float attackDistance = 100f;
	
	float health = 100f;

	float[] distanceToLights;

	bool moveToLights = false;

	int enemyAlert = 0;

	bool hurtPlayer;
	
	public float SpeedX
	{
		get
		{
			//Some other code
			return speedX;
		}
		set
		{
			//Some other code
			speedX = value;
		}
	}

	public float SpeedY
	{
		get
		{
			//Some other code
			return speedY;
		}
		set
		{
			//Some other code
			speedY = value;
		}
	}

	public bool Right
	{
		get
		{
			//Some other code
			return right;
		}
		set
		{
			//Some other code
			right = value;
		}
	}

	public bool Up
	{
		get
		{
			//Some other code
			return up;
		}
		set
		{
			//Some other code
			up = value;
		}
	}

	public float Distance
	{
		get
		{
			//Some other code
			return distance;
		}
		set
		{
			//Some other code
			distance = value;
		}
	}

	public float Health
	{
		get
		{
			//Some other code
			return health;
		}
		set
		{
			//Some other code
			health = value;
		}
	}
	
	public void Start ()
	{
		this.transform.localPosition = new Vector3(Random.Range(-GameManager.widthOfScreen / 2, GameManager.widthOfScreen / 2), Random.Range(-GameManager.heightOfScreen / 2, GameManager.heightOfScreen / 2),0);
		speedX = Random.Range (1, 2);
		speedY = Random.Range (1, 2);
		right = RandomBool ();
		up = RandomBool ();


		
	}
	
	public void Update()
	{

		this.GetComponent<Image> ().color = new Color (255f, 255f, 255f, Random.Range (100, 200));

		GameObject[] lights = GameObject.FindGameObjectsWithTag ("Light");
		for (int i = 0; i < lights.Length; i++) {
			distanceToLights = new float[lights.Length];
			distanceToLights[i] = Vector3.Distance (this.transform.position, lights[i].transform.position);
			//distanceToEnemies = 
			//Debug.Log(distanceToLights[i]);
			if (distanceToLights[i] < 100f)
			{

				//float temp = ;
//				Debug.Log((100f - distanceToLights[i])/100f *255f);
				this.GetComponent<Image>().color = new Color(255f,255f,255f, (100f - distanceToLights[i])/100f *255f);

			}
			else
			{
				this.GetComponent<Image>().color = new Color(255f,255f,255f,0);
			}
			if (distanceToLights[i] < 100f)
			{
				moveToLights = true;
				this.transform.position = Vector3.MoveTowards(this.transform.position, lights[i].transform.position, 25f * Time.deltaTime);
			}
			else
			{
				moveToLights = false;
			}
		}

		if (lights.Length == 0) {
			this.GetComponent<Image>().color = new Color(255f,255f,255f,0);		
		}
	
		if (this.transform.localPosition.x < - GameManager.widthOfScreen / 2)
		{
			right = true;
		}
		else if (this.transform.localPosition.x >  GameManager.widthOfScreen / 2)
		{
			right = false;
		}
		
		if (this.transform.localPosition.y < - GameManager.heightOfScreen / 2)
		{
			up = true;
		}
		else if (this.transform.localPosition.y >  GameManager.heightOfScreen / 2)
		{
			up = false;
		}
		

		
		distance = Vector3.Distance (this.transform.position, GameObject.Find("Player").transform.position);
		//Debug.Log (distance);
		audio.pitch = (1 - Distance / Mathf.Sqrt (GameManager.widthOfScreen * GameManager.widthOfScreen + GameManager.heightOfScreen * GameManager.heightOfScreen)) * 2.5f;
		audio.volume = (1 - Distance / Mathf.Sqrt (GameManager.widthOfScreen * GameManager.widthOfScreen + GameManager.heightOfScreen * GameManager.heightOfScreen)) * 1;

		if (!moveToLights)
		{
			if (distance < attackDistance)
			{
				this.transform.position = Vector3.MoveTowards(this.transform.position, GameObject.Find("Player").transform.position, 50f * Time.deltaTime);
				if (!hurtPlayer)
				{
					StartCoroutine(HurtPlayer());
					hurtPlayer = true;
				}

			}
			else
			{
				if (right)
				{
					this.transform.Translate (new Vector3 (speedX, 0, 0));
				}
				else 
				{
					this.transform.Translate (new Vector3 (-speedX, 0, 0));
				}
				
				if (up)
				{
					this.transform.Translate (new Vector3 (0, speedY, 0));
				}
				else 
				{
					this.transform.Translate (new Vector3 (0, -speedY, 0));
				}
			}
		}
		else
		{
			if (distance < attackDistance)
			{
				this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(-GameObject.Find("Player").transform.position.x, -GameObject.Find("Player").transform.position.y, 0), 50f * Time.deltaTime);
			}
		}

		if (distance < attackDistance) {
			InvokeRepeating("EnemyAlert", 1, 1);		
		}
		else {CancelInvoke();}
		if (health < 0 || health == 0) {
			Destroy(this.gameObject);		
		}
	}

	public bool RandomBool()
	{
		int random = Random.Range (0, 2);
		if (random == 0) {
			
			return false;	
		} else {
			return true;		
		}
		
		
	}

	void EnemyAlert ()
	{
		enemyAlert++;

		if (enemyAlert % 2 == 1)
			
		{
			
			this.GetComponent<Image>().color = new Color(255f,255f,255f,255f);
			
		}
		
		else
			
			this.GetComponent<Image>().color = new Color(255f,255f,255f,0);
		
		StartCoroutine (CancelEnemyAlert ());
		//CancelInvoke ();
	}

	IEnumerator CancelEnemyAlert ()
	{
		yield return new WaitForSeconds (2);
		CancelInvoke ();
	}

	IEnumerator HurtPlayer ()
	{
		yield return new WaitForSeconds (1);
		Player.health--;
		hurtPlayer = false;
	}
	

	


}

