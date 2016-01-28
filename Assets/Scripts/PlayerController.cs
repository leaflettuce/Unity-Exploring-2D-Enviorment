using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	public float speed = 7f;
	public EnemyHealth EnemyHealth;
	public Sprite spriteUp;
	public Sprite spriteDown;
	public Sprite spriteLeft;
	public Sprite spriteRight;
	public float attackSpeed = .75f;
	public float attackSpeedRanged = 1.5f;
	private Rigidbody2D rb2d;
	private int damage;
	private bool recentlyAttacked = false;
	public GameObject frag;
	public float holdTime = 0f;
	private float lastTime = 0f;
	public int fragAmmo = 10;

	private bool facingUp = true;
	private bool facingDown = false;
	private bool facingLeft = false;
	private bool facingRight = false;
	private RaycastHit2D hit;
	private new SpriteRenderer renderer;
	private bool first= true;

	public GameObject ammoCount;

	// Use this for initialization
	void Start () {
		//rb2d = GetComponent<Rigidbody2D> ();
		damage = 50;

		renderer = GetComponent<SpriteRenderer>();
		renderer.sprite = spriteUp;

	}
	
	// Update is called once per frame
	void Update () {

		ammoCount.GetComponent<Text> ().text = fragAmmo.ToString ();
		moveForward(); // Player Movement

		if (Input.GetMouseButton(0) && !recentlyAttacked)
			StartCoroutine(attackSword ());

		if (Input.GetMouseButton (1))
			{
			if (holdTime >= .5f)
				holdTime = .5f;
			
			if (first) 
			{
				holdTime = 0f;
				first = false;
			}
				

			holdTime += .01f;
		}

		if (Input.GetMouseButtonUp(1) && !recentlyAttacked && fragAmmo >= 1)
		{
			StartCoroutine(attackGrenade ());
			StartCoroutine(resetFirst());
		}

	}

	void moveForward()
	{
		if(Input.GetKey("w"))
		{
			transform.Translate(0,speed * Time.deltaTime,0);
			renderer.sprite = spriteUp;
			if (!facingUp)
				transform.position = new Vector2(transform.position.x +.25f, transform.position.y);

			facingUp = true;
			facingDown = false;
			facingLeft = false;
			facingRight = false;
			return;
		}
		if(Input.GetKey("s"))
		{
			transform.Translate(0,-speed * Time.deltaTime,0);
			renderer.sprite = spriteDown;
			if (!facingDown)
				transform.position = new Vector2(transform.position.x -.25f, transform.position.y);

			facingUp = false;
			facingDown = true;
			facingLeft = false;
			facingRight = false;
			return;
		}
		if(Input.GetKey("a"))
		{
			transform.Translate(-speed * Time.deltaTime,0 ,0);
			renderer.sprite = spriteLeft;
			if (!facingLeft)
				transform.position = new Vector2(transform.position.x, transform.position.y);

			facingUp = false;
			facingDown = false;
			facingLeft = true;
			facingRight = false;
			return;
		}
		if(Input.GetKey("d"))
		{
			transform.Translate(speed * Time.deltaTime,0 ,0);
			renderer.sprite = spriteRight;
			if (!facingRight)
				transform.position = new Vector2(transform.position.x, transform.position.y);

			facingUp = false;
			facingDown = false;
			facingLeft = false;
			facingRight = true;
			return;
		}
	}

	IEnumerator attackSword()
	{
		if (facingUp)
			hit =  Physics2D.Raycast(transform.position, Vector2.up, 1.1f, 1 << LayerMask.NameToLayer("Enemy"));
		if (facingDown)
			hit =  Physics2D.Raycast(transform.position, Vector2.down, 1.1f, 1 << LayerMask.NameToLayer("Enemy"));
		if (facingLeft)
			hit =  Physics2D.Raycast(transform.position, Vector2.left, 1.1f, 1 << LayerMask.NameToLayer("Enemy"));
		if (facingRight)
			hit =  Physics2D.Raycast(transform.position, Vector2.right, 1.1f, 1 << LayerMask.NameToLayer("Enemy"));

		if (hit.collider != null) 
		{
			recentlyAttacked = true;
			hit.collider.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
			yield return new WaitForSeconds(attackSpeed);
			recentlyAttacked = false;
		}
	}

	IEnumerator attackGrenade()
	{
		fragAmmo -= 1;

		if (facingUp)
			Instantiate(frag, new Vector2(transform.position.x, transform.position.y + .75f), Quaternion.identity);

		if (facingDown)
			Instantiate(frag, new Vector2(transform.position.x, transform.position.y - .75f), Quaternion.identity);
		if (facingLeft)
			Instantiate(frag, new Vector2(transform.position.x - .75f, transform.position.y), Quaternion.identity);
		if (facingRight)
			Instantiate(frag, new Vector2(transform.position.x + .75f, transform.position.y), Quaternion.identity);

		recentlyAttacked = true;
		yield return new WaitForSeconds(attackSpeedRanged);
		recentlyAttacked = false;
	}

	IEnumerator resetFirst()
	{
		yield return new WaitForSeconds (.1f);
		first = true;
	}
}