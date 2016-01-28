using UnityEngine;
using System.Collections;

public class firewall : MonoBehaviour {
	public GameObject player;
	private float speed = 3.5f;
	private int damage = 30;
	private Vector2 target;
	private Vector2 bossPos;
	public PlayerHealth PlayerHealth;
	private Rigidbody2D rb2d;
	private GameObject boss;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		boss = GameObject.Find ("Boss");
		bossPos = boss.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		move ();
	}
	
	void move()
	{
		transform.position = Vector2.MoveTowards (transform.position, bossPos, -speed * Time.deltaTime);
	}
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") {
			player.GetComponent<PlayerHealth> ().takeDamage (damage);
		}
		
		if (other.gameObject.tag == "wall")
			Destroy (gameObject);
	}
}
