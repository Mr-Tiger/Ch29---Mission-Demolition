using UnityEngine;
using System.Collections;

public class victorySquare : MonoBehaviour {

	public string nextLevel;
	public GUIText topText;
	public GUIText botText;
	private bool isDone=false;

	void Start () {
		topText.enabled = false;
		botText.enabled = false;
	}

	void OnTriggerEnter(Collider other) {
		if (isDone == true) {
			return;
		}
		if (other.tag == "Projectile") {
			StartCoroutine(onwardLevel());
			isDone=true;
		}
	}
	void Update () {
		if(Input.GetKeyDown("r")==true){
			Application.LoadLevel(Application.loadedLevelName);
		}
	}

	IEnumerator onwardLevel(){
		topText.enabled = true;
		topText.text="Level Complete";
		if (nextLevel != "") {
			yield return new WaitForSeconds (1f);
			topText.text = "Next Level in...";
			botText.text="3";
			botText.enabled=true;
			yield return new WaitForSeconds (1f);
			botText.text="2";
			yield return new WaitForSeconds (1f);
			botText.text="1";
			yield return new WaitForSeconds (1f);
			Application.LoadLevel(nextLevel);
		}
		yield return new WaitForSeconds (2f);
		topText.text="You Win";
	}

}
