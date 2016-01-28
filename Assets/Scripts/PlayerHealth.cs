using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public int maxHealth = 300;
	public int currentHealth;
	private GameObject player;
	public GameObject spawnPoint;

	private SpriteRenderer sprite;
	private Color defaultColor;
	private bool recentlyDamaged = false;

	public Texture2D bgImage; 
	public Texture2D fgImage; 
	
	public float healthBarLength;
	public GameObject healthCount;
	public GameObject healthBar;

	void Start () {
		currentHealth = maxHealth;
		sprite = GetComponent<SpriteRenderer> ();
		defaultColor = sprite.color;
		player = GameObject.Find("Player");

	}
	

	void Update () {
		AddjustCurrentHealth();
		healthCount.GetComponent<Text> ().text = currentHealth.ToString () + " / " +maxHealth.ToString() ;
		healthBar.GetComponent<Slider> ().maxValue = maxHealth;
		healthBar.GetComponent<Slider> ().value = currentHealth;

		if (currentHealth <= 0)
		{
			death();
		}
	}

	void death()
	{
		player.transform.position = spawnPoint.transform.position;
		currentHealth = maxHealth;
	}

	public void takeDamage(int damage)
	{
		if (!recentlyDamaged) 
		{
			currentHealth -= damage;
			recentlyDamaged = true;
			StartCoroutine(immuneTime());
			StartCoroutine (changeColor ());
		}

	}

//	void OnCollisionEnter2D(Collision2D other)
//	{
//		if (other.gameObject.CompareTag("Enemy"))
//		{
//			transform.position = Vector2.MoveTowards(transform.position, other.transform.position, -25f * Time.deltaTime);
//			takeDamage(30);
//		}
//	}

	IEnumerator changeColor()
	{
		sprite.color = new Color (255f, 255f, 255f, .5f);
		yield return new WaitForSeconds(1.5f);
		sprite.color = defaultColor;
	}

	IEnumerator immuneTime()
	{
		yield return new WaitForSeconds (1.5f);
		recentlyDamaged = false;
	}
	

	public void AddjustCurrentHealth(){

		if(currentHealth <0)
			currentHealth = 0;
		
		if(currentHealth > maxHealth)
			currentHealth = maxHealth;
		
		if(maxHealth <1)
			maxHealth = 1;
		
		healthBarLength =(Screen.width /2) * (currentHealth / (float)maxHealth);
	}
}
