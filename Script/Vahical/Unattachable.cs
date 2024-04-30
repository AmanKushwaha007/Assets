using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unattachable : MonoBehaviour
{
    public WheelCollider X;


    /// <summary>
    /// It take time from caller and after time's up
    /// then it destory itself
    /// </summary>
    /// <param name="Time"></param>
    public void unattachedNow(float Time)
    {
        if(X != null)
        {
            X.enabled = false;
            GetComponent<Collider>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
        }

        Invoke("detroyGameObject",Time);
    }


    /// <summary>
    /// Destory gameObject Insted
    /// </summary>
    public void detroyGameObject()
    {
        Destroy(gameObject);
    }
}
