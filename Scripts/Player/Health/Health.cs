
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")] 
    [SerializeField] private float startingHP;
    public float currentHP { get; private set;}
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int flashCount;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHP = startingHP;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void takeDamage(float _damage)
    {
        currentHP = Mathf.Clamp(currentHP - _damage, 0, startingHP);

        if (currentHP > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());

        }
        else
        {
            if (!dead)
            { 
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            takeDamage(1);

    }

    public void AddHealth(float _value)
    {
        currentHP = Mathf.Clamp(currentHP + _value,0,startingHP);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < flashCount; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.05f);
            yield return new WaitForSeconds(iFrameDuration / (flashCount * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (flashCount * 2));

        } 
        Physics2D.IgnoreLayerCollision(9, 10, false);

    }









}
