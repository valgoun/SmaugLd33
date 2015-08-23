using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;


public class textManager : MonoBehaviour {

	public Text _text;
	public Button _Play;
	string[] _stringArray = new string[5];
	AudioSource audio;

	public GameObject _DualDragoonObj;
	Vector3 _DualDragoonPos;

	// Use this for initialization
	void Start () {
		_DualDragoonPos = _DualDragoonObj.transform.position;
		_DualDragoonObj.SetActive(false);
		//Application.LoadLevelAdditiveAsync("NomLevel");
		audio = GetComponent<AudioSource>();
		_text.color = new Color(1, 1, 1, 0);
		_stringArray[0] = "Who is the monster?";
		_stringArray[1] = "Is it the one they call \"monster\"";
		_stringArray[2] = "Or the thieves?";
		_stringArray[3] = "They came for my precious gold, thieves!";
		_stringArray[4] = "Let's just burn them all...";

		StartCoroutine("ShowText", 0);
	}
	
	// Update is called once per frame
	void Update () {
		float sum = 0;
		float[] spectrum = audio.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);
		int i = 1;
		while (i < 1023) {
			Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
			Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.yellow);
			i++;
		}
		foreach (float j in spectrum)
		{
			sum += j;
		}

		if (_DualDragoonObj.activeSelf)
		{
		_DualDragoonObj.transform.position = _DualDragoonPos + new Vector3(0, sum * 2, 0);
		}
	}
	
	IEnumerator ShowText(int i)
	{
		if (i==0)
		{
			yield return new WaitForSeconds(2);
		}

		if (i>=_stringArray.Length)
		{
			_Play.gameObject.SetActive(true);
			_DualDragoonObj.SetActive(true);
			return true;
		}
		_text.text = _stringArray[i];
		_text.DOFade(1, 1);
		yield return new WaitForSeconds(5);
		_text.DOFade(0, 1);
		yield return new WaitForSeconds(2);
		StartCoroutine("ShowText", i+1);
	}

	public void LoadLevel()
	{
		//Application.LoadLevel("Bonlevel");
	}
}
