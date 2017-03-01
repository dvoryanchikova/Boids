using UnityEngine;
using System.Collections;

public class HeartOfTheSwarm : MonoBehaviour {

	public Transform boidPrefab;
	public int swarmCount = 10;

	// Use this for initialization
	void Start () {

		for(var i = 0; i<swarmCount; i++){
			Instantiate (boidPrefab, Random.insideUnitSphere * 15, Quaternion.identity);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
