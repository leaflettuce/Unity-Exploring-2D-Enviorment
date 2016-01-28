using UnityEngine;
using System.Collections;

public class frag : MonoBehaviour {
	public GameObject player;
	public float speed = 8f;
	public float timer;
	public GameObject explosion;
	private Vector2 checkPos;
	private bool recentlyThrown = false;
	private bool cancelMove = false;
	private PlayerController PlayerController;
	private float holdTime;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine (countDown ());

		checkPos = player.transform.position;
		if (Vector2.Distance (checkPos, transform.position) < 8 && !recentlyThrown && !cancelMove)
			StartCoroutine( move ());
	}

	IEnumerator move()
	{
		holdTime = player.GetComponent<PlayerController> ().holdTime;
		transform.position = Vector2.MoveTowards (transform.position, player.transform.position, -speed * Time.deltaTime);
		yield return new WaitForSeconds (holdTime);
		recentlyThrown = true;
		yield return new WaitForSeconds (1.4f - holdTime);
		recentlyThrown = false;
	}

	IEnumerator countDown()
	{
		yield return new WaitForSeconds(1.4f);
		explode ();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
			explode ();
		else
			cancelMove = true;
	}

	void explode ()
	{
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

}
