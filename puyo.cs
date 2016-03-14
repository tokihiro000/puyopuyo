using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class puyo : MonoBehaviour {
	public GameObject[] puyopuyo;
	GameObject[] puyoList;
	int[,] puyoIndex;

	// Use this for initialization
	void Awake () {
		puyoList = new GameObject[2];
		puyoIndex = new int[2, 2];

		int p1 = Random.Range (0, 4);
		int p2 = Random.Range (0, 4);

		puyoList[0] = (GameObject) Instantiate (puyopuyo[p1], this.transform.position, Quaternion.identity);
		puyoList[1] = (GameObject) Instantiate (puyopuyo[p2], this.transform.position, Quaternion.identity);
		puyoList [0].SetActive (false);
		puyoList [1].SetActive (false);
	}

	public void setDefaultPos(ref List<float> pointXList, ref List<float> pointYList, int indexX, int indexY) {
		puyoList [0].transform.position = new Vector3 (pointXList[indexX], pointYList[indexY]);
		puyoList [1].transform.position = new Vector3 (pointXList[indexX], pointYList[indexY - 1]);
		puyoIndex [0, 0] = indexX; puyoIndex [0, 1] = indexY;
		puyoIndex [1, 0] = indexX; puyoIndex [1, 1] = indexY - 1;
		puyoList [0].SetActive (true);
		puyoList [1].SetActive (true);
	}

	public bool moveDown (ref List<float> pointXList, ref List<float> pointYList, ref int [,] map) {
		if (!isPointEnable(puyoIndex[0, 0], puyoIndex[0, 1] - 1, ref map) || !isPointEnable(puyoIndex[1, 0], puyoIndex[1, 1] - 1, ref map)) {
			map[puyoIndex[0, 0], puyoIndex[0, 1]] = 1;
			map[puyoIndex[1, 0], puyoIndex[1, 1]] = 1;
			return false;
		}

		puyoIndex [0, 1] = puyoIndex[0, 1] - 1;
		puyoIndex [1, 1] = puyoIndex[1, 1] - 1;
		puyoList [0].transform.position = new Vector3 (pointXList[puyoIndex [0, 0]], pointYList[puyoIndex [0, 1]]);
		puyoList [1].transform.position = new Vector3 (pointXList[puyoIndex [1, 0]], pointYList[puyoIndex [1, 1]]);

		return true;
	}

	public void moveLeft (ref List<float> pointXList, ref List<float> pointYList, ref int [,] map) {
		if (isPointEnable(puyoIndex[0, 0] - 1, puyoIndex[0, 1], ref map) && isPointEnable(puyoIndex[1, 0] - 1, puyoIndex[1, 1], ref map)) {
			puyoIndex [0, 0] = puyoIndex[0, 0] - 1;
			puyoIndex [1, 0] = puyoIndex[1, 0] - 1;
			puyoList [0].transform.position = new Vector3 (pointXList[puyoIndex [0, 0]], pointYList[puyoIndex [0, 1]]);
			puyoList [1].transform.position = new Vector3 (pointXList[puyoIndex [1, 0]], pointYList[puyoIndex [1, 1]]);
		}
	}

	public void moveRight (ref List<float> pointXList, ref List<float> pointYList, ref int [,] map) {
		if (isPointEnable(puyoIndex[0, 0] + 1, puyoIndex[0, 1], ref map) && isPointEnable(puyoIndex[1, 0] + 1, puyoIndex[1, 1], ref map)) {
			puyoIndex [0, 0] = puyoIndex[0, 0] + 1;
			puyoIndex [1, 0] = puyoIndex[1, 0] + 1;
			puyoList [0].transform.position = new Vector3 (pointXList[puyoIndex [0, 0]], pointYList[puyoIndex [0, 1]]);
			puyoList [1].transform.position = new Vector3 (pointXList[puyoIndex [1, 0]], pointYList[puyoIndex [1, 1]]);
		}
	}

//	public void rotate_r () {
//		if (isPointEnable(puyoIndex[0, 0] + 1, puyoIndex[0, 1], ref map) && isPointEnable(puyoIndex[1, 0] + 1, puyoIndex[1, 1], ref map)) {
//			puyoIndex [0, 0] = puyoIndex[0, 0] + 1;
//			puyoIndex [1, 0] = puyoIndex[1, 0] + 1;
//			puyoList [0].transform.position = new Vector3 (pointXList[puyoIndex [0, 0]], pointYList[puyoIndex [0, 1]]);
//			puyoList [1].transform.position = new Vector3 (pointXList[puyoIndex [1, 0]], pointYList[puyoIndex [1, 1]]);
//		}
//	}

	private bool isPointEnable(int indexX, int indexY, ref int[,] map) {
		if (map [indexX, indexY] != 0) {
			return false;
		}
		return true;
	}
}
