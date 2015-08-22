﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	private float LifeTime = 0f;
	private bool IsInitiliaze = false;
	[SerializeField]
	private float
		speed = 5f;
	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody> ().AddForce (transform.localRotation * Vector3.forward * speed, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (IsInitiliaze) {
			if (LifeTime <= 0)
				Destroy (gameObject);
			LifeTime -= Time.deltaTime;
		}

	}

	public void Initialize (float lifeTime, float speedMultiplier)
	{
		LifeTime = lifeTime;
		speed *= speedMultiplier;
		IsInitiliaze = true;
		Debug.Log (speed);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Ennemy") {
			Destroy (col.gameObject);
		}
	}
}
