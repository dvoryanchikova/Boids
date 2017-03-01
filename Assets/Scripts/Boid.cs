using UnityEngine;
using System.Collections;

public class Boid : MonoBehaviour {

	public Vector3 velocity;


	private float cohesionRadius = 10;
	private float separationDistance = 5;
	private Collider[] boids;
	private Vector3 cohesion;
	private Vector3 separation;
	private int separationCount;
	private Vector3 aligment;
	private float maxSpeed = 15;

	void Start () {
		InvokeRepeating ("CalculateVelocity", 0, 1);
	}

	void CalculateVelocity(){
		
		velocity = Vector3.zero;
		cohesion = Vector3.zero;
		separation = Vector3.zero;
		separationCount = 0;
		aligment = Vector3.zero;

		boids = Physics.OverlapSphere (transform.position, cohesionRadius);
		foreach( var boid in boids){
			
			cohesion += boid.transform.position;
			aligment += boid.GetComponent<Boid> ().velocity;

			if(boid != GetComponent<Collider>() && (transform.position - boid.transform.position).magnitude < separationDistance){

				separation += (transform.position - boid.transform.position) / (transform.position - boid.transform.position).magnitude;
				separationCount++;
			}
		}
		cohesion = cohesion / boids.Length;
		cohesion -= transform.position;
		cohesion = Vector3.ClampMagnitude (cohesion, maxSpeed);

		if(separationCount > 0){
			
			separation = separation / separationCount;
			separation = Vector3.ClampMagnitude (separation, maxSpeed);

		}
		aligment = aligment / boids.Length;
		aligment = Vector3.ClampMagnitude (aligment, maxSpeed);

		velocity += cohesion + separation*10 + aligment*1.5f;
		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);
	}
	
	void Update () {
	
		if(transform.position.magnitude > 15){
			
			velocity += -transform.position.normalized;
		}

		transform.position += velocity * Time.deltaTime;

		Debug.DrawRay (transform.position, aligment, Color.blue);
		Debug.DrawRay (transform.position, separation, Color.green);
		Debug.DrawRay (transform.position, cohesion, Color.magenta);
	}
}
