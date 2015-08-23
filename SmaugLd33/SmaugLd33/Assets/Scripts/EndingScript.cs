using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class EndingScript : MonoBehaviour {

	public Text _text;
	string[] _Credits = new string[4];

	// Use this for initialization
	void Start () {
		_text.color = new Color(1, 1, 1, 0);
		_Credits[0] = "Developer\n\nBenjamin Blanchard";
		_Credits[1] = "Artiste\n\nMassi Belabbas";
		_Credits[2] = "Level Designers\n\nClément Fitoussi\nRémi Leblanc";
		_Credits[3] = "Sound Designer\n\nGuillaume Dusquesne";

		StartCoroutine("PerformCredit", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator PerformCredit(int i)
	{
		if (i >= _Credits.Length)
		{
			_text.color = new Color(1, 1, 1, 1);
			_text.rectTransform.localPosition = Vector3.zero;
			_text.text = "Thank You";
			return true;
		}
		_text.text = _Credits[i];
		yield return new WaitForSeconds(1);
		_text.rectTransform.localPosition = new Vector3(0, 100, 0);
		_text.DOFade(1, 1f);
		_text.rectTransform.DOAnchorPos(new Vector2(0,0), 2);
		yield return new WaitForSeconds(4);
		_text.rectTransform.DOAnchorPos(new Vector2(0, -100), 2);
		_text.DOFade(0, 1f);
		yield return new WaitForSeconds(2);
		StartCoroutine("PerformCredit", i +1);
	}


}
