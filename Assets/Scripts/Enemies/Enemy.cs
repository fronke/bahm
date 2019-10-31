using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    NavMeshAgent agent;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {

        Transform playerTransform = FindPlayerPosition();
        if (playerTransform != null)
        {
            agent.SetDestination(playerTransform.position);
        }     

    }

    Transform FindPlayerPosition()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            return players[0].transform;
        }
        else
        {
            return null;
        }
    }
}
