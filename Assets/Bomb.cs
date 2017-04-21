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
		GameObject explosionBase = Instantiate(Resources.Load<GameObject>("ExplosionBase"), new Vector3(pos.x, pos.y + 2, pos.z), Quaternion.identity);
		explosionList.Add(explosionBase);


		List<RaycastHit> rayHits = new List<RaycastHit>();		
		RaycastHit hit;
		if (Physics.Raycast(new Ray(pos, new Vector3(1, 0, 0)), out hit, range))
		{
			float dis = Vector3.Distance(pos, hit.transform.position);
			if (hit.transform.gameObject.tag == "Metal")
			{
				dis--;
			}
			BiemRight(pos, dis);
			rayHits.Add(hit);
		}
		else
		{
			BiemRight(pos, range);
		}
		if (Physics.Raycast(new Ray(pos, new Vector3(-1, 0, 0)), out hit, range))
		{
			float dis = Vector3.Distance(pos, hit.transform.position);
			if (hit.transform.gameObject.tag == "Metal")
			{
				dis--;
			}
			BiemLeft(pos, dis);
			rayHits.Add(hit);
		}
		else
		{
			BiemLeft(pos, range);
		}
		if (Physics.Raycast(new Ray(pos, new Vector3(0, 0, 1)), out hit, range))
		{
			float dis = Vector3.Distance(pos, hit.transform.position);
			if (hit.transform.gameObject.tag == "Metal")
			{
				dis--;
			}
			BiemUp(pos, dis);
			rayHits.Add(hit);
		}
		else
		{
			BiemUp(pos, range);
		}
		if (Physics.Raycast(new Ray(pos, new Vector3(0, 0, -1)), out hit, range))
		{
			float dis = Vector3.Distance(pos, hit.transform.position);
			if (hit.transform.gameObject.tag == "Metal")
			{
				dis--;
			}
			BiemDown(pos, dis);
			rayHits.Add(hit);
		}
		else
		{
			BiemDown(pos, range);
		}

		foreach (Collider c in Physics.OverlapBox(pos, new Vector3(0.1f, 0.1f, 0.1f)))
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
			if (gO.tag == "Bomb")
			{
				if (gO.GetComponent<Bomb>().detonated == false)
				{
					gO.GetComponent<Bomb>().time = 1;
				}
			}
		}
	}

	private void BiemDown(Vector3 pos, float distance)
	{
		for (int i = 1; i <= distance; i++)
		{
			if (i == distance)
			{
				GameObject explosion = Instantiate(Resources.Load<GameObject>("ExplosionEnd"), new Vector3(pos.x, pos.y + 2, pos.z - i), Quaternion.identity);
				explosion.transform.Rotate(new Vector3(0, 90, 0));
				explosionList.Add(explosion);
			}
			else
			{
				GameObject explosion = Instantiate(Resources.Load<GameObject>("ExplosionMid"), new Vector3(pos.x, pos.y + 2, pos.z - i), Quaternion.identity);
				explosion.transform.Rotate(new Vector3(0, 90, 0));
				explosionList.Add(explosion);
			}
		}
	}

	private void BiemUp(Vector3 pos, float distance)
	{
		for (int i = 1; i <= distance; i++)
		{
			if (i == distance)
			{
				GameObject explosion = Instantiate(Resources.Load<GameObject>("ExplosionEnd"), new Vector3(pos.x, pos.y + 2, pos.z + i), Quaternion.identity);
				explosion.transform.Rotate(new Vector3(0, 270, 0));
				explosionList.Add(explosion);
			}
			else
			{
				GameObject explosion = Instantiate(Resources.Load<GameObject>("ExplosionMid"), new Vector3(pos.x, pos.y + 2, pos.z + i), Quaternion.identity);
				explosion.transform.Rotate(new Vector3(0, 270, 0));
				explosionList.Add(explosion);
			}
		}
	}

	private void BiemLeft(Vector3 pos, float distance)
	{
		for (int i = 1; i <= distance; i++)
		{
			if (i == distance)
			{
				GameObject explosion = Instantiate(Resources.Load<GameObject>("ExplosionEnd"), new Vector3(pos.x - i, pos.y + 2, pos.z), Quaternion.identity);
				explosion.transform.Rotate(new Vector3(0, 180, 0));
				explosionList.Add(explosion);
			}
			else
			{
				GameObject explosion = Instantiate(Resources.Load<GameObject>("ExplosionMid"), new Vector3(pos.x - 1, pos.y + 2, pos.z), Quaternion.identity);
				explosion.transform.Rotate(new Vector3(0, 180, 0));
				explosionList.Add(explosion);
			}
		}
	}

	private void BiemRight(Vector3 pos, float distance)
	{
		for (int i = 1; i <= distance; i++)
		{
			if (i == distance)
			{
				GameObject explosion = Instantiate(Resources.Load<GameObject>("ExplosionEnd"), new Vector3(pos.x + i, pos.y + 2, pos.z), Quaternion.identity);
				explosionList.Add(explosion);
			}
			else
			{
				GameObject explosion = Instantiate(Resources.Load<GameObject>("ExplosionMid"), new Vector3(pos.x + 1, pos.y + 2, pos.z), Quaternion.identity);
				explosionList.Add(explosion);
			}
		}
	}
}
