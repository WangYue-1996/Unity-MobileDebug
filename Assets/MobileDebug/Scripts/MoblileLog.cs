using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public class MoblileLog : MonoBehaviour
{
	private class MyLog
	{
		public LogType logType;
		public string message;
	}
	//储存所有Debug信息
	MyLog[] logArray;//存放消息数组
	int logIndex;//当前索引
	int logCount;//记录消息的个数
	int logCapacity;//容量
	//总开关和各类型Debug的开关
	bool enableLog;
	bool showLog = true;
	bool showWarning = true;
	bool showError = true;
	bool showStackTrace;
	//UI组件
	Text logTex;
	Button clearBtn;
	Toggle enableLogTog;
	Toggle enableWaringTog;
	Toggle enableErrorTog;
	GameObject viewObject;


	void Awake()
	{
		//添加自定义事件
		Application.logMessageReceived += LogCallBack;
		//获取组件
		viewObject = transform.Find("ViewObject").gameObject;
		logTex = transform.Find("ViewObject/Viewport/LogText").GetComponent<Text>();
		clearBtn = transform.Find("ViewObject/ClearBtn").GetComponent<Button>();
		enableLogTog = transform.Find("ViewObject/EnableLog").GetComponent<Toggle>();
		enableWaringTog = transform.Find("ViewObject/EnableWaring").GetComponent<Toggle>();
		enableErrorTog = transform.Find("ViewObject/EnableError").GetComponent<Toggle>();
		//添加UI事件
		enableLogTog.onValueChanged.AddListener((bool b) =>{showLog = b;UpdateText();});
		enableWaringTog.onValueChanged.AddListener((bool b)=> {showWarning = b;UpdateText();});
		enableErrorTog.onValueChanged.AddListener ((bool b) => {showError = b;UpdateText ();});
		clearBtn.onClick.AddListener (() => {logArray = new MyLog[logCapacity];logIndex=0;UpdateText();});
	}

	void LogCallBack(string condition,string stackTrace,LogType logType)
	{
		logCount++;
		logIndex = logIndex >= logCapacity ? 0 : logIndex;
		if(showStackTrace)
			logArray[logIndex] = new MyLog (){ logType = logType, message = logCount+":"+condition+"\n->"+stackTrace};
		else
			logArray[logIndex] = new MyLog (){ logType = logType, message = logCount+":"+condition};
		UpdateText ();
		logIndex++;
	}

	void UpdateText()
	{
		if (viewObject.activeSelf) {
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < logCapacity; i++) {
				int j = (logIndex + 1 + i) % logCapacity;
				if (logArray [j] != null)
					sb.Append (AppendText (logArray [j]));
			}
			logTex.text = sb.ToString ();
		}
	}

	//这里改变Log颜色
	string AppendText(MyLog log)
	{
		switch (log.logType) {
		case LogType.Log:
			if (!showLog)
				return null;
			else
				return log.message + "\n";
		case LogType.Warning:
			if (!showWarning)
				return null;
			else
				return string.Format ("<color=orange>{0}</color>\n", log.message);
		default:
			if (!showError)
				return null;
			else
				return string.Format ("<color=red>{0}</color>\n", log.message);
		}
	}
		
	public void Initialization (int capacity,bool showStackTrace)
	{
		this.showStackTrace = showStackTrace;
		logCapacity = capacity;
		logArray = new MyLog[logCapacity];
	}

	public void Show ()
	{
		viewObject.SetActive (true);
		UpdateText ();
	}

	public void Close ()
	{
		viewObject.SetActive (false);
	}
}