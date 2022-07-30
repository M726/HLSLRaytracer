using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public string path;
    public KeyCode screenshotKey = KeyCode.F11;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(screenshotKey)) {
            string date = System.DateTime.Now.Year.ToString() + "-" + 
                          System.DateTime.Now.Month.ToString() + "-" + 
                          System.DateTime.Now.Day.ToString() + "-" + 
                          System.DateTime.Now.Hour.ToString() + "-" +
                          System.DateTime.Now.Minute.ToString() + "-" +
                          System.DateTime.Now.Second.ToString();
            ScreenCapture.CaptureScreenshot(path + "\\screenshot_" + date + ".png");
        }
    }
}
