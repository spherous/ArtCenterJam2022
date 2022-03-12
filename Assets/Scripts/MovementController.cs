using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
public class MovementController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public float maxSpeed;
    public float horizontalInput;
    public float verticalInput;
    public float accelerationRate;
    private float acceleration; 
    private float velocityX;
    private float velocityY;
    private void Awake()
    {
        acceleration = maxSpeed / accelerationRate;
    }
    private void Update()
    {
        if (horizontalInput > 0)
        {
            velocityX += acceleration * Time.deltaTime;
        }
        else if (horizontalInput < 0)
        {
            velocityX += -acceleration * Time.deltaTime;
        }
        if (verticalInput > 0)
        {
            velocityY += acceleration * Time.deltaTime;
        }
        else if (verticalInput < 0)
        {
            velocityY += -acceleration * Time.deltaTime;
        }
        if (horizontalInput < 0.5 && horizontalInput > -0.5)
        {
            if (velocityX > 0)
            {
                velocityX -= Mathf.Clamp(acceleration * Time.deltaTime, 0, velocityX);
            }
            else
            {
                velocityX -= Mathf.Clamp(-acceleration * Time.deltaTime, velocityX, 0);
            }
        }
        if (verticalInput < 0.5 && verticalInput > -0.5)
        {
            if (velocityY > 0)
            {
                velocityY -= Mathf.Clamp(acceleration * Time.deltaTime, 0, velocityY);
            }
            else
            {
                velocityY -= Mathf.Clamp(-acceleration * Time.deltaTime, velocityY, 0);
            }

        }
        velocityY = Mathf.Clamp(velocityY, -maxSpeed, maxSpeed);
        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(velocityX,velocityY);
    }
    public void MoveHorizontal(CallbackContext context) 
    {
        horizontalInput = context.ReadValue<float>();
    }
    public void MoveVertical(CallbackContext context)
    {
        verticalInput = context.ReadValue<float>();
    }
}