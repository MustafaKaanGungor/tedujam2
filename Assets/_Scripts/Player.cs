using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;}
    private Rigidbody2D playerRb;

    float rotationAngle = 0f;
    [SerializeField] private float speedFactor = 30f;
    [SerializeField] private float turnFactor = 3.5f;

    private void Awake() {
        Instance = this;

        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        ForwardMovement();
        TurningMovement();
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

        Vector2 swimmingVector = transform.up * GameInput.Instance.GetMovementVector().y * speedFactor;
    
        playerRb.AddForce(swimmingVector * Time.deltaTime, ForceMode2D.Force);
    }

    private void TurningMovement() {
        /*
        rotationAngle -= GameInput.Instance.GetMovementVector().x * turnFactor;

        playerRb.MoveRotation(rotationAngle);
        */

        transform.Rotate(0, 0, GameInput.Instance.GetMovementVector().x * turnFactor * Time.deltaTime);
    }
}
