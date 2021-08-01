using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectSettings : MonoBehaviour
{

    public bool TestMode;
    public int TestModeMaxVideoFrames;
    public int TetModeImageDisplayTime;

    public int LowBatteryImageDisplayTime;
    public int EskerImageDisplayTime;

    public ScreenOrientation screenOrientation;

    // Start is called before the first frame update
    void Start()
    {

        Screen.orientation = screenOrientation;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
