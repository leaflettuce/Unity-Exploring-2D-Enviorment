using UnityEngine;
using System.Collections;

public class hellfire : MonoBehaviour {
	public int enemyCount;
	private float changeTime;
	private float destroyTime;
	private bool done = false;
	public PlayerHealth PlayerHealth;
	public GameObject player;
	private int damage = 30;
	private float speed = 2.5f;
	
	// Use this for initializations
	void Start () {
		changeTime = Time.time + .3f;
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time < changeTime)
			transform.localScale += new Vector3 (4.0f, 4.0f, 0.0f) * speed * Time.deltaTime;
		else
			done = true;
		
		if (done) 
		{
			StartCoroutine( destroyShield());
		}
	}
	
	IEnumerator destroyShield()
	{
		destroyTime = Time.time + .3f;
		
		if (Time.time < destroyTime)
			transform.localScale += new Vector3 (-4.0f, -4.0f, 0.0f) * speed * Time.deltaTime;
		
		yield return new WaitForSeconds (.3f);
		Destroy (gameObject);
	}
	
	void OnTriggerEnter2d(Collider2D other)
	{
		if (other.gameObject.name == "Player")
			player.GetComponent<PlayerHealth> ().takeDamage (damage);
		
	}
}
