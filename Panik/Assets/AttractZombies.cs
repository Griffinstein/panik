using UnityEngine;
using System.Collections;

public class AttractZombies : MonoBehaviour {
	public float priority = 1f;
	public float radius = 10f;

	private NavMeshAgent nav;                       // Reference to the NavMeshAgent component.
	private SphereCollider col;                     // Reference to the sphere collider trigger component.
	private HashIDs hash;                           // Reference to the HashIDs.

	void Awake ()
	{
		nav = GetComponent<NavMeshAgent>();
		col = GetComponent<SphereCollider>();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == Tags.zombie) {
			if (other.gameObject.GetComponent<EnemyAI>().interest < priority){
				other.gameObject.GetComponent<EnemyAI>().currentWayPoint = this.transform.position;
				other.gameObject.GetComponent<EnemyAI>().purpose = true;
				other.gameObject.GetComponent<EnemyAI>().interest = priority;
			}
		}
	}

	void OnTriggerExit (Collider other)
	{

	}
}
