using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
using UnityEngine.UI;

public class recordcontrol : MonoBehaviour
{
    public Recorder recorder;
    public GameObject mutetext;
    [SerializeField]
    private Text muteText;
    // Start is called before the first frame update
    void Start()
    {
        recorder.TransmitEnabled = false;
        muteText.text = "Muted";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.M)) {
            if (mutetext.activeSelf)
            {
                mutetext.SetActive(false);

            }
            if (recorder.TransmitEnabled==false)
            {
                recorder.TransmitEnabled = true;
                muteText.text = "Unmuted";
            } else
            {
                recorder.TransmitEnabled = false;
                muteText.text = "Muted";
            }
            Debug.Log(recorder.TransmitEnabled);
        }        
    }

}
