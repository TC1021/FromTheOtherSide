using UnityEngine;
using System;

/*
 * Make your class implement the interface AudioProcessor.AudioCallbaks
 */
using System.Collections;


public class BeatDetector : MonoBehaviour, AudioProcessor.AudioCallbacks
{
	protected GameObject iOnBeat, iDefault,iError;
	public float waitBeat=0.50f;

    void Start()
    {
		//Inicializar indicadores del beat
		iDefault= GameObject.Find("beat_marker_red");
		iOnBeat= GameObject.Find("beat_marker_green");  iOnBeat.SetActive (false);
		//SET AUDIOCALLBACK, En ese objeto caera el beat
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.addAudioCallback(this);
    }

	IEnumerator activateBeatIndicator() //Prender foco verde, esperar x segundos, apagar
	{
		activate (iOnBeat);
		deactivate (iDefault);
		yield return new WaitForSeconds(waitBeat);
		deactivate (iOnBeat);
		activate (iDefault);
	}
	public void onOnbeatDetected() //ON BEAT, aqui ejecutamos lo que pasara cada beat
    {
		Debug.Log("BEAT");
		StartCoroutine(activateBeatIndicator());
	}

	public void deactivate(GameObject x){x.SetActive (false);}
	public void activate(GameObject x){x.SetActive (true);}
	void Update()
	{

	}
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
