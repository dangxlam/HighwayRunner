using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action OnCoinCollected;

   
    void Update()
    {
        transform.Rotate(60 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            FindObjectOfType<AudioManager>().PlaySound("PickCoin");
            //GameManager.numOfCoins += 1;
            OnCoinCollected.Invoke();
            Destroy(gameObject);
            Debug.Log(GameManager.numOfCoins); 
        }
    }
}
