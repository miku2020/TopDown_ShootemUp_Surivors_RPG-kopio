using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_movement : MonoBehaviour
{
    [SerializeField]
    private float movespeed;
    [SerializeField]
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    [SerializeField]
    private GameObject WieldedWeapon;

    [SerializeField]
    private SpriteRenderer WeaponspriteRenderer;
    [SerializeField]
    private SpriteRenderer PlayerSprite;
    private int originalSortingOrder;
    private int currentSortingOrder;
    private int originalPlayerSortingOrder;

    private float WeaponAngle;

    [SerializeField]
    private List<Sprite>sprites;

    [SerializeField]
    private CircleCollider2D PlayerCol;
    private bool LvLChange = false;

    private void Start()
    {
        originalPlayerSortingOrder = PlayerSprite.sortingOrder;
        originalSortingOrder = WeaponspriteRenderer.sortingOrder;
        currentSortingOrder = originalSortingOrder;
    }

    void Update()
    {
        PlayerInputs();
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        WieldedWeapon.transform.rotation = rotation;

        WeaponAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log(WeaponAngle);
        // currently Player is on order 2 and Sword is on 2, if bug later appears where sword is constantly above player change sword on layer 1 and change order change from 1 to 2
        if (WeaponAngle <= 0f)
        {
            // Set a higher sorting order to bring the object to the front
            WeaponspriteRenderer.sortingOrder = currentSortingOrder + 2;
        }
        else
        {
            // Return to the original sorting order
            WeaponspriteRenderer.sortingOrder = currentSortingOrder;
        }

        //Player rotation
        if (WeaponAngle >= -45f && WeaponAngle <= 45f)
        {
            PlayerSprite.sprite = sprites[0];
        }
        if (WeaponAngle >= 45f && WeaponAngle <= 135f)
        {
            PlayerSprite.sprite = sprites[1];
        }
        if (WeaponAngle >= 135f || WeaponAngle <= -135f)
        {
            PlayerSprite.sprite = sprites[2];
        }
        if (WeaponAngle >= -135f && WeaponAngle <= -45f)
        {
            PlayerSprite.sprite = sprites[3];
        }
       

    }

    void FixedUpdate()
    {
        
        PlayerMove();
        
    }

    void PlayerInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void PlayerMove()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * movespeed, moveDirection.y * movespeed);
    }

    private void OnTriggerEnter2D(Collider2D lvltrigger)
    {
        if (lvltrigger.gameObject.tag == "EnableHeightChange")
        {
            LvLChange = true;
            Debug.Log("bool");
        }
        if (lvltrigger.gameObject.tag == "GroundLvL1" && LvLChange == true)
        {
            PlayerCol.excludeLayers |= 1 << LayerMask.NameToLayer("SecondLevel");
            PlayerCol.excludeLayers &= ~(1 << LayerMask.NameToLayer("FirstLevel"));
            LvLChange = false;
            Debug.Log("lvl1");

            currentSortingOrder = originalSortingOrder;
            PlayerSprite.sortingOrder = originalPlayerSortingOrder;

        }
        if (lvltrigger.gameObject.tag == "GroundLvL2" && LvLChange == true)
        {
            PlayerCol.excludeLayers |= 1 << LayerMask.NameToLayer("FirstLevel");
            PlayerCol.excludeLayers &= ~(1 << LayerMask.NameToLayer("SecondLevel"));
            LvLChange = false;
            Debug.Log("lvl2");
            PlayerSprite.sortingOrder = 9;
            currentSortingOrder = 8;
        }
    }

}
