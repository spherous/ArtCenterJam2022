using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField]public MovementController movementController;
    [SerializeField] public Transform lightRotator;
    public SpriteRenderer spriteRenderer;
    private float rotation;
    private float horizontalInput;
    private float verticalInput;
    private float timeCounter;
    public float animationFPS;
    public Sprite[] nSpriteArray;
    public Sprite[] sSpriteArray;
    public Sprite[] eSpriteArray;
    public Sprite[] wSpriteArray;
    public Sprite[] neSpriteArray;
    public Sprite[] seSpriteArray;
    public Sprite[] swSpriteArray;
    public Sprite[] nwSpriteArray;

    private void Update()
    {
        rotation = lightRotator.rotation.eulerAngles.z -180;
        Debug.Log(rotation);
        horizontalInput = movementController.horizontalInput;
        verticalInput = movementController.verticalInput;

        if (verticalInput > 0 && horizontalInput > -0.5 && horizontalInput < 0.5) { spriteRenderer.sprite = nSpriteArray[0];}
        if (verticalInput < 0 && horizontalInput > -0.5 && horizontalInput < 0.5) { spriteRenderer.sprite = sSpriteArray[0];}
        if (horizontalInput > 0 && verticalInput > -0.5 && verticalInput < 0.5) { spriteRenderer.sprite = eSpriteArray[0];}
        if (horizontalInput < 0 && verticalInput > -0.5 && verticalInput < 0.5) { spriteRenderer.sprite = wSpriteArray[0];}
        if (horizontalInput > 0 && verticalInput > 0) { spriteRenderer.sprite = neSpriteArray[0];}
        if (horizontalInput > 0 && verticalInput < 0) { spriteRenderer.sprite = seSpriteArray[0];}
        if (horizontalInput < 0 && verticalInput > 0) { spriteRenderer.sprite = nwSpriteArray[0];}
        if (horizontalInput < 0 && verticalInput < 0) { spriteRenderer.sprite = swSpriteArray[0];}
        if (horizontalInput > -0.5 && horizontalInput < 0.5 && verticalInput > -0.5 && verticalInput < 0.5)
        {
            if (rotation < 22.6 && rotation > -22.5) { spriteRenderer.sprite = sSpriteArray[0]; }
            else if (rotation < -22.6 && rotation > -67.5) { spriteRenderer.sprite = swSpriteArray[0]; }
            else if (rotation < -67.6 && rotation > -112.5) { spriteRenderer.sprite = wSpriteArray[0]; }
            else if (rotation < -112.6 && rotation > -157.5) { spriteRenderer.sprite = nwSpriteArray[0]; }

            else if (rotation < 67.5  && rotation > 22.6) { spriteRenderer.sprite = seSpriteArray[0]; }
            else if (rotation <  112.5 && rotation > 67.6) { spriteRenderer.sprite = eSpriteArray[0]; }
            else if (rotation < 157.5 && rotation > 112.6) { spriteRenderer.sprite = neSpriteArray[0]; }
            else { spriteRenderer.sprite = nSpriteArray[0]; }

        }
    }
}
