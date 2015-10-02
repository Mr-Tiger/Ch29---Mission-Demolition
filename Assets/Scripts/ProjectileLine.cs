using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileLine : MonoBehaviour {
	static public ProjectileLine S;
	
	public float minDist;
	
	public LineRenderer line;
	private GameObject _poi;
	public List<Vector3> points;
	
	// Use this for initialization
	void Start () {
		S = this;
		line = GetComponent<LineRenderer>();
		line.enabled = false;
		points = new List<Vector3>();
	}
	
	public GameObject poi{
		get{
			return( _poi);
		}set{
			_poi = value;
			line.enabled=false;
			points=new List<Vector3>();
			AddPoint();
		}
	}
	
	public void Clear(){
		_poi = null;
		line.enabled = false;
		points = new List<Vector3> ();
	}
	public void AddPoint(){
		Vector3 pt = poi.transform.position;
		if (points.Count > 0 && (pt - lastPoint).magnitude < minDist) {
			return;
		}
		if (points.Count == 0) {

			//points.Add (pt + launchPosDiff);
			points.Add (followCamera.S.camera.ScreenToWorldPoint(Input.mousePosition));
			points.Add (pt);
			line.SetVertexCount (2);
			line.SetPosition (0, points [0]);
			line.SetPosition (1, points [1]);
			line.enabled = true;
		} else {
			points.Add(pt);
			line.SetVertexCount(points.Count);
			line.SetPosition(points.Count-1,lastPoint);
			line.enabled = true;
		}
	}
	
	public Vector3 lastPoint{
		get{
			if(points == null){
				return(Vector3.zero);
			}
			return(points[points.Count-1]);
		}
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		if (poi == null) {
			if (followCamera.S.pointOfIntrest != null) {
				if (followCamera.S.pointOfIntrest.tag == "Projectile") {
					poi = followCamera.S.pointOfIntrest;
				} else {
					return;
				}
			} else {
				return;
			}
		}
			AddPoint();
		}
}