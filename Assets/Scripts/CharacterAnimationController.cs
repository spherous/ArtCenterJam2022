using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField]public MovementController movementController;
    [SerializeField] public Transform lightRotator;
    public SpriteRenderer spriteRenderer;
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
    }
}
