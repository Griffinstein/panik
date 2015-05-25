using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float roamSpeed = 1f;                            // The nav mesh agent's speed when roaming.
	public float investSpeed = 2f;                          // The nav mesh agent's speed when investigating.
	public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
	public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
	public float investWaitTime = 5f;                        
	public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
	public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.
	public Vector3 currentWayPoint;
	public float walkRadius = 10f;
	public bool purpose = false;
	public float interest = 0f;
	public bool walker = true;								//if true, zombie is a walker, if false, zombie is a lurker
	
	private EnemySight enemySight;                          // Reference to the EnemySight script.
	private NavMeshAgent nav;                               // Reference to the nav mesh agent.
	private Transform player;                               // Reference to the player's transform.
	private PlayerHealth playerHealth;                      // Reference to the PlayerHealth script.
	private LastPlayerSighting lastPlayerSighting;          // Reference to the last global sighting of the player.
	private float chaseTimer;                               // A timer for the chaseWaitTime.
	private float interestTimer;                               // A timer for the chaseWaitTime.
	private float patrolTimer;                              // A timer for the patrolWaitTime.
	private int wayPointIndex;                              // A counter for the way point array.
	
	
	void Awake ()
	{
		// Setting up the references.
		enemySight = GetComponent<EnemySight>();
		nav = GetComponent<NavMeshAgent>();
		CalulateNextMovement ();
		interestTimer = 0f;
		////player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		////playerHealth = player.GetComponent<PlayerHealth>();
		////lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
	}
	
	
	void Update ()
	{
		losingInterst ();
		SetSpeed ();

//		// If the player is in sight and is alive...
//		if(enemySight.playerInSight && playerHealth.health > 0f)
//			// ... shoot.
//			Shooting();
//		
//		// If the player has been sighted and isn't dead...
//		else if(enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0f)
//			// ... chase.
//			Chasing();
//		
//		// Otherwise...
//		else
			// ... patrol.
			Roaming();
	}


	void losingInterst() {
		Debug.Log (interest);
		if (purpose) {
			interestTimer += Time.deltaTime;
			
			if(interestTimer >= investWaitTime)
			{
				interest -= 0.1f;
				interestTimer = 0f;
			}
			
			if(interest <= 0f) {
				interest = 0f;
				purpose = false;
			}
		}
	}

	
	void SetSpeed(){
		// if player in sight then nav.speed = chaseSpeed;
		// if player is eating then nav.speed = 0f;
		if (purpose)
			nav.speed = investSpeed;
		else
			nav.speed = roamSpeed;
	}
	
	
//	void Shooting ()
//	{
//		// Stop the enemy where it is.
//		nav.Stop();
//	}
	
	
	void Chasing ()
	{
		// Create a vector from the enemy to the last sighting of the player.
		Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
		
		// If the the last personal sighting of the player is not close...
		if(sightingDeltaPos.sqrMagnitude > 4f)
			// ... set the destination for the NavMeshAgent to the last personal sighting of the player.
			nav.destination = enemySight.personalLastSighting;
		
		// Set the appropriate speed for the NavMeshAgent.
		nav.speed = roamSpeed;
		
		// If near the last personal sighting...
		if(nav.remainingDistance < nav.stoppingDistance)
		{
			// ... increment the timer.
			chaseTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(chaseTimer >= chaseWaitTime)
			{
				// ... reset last global sighting, the last personal sighting and the timer.
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
				chaseTimer = 0f;
			}
		}
		else
			// If not near the last sighting personal sighting of the player, reset the timer.
			chaseTimer = 0f;
	}

	void Roaming ()
	{
		// If near the next waypoint or there is no destination...
		if(nav.remainingDistance < nav.stoppingDistance)
		{
			// ... increment the timer.
			patrolTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(patrolTimer >= patrolWaitTime)
			{	
				// Reset the timer.
				patrolTimer = 0;

				patrolWaitTime = Random.Range (1f, 10f);
				Debug.Log(patrolWaitTime);
				CalulateNextMovement();
			}
		}
		else
			// If not near a destination, reset the timer.
			patrolTimer = 0;
		
		// Set the destination to the patrolWayPoint.
		nav.destination = currentWayPoint;
	}

	void CalulateNextMovement ()
	{
		Vector3 randomDirection = Random.insideUnitSphere * walkRadius;

		randomDirection += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
		Vector3 finalPosition = hit.position;

		Debug.Log(randomDirection);
		currentWayPoint = randomDirection;
	}
	
	
	void Patrolling ()
	{
		// Set an appropriate speed for the NavMeshAgent.
		nav.speed = roamSpeed;
		
		// If near the next waypoint or there is no destination...
		if(nav.remainingDistance < nav.stoppingDistance)
		{
			// ... increment the timer.
			patrolTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(patrolTimer >= patrolWaitTime)
			{
				// ... increment the wayPointIndex.
				if(wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;
				
				// Reset the timer.
				patrolTimer = 0;
			}
		}
		else
			// If not near a destination, reset the timer.
			patrolTimer = 0;
		
		// Set the destination to the patrolWayPoint.
		nav.destination = patrolWayPoints[wayPointIndex].position;
	}
}