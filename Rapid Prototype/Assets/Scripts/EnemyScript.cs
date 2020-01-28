using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Hit");
        if (other.gameObject.tag == "Player")
        {
            Object.Destroy(this.gameObject);
        }
    }
}
