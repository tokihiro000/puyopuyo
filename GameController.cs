using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {
	public int horizon, vertical, createPosX, createPosY;
	public GameObject puyo;
	GameObject activePuyo;
	puyo activePuyoScript;
	GameObject[,] objMap;
	int[,] map;
	List<float> pointXList, pointYList;
	Coroutine co;

	enum State {
		Ready,
		Play,
		GameOver
	}

	State state;


	void Start () {
		state = State.Ready;
	}

	void GameReady () {
		objMap = new GameObject[horizon, vertical];
		map = new int[horizon + 2, vertical + 2];
		pointXList = new List<float> ();
		pointYList = new List<float> ();

		for (int i = 0; i < horizon + 2; i++) {
			for (int j = 0; j < vertical + 2; j++) {
				if (i == 0 || i == horizon + 1 || j == 0) {
					map [i, j] = 10;
				} else {
					map [i, j] = 0;
				}
			}
		}

		for (int i = 0; i < horizon + 2; i++) {
			pointXList.Add (-2.5f + i * 0.7f);
		}

		for (int i = 0; i < vertical + 2; i++) {
			pointYList.Add (-4.2f + i * 0.45f);
		}
	}

	void CreatePuyo () {
		activePuyo = (GameObject) Instantiate (puyo, this.transform.position, Quaternion.identity);
		activePuyoScript = activePuyo.GetComponentInChildren<puyo> ();
		activePuyoScript.setDefaultPos (ref pointXList, ref pointYList, createPosX, createPosY);
		if (co != null) {
			StopCoroutine (co);
		}
		co = StartCoroutine ("autoDown");
	}

	private IEnumerator autoDown() {
		while (true) {
			yield return new WaitForSeconds (1.0f);
			if (!activePuyoScript.moveDown (ref pointXList, ref pointYList, ref map)) {
				nextTurn ();
			}
		}
	}

	private void nextTurn() {
		CreatePuyo ();
	}

	void Update () {
		switch(state) {
		case State.Ready:
			if (Input.GetKeyDown ("down")) {
				GameReady ();
				CreatePuyo ();
				state = State.Play;
			}
			break;
		case State.Play:
			if (Input.GetKeyDown ("down")) {
				if (!activePuyoScript.moveDown (ref pointXList, ref pointYList, ref map)) {
					nextTurn ();
				}
			}
			if (Input.GetKeyDown ("left")) {
				activePuyoScript.moveLeft (ref pointXList, ref pointYList, ref map);
			}
			if (Input.GetKeyDown ("right")) {
				activePuyoScript.moveRight (ref pointXList, ref pointYList, ref map);
			}
			break;
		case State.GameOver:
			break;
		default:
			break;
		}
	}
}
