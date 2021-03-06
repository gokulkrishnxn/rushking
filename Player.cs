using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gunArm;

    private Camera thecam;

    public Animator anim;

    public GameObject bulletToFire;

    public Transform FirePoint;

    public float timeBetweenShots;

    private float shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        thecam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

       // transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

        theRB.velocity = moveInput * moveSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = thecam.WorldToScreenPoint(transform.localPosition);

        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        } else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        // rotate gun arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);


        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, FirePoint.position, FirePoint.rotation);
            shotCounter = timeBetweenShots;
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, FirePoint.position, FirePoint.rotation);

                shotCounter = timeBetweenShots;
            }
        }




        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isMoveing", true);
        } else
        {
            anim.SetBool("isMoveing", false);
        }
    }
}
