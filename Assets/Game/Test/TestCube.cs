using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TestCube : MonoBehaviour
{
    // public Rigidbody rb;
    // public CharacterController cc;

    // private void Update()
    // {
    //     // if (Input.GetKey(KeyCode.C))
    //     // {
    //     // Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    //     // Debug.Log("Horizontal = " + input.x);
    //     // Debug.Log("Vertical = " + input.z);
    //     // // rb.velocity = transform.forward.normalized * Time.deltaTime * 1000f;
    //     // cc.Move(input * Time.deltaTime * 200f);

    //     if (Input.GetKey(KeyCode.A))
    //         cc.Move(Vector3.left * Time.deltaTime * 200f);
    //     if (Input.GetKey(KeyCode.D))
    //         cc.Move(Vector3.right * Time.deltaTime * 200f);
    //     if (Input.GetKey(KeyCode.W))
    //         cc.Move(Vector3.forward * Time.deltaTime * 200f);
    //     if (Input.GetKey(KeyCode.S))
    //         cc.Move(Vector3.back * Time.deltaTime * 200f);
    //     // }
    // }

    // private void OnCollisionEnter(Collision other)
    // {
    //     Debug.Log(other.gameObject.name);
    // }

    // public void Test()
    // {
    //     Debug.Log("1111111111111111111111111");
    // }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Task3();
        }
    }

    // async Task Task1()
    // {
    //     Helper.DebugLog("Task 1 complete");
    // }

    // async Task Task2()
    // {
    //     Helper.DebugLog("Task 2 complete");
    // }

    async void Task3()
    {
        var tasks = new List<Task>();
        tasks.Add(Task.Run(() =>
        {
            // Task1();
            // Task2();
            Helper.DebugLog("Task 1 complete");
            Helper.DebugLog("Task 2 complete");
        }));

        Task t = Task.WhenAll(tasks.ToArray());
        try
        {
            await t;
        }
        catch { }

        Helper.DebugLog("Task 3 complete");
    }
}
