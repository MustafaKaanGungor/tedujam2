using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;}
    private Rigidbody2D playerRb;

    float rotationAngle = 0f;
    [SerializeField] private float speedFactor = 30f;
    [SerializeField] private float turnFactor = 3.5f;
    [SerializeField] private float driftFactor = 3f;
    [SerializeField] private float updatedTurnFactor;
    [SerializeField] private float turnModifier = 1;

    private void Awake() {
        Instance = this;

        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        ForwardMovement();
        TurningMovement();
        KillOrthogonalVelocity();
    }

    private void ForwardMovement() {

        if (GameInput.Instance.GetMovementVector().x == 0)
        {
            playerRb.linearDamping = Mathf.Lerp(playerRb.linearDamping, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            playerRb.linearDamping = 0;
        }

        Vector2 swimmingVector = transform.right * GameInput.Instance.GetMovementVector().y * speedFactor;
    
        playerRb.AddForce(swimmingVector * Time.deltaTime, ForceMode2D.Force);
        

        //playerRb.MovePosition(transform.position + transform.right * Time.deltaTime * GameInput.Instance.GetMovementVector().y * speedFactor);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(playerRb.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right*Vector2.Dot(playerRb.linearVelocity, transform.right);

        playerRb.linearVelocity = forwardVelocity + rightVelocity * driftFactor;
    }

    private void TurningMovement() {
        /*
        rotationAngle -= GameInput.Instance.GetMovementVector().x * turnFactor;

        playerRb.MoveRotation(rotationAngle);
        */
        float speed = Vector2.Dot(playerRb.linearVelocity, transform.right);
        Debug.Log(playerRb.linearVelocity.magnitude);
        updatedTurnFactor = turnFactor / playerRb.linearVelocity.magnitude;
        updatedTurnFactor = Mathf.Clamp(updatedTurnFactor, 250, 300);
        
        transform.Rotate(0, 0, GameInput.Instance.GetMovementVector().x * updatedTurnFactor * Time.deltaTime * turnModifier);
    }
}
