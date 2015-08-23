using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
	private GameObject
		ComboText;
	private float LifeTime = 0f;
	private bool IsInitiliaze = false;
	[SerializeField]
	private float
		speed = 5f;
	private bool OnlyOnce = true;

	public float
		rotationspeed = 5f;
	public int Combo = 0;
	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody2D> ().AddRelativeForce (Vector2.right * speed, ForceMode2D.Impulse);

	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (0f, 0f, rotationspeed);

		if (IsInitiliaze) {
			if (LifeTime <= 0) {
				ComboText.SetActive (false);
				Destroy (gameObject);
			}
			LifeTime -= Time.deltaTime;
			Debug.Log (Combo);
			if (Combo >= 1 && OnlyOnce)
				StartCoroutine (ComboDisplay ());
		}

	}

	IEnumerator ComboDisplay ()
	{
		OnlyOnce = false;
		ComboText.SetActive (true);
		yield return new WaitForSeconds (1f);
		ComboText.SetActive (false);
		OnlyOnce = true;

	}

	public void Initialize (float lifeTime, float speedMultiplier, GameObject ComboTextArg)
	{
		LifeTime = lifeTime;
		speed *= speedMultiplier;
		IsInitiliaze = true;
		ComboText = ComboTextArg;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Ennemy") {
			Destroy (col.gameObject);
			Combo++;
		}
	}
}
