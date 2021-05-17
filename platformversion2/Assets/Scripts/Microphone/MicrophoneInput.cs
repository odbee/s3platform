using UnityEngine;
using UnityEngine.UI; //for accessing Sliders and Dropdown
using System.Collections.Generic; // So we can use List<>
using Photon.Voice.Unity;
using Photon.Voice.PUN;



public class MicrophoneInput : MonoBehaviour {
	public float minThreshold = 0;
	public float frequency = 0.0f;
	public int audioSampleRate = 44100;
	public string microphone;
	public FFTWindow fftWindow;
	public Dropdown micDropdown;
	public Slider thresholdSlider;
	public Recorder recorder;
	public int micval=0;

	private List<string> options = new List<string>();

	void Start() {

		var enumerator = Recorder.PhotonMicrophoneEnumerator;
		if (enumerator.IsSupported)
		{
			for (int i = 0; i < enumerator.Count; i++)
			{
				string stuff = "PhotonMicrophone Index={" + i +"} ID= " + enumerator.IDAtIndex(i) + " Name= " + enumerator.NameAtIndex(i);
				options.Add( stuff);
				Debug.LogFormat("PhotonMicrophone Index={0} ID={1} Name={2}", i, enumerator.IDAtIndex(i),
					enumerator.NameAtIndex(i));
			}
		}


		microphone = options[PlayerPrefsManager.GetMicrophone ()];
		minThreshold = PlayerPrefsManager.GetThreshold ();

		//add mics to dropdown
		micDropdown.AddOptions(options);
		micDropdown.onValueChanged.AddListener(delegate {
			micDropdownValueChangedHandler(micDropdown);
		});

		//initialize input with default mic
		UpdateMicrophone ();
	}

	void UpdateMicrophone(){

		
		// Mute the sound with an Audio Mixer group becuase we don't want the player to hear it
		Debug.Log(Microphone.IsRecording(microphone).ToString());

		if (Microphone.IsRecording (microphone)) { //check that the mic is recording, otherwise you'll get stuck in an infinite loop waiting for it to start
			while (!(Microphone.GetPosition (microphone) > 0)) {
			} // Wait until the recording has started. 
		
			Debug.Log ("recording started with " + microphone);
			// Start playing the audio source
		} else {
			//microphone doesn't work for some reason

			Debug.Log (microphone + " doesn't work!");
		}
	}


	public void micDropdownValueChangedHandler(Dropdown mic){
		Debug.Log(mic.value);
		recorder.PhotonMicrophoneDeviceId = mic.value;
		recorder.RestartRecording();
		microphone = options[mic.value];
		//UpdateMicrophone ();
	}


}