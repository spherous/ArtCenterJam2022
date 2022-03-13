using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] public MovementController movementController;
    [SerializeField] public LightRotationController lrc;
    [SerializeField] public Transform lightRotator;
    public FootstepAudio footstepAudio;
    public SpriteRenderer spriteRenderer;
    private float rotation;
    private float horizontalInput;
    private float verticalInput;
    private float timeCounter;
    public float animationFPS;
    private int frame;
    private int frameCount;
    private bool waiting;
    private bool isAnimating;
    private bool started;
    public Sprite[] nSpriteArray;
    public Sprite[] sSpriteArray;
    public Sprite[] eSpriteArray;
    public Sprite[] wSpriteArray;
    public Sprite[] neSpriteArray;
    public Sprite[] seSpriteArray;
    public Sprite[] swSpriteArray;
    public Sprite[] nwSpriteArray;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        frameCount = 4;
        frame = 0;
        timeCounter = Time.timeSinceLevelLoad + animationFPS;
        waiting = false;
        isAnimating = false;
        started = false;
    }
    private void Update()
    {
        if(gameManager != null && gameManager.gameOver)
            return;
            
        if (horizontalInput > -0.5 && horizontalInput < 0.5 && verticalInput > -0.5 && verticalInput < 0.5)
        {
            frame = 0;
            isAnimating = false;
            lrc.moveDir = 0;
            if (rotation < 22.6 && rotation > -22.5) { spriteRenderer.sprite = sSpriteArray[0]; }
            else if (rotation < -22.6 && rotation > -67.5) { spriteRenderer.sprite = swSpriteArray[0]; }
            else if (rotation < -67.6 && rotation > -112.5) { spriteRenderer.sprite = wSpriteArray[0]; }
            else if (rotation < -112.6 && rotation > -157.5) { spriteRenderer.sprite = nwSpriteArray[0]; }
            else if (rotation < 67.5 && rotation > 22.6) { spriteRenderer.sprite = seSpriteArray[0]; }
            else if (rotation < 112.5 && rotation > 67.6) { spriteRenderer.sprite = eSpriteArray[0]; }
            else if (rotation < 157.5 && rotation > 112.6) { spriteRenderer.sprite = neSpriteArray[0]; }
            else { spriteRenderer.sprite = nSpriteArray[0]; }
            started = false;
        }
        else
        {
            isAnimating = true;
            if (!started)
            {
                updateSprite();
                started = true;
            }
            
        }

        rotation = lightRotator.rotation.eulerAngles.z - 180;
        horizontalInput = movementController.horizontalInput;
        verticalInput = movementController.verticalInput;

        if (isAnimating)
        {

            if (waiting)
            {

                if (Time.timeSinceLevelLoad >= timeCounter)
                {
                    waiting = false;
                    if (frame >= frameCount)
                    {
                        frame = 1;
                        started = false;
                    }
                    else
                    {
                        waiting = true;
                        frame++;
                        updateSprite();
                    }
                    if (frame == 2 || frame == 4)
                    {
                        footstepAudio.GroundType();
                    }


                }
            }
        }
        else
        {
            frame = 0;
            waiting = false;
        }
    }
    void updateSprite()
    {

        if (verticalInput > 0 && horizontalInput > -0.5 && horizontalInput < 0.5)
        {
            spriteRenderer.sprite = nSpriteArray[frame];
            lrc.moveDir = 1;
        }
        else if (verticalInput < 0 && horizontalInput > -0.5 && horizontalInput < 0.5)
        {
            spriteRenderer.sprite = sSpriteArray[frame];
            lrc.moveDir = 5;
        }
        else if (horizontalInput > 0 && verticalInput > -0.5 && verticalInput < 0.5)
        {
            spriteRenderer.sprite = eSpriteArray[frame];
            lrc.moveDir = 3;
        }
        else if (horizontalInput < 0 && verticalInput > -0.5 && verticalInput < 0.5)
        {
            spriteRenderer.sprite = wSpriteArray[frame];
            lrc.moveDir = 7;
        }
        else if (horizontalInput > 0 && verticalInput > 0)
        {
            spriteRenderer.sprite = neSpriteArray[frame];
            lrc.moveDir = 2;
        }
        else if (horizontalInput > 0 && verticalInput < 0)
        {
            spriteRenderer.sprite = seSpriteArray[frame];
            lrc.moveDir = 4;
        }
        else if (horizontalInput < 0 && verticalInput > 0)
        {
            spriteRenderer.sprite = nwSpriteArray[frame];
            lrc.moveDir = 8;
        }
        else if (horizontalInput < 0 && verticalInput < 0)
        {
            spriteRenderer.sprite = swSpriteArray[frame];
            lrc.moveDir = 6;
        }
        timeCounter = Time.timeSinceLevelLoad + animationFPS;
        waiting = true;
    }

}
