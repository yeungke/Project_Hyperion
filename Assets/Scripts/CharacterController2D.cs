using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 8f;                            // Amount of force added when the player jumps.
	[Range(0, 1)][SerializeField] private float m_CrouchSpeed = .4f;            // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = true;                          // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WalkableSurfaces;                      // A mask determining the ground relative to the character
	[SerializeField] private LayerMask m_CeilingLayer;                          // A mask determining a ceiling relative to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	
	// Slope Components
	public CircleCollider2D m_SlopeCollider;                                    // A collider that will disable gravity while on slopes
	private Vector2 slopeColliderSize;
	private Vector2 slopeNormalPerp;
	private float slopeDownAngle;
	private float slopeDownAngleOld;
	private float slopeSideAngle;
	private bool isOnSlope;
	[SerializeField] private float slopeCheckDistance;

	// Slope friction components
	[SerializeField] PhysicsMaterial2D noFriction;
	[SerializeField] PhysicsMaterial2D fullFriction;

	public bool m_Grounded;             // Whether or not the player is grounded.
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	const float k_CeilingRadius = .2f;  // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;  // The player's Rigidbody
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Jump Mechanic Variables")]
	[Space(5)]
	private float jumpTimeCounter;
	public bool isJumping;
	[SerializeField] private float jumpTime = 0.25f;
	[SerializeField] private bool secondJump = true;

	// Charge Jump
	[SerializeField] private float chargeJumpForce;
	[SerializeField] private float chargeJumpSpeed;
	[SerializeField] private float chargeJumpMax;
	[SerializeField] private float chargeJumpTime;
	private bool hasChargeJumped;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WalkableSurfaces);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump, bool jumpHold, float xInput)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_CeilingLayer))
			{
				crouch = true;
			}
		}

		// Only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// If the player is crouching...
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// While crouching with the Charge Jump upgrade, add to the jump timer if it is less than the cap
				if (chargeJumpTime < chargeJumpMax && UpgradeManager.instance.IsEnabled(Upgrades.CHARGEJUMP))
				{
					// Charge Jump time is calculated by Delta Time and a speed multiplier
					chargeJumpTime += Time.deltaTime * chargeJumpSpeed;
				}

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				chargeJumpTime = 0;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			/*
			if (m_Grounded && !isOnSlope && !isJumping) // If the player is on flat ground
            {
				Vector3 targetVelocity = new Vector2(move * 10f * slopeNormalPerp.x * -xInput, 0.0f);
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}
			if (m_Grounded && isOnSlope && !isJumping) // If the player is on a slope
			{
				Vector3 targetVelocity = new Vector2(move * 10f * slopeNormalPerp.x * -xInput, move * slopeNormalPerp.y * -xInput);
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}
			else if (!m_Grounded) // If the player is in the air
			{
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}
			*/

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Set the bool to indicate that the player is jumping, and set the jump time counter.
			isJumping = true;
			jumpTimeCounter = jumpTime;

			// If the player is already crouching and they have a fully charged jump...
			if (chargeJumpTime >= chargeJumpMax)
			{
				// Perform a charge jump; reset the charging jump bool and timer
				m_Rigidbody2D.velocity = Vector2.up * chargeJumpForce;
				m_Grounded = false;
				chargeJumpTime = 0;
				hasChargeJumped = true;
			}
			else
			{
				// Perform a normal jump; change the upward velocity of the player.
				m_Grounded = false;
				m_Rigidbody2D.velocity = Vector2.up * m_JumpForce;
				chargeJumpTime = 0;
			}
		}

		// If the player jumps while they are in the air, after they are no longer going any higher...
		if (!m_Grounded && secondJump && jump && m_Rigidbody2D.velocity.y <= 0
			&& UpgradeManager.instance.IsEnabled(Upgrades.DOUBLEJUMP))
        {
			// Indicate that the player is jumping, and set the jump time counter again.
			isJumping = true;
			jumpTimeCounter = jumpTime;
			// Change the upward velocity of the player.
			m_Rigidbody2D.velocity = Vector2.up * m_JumpForce;
			secondJump = false;
        }

		// Reset the second jump and charge jump after you land on the ground
		if (m_Grounded)
        {
			secondJump = true;
			hasChargeJumped = false;
        }

		// When the timer is started, run the timer until it hits 0
		if (jumpTimeCounter > 0)
			jumpTimeCounter -= Time.deltaTime;
		else
			jumpTimeCounter = 0;

		// If the player has not performed a charge jump...
		if (!hasChargeJumped)
		{
			// If the player holds the jump key while they are in the air...
			if (isJumping && jumpHold)
			{
				// Maintain the player's jump velocity so long as the counter is > 0
				if (jumpTimeCounter > 0)
					m_Rigidbody2D.velocity = Vector2.up * m_JumpForce;
				// When the timer runs out, end the jump velocity
				else
					isJumping = false;
			}
			// If the player lets go of the jump button, immediately stop the jump velocity
			else if (isJumping && !jumpHold)
				isJumping = false;
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Rotate the player on the Y-axis, allowing the player use weapons in the other direction
		transform.Rotate(0f, 180f, 0f);
	}

	public void SlopeCheck(float moveInput)
	{
		Vector2 checkPosition = transform.position - new Vector3(0, slopeColliderSize.y / 2);
		SlopeCheckHorizontal(checkPosition);
		SlopeCheckVertical(checkPosition, moveInput);
	}

	private void SlopeCheckHorizontal(Vector2 checkPosition)
	{
		RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPosition, transform.right, slopeCheckDistance, m_WalkableSurfaces);
		RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPosition, -transform.right, slopeCheckDistance, m_WalkableSurfaces);

		if (slopeHitFront)
        {
			isOnSlope = true;
			slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
		else if (slopeHitBack)
        {
			isOnSlope = true;
			slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
		else
        {
			slopeSideAngle = 0.0f;
			isOnSlope = false;
        }
	}

	private void SlopeCheckVertical(Vector2 checkPosition, float moveInput)
	{
		RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, slopeCheckDistance, m_WalkableSurfaces);

		if (hit)
		{
			// Retrieve the angle of the ground/slope relative to the player
			slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;	// Store angle parallel to ground
			slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);			// Store angle 90 deg to ground

			// Detect if the player is on a slope based on changes in the slope angle
			if (slopeDownAngle != slopeDownAngleOld)
            {
				isOnSlope = true;
            }
			slopeDownAngleOld = slopeDownAngle;

			Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);		// Ray cast parallel to ground
			Debug.DrawRay(hit.point, hit.normal, Color.green);			// Ray cast 90 deg to ground
		}

		if (isOnSlope && moveInput == 0.0f)
        {
			m_Rigidbody2D.sharedMaterial = fullFriction;
        }
		else
        {
			m_Rigidbody2D.sharedMaterial = noFriction;
        }
	}

	private void Start()
    {
		secondJump = true;
    }
}