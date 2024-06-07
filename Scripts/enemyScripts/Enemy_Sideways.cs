
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_Sideways : MonoBehaviour

{
    [SerializeField] private float damage;
    [SerializeField] private float MovementDistance;
    [SerializeField] private float Speed;

    private bool movingleft;
   
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - MovementDistance;
        rightEdge = transform.position.x + MovementDistance;

    }

    private void Update()
    {
        if (movingleft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingleft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingleft = true;
        }
          
   

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            collision.GetComponent<Health>().takeDamage(damage);

        }

    }
}
