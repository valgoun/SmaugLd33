﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldValue : MonoBehaviour
{
	[SerializeField]
	private ProtectZone
		GoldReader;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Text> ().text = GoldReader.Gold.ToString ();
	}
}
