using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;
	public BossController BossController;

	private SpriteRenderer sprite;
	private Color defaultColor;
	// Use this for initialization
	void Start () {
		if (gameObject.name == "Acolyte")
		    maxHealth = 250;
		if (gameObject.name == "Priest")
			maxHealth = 1500;

		currentHealth = maxHealth;
		sprite = GetComponent<SpriteRenderer> ();
		defaultColor = sprite.color;
		BossController = GetComponent<BossController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0)
		{
			death();
		}
	}

	void death()
	{
		Destroy (gameObject);
	}
	
	public void takeDamage(int damage)
	{
		currentHealth -= damage;
		StartCoroutine (changeColor ());
		if (gameObject.name == "Boss")
			BossController.checkPhase (currentHealth);
	}

	IEnumerator changeColor()
	{
		sprite.color = Color.red;
		yield return new WaitForSeconds(.1f);
		sprite.color = defaultColor;
	}
}
