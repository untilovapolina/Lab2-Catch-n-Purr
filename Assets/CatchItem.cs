using UnityEngine;

public class CatchItem : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 offset;

    public float fallSpeed = 2f;

    public bool wasCaught = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(0, -fallSpeed);
    }

    void OnMouseDown()
    {
        wasCaught = true;

        rb.linearVelocity = Vector2.zero;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        newPos.z = 0f;
        transform.position = newPos;
    }

    void OnMouseUp()
    {
        rb.linearVelocity = new Vector2(0, -fallSpeed);
    }
}