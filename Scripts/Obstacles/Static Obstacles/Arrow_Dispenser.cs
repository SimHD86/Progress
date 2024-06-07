
using Unity.VisualScripting;
using UnityEngine;

public class Arrow_Dispenser : MonoBehaviour
{
    [SerializeField] private float attackCD;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] arrows;
    private float CDTimer;

    private void Attack()
    {
        CDTimer = 0;
        arrows[FindArrow()].transform.position = firepoint.position;
        arrows[FindArrow()].GetComponent<Enemy_Projectile>().ActivateProjectile();

    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        CDTimer += Time.deltaTime;

        if (CDTimer >= attackCD)
            Attack();
    }
}
