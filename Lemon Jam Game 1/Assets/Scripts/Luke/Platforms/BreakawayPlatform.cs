using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakawayPlatform : MonoBehaviour
{
    public Animator myAnimator;

    private AudioSource Break;

    private void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        Break = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player1") || (collision.gameObject.tag == "Player2") || (collision.gameObject.tag == "Player3") || (collision.gameObject.tag == "Player4"))
        {
            gameObject.GetComponent<Animator>().SetBool("IsSteppedOn", true);
            
            this.Wait(0.15f, () =>
            {
                Break.Play();
            });

            this.Wait(1f, () =>
            {
                Destroy(gameObject);
            });
        }
    }
}
