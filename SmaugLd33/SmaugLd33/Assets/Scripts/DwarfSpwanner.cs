using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DwarfSpwanner : MonoBehaviour
{
	[SerializeField]
	private GameObject
		Dwarf;
	[SerializeField]
	private List<PathManager>
		Paths;
	[SerializeField]
	private List<float>
		Timeline;
	[SerializeField]
	private List<int>
		PathIndex;
	[SerializeField]
	private List<int>
		NumberOfDwarf;
	[SerializeField]
	private List<float>
		TimeBetweenSpwan;
	public bool Victory = false;

	private float timer = 0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		for (int i = 0; i < Timeline.Count; i++) {
			if (timer >= Timeline [i]) {
				StartCoroutine (SpwanWave (Paths [PathIndex [i]], TimeBetweenSpwan [i], NumberOfDwarf [i]));
				Timeline.RemoveAt (i);
				PathIndex.RemoveAt (i);
			} else {
				break;
			}
		}
		if (Timeline.Count == 0)
			Victory = true;
	}

	IEnumerator SpwanWave (PathManager path, float timebetweenspwan, int SpwanNumber)
	{
		int number = 0;
		while (number < SpwanNumber) {
			GameObject DwarfClone = Instantiate (Dwarf, path.CheckPoints [0].transform.position, Quaternion.identity) as GameObject;
			DwarfClone.GetComponent<FollowPath> ().Init (path);
			number++;
			yield return new WaitForSeconds (timebetweenspwan);
		}
	}

	public int getWaveNumber ()
	{
		return Timeline.Count;
	}
}
