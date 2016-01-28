using UnityEngine;
using System.Collections;

public class slash : MonoBehaviour {
	private float changeTime;
	private float destroyTime;
	private bool done = false;
	private float speed = 5f;
	
	// Use this for initialization
	void Start () {
		changeTime = Time.time + .15f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time < changeTime) {
			transform.localScale += new Vector3 (4.0f, 4.0f, 0.0f) * speed * Time.deltaTime;
		} else
			done = true;
		
		if (done)
		{
			StartCoroutine( destroyShield());
		}
	}
	
	IEnumerator destroyShield()
	{
		destroyTime = Time.time + .15f;
		
		if (Time.time < destroyTime)
			transform.localScale += new Vector3 (-4.0f, -4.0f, 0.0f) * speed * Time.deltaTime;
		
		yield return new WaitForSeconds (.15f);
		Destroy (gameObject);
	}
}