using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRay : MonoBehaviour
{
    public RaycastHit hit;
    public Transform tf_Target;

    private void Update()
    {
        Vector3 dir = (tf_Target.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, dir * 10f, out hit))
        {
            if (hit.collider.gameObject.CompareTag("TestTag"))
            {
                Debug.Log("22222222222222222222222222");
            }
        }

        Debug.DrawRay(transform.position, dir * 10f, Color.red);
    }
}
