using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle Dynamic"))
        {
            Helper.DebugLog("Obstacle Dynamic: " + other.transform.CompareTag("Obstacle Dynamic"));
            SoundManager.Instance.PlaySoundObstacleDynamic(other.transform.position);
            return;
        }
    }
}
