using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class FrameHelper : MonoBehaviour {

	private int _cnt = 0;
	private float _dt = 0;
	private float _frame = 0;

    private float _memory = 0;
    private float _totalMemory = 0;
	private float _currenUsedMemeory = 0;

	#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern float getAvailableMemorySize();
    [DllImport("__Internal")]
    private static extern float getTotalMemorySize();
	[DllImport("__Internal")]
	private static extern float getUsedMemory();
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_dt += Time.deltaTime;
		_cnt += 1;
		if(_cnt == 5)
		{
			_frame = _cnt * 1.0f/(_dt);
			_cnt = 0;
			_dt = 0;
			if (Application.platform == RuntimePlatform.IPhonePlayer) {
				_memory = getAvailableMemorySize ();
				_totalMemory = getTotalMemorySize ();
				_currenUsedMemeory = getUsedMemory ();
			} else if (Application.platform == RuntimePlatform.OSXEditor) {
				_totalMemory = GetProfilerMemory();
				_currenUsedMemeory = GetProfilerMemory ();
			}
        }
        
    }

	float GetProfilerMemory()
	{
        //uint allocatedMemory = Profiler.GetTotalAllocatedMemory ();
        uint allocatedMemory = 0;
        return allocatedMemory / 1024.0f / 1024;
	}

#if Debug
    void OnGUI()
	{
		GUI.Label(new Rect(Screen.width - 200,30,100,40),"frame : " + _frame);
       // if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
			GUI.Label(new Rect(200, 30, 100, 40), "usedMemory : " + _currenUsedMemeory);
			GUI.Label(new Rect(400, 30, 100, 40), "avaibleLeft : " + _memory);
        }
    }
#endif
	#endif
}
