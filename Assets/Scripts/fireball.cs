using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {
	public GameObject player;
	private float speed = 500f;
	private int damage = 30;
	private Vector2 target;
	private Vector2 playerPos;
	public PlayerHealth PlayerHealth;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerPos = player.transform.position;
		target = new Vector2 (playerPos.x - transform.position.x, playerPos.y - transform.position.y);
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		move ();
	}

	void move()
	{
		//transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime);
		rb2d.velocity =  target.normalized * speed * Time.deltaTime;
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			player.GetComponent<PlayerHealth> ().takeDamage (damage);
			Destroy (gameObject);
		}

		if (other.gameObject.tag == "wall" || other.gameObject.tag == "column")
			Destroy (gameObject);
	}
}
