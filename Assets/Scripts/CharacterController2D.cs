using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[SerializeField] private bool m_AirControl = false;							// Verifica se o personagem pode se movimentar enquanto está pulando
    [SerializeField] private Transform m_GroundCheck;                           // Verifica se o personagem está no chão
    [SerializeField] private LayerMask m_WhatIsGround;                          // Verifica o que é chão para o personagem
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;


	const float k_GroundedRadius = .05f; // Raio do círculo para determinar o que é chão
	private bool m_Grounded; // Booleano que verifica se o jogador está no chão
	private bool is_facingRight = true; // Verifica se o jogador está olhando para a direita
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D rb;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

    private void FixedUpdate()
    {
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (wasGrounded)
					OnLandEvent.Invoke();
			}
        }
    }

	public void Move(float move, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
			// And then smoothing it out and applying it to the character
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !is_facingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && is_facingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			rb.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		is_facingRight = !is_facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
