using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour {
	
	void Start () {
		//为 MoblileDebugger 添加按钮
		MoblileDebugger.AddTestFunction ("打印Log", Log);
		MoblileDebugger.AddTestFunction ("打印Warning", Warning);
		MoblileDebugger.AddTestFunction ("打印Error", Error);
	}

	void Log()
	{
		Debug.Log ("Log");
	}

	void Warning()
	{
		Debug.LogWarning ("Warning");
	}

	void Error()
	{
		Debug.LogError ("Error");
	}

}
