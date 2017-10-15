using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoblileTester : MonoBehaviour {

	GameObject viewObject;
	Transform buttonGroup;

	void Awake()
	{
		viewObject = transform.Find ("ViewObject").gameObject;
		buttonGroup = transform.Find ("ViewObject/Viewport/ButtonGroup").transform;
	}

	public void AddTestFunction(string buttonName,UnityEngine.Events.UnityAction function)
	{
		Button btn = Instantiate (Resources.Load<GameObject> ("MobileTestButton"), buttonGroup).GetComponent<Button> ();
		btn.GetComponentInChildren<Text> ().text = buttonName;
		btn.onClick.AddListener (function);
	}

	public void Show ()
	{
		viewObject.SetActive (true);
	}

	public void Close ()
	{
		viewObject.SetActive (false);
	}
}
