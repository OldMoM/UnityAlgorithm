using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform squad;

    private Vector3 dir;
    private float speed = 5;
    private Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        dir = squad.right;
        var meshRenderer = squad.GetComponent<MeshRenderer>();
        size = meshRenderer.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        var f = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");


        transform.position += speed * dir * f * Time.deltaTime;

        //限制上下移动范围
        var hPosition = transform.position + speed * squad.up * v * Time.deltaTime;
        var h_new = Mathf.Clamp(hPosition.y, squad.position.y - size.y / 2, squad.position.y + size.y / 2);
        hPosition.y = h_new;
        transform.position = hPosition;

    }
}
