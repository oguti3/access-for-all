using UnityEngine;

/// <summary>
/// Attach this script to any car GameObject.
/// Call the public helper methods to move the car programmatically.
/// All movement is relative to the car's own local axes.
/// </summary>
public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Units per second the car moves forward/backward")]
    public float moveSpeed = 5f;

    [Tooltip("Units per second the car strafes left/right")]
    public float strafeSpeed = 4f;

    [Tooltip("Degrees per second the car rotates when turning")]
    public float turnSpeed = 90f;

    [Tooltip("If true, movement uses physics (Rigidbody). If false, uses Transform.")]
    public bool usePhysics = false;

    private Rigidbody rb;

    // ─── Internal velocity accumulator (physics mode) ────────────────────────
    private Vector3 _frameVelocity = Vector3.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (usePhysics && rb == null)
        {
            Debug.LogWarning($"[CarController] '{name}': usePhysics is true but no Rigidbody found. " +
                             "Add a Rigidbody or set usePhysics to false.");
        }
    }

    void FixedUpdate()
    {
        // Flush accumulated physics velocity each physics step
        if (usePhysics && rb != null)
        {
            rb.MovePosition(rb.position + _frameVelocity * Time.fixedDeltaTime);
            _frameVelocity = Vector3.zero;
        }
    }

    // =========================================================================
    //  PUBLIC HELPER FUNCTIONS
    // =========================================================================

    /// <summary>Move the car forward by <paramref name="units"/> world units.</summary>
    public void MoveForward(float units)
    {
        Translate(transform.forward * units);
    }

    /// <summary>Move the car backward by <paramref name="units"/> world units.</summary>
    public void MoveBackward(float units)
    {
        Translate(-transform.forward * units);
    }

    /// <summary>Strafe the car to the right by <paramref name="units"/> world units.</summary>
    public void MoveRight(float units)
    {
        Translate(transform.right * units);
    }

    /// <summary>Strafe the car to the left by <paramref name="units"/> world units.</summary>
    public void MoveLeft(float units)
    {
        Translate(-transform.right * units);
    }

    /// <summary>
    /// Drive forward continuously. Call each frame (e.g. inside Update).
    /// Uses <see cref="moveSpeed"/> and <see cref="Time.deltaTime"/> automatically.
    /// </summary>
    public void DriveForward()
    {
        Translate(transform.forward * moveSpeed * Time.deltaTime);
    }

    /// <summary>Drive backward continuously. Call each frame.</summary>
    public void DriveBackward()
    {
        Translate(-transform.forward * moveSpeed * Time.deltaTime);
    }

    /// <summary>Strafe right continuously. Call each frame.</summary>
    public void DriveRight()
    {
        Translate(transform.right * strafeSpeed * Time.deltaTime);
    }

    /// <summary>Strafe left continuously. Call each frame.</summary>
    public void DriveLeft()
    {
        Translate(-transform.right * strafeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Rotate (yaw) the car to the right by <paramref name="degrees"/> degrees.
    /// </summary>
    public void TurnRight(float degrees)
    {
        transform.Rotate(Vector3.up, degrees, Space.World);
    }

    /// <summary>Rotate (yaw) the car to the left by <paramref name="degrees"/> degrees.</summary>
    public void TurnLeft(float degrees)
    {
        transform.Rotate(Vector3.up, -degrees, Space.World);
    }

    /// <summary>
    /// Turn right continuously. Call each frame.
    /// Uses <see cref="turnSpeed"/> and <see cref="Time.deltaTime"/> automatically.
    /// </summary>
    public void TurnRightContinuous()
    {
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime, Space.World);
    }

    /// <summary>Turn left continuously. Call each frame.</summary>
    public void TurnLeftContinuous()
    {
        transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Move the car along an arbitrary world-space <paramref name="direction"/> vector
    /// by a given <paramref name="units"/> distance. Useful for diagonal movement.
    /// </summary>
    public void MoveInDirection(Vector3 direction, float units)
    {
        Translate(direction.normalized * units);
    }

    /// <summary>
    /// Instantly teleport the car to <paramref name="worldPosition"/>.
    /// </summary>
    public void TeleportTo(Vector3 worldPosition)
    {
        if (usePhysics && rb != null)
            rb.MovePosition(worldPosition);
        else
            transform.position = worldPosition;
    }

    /// <summary>
    /// Instantly set the car's yaw rotation (Y axis, world space).
    /// </summary>
    public void SetRotationY(float degrees)
    {
        Vector3 euler = transform.eulerAngles;
        euler.y = degrees;
        transform.eulerAngles = euler;
    }

    // =========================================================================
    //  PRIVATE HELPERS
    // =========================================================================

    private void Translate(Vector3 delta)
    {
        if (usePhysics && rb != null)
        {
            // Accumulate; applied in FixedUpdate to keep physics happy
            _frameVelocity += delta / Time.fixedDeltaTime;
        }
        else
        {
            transform.position += delta;
        }
    }
}