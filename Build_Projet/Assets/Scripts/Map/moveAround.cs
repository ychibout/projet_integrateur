using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Louis lambrecht
//wander behavior as inspired by Craig W. Reynolds
//small displacement every frames

public class moveAround : MonoBehaviour {

	// circle will be used to calculate forces
	public float circleR = 1.0f;
	public float angleChangeChance = 0.05f;
	public float MaxRadius = 15f; //smaller radius = stronger moves

	public float mass = 30;
	public float maxSpeed = 10;
	public float maxForce = 15;

	private Vector3 velocity;
	private Vector3 moveForce;
	private Vector3 circleCenter;

	//collision avoidance
	private Vector3 ahead;
	private Vector3 ahead2;
	public float MAX_SEE_AHEAD = 5.0f;
	public float sphereRadius = 5.0f;


	// Use this for initialization
	void Start () {
		velocity = Random.onUnitSphere; //random point on the surface of the sphere
		circleCenter = velocity;
		moveForce = randomWander();
	}

	// Update is called once per frame
	void Update () {
		var desiredVelocity = wander();

		var steeringForce = desiredVelocity - velocity;
		steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce); //truncate
		steeringForce /= mass;

		velocity = Vector3.ClampMagnitude(velocity + steeringForce, maxForce);
		transform.position += velocity * Time.deltaTime;
		transform.forward = velocity.normalized;
	}

	private Vector3 wander(){
		if (transform.position.magnitude > MaxRadius) //sqrMagnitude faster to compute ?
		{
			var displacement = (circleCenter - transform.position).normalized;
			moveForce = velocity.normalized + displacement;
		}
		else if (Random.value < angleChangeChance)
		{
			moveForce = randomWander();
		}

		moveForce = moveForce.normalized * maxSpeed;
		return moveForce;
	}

	private Vector3 randomWander()
	{
		var circleC = velocity.normalized;
		var getPos = Random.insideUnitCircle; //sets the position to somewhere inside the circle

		var displacement = new Vector3(getPos.x, getPos.y) * circleR;
		displacement = Quaternion.LookRotation(velocity) * displacement;

		var moveForce = circleC + displacement;
		return moveForce;
	}

	/*private Vector3 collisionForce(){
		ahead = transform.position + velocity.normalized * MAX_SEE_AHEAD;
		ahead2 = transform.position + velocity.normalized * MAX_SEE_AHEAD * 0.5;
		var mostThreatening = findMostThreateningObstacle ();
		Vector3 avoidance = new Vector3 (0,0,0);

	}

	private GameObject findMostThreateningObstacle (){
		int mostThreatening = 0;
		for (int i = 0; i < GameObject.FindGameObjectsWithTag("obstacle").Length; i++) {
			GameObject obstacle = GameObject.FindGameObjectsWithTag ("obstacle") [i];
			bool collistion = PointInSphere (ahead, obstacle, sphereRadius) || PointInSphere (ahead2, obstacle, sphereRadius);

			if(collistion &&(mostThreatening = 0 ||
		}

	}

	public bool PointInSphere (Vector3 pnt, Vector3 sphereCenter, float sphereRadius){
		return(sphereCenter - pnt).magnitude < sphereRadius;
	}*/
}
