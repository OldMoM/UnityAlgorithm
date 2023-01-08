using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerController:MonoBehaviour
{
    public int speed;

    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var h_flag = Input.GetAxisRaw("Horizontal");
        var v_flag = Input.GetAxisRaw("Vertical");
        velocity = new Vector3(h_flag, 0, v_flag) * speed;
        rigid.position += velocity * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, .5f);
    }
}
