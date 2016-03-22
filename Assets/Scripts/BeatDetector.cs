using UnityEngine;
using System;

/*
 * Make your class implement the interface AudioProcessor.AudioCallbaks
 */
using System.Collections;


public class BeatDetector : MonoBehaviour, AudioProcessor.AudioCallbacks
{
	protected GameObject iOnBeat, iDefault,iError;

    void Start()
    {
		iDefault= GameObject.Find("beat_marker_red");
		iOnBeat= GameObject.Find("beat_marker_green"); iOnBeat.SetActive (false);
		iError= GameObject.Find("beat_marker");	   iError.SetActive (false);
		//SET AUDIOCALLBACK
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.addAudioCallback(this);
    }
	IEnumerator wait(float seconds){yield return new WaitForSeconds(seconds); }
    
	public void onOnbeatDetected()
    {
		Debug.Log("BEAT");
		activate (iOnBeat);
		deactivate (iDefault);
		wait (1.0f);
		deactivate (iOnBeat);
		activate (iDefault);
    }

	public void deactivate(GameObject x){x.SetActive (false);}
	public void activate(GameObject x){x.SetActive (true);}

    //This event will be called every frame while music is playing
    public void onSpectrum(float[] spectrum)
    {
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i], 0);
            Debug.DrawLine(start, end);
        }
    }
}
