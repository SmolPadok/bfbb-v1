﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerPunch : MonoBehaviour {

	public float punchpower;
	public float powerAddAttack, powerAddDefense;
    public float knockback, pencilKnockback, woodyKnockback, spongyKnockback, penKnockback;
	public float powerValue;
	public float hadokenSpeed;
	public Transform opponentSpawn;
	public AudioSource audio;
	public List<AudioClip> punchList;
	public GameObject opponent, player, cam;

	// Use this for initialization
	void Start () {

		if (player.GetComponent<Pencil_ControllerPlayer> ().enabled == true) {

			punchpower = player.GetComponent<Pencil_ControllerPlayer> ().punchpower;
            knockback = pencilKnockback;

		} else if (player.GetComponent<Woody_ControllerPlayer> ().enabled == true) {

            punchpower = player.GetComponent<Woody_ControllerPlayer> ().punchpower;
            knockback = woodyKnockback;

		} else if (player.GetComponent<Spongy_ControllerPlayer> ().enabled == true) {

            punchpower = player.GetComponent<Spongy_ControllerPlayer> ().punchpower;
            knockback = spongyKnockback;

        }
        else if (player.GetComponent<Pen_ControllerPlayer>().enabled == true)
        {

            punchpower = player.GetComponent<Pen_ControllerPlayer>().punchpower;
            knockback = penKnockback;

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

        CameraShaker.Instance.ShakeOnce(2f, 5f, 0.1f, 0.5f);

		if (other.gameObject.CompareTag ("Opponent")) {
			opponent.GetComponent<MainOpponent_Controller> ().newHealth -= (punchpower / 100f);
			player.GetComponent<Player_Controller> ().currentPower += powerAddAttack;
			opponent.GetComponent<MainOpponent_Controller> ().currentPower += powerAddDefense;
			int randomize = Random.Range (0, punchList.Count);
			AudioClip punch = punchList [randomize];
			audio.PlayOneShot(punch);
			cam.GetComponent<Camera_Controller> ().ZoomPunch ();

			if (gameObject.name == "PunchTriggerR") {

				opponent.GetComponent<Rigidbody> ().AddForce (Vector3.right * knockback, ForceMode.Impulse);
                opponent.GetComponent<MainOpponent_Controller>().SpawnOpponentDebris();

			} else if (gameObject.name == "PunchTriggerL") {

                opponent.GetComponent<Rigidbody> ().AddForce (Vector3.left * knockback, ForceMode.Impulse);
                opponent.GetComponent<MainOpponent_Controller>().SpawnOpponentDebris();

            }
		}
	}
}
