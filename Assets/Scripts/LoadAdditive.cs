using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadAdditive : MonoBehaviour {
    public string level;

    public void AddOnClick(){
		SceneManager.LoadScene (level);
	}
}
