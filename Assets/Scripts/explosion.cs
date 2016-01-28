using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {
	private int damage = 150;
	private bool hit = false;
	public EnemyHealth EnemyHealth;
	public PlayerHealth PlayerHealth;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine (lifetime ());
	}

	IEnumerator lifetime()
	{
		yield return new WaitForSeconds (.2f);
		Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
		}

		if (other.gameObject.name == "Player")
		{
			other.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
		}
			
	}
}
