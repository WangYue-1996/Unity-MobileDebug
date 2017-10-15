using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoblileDebugger : MonoBehaviour {

	[Header("是否开启Log")]
	public bool enableLog;
	[Header("是否开启栈堆追踪")]
	public bool showStackTrace;
	[Header("Log最大数量")]
	public int logerCapacity;
	[Header("是否开启Test")]
	public bool enableTest;

	//Log部分
	Button enabelLogBtn;
	GameObject logObject;
	MoblileLog log;
	//Test部分
	Button enableTestBtn;
	GameObject testerObject;
	MoblileTester tester;
	//记录界面是否开启中
	bool logerShowing;
	bool testerShowing;

	static MoblileDebugger instance;

	void Awake()
	{
		if (instance != null&&instance != this)
			Destroy (transform.parent.gameObject);

		instance = this;

		if (enableLog) {
			enabelLogBtn = transform.Find ("EnableLogBtn").GetComponent<Button> ();
			enabelLogBtn.onClick.AddListener (EnableLogBtnOnClick);
			logObject = transform.Find ("LogerObject").gameObject;
			log = logObject.GetComponent<MoblileLog> ();
			log.Initialization (logerCapacity,showStackTrace);
		}
		if (enableTest) {
			enableTestBtn = transform.Find ("EnableTestBtn").GetComponent<Button> ();
			enableTestBtn.onClick.AddListener (EnableTestBtnOnClick);
			testerObject = transform.Find ("TesterObject").gameObject;
			tester = testerObject.GetComponent<MoblileTester> ();
		}

		DontDestroyOnLoad (transform.parent.gameObject);
	}

	public static void AddTestFunction(string functionName,UnityEngine.Events.UnityAction function)
	{
		instance.tester.AddTestFunction (functionName,function);
	}

	void EnableLogBtnOnClick()
	{
		if (logerShowing)
			log.Close ();
		else
			log.Show ();
		logerShowing = !logerShowing;
	}

	void EnableTestBtnOnClick()
	{
		if (testerShowing)
			tester.Close ();
		else
			tester.Show ();
		testerShowing = !testerShowing;
	}
}
