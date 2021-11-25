using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamege : MonoBehaviour
{

    public float Health = 100f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Takedamege(float amount)
    {
        Health -= amount;

        if(Health <= 0f)
        {
            Die();
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}
