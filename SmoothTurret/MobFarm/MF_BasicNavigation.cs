﻿using UnityEngine;
using System.Collections;

public class MF_BasicNavigation : MonoBehaviour {

	public float thrust = 100f;
	public float turnRate = 90f;
	public bool randomWpt;
	public bool stopAtLastWpt;
	public float waypointProx = 2f;
	public GameObject waypointGroup;
	public Transform[] waypoints;
	public int curWpt;
	
	bool reachedLastWpt;
	
	void Start () {
		if (waypointGroup) { // build waypoint list from children
			waypoints = new Transform[waypointGroup.transform.childCount];
			for ( int i=0; i < waypointGroup.transform.childCount; i++ ) {
				waypoints[i] = waypointGroup.transform.GetChild(i);
			}
		}
	}
	
	void FixedUpdate () {
		if ( reachedLastWpt == false ) { // stop if at last waypoint
			if ( waypoints.Length > 0 ) {
				if ( waypoints[curWpt] ) {

					// next waypoint
					if ( Vector3.Distance(transform.position, waypoints[curWpt].position) < waypointProx ) {
						if ( stopAtLastWpt == true && curWpt == waypoints.Length - 1 ) {
							reachedLastWpt = true;
						} else {
							if ( randomWpt == true ) {
								curWpt = Random.Range( 0, waypoints.Length );
							} else {
								curWpt = MFmath.Mod( curWpt +1, waypoints.Length);
							}
						}
					}

					//turn
					Quaternion _rot = Quaternion.LookRotation( waypoints[curWpt].position - rigidbody.position, transform.up );
					rigidbody.MoveRotation( Quaternion.RotateTowards( rigidbody.rotation, _rot, turnRate * Time.fixedDeltaTime ) );

					//move
					if (rigidbody) {
						rigidbody.AddForce( transform.forward * thrust * Time.fixedDeltaTime);
					} else {
						transform.position = transform.position + (transform.forward * thrust * Time.fixedDeltaTime);
					}

				}
			}
		}
	}
}
