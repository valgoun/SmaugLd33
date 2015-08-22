using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour
{
	[SerializeField]
	private PathManager
		path;
	[SerializeField]
	private float
		MinimumDist = 0.1f;
	[SerializeField]
	private float
		Speed = 5f;
	private GameObject ActivePoint;
	 
	// Use this for initialization
	void Start ()
	{
		ActivePoint = path.CheckPoints [0];
		transform.LookAt (ActivePoint.transform, Vector3.back);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.forward * Time.deltaTime * Speed, Space.Self);
		if ((transform.position - ActivePoint.transform.position).magnitude < MinimumDist) {
			ActivePoint = path.CheckPoints [path.CheckPoints.IndexOf (ActivePoint) + 1];
			transform.LookAt (ActivePoint.transform, Vector3.back);
		}
	}
}
