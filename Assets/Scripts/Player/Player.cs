using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private GameObject planet;
  [SerializeField] private float speed;
  [SerializeField] private float gravity;
  private Rigidbody rb;
  private Vector3 gravityDirection;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    if(rb == null)
    {
      Debug.LogError("Rigidbody is not attached to the player object.");
    }
  }

  void Update()
  {
    ChangeGravity();
    UpdateMove();
  }

  private void UpdateMove()
  {
    if (Input.GetKey(KeyCode.W))
    {
      rb.AddForce(Vector3.forward * speed);
    }
    if (Input.GetKey(KeyCode.S))
    {
      rb.AddForce(Vector3.back * speed);
    }
    if (Input.GetKey(KeyCode.A))
    {
      rb.AddForce(Vector3.left * speed);
    }
    if (Input.GetKey(KeyCode.D))
    {
      rb.AddForce(Vector3.right * speed);
    }
  }

  private void ChangeGravity()
  {
    gravityDirection = (planet.transform.position - transform.position).normalized;
    rb.AddForce(gravityDirection * gravity);
  }
}