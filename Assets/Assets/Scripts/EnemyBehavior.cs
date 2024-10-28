using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour

//on collision if with player then call damage player function
//in player controler if player dies disable movement iskinetic (add to lose screen) and prompt repawn to minus gold 
{
    public int enemyHealth;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            Debug.Log("Attack");
            player.TakeDamage();
        }
        else if (other.gameObject.tag == "Bullet")
        {
            //BulletController bullet = other.gameObject.GetComponent<BulletController>();
            Debug.Log("Hit");
            TakeDamage();
        }
    }


    public void TakeDamage()
    {
        if(enemyHealth != 0)
        {
            enemyHealth -= 10;
        }
        else if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Target exit");
    }
}

