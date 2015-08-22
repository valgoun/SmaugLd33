using UnityEngine;
using System.Collections;

public class Spawnner : MonoBehaviour
{
	[SerializeField]
	private GameObject
		Ball;
	[SerializeField]
	private float
		MinLifeTime = 2.0f;
	[SerializeField]
	private float
		MaxLifeTime = 6.0f;
	[SerializeField]
	private float
		MaxHoldTime = 2.0f;
	[SerializeField]
	private float
		Speed = 5.0f;
	[SerializeField]
	private float
		BulletSpeedMultiplier = 2.0f;


	private float HoldTime = 0f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (r, out hit)) {
			transform.LookAt (hit.point);
		}

		if (Input.GetMouseButton (0)) {
			HoldTime += Time.deltaTime;
		}
		if (Input.GetMouseButtonUp (0)) {
			HoldTime /= MaxHoldTime;
			HoldTime = Mathf.Clamp01 (HoldTime);
			GameObject ball = Instantiate (Ball, transform.position, transform.localRotation) as GameObject;
			ball.GetComponent<Bullet> ().Initialize (Mathf.Lerp (MinLifeTime, MaxLifeTime, HoldTime), Mathf.Lerp (1, BulletSpeedMultiplier, HoldTime));
			HoldTime = 0;
		}

		Vector3 move = Vector3.up * Input.GetAxis ("Vertical");
		transform.Translate (move * Time.deltaTime * Speed, Space.World);
	}
}
