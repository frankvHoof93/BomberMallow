using System;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public int time;
	public int range;
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
		GameObject explosionBase = Instantiate(Resources.Load<GameObject>("Explosion"), new Vector3(pos.x, pos.y + 2, pos.z), Quaternion.identity);
		explosionList.Add(explosionBase);


		List<RaycastHit> rayHits = new List<RaycastHit>();		
		RaycastHit hit;
		if (Physics.Raycast(new Ray(pos, new Vector3(1, 0, 0)), out hit, range))
		{
			rayHits.Add(hit);
		}
		if (Physics.Raycast(new Ray(pos, new Vector3(-1, 0, 0)), out hit, range))
		{
			rayHits.Add(hit);
		}
		if (Physics.Raycast(new Ray(pos, new Vector3(0, 0, 1)), out hit, range))
		{
			rayHits.Add(hit);
		}
		if (Physics.Raycast(new Ray(pos, new Vector3(0, 0, -1)), out hit, range))
		{
			rayHits.Add(hit);
		}
		
		foreach(Collider c in Physics.OverlapBox(pos, new Vector3(0.1f, 0.1f, 0.1f)))
		{
			GameObject gO = c.gameObject;
			if (gO.tag != "Metal" && gO.tag != "Bomb")
			{
				Destroy(gO);
			}
		}

		foreach (RaycastHit rayHit in rayHits)
		{
			GameObject gO = rayHit.transform.gameObject;
			if (gO.tag != "Metal" && gO.tag != "Bomb")
			{
				Destroy(gO);
			}
		}
	}
}
