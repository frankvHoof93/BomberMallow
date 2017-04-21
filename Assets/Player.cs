using UnityEngine;

public class Player : MonoBehaviour {

    private Vector2 direction = Vector2.zero;
    private bool bomb = false;
    private int counter = 0;

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void SetBomb()
    {
        this.bomb = true;
    }

	private void DoMove ()
	{
		Vector3 pos = transform.position;
        Vector3 newPos = pos;
		if (direction.y > 0 && (Mathf.Abs(direction.y) >= Mathf.Abs(direction.x)))
			newPos = new Vector3(pos.x, pos.y, pos.z + 1);
		else if (direction.y < 0 && (Mathf.Abs(direction.y) >= Mathf.Abs(direction.x)))
			newPos = new Vector3(pos.x, pos.y, pos.z - 1);
		else if (direction.x < 0 && (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y)))
			newPos = new Vector3(pos.x - 1, pos.y, pos.z);
		else if (direction.x > 0 && (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y)))
			newPos = new Vector3(pos.x + 1, pos.y, pos.z);
        bool check = CheckPosition(newPos);
        Debug.Log("Can Move: " + check);
        if (check)
            transform.position = newPos;
	}

    private bool CheckPosition(Vector3 position)
    {
        position.y -= .5f;
        RaycastHit[] hits = Physics.SphereCastAll(position, .3f, Vector3.down);
        if (hits.Length == 0)
            return false;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Metal" || hit.collider.tag == "toHit")
                return false;
        }
        return true;
    }

	private void DropBomb ()
	{
		Vector3 pos = this.GetComponent<Renderer>().transform.position;
		GameObject bomb = Instantiate(Resources.Load<GameObject>("Bomb"), pos, Quaternion.identity);
		bomb.name = "Bomb";
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            this.direction.x = -1;
        if (Input.GetKeyDown(KeyCode.F2))
            this.direction = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.F3))
            bomb = true;
    }

    private void FixedUpdate()
    {
        counter--;
        if (this.direction != Vector2.zero && counter < 0)
        {
            DoMove();
            counter = 30;
        }

        if (this.bomb)
        {
            DropBomb();
            bomb = false;
        }
    }
}
