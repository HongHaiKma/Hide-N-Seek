using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInMap : MonoBehaviour
{
    public Rigidbody rb_Owner;
    public DoorInMapDir m_DoorDir;

    private void Update()
    {
        // if (m_DoorDir == DoorInMapDir.Forward)
        // {
        //     rb_Owner.centerOfMass = Vector3.forward;
        // }
        // if (m_DoorDir == DoorInMapDir.BackWard)
        // {
        //     rb_Owner.centerOfMass = Vector3.back;
        // }
        // if (m_DoorDir == DoorInMapDir.Right)
        // {
        //     rb_Owner.centerOfMass = Vector3.right;
        // }
        // if (m_DoorDir == DoorInMapDir.Left)
        // {
        //     rb_Owner.centerOfMass = Vector3.left;
        // }
        // rb_Owner.ResetCenterOfMass();
    }
}

public enum DoorInMapDir
{
    Forward = 0,
    BackWard,
    Right,
    Left
}
