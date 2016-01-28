using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {
	public int enemyCount;
	private float changeTime;
	private float destroyTime;
	private float speed = 1.2f;

	// Use this for initialization
	void Start () {
		changeTime = Time.time + .8f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		enemyCount = GameObject.FindGameObjectsWithTag("Enemy"). Length;


		if (Time.time < changeTime) {
			transform.localScale += new Vector3 (6.0f, 6.0f, 0.0f) * speed * Time.deltaTime;
		}

		if (enemyCount == 1) 
		{
			StartCoroutine( destroyShield());
		}
	}

	IEnumerator destroyShield()
	{
		destroyTime = Time.time + .8f;
		
		if (Time.time < destroyTime)
			transform.localScale += new Vector3 (-6.0f, -6.0f, 0.0f) * Time.deltaTime;

		yield return new WaitForSeconds (.8f);
		Destroy (gameObject);
	}
}
