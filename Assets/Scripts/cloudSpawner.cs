using UnityEngine;
using System.Collections;

public class cloudSpawner : MonoBehaviour {

	GameObject[] clouds;
	public GameObject[] cloudPrefab;
	public int startAmount;

	public Vector2  minXY;
	public Vector2 maxXY;

	public float scaleCloudMin;
	public float scaleCloudMax;
	public float cloudSpeed;

	// Use this for initialization
	void Start () {
		clouds = new GameObject[startAmount];
		for (int c=0; c<clouds.Length; c++) {
			clouds[c] = Instantiate(cloudPrefab[Random.Range(0,cloudPrefab.Length)],new Vector3(Random.Range(minXY.x,maxXY.x),Random.Range(minXY.y,maxXY.y),2f), new Quaternion(0f,0f,0f,0f) ) as GameObject ;
			clouds[c].transform.localScale = new Vector3(
				Random.Range(scaleCloudMin,scaleCloudMax)
			   ,Random.Range(scaleCloudMin,scaleCloudMax)
			   , 1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int c=0; c<clouds.Length; c++) {
			//moves clouds
			clouds[c].transform.position+= new Vector3(-.002f,0f);
			//destroys cloud when it gets off screen
			if(clouds[c].transform.position.x<minXY.x-10){
			//makes a new One
				GameObject.Destroy(clouds[c].gameObject);
				clouds[c]= Instantiate(
					cloudPrefab[Random.Range(0,cloudPrefab.Length)],
					new Vector3(maxXY.x
				            ,Random.Range(minXY.y,maxXY.y),2f)
							,new Quaternion(0f,0f,0f,0f) ) as GameObject ;

			}
		}
	}
}
