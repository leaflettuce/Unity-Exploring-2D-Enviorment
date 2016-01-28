using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject player;
	public PlayerHealth PlayerHealth;
	public EnemyHealth EnemyHealth;

	private float speed = 3f;
	private int currentHealth;
	private bool recentlyAttacked = false;
	private int damage = 10;

	private float xDiff;
	private float yDiff;
	private Animator animator;
	private bool started = false;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		EnemyHealth = GetComponent<EnemyHealth>();
		currentHealth = EnemyHealth.currentHealth;
		PlayerHealth = player.GetComponent<PlayerHealth> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector2.Distance (player.transform.position, transform.position) > 1.5)
			move ();
		else 
			if (!recentlyAttacked)
				StartCoroutine( attack ());
	}

	void move()
	{
		if (started)
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
		else
			StartCoroutine (startShit ());
	}

	IEnumerator attack()
	{
		recentlyAttacked = true;
		animator.Play ("attack");
		PlayerHealth.takeDamage(damage);
		yield return new WaitForSeconds (1.5f);
		recentlyAttacked = false;
		animator.Play ("New State");
	}

	IEnumerator startShit()
	{
		yield return new WaitForSeconds (1.2f);
		started = true;
	}
}
