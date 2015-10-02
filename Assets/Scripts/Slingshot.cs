using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	static public Slingshot S; 
	public GameObject launchPoint;
	public Transform launchTransform;

	public GameObject projectilePreFab;
	public GameObject projectile;

	public bool isAiming;
	public float maxPower;
	public float launchPower;
	public int numShots;
	public GUIText ammoText;

	// Use this for initialization
	void Start () {
		S = this;
		ammoText.text="Ammo: "+numShots;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isAiming == true) {
			if(Input.GetMouseButton(0)==false){
				//Projectile is fired
				isAiming=false;
				projectile.rigidbody.isKinematic=false;

				//clamping mouse fire power
				Vector3 mousePos= Input.mousePosition;
				mousePos= Camera.main.ScreenToWorldPoint(mousePos);
				mousePos.x=-(mousePos.x-launchTransform.position.x)*launchPower;
				mousePos.y=-(mousePos.y-launchTransform.position.y)*launchPower;

				mousePos.x = Mathf.Clamp(mousePos.x,-maxPower,maxPower);
				//mousePos.y = Mathf.Clamp(mousePos.y,-maxPower*1.5f,maxPower*1.5f);
				mousePos.y = Mathf.Clamp(mousePos.y,-maxPower,maxPower);
				Debug.Log(mousePos.x+"    "+mousePos.y);
				projectile.rigidbody.velocity = new Vector3(mousePos.x,mousePos.y,0f);
				ProjectileLine.S.Clear();
				followCamera.S.pointOfIntrest= projectile;
				projectile = null;
				numShots-=1;
				ammoText.text="Ammo: "+numShots;
				if(numShots==0){
					ammoText.text=ammoText.text+" Press r to restart level";
				}
			}

		}
	}

	void OnMouseOver(){
		if (numShots == 0) {
			return;
		}
		if (followCamera.S.overView == true) {
			return;
		}
		launchPoint.SetActive (true);
		if (Input.GetMouseButtonDown (0) == true) {
			isAiming = true;
				projectile= Instantiate(projectilePreFab) as GameObject;
				projectile.transform.position=launchTransform.position;
				projectile.rigidbody.isKinematic = true;
		}
	}
	void OnMouseExit(){
		launchPoint.SetActive (false);
	}

}
