using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public int time;
	private bool detonated = false;
	private List<GameObject> explosionList = new List<GameObject>();

	void FixedUpdate() {
		if (time <= -60)
		{
			foreach (GameObject explosion in explosionList)
			{
				Destroy(explosion);
			}
			GameObject bomb = this.GetComponent<Bomb>().gameObject;
			Destroy(bomb);
		}
		else if (time == 0)
		{
			this.Explode();
			time--;
		}
		else
		{
			time--;
		}
	}

	void Explode()
	{
		detonated = true;
		Vector3 pos = this.GetComponent<Renderer>().transform.position;
		GameObject explosionBase = Instantiate(Resources.Load<GameObject>("ExplosionBase"), pos, Quaternion.identity);
		explosionList.Add(explosionBase);

		Vector3 direction = new Vector3(1, 0, 0);
		RaycastHit[] rayHits = Physics.RaycastAll(pos, direction, 1);

		foreach (RaycastHit rayHit in rayHits)
		{
			GameObject gO = rayHit.transform.gameObject;
			Destroy(gO);
		}
	}
}
