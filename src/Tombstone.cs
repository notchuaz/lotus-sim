using UnityEngine;

public class Tombstone : MonoBehaviour
{
	private Rigidbody2D rb;

	private Animator animator;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Ground"))
		{
			animator.SetBool("IsGrounded", value: true);
		}
	}
}
