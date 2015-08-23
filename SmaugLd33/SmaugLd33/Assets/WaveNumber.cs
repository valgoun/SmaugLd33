using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveNumber : MonoBehaviour
{
	[SerializeField]
	private DwarfSpwanner
		Source ;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Text> ().text = Source.getWaveNumber ().ToString ();
	}
}
