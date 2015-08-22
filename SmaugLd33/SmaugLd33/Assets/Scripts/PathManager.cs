using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{

	public List<GameObject> CheckPoints;
	private bool visible = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnDrawGizmos ()
	{
		if (!visible)
			return;
		Gizmos.color = Color.green;
		foreach (var item in CheckPoints) {
			Gizmos.DrawSphere (item.transform.position, 1f);
		}
		Gizmos.color = Color.blue;
		for (int i = 0; i < CheckPoints.Count - 1; i++) {
			Gizmos.DrawLine (CheckPoints [i].transform.position, CheckPoints [i + 1].transform.position);
		}
	}

	public void ToggleGizmos ()
	{
		visible = !visible;
	}
}
