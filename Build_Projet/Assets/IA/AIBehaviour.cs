using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AIBehaviour : NetworkBehaviour {

	// AI states
	enum AIState
	{
		Roaming,            // NO track and no attack
		FightingLongRange,  // Track the prey and shoot it from long range - use missiles
		FightingCloseRange, // Track the prey and try to kill it with all weapons availiable - use missiles and laser
		RunningAway			// Running away from enemy
	}

	/* -----------------
	 * Public variables
	 * ----------------- */

	// Game objects representing the Ai, its aim, its detection area and its respanw points
	public GameObject _LaserPrefab;		   	// Laser prefab
	public GameObject _MissilePrefab; 		// Missile prefab
	public GameObject[] _SpawnPoints;	   	// Spawn points for AI
	public string _EnemyTeamTag;		   	// Enemy tag

	// Audio sources
	public AudioSource LaserShoot; 		   // sound object for lasers -- "pew pew pew"
	public AudioSource shipSound; 		   // sound object for reactor noise

	// Map radius
	public float _MapRadius; 			   // sphere radius, the limit of the battlefield -- "Don't panic and stay in battlefield boy !"

	// Global variables
	public float MAXMISSDIST = 120.0f; 	   // Maximum distance that a missile can go trhough
	public float LIFE = 20.0f;			   // Maximum life

	// Status variable -- changed by server only 
	[SyncVar] // mutex
	public float Life;

	/* -----------------
	 * Private variables
	 * ----------------- */


	// Game objects representing the Ai, its aim and its detection area
	private GameObject _Assaillant; 		   // Assaillant of the current AI
	private GameObject _Prey; 			       // Aim of the current AI

	// Global variables for ai behavior
	private AIState _currentState = AIState.Roaming;
	private AIState _previousState;

	// Global macros for AI speed and behavior
	private float AI_speed; 					        // Actual AI speed
	private const float Normal_speed = 30.0f; 	        // Speed when normal flight
	private const float Boost_speed = 60.0f; 	        // Speed when boost is activated
	private const float Rotation_speed = 0.6f;          // Speed when rotating
	private const float KeepDirection_time = 5.0f;      // Default time for AI to keep direction in roaming mode
	private const float CanShootMissile_time = 2.5f;    // Default time for AI to keep calm and don't shoot -- "Don't forget to reload boy !"
	private const float CanShootLaserSalve_time = 1.0f; // Default time for AI to keep calm and don't launch a laser salve -- "Don't overheat the laser weaponery dude !"

	// Global variable to stock and calculate direction of the movement
	private Vector3 TargetAI; 					   // target position of AI -- "Find them and become their worst nightmare !"
	private Vector3 DirectionAI;				   // Direction AI has to follow -- "Move alone on the darkness, obey to witness."
	private float DirectionErrorAI;			   	   // Error epsilone for direction to follow
	private float KeepDirectionTimer; 			   // Timer used by AI to keep direction -- "Don't think, just move on guy."
	private float CanShootMissileTimer;			   // Timer used by AI to know when it can shoot missille -- "Make a big boom."
	private float CanShootLaserSalveTimer;		   // Timer used by AI to know when it can shoo laser salve -- "FIRE ! FIRE ! FIRE !"
	private RaycastHit _Hit; 					   // Hit for weaponery raycast -- "Aim and shoot, never miss them 'cause they won't miss you."

	// Global variable for AI reaction while meeting another AI
	private float AttackInitiative;				   // Initiative represented by a float. Better the float is, more the AI will take initiative during fight
	private float AdditionalDamage;				   // Damage to add to each attacks of AI or Player. This variable depends on the level of the shooter -- "Training is the key of success."

	// Use AI states like variable :
	private AIState CurrentState 
	{
		get { return _currentState; }
		set { _currentState = value; }
	}




	// Use this for initialization
	void Start () 
	{
		// Initializing status variables
		CurrentState = AIState.Roaming;
		TargetAI = new Vector3 (0.0f, 0.0f, 0.0f);
		KeepDirectionTimer = 0;
		CanShootMissileTimer = 0;
		CanShootLaserSalveTimer = 0;
		DirectionErrorAI = 2.0f;

		// Initializing attack damages, intiative and life
		AttackInitiative = Random.Range (1, 100);
		AdditionalDamage = Random.Range (0.0f, 2.0f);
		LIFE += Random.Range (0.0f, 5.0f);
		Life = LIFE;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (CurrentState) 
		{
		case AIState.Roaming: // idle movement
			// If timer is out or if the AI is arrived at destination with an accepted error 
			if (KeepDirectionTimer <= 0 || (Vector3.Distance(transform.position, DirectionAI) <= DirectionErrorAI)) 
			{
				// Choose the new direction to follow
				DirectionAI = ChooseRandomDirection ();
				KeepDirectionTimer = KeepDirection_time;
			} 
			else 
			{
				KeepDirectionTimer -= 0.1f * Time.deltaTime;
				// Move to the right direction
				Move (DirectionAI);
			}
			
			break;

		case AIState.FightingLongRange: // chase movement and attack
			Move (_Prey.transform.position);
			// Attack ?
				AttackFromLongRange();
			break;

		case AIState.FightingCloseRange: // chase movement and attack
			Move (_Prey.transform.position);
			// Attack ?
			AttackFromCloseRange();
			break;

		case AIState.RunningAway: // fleeing behavior
			Move (ChooseRunAwayDirection(_Assaillant.transform.position));
			break;
		}
	}


	/**********************
	 *  Movement methods  *
	 **********************/


	/**
	* Method : Move
	* Param :  void
	* Desc : Move the AI forward
	* Return : Void
	**/
	void Move(Vector3 target)
	{
		RotateView (target);
		MoveForward ();
	}


	/**
	* Method : MoveForward
	* Param :  void
	* Desc : Move the AI forward
	* Return : Void
	**/
	//[ClientRpc] // method called on the server side
	void MoveForward()
	{
		// Moving forward all the time
		transform.position += transform.forward * Normal_speed * Time.deltaTime;
	}


	/**
	* Method : RotateView
	* Param :  void
	* Desc : Apply the rotation of the AI movement step by step to the target
	* Return : Void
	**/
	//[ClientRpc] // method called on the server side
	void RotateView(Vector3 target)
	{
		Vector3 MyOwnPosition = transform.position;
		// Rotate not instantly the AI ship
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - MyOwnPosition, Vector3.up), Time.deltaTime * Rotation_speed);

	}


	/**
	* Method : ChooseDirectionRandom
	* Param :  void
	* Desc : Choose a random direction for the AI to follow. Verify if the new direction is in the battlefield.
	* Return : Vector3 - the direction to follow for the AI
	**/
	Vector3 ChooseRandomDirection()
	{
		return Random.insideUnitSphere * _MapRadius;
	}



	/**
	* Method : ChooseRunAwayDirection
	* Param :  void
	* Desc : Choose the opposite direction of assaillant.
	* Return : Vector3 - the direction to follow for the AI
	**/
	Vector3 ChooseRunAwayDirection(Vector3 assaillantPosition)
	{
		Transform tempo = transform;
		Vector3 answer;

		// Calculate the opposite direction 
		tempo.rotation = Quaternion.LookRotation (tempo.position - assaillantPosition);
		answer = tempo.position + (tempo.forward * 5);

		// Verify if the new direction stay in the battlefield
		if (Vector3.Distance (Vector3.zero, answer) >= _MapRadius) 
		{
			answer = ChooseRandomDirection ();
		}

		return answer;
	}

	
	/*********************
	 * Attacks intensity *
	 *********************/


	/**
	* Method : ChangeStateRoaming
	* Param :  void
	* Desc : Set the CurrentState to Roaming
	* Return : Void
	**/
	void ChangeStateRoaming()
	{
		CurrentState = AIState.Roaming;
		KeepDirectionTimer = KeepDirection_time;
	}


	/**
	* Method : ChangeStateLowFight
	* Param :  void
	* Desc : Set the CurrentState to LowFight
	* Return : Void
	**/
	void ChangeStateLowFight()
	{
		CurrentState = AIState.FightingLongRange;
	}


	/**
	* Method : ChangeStateFightForDeath
	* Param :  void
	* Desc : Set the CurrentState to FightForDeath
	* Return : Void
	**/
	void ChangeStateFightForDeath()
	{
		CurrentState = AIState.FightingCloseRange;
	}


	/********************
	 *  flee intensity  *
	 ********************/


	/**
	* Method : ChangeStateRunningAway
	* Param :  void
	* Desc : Set the CurrentState to RunningAway
	* Return : Void
	**/
	void ChangeStateRunningAway()
	{
		CurrentState = AIState.RunningAway;
	}


	/*******************
	 * Attack Behavior *
	 *******************/


	//-------------------
	// Long range attacks
	//


	/**
	* Method : AttackFromLongRange
	* Param : void
	* Desc : Create a ray between the current AI and the prey, verify if nothing else is between the aim and the current AI, then shot a missile
	* Return : Void
	**/
	void AttackFromLongRange()
	{
		Vector3 rayDirection =  _Prey.transform.position - transform.position;
		
		if (CanShootMissileTimer <= 0.0f) 
		{
			if ((Physics.Raycast (transform.position, rayDirection, out _Hit, MAXMISSDIST)) && (_Hit.transform.tag == _EnemyTeamTag)) 
			{
				TargetAI = _Prey.transform.position;
				
				if (RollTheDice (85.0f)) // 65% chance to launch a missile
				{ 
					// missile launch animation by creating the weapon and send it
					UseWeaponery (_MissilePrefab);
				}
				CanShootMissileTimer = CanShootMissile_time;
			}
		} 
		else 
		{
			CanShootMissileTimer -= 0.1f * Time.deltaTime;		 
		}
	}


	//--------------------
	// Close range attacks
	//

	/**
	* Method : AttackFromCloseRange
	* Param : void
	* Desc : Create a ray between the current AI and the prey, verify if nothing else is between the aim and the current AI, then shot a missile or a laser ray salve
	* Return : Void
	**/
	void AttackFromCloseRange()
	{
		float laserRayNumber = 0.0f;
		int i;
		TargetAI = _Prey.transform.position;

		// Shoot a missile ?
		if (CanShootMissileTimer <= 0.0f) 
		{
			if (RollTheDice (50)) // 50% chance to decide to launch a missile
			{ 
				UseWeaponery (_MissilePrefab);
			}
			CanShootMissileTimer = CanShootMissile_time;
		}
		else 
		{
			CanShootMissileTimer -= 0.1f * Time.deltaTime;		 
		}

		// Shoot a lasers salve ?
		if (CanShootLaserSalveTimer <= 0.0f) 
		{
			Vector3 rayDirection = TargetAI - transform.position;
			if ((Physics.Raycast (transform.position, rayDirection, out _Hit, MAXMISSDIST)) && (_Hit.transform.tag == _EnemyTeamTag)) 
			{
				if (RollTheDice (90.0f)) // 75% chance to decide to shoot a laser slave
				{ 
					laserRayNumber = Random.Range (3.0f, 6.0f); // shoot between 4 and 8 lasers by salve
					// Shoot the salve ! Don't let anyone alive !
					for (i = 0; i < laserRayNumber; i++)
						UseWeaponery (_LaserPrefab);
				}
			}
			CanShootLaserSalveTimer = CanShootLaserSalve_time;
		} 
		else 
		{
			CanShootLaserSalveTimer -= 0.1f * Time.deltaTime;
		}
		
	}



	/**************************
	 * Get the prey/hunter GO *
	 * Rmv the prey/hunter GO *
	 **************************/

	//--------------------------------------------------
	// Tracking someone
	//

	/**
	* Method : IamYourPrey
	* Param :  GameObject, prey - the Enemy entenring the vision trigger of current AI
	* Desc : Set the _Prey GameObject with the GO entering the chase trigger
	* Return : Void
	**/
	public void IamYourPrey(GameObject prey)
	{
		// Verify if we are not running away or already fighting with someone
		if ((_Prey == null) && (CurrentState != AIState.RunningAway)) 
		{
			// Verify if the enemy is a human player or an IA
			if (prey.GetComponent<AIBehaviour> () != null) // If the enemy is an AI
			{
				// Verify the attack initiative of the enemy and compare it with our. If current AI has a greater one, it'll attack
				if (prey.GetComponent<AIBehaviour> ().GetInitiative () < AttackInitiative) 
				{
					_Prey = prey;
					ChangeStateLowFight ();
				} 
				// If the current AI has a smaller one (or an equal one to simplify interactions), it'll flee
				else 
				{
					IamYourDoom (prey);
				}
			} 
			else // If the enemy is a human player
			{
				if (RollTheDice (65)) // 65% chance to chase player
				{ 
					_Prey = prey;
					ChangeStateLowFight ();
				} 
				else 
				{
					IamYourDoom (prey);
				}
			}

		}
	}


	/**
	* Method : GettingCloser
	* Param :  GameObject, prey - the Enemy entenring the vision trigger of current AI
	* Desc : Set the _Prey GameObject with the GO entering the chase trigger
	* Return : Void
	**/
	public void GettingCloser(GameObject prey)
	{
		if ((_Prey != null) && (CurrentState == AIState.FightingLongRange)) 
		{
			ChangeStateFightForDeath();
		}
	}


	/**
	 * Method : IamNoLongerClose
	 * Param : void
	 * Desc : Setting the 
	 * Return : void
	 **/
	public void IamNoLongerClose()
	{
		if ((_Prey != null) && (CurrentState == AIState.FightingLongRange)) 
		{
			ChangeStateLowFight ();
		}
	}

	/**
	* Method : IamNoLongerThere
	* Param : void
	* Desc : Set the _Prey GameObject to null because the prey is no longer chasable
	* Return : Void
	**/
	public void IamNoLongerThere()
	{
		if (_Prey != null) 
		{
			_Prey = null;
			ChangeStateRoaming ();
		} 
		else 
		{
			IcantChaseYouAnymore ();
		}
	}


	//--------------------------------------------------
	// Fleeing someone
	//

	/**
	* Method : IamYourDoom
	* Param :  GameObject, assaillant - the Enemy entenring the back trigger of current AI
	* Desc : Set the _Prey GameObject with the GO entering the chase trigger
	* Return : Void
	**/
	public void IamYourDoom(GameObject assaillant)
	{
		_Assaillant = assaillant;
		ChangeStateRunningAway ();
	}


	/**
	* Method : IcantChaseYouAnymore
	* Param : void
	* Desc : Set the _Assaillant GameObject to null because the prey is no longer chasable
	* Return : Void
	**/
	public void IcantChaseYouAnymore()
	{
		_Assaillant = null;
		ChangeStateRoaming();
	}



	/*********************
	 * Secondary methods *
	 *********************/


	/**
	* Method : RollTheDice
	* Param : float, percentage - The percentage of chance to hit the aim
	* Desc : Give a random number, if this number is under the percentage, the action will happen or be successfull, 
	*        else nothing happen or the action fail.
	* Return : bool, true if the shot hit the aim, else false.
	**/
	bool RollTheDice(float percentage)
	{
		float Try = Random.Range (0, 100); // Roll the dice !

		if (Try <= percentage) // Were you luky enough to hit your aim ?
		{
			return true;
		}

		return false;
	}


	/**
	 * Method : UseWeaponery
	 * Param : GameObject, weaponery - the weapon use to attack the enemy
	 * Desc : Create an instance of the weaponery prefab and send it to the aim by calling SendMessage 
	 *        with the function GoTo and an ArrayList as arguments. The ArrayList is composed as the 
	 *        following : agrs[0] is the _Prey GameObject and args[1] is the AdditionalDamage that the 
	 *        shooter deal due to its level (or randomly chosen for AI)
	 * Return : void
	 **/
	void UseWeaponery(GameObject weaponery)
	{
		// Create a list of arguments to simplify the argument communication
		ArrayList args = new ArrayList (2);
		args.Add (_Prey);
		args.Add (AdditionalDamage);
		args.Add (transform.gameObject);
		// Instantiate an object of the used weapon and create it on each client
		Vector3 spawnPosition = transform.position + (transform.forward * Normal_speed);
		GameObject weapon = Instantiate (weaponery, spawnPosition, Quaternion.identity);
		NetworkServer.Spawn (weapon);

		// Call the good appropriate method on each case
		if (weapon.GetComponent<LaserBehaviour> () != null) // If we shoot a laser
		{
			weapon.GetComponent<LaserBehaviour> ().GoTo (args);
		} 
		else // If we launch a missile
		{
			weapon.GetComponent<MissileBehaviour> ().GoTo (args);
		}
	}


	/**
	 * Method : TakeDamage
	 * Param : float, damage - Amount of taken damage
	 * Desc : Apply the taken damage on the current object life. If life <= 0, then the object is destroyed
	 * Return : void
	 **/
	public void TakeDamage(float damage) // Already executed on server
	{
		// Verify if we are on the server side
		if (!isServer) // We can't call the method on the client side -- don't cheat !
			return;

		// Damage taken is 
		Life -= damage;
		if (Life < 1.0f) // The current object die
		{ 
			Life = LIFE;
			IamNoLongerThere ();
			IcantChaseYouAnymore ();
			Respawn ();
		}
	}


	/**
	 * Method : IamDead
	 * Param : void
	 * Desc : Called by the server, this method tell to the AI that it has taken too much damage and it is dead.
	 * Return : void
	 **/
	public void Respawn()
	{
		Vector3 SpawnPoint = Vector3.zero;
		int i = 0;

		if (!isServer) // Verify if we are on server side -- only server can bring back AI to life
			return;

		if ((_SpawnPoints != null) && (_SpawnPoints.Length > 0)) 
		{
			i = Random.Range (0, _SpawnPoints.Length);
			SpawnPoint = _SpawnPoints [i].transform.position;
		}

		transform.position = SpawnPoint;
	}


	/**
	 * Method : GetInitiative
	 * Param : void
	 * Desc : return the current object AttackInitiative attribut
	 * Return : float - AttackInitiative attribut
	 **/
	float GetInitiative()
	{
		return AttackInitiative;
	}
}

