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
	private bool _isInit = false;
	 
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!_isInit)
			return;

		transform.Translate (Vector3.forward * Time.deltaTime * Speed, Space.Self);
		if ((transform.position - ActivePoint.transform.position).magnitude < MinimumDist) {
			ActivePoint = path.CheckPoints [path.CheckPoints.IndexOf (ActivePoint) + 1];
			transform.LookAt (ActivePoint.transform, Vector3.back);
		}
	}

	public void Init (PathManager argPath)
	{
		path = argPath;
		ActivePoint = path.CheckPoints [1];
		transform.LookAt (ActivePoint.transform, Vector3.back);
		_isInit = true;
	}
}
