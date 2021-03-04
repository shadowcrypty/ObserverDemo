using System.Collections;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    #region Field Declarations

    private Camera currentCamera;
    private bool shouldPulse;

    #endregion

    #region Start & Update

    private void Start()
    {
        currentCamera = GetComponent<Camera>();
    }
    private void Update()
    {
        if (shouldPulse)
            currentCamera.backgroundColor = Color.Lerp(Color.black, Color.green, Mathf.PingPong(Time.time, .2f));
    }

    #endregion

    #region Public Methods

    public void StartPulsing()
    {
        StartCoroutine(pulseTimer());
    }

    public void StopPulsing()
    {
        shouldPulse = false;
        currentCamera.backgroundColor = Color.black;
    }

    #endregion

    private IEnumerator pulseTimer()
    {
        shouldPulse = true;
        yield return new WaitForSeconds(5);
        StopPulsing();
    }
}
