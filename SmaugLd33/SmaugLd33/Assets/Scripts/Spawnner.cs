using UnityEngine;
using System.Collections;

public class Spawnner : MonoBehaviour
{
	[SerializeField]
	private GameObject
		Ball;
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
	}
}
