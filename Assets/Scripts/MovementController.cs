using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
public class MovementController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private GameManager gameManager;
    public float maxSpeed;
    private float speedMax;
    public float horizontalInput;
    public float verticalInput;
    public float timeToMaxSpeed;
    private float velocityX;
    private float velocityY;
    public float dashCooldown;
    private float dashTime;
    public float dashRate;
    private float dashCooldownTimer;
    private bool dashed;
    public float dashSpeed;
    public bool backpedaling;
    private void Awake()
    {
        backpedaling = false;
        speedMax = maxSpeed;
        dashCooldownTimer = Time.timeSinceLevelLoad + dashCooldown;
        dashTime = Time.timeSinceLevelLoad + dashRate;

    }
    private void Update()
    {

        if(gameManager.gameOver)
            return;

        if (dashed)
        {
            if (Time.timeSinceLevelLoad >= dashTime){ maxSpeed = speedMax;}
            if (Time.timeSinceLevelLoad >= dashCooldownTimer) { dashed = false; }
        }
        float acceleration = maxSpeed / timeToMaxSpeed;

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
        if(gameManager.gameOver)
            return;
        rb.velocity = new Vector2(velocityX,velocityY);
    }
    public void MoveHorizontal(CallbackContext context) 
    {
        if(gameManager.gameOver)
            return;
        horizontalInput = context.ReadValue<float>();
    }
    public void MoveVertical(CallbackContext context)
    {
        if(gameManager.gameOver)
            return;
        verticalInput = context.ReadValue<float>();
    }
    public void Dash(CallbackContext context)
    {
        if(gameManager.gameOver)
            return;
        if (context.started)
        {
            if(dashed) { return; }
            speedMax = maxSpeed;
            maxSpeed = maxSpeed * dashSpeed;
            rb.AddForce(rb.velocity.normalized * dashSpeed, ForceMode2D.Impulse);
            dashed = true;
            dashCooldownTimer = Time.timeSinceLevelLoad + dashCooldown;
            dashTime = Time.timeSinceLevelLoad + dashRate;
        }
    }
    
    public void BackPedal(CallbackContext context)
    {
        if (gameManager.gameOver)
            return;
        if (context.started)
        {
            maxSpeed = maxSpeed / 2;
            backpedaling = true;
        }
        if (context.canceled)
        {
            maxSpeed = speedMax;
            backpedaling = false;
            return;
        }
    }
    public void SetVelocityToZero() => rb.velocity = Vector2.zero;

}