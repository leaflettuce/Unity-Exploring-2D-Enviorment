using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {

	public int basicDamage = 20;
	public int knockBackDamage = 10;
	public float knockBackDistance = 6f;
	private float knockBackSpeed = 1.5f;
	public float attackSpeed = 1f;
	public EnemyHealth EnemyHealth;
	public PlayerHealth PlayerHealth;
	public GameObject Acolyte;
	public GameObject Barrier;
	public GameObject fireball;
	private Vector2 randomLoc;
	public GameObject firewall;

	private GameObject player;
	private bool recentlyAttacked = false;
	private bool phase1 = false;
	private bool phase2 = false;
	private bool phase3 = false;
	private int currentHealth;
	private bool recentlyThrown = false;
	private bool initiated = false;
	private Vector2 throwDir;
	public GameObject hellfire;
	private bool hellReady = true;
	public GameObject slashImage;

	private float i;
	private float rate;

	private bool phase2Spawn = false;
	private bool phase3Spawn = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		EnemyHealth = GetComponent<EnemyHealth>();
		currentHealth = EnemyHealth.currentHealth;
		PlayerHealth = player.GetComponent<PlayerHealth> ();

	}

	void Update()
	{
		if (!initiated && Vector2.Distance (player.transform.position, transform.position) < 7.5) {
			initiated = true;
			currentHealth = EnemyHealth.currentHealth;
			checkPhase (currentHealth);
		}

//		if (phase3 && hellReady) 
//		{
//			randomLoc = new Vector2(Random.Range (3f,10f), Random.Range (3f,10f));
//			hellReady = false;
//			Instantiate(hellfire, randomLoc, Quaternion.identity);
//			StartCoroutine(resetHell());
//		}


		if (recentlyThrown)
		{
			player.transform.position = Vector2.Lerp (player.transform.position, throwDir, knockBackSpeed * Time.deltaTime);
		}

		if (initiated && !recentlyAttacked) 
		{
			if (Vector2.Distance (player.transform.position, transform.position) < 1.8) 
			{
				if (!phase1 && !recentlyThrown)
					StartCoroutine (throwBack ());

				StartCoroutine( attackMelee ());
			} 

			if (Vector2.Distance (player.transform.position, transform.position) >= 2.5f)
				StartCoroutine( attackRanged ());
		}
	}

	IEnumerator throwBack()
	{
		yield return new WaitForSeconds (2f);

		if (Vector2.Distance (player.transform.position, transform.position) <= 3)
		{
			Debug.Log("knock back");
			PlayerHealth.takeDamage(knockBackDamage);
			throwDir = Vector2.MoveTowards(player.transform.position,  transform.position, -knockBackDistance);
			recentlyThrown = true;

			if (phase3)
				StartCoroutine( castFirewall ());
		}
		StartCoroutine( changeBack ());
	}

	IEnumerator attackMelee()
	{
		recentlyAttacked = true;
		yield return new WaitForSeconds (.7f);
		Instantiate (slashImage, new Vector2 (6.25f, 12f), Quaternion.identity);
		if (Vector2.Distance (player.transform.position, transform.position) < 2) 
			PlayerHealth.takeDamage(basicDamage);
		yield return new WaitForSeconds (.8f);
		recentlyAttacked = false;
	}

	IEnumerator attackRanged()
	{
		recentlyAttacked = true;
		yield return new WaitForSeconds(.7f);
		if (Vector2.Distance (player.transform.position, transform.position) >= 2.5f)
			Instantiate(fireball, new Vector2(transform.position.x, transform.position.y - .75f), Quaternion.identity);
		yield return new WaitForSeconds(.8f);
		recentlyAttacked = false;
	}

	IEnumerator changeBack()
	{
		yield return new WaitForSeconds (knockBackSpeed);
		recentlyThrown = false;	
	}
	public void checkPhase(int health)
	{
		if (health > 1000) {
			phase1 = true;
			phase2 = false;
			phase3 = false;
		}

		if (health <= 1000 && currentHealth > 500)
		{
			phase1 = false;
			phase2 = true;
			phase3 = false;

			if (!phase2Spawn)
			{
				phase2Spawn = true;

				throwDir = Vector2.MoveTowards(player.transform.position,  transform.position, -knockBackDistance);
				recentlyThrown = true;
				shield();
				Spawn();
				StartCoroutine( changeBack());
			}
		}

		if (health <= 500){
			phase1 = false;
			phase2 = false;
			phase3 = true;

			if (!phase3Spawn)
			{
				phase3Spawn = true;
				
				throwDir = Vector2.MoveTowards(player.transform.position,  transform.position, -knockBackDistance);
				recentlyThrown = true;
				StartCoroutine( castFirewall ());
				shield();
				Spawn();
				StartCoroutine( changeBack());
			}
		}
	}

	void Spawn()
	{
		if (phase2) {
			Instantiate (Acolyte, new Vector2 (9, 11), Quaternion.identity);
			Instantiate (Acolyte, new Vector2 (3, 11), Quaternion.identity);
		}
		if (phase3) {
			Instantiate (Acolyte, new Vector2 (9, 11), Quaternion.identity);
			Instantiate (Acolyte, new Vector2 (3, 11), Quaternion.identity);
			Instantiate (Acolyte, new Vector2 (9, 4), Quaternion.identity);
			Instantiate (Acolyte, new Vector2 (3, 4), Quaternion.identity);
		}
	}

	void shield()
	{
		Instantiate (Barrier, new Vector2 (6.25f, 12f), Quaternion.identity);
	}

	IEnumerator resetHell()
	{
		yield return new WaitForSeconds (.8f);
		hellReady = true;
	}

	IEnumerator castFirewall()
	{
		yield return new WaitForSeconds (1.5f);
		Instantiate (firewall, new Vector2 (6.25f, 11f), Quaternion.identity);
		yield return new WaitForSeconds (2f);
		Instantiate (firewall, new Vector2 (6.25f, 11f), Quaternion.identity);
		yield return new WaitForSeconds (2f);
		Instantiate (firewall, new Vector2 (6.25f, 11f), Quaternion.identity);
	}
}
