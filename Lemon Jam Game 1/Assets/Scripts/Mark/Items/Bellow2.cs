using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bellow2 : MonoBehaviour
{
    PlayerController2 p2;

    Arm2 arm;

    PolygonCollider2D wind;

    private bool isActive;

    [SerializeField] private float maxAmmo; //ammo

    private float ammoCount;

    private float powerMultiplier; //power

    public Animator myAnimator;

    public GameObject steam;

    private Vector3 kettlepos;

    private AudioSource audio;

    void Start()
    {
        p2 = transform.parent.parent.parent.GetComponent<PlayerController2>();

        arm = transform.parent.parent.GetComponent<Arm2>();

        wind = GetComponent<PolygonCollider2D>();

        ammoCount = maxAmmo;

        powerMultiplier = 1.0f; //power

        myAnimator.GetComponent<Animator>();

        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        kettlepos = transform.position;

        if (p2.controllerAngle < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            //transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;  //this had issues, leaving this in just in case...
        }
        else
        {
            transform.localScale = Vector3.one;
            //transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        }

        if (ammoCount <= 0)
        {
            transform.parent.parent.parent.GetComponent<PlayerItemHandler>().DestroyItem(); //ammo
        }

        if (isActive)
        {
            wind.enabled = true;

            ammoCount -= Time.deltaTime;
        }
        else
        {
            wind.enabled = false;
        }
    }

    public void StartWind()
    {
        isActive = true;
        myAnimator.Play("Kettle", -1, 0f);

        audio.mute = false;

        //maybe instantiate a cloud here and it rides the wind and waits and destroys??
        Instantiate(steam, kettlepos, Quaternion.identity);
        steam.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(Mathf.Deg2Rad * arm.angleCorrected), Mathf.Sin(Mathf.Deg2Rad * arm.angleCorrected)) * 2.5f * powerMultiplier, ForceMode2D.Impulse);
    }

    public void StopWind()
    {
        isActive = false;

        audio.mute = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive && !collision.CompareTag("Player2"))
        {
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(Mathf.Deg2Rad * arm.angleCorrected), Mathf.Sin(Mathf.Deg2Rad * arm.angleCorrected)) * 2.5f * powerMultiplier, ForceMode2D.Impulse);
        }
    }

    public void MultiplyPower(float PowerM_Delta)
    {
        powerMultiplier += PowerM_Delta;

        this.Wait(5.0f, () =>
        {
            powerMultiplier -= PowerM_Delta;
        });
    }
}
