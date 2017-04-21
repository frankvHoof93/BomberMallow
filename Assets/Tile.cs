using UnityEngine;

public class Tile : MonoBehaviour {

	void OnMouseDown()
	{
		Vector3 pos = this.GetComponent<Renderer>().transform.position;
		pos.y = 0.5f;
		GameObject bomb = Instantiate(Resources.Load<GameObject>("Bomb"), pos, Quaternion.identity);
		bomb.name = "Bomb";
	}
}
