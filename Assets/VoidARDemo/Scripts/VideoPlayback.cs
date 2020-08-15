using UnityEngine;
using System.Collections;

public class VideoPlayback : MonoBehaviour {
	// Use this for initialization
	void Start () {
        GetComponent<VideoRecordBehaviour>().AddEventListener(VoidAREvent.SUCCESS, onSuccess);
	}

    void onSuccess(VoidAREvent evt) {
        Debug.Log("REC Success path:" + evt.data);
        Handheld.PlayFullScreenMovie(evt.data as string, Color.black, FullScreenMovieControlMode.CancelOnInput);
    }
}