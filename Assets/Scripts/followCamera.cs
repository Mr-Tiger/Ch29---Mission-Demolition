using UnityEngine;
using System.Collections;

public class followCamera : MonoBehaviour {
	public static followCamera S; 

	public GameObject CameraSlingshotPos;
	public GameObject OverAllPos;
	public GameObject pointOfIntrest;
	public bool overView;


	public float easing=0.05f;
	public Vector2 minXY;

	// Use this for initialization
	void Start () {
		S = this;
		this.transform.position = OverAllPos.transform.position;
		this.camera.orthographicSize =20;
		overView = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (overView == true) {
			this.transform.position = OverAllPos.transform.position;
			this.camera.orthographicSize =20;
			if(Input.GetMouseButton(0)==true){
				overView=false;
				transform.position = CameraSlingshotPos.transform.position;
				this.camera.orthographicSize =10;
			}
			return;
		}

		if (Input.GetMouseButtonDown (1) == true) {
			overView=true;
			pointOfIntrest=null;
		}

		if (pointOfIntrest == null) {
			return;
		}

		Vector3 tempPos=pointOfIntrest.transform.position;
		tempPos.x = Mathf.Max (minXY.x, tempPos.x);
		tempPos.y = Mathf.Max (minXY.y, tempPos.y);
		tempPos.z = -10;

		transform.position = Vector3.Lerp(transform.position, tempPos,easing);
		this.camera.orthographicSize = tempPos.y + 10;

		if (pointOfIntrest.rigidbody.IsSleeping() == true ) {
			transform.position = Vector3.Lerp(transform.position, CameraSlingshotPos.transform.position ,.5f);
		}
	
	

	}

/*	IEnumerator resetCamera(){
		yield return new WaitForSeconds(2);
		transform.position=startPos;
		pointOfIntrest=null;
	}*/
}
