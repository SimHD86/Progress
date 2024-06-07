
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.WSA;

public class FireDispenser : MonoBehaviour
{
    [SerializeField] private float Damage;


    [Header("FireDispenser Timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer SpriteRend;

    private bool triggered; //when the trap is triggered
    private bool active; //when the trap is activated and throwing fire


    private void Awake()
    {
       anim = GetComponent<Animator>();
        SpriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)  
                StartCoroutine(ActivateFireDispenser()); //trigger the trap
            if (active)
                collision.GetComponent<Health>().takeDamage(Damage);
            
        }
        

    }

    private IEnumerator ActivateFireDispenser()
    {
        //turn red to notify when active
        triggered = true;
        SpriteRend.color = Color.red; 

        //delay, activation, reset 
        yield return new WaitForSeconds(activationDelay);
        SpriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        //wait X seconds to reset
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }



}
