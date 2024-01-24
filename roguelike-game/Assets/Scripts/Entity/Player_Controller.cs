using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Player_Controller : Base_Controller
{
    public Action skill = null;
    private float h;
    private float v;
    protected override void Start()
    {
        base.Start();
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(Attack());
    }
    protected override void Update()
    {
        base.Update();h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (state != State.Death)
        {
            if (h != 0 || v != 0)
            {
                state = State.Moving;
            }
        }
        if (skill != null)
        {
            skill.Invoke();
        }
    }
    private IEnumerator Attack()
    {
        while(true)
        {
            //Managers.Resource.instantiate...
            if(state == State.Death)
            {
                yield break;
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }
    protected override void moving()
    {
        transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime * h, transform.position.y + moveSpeed * Time.deltaTime * v);
    }
    protected override void death()
    {
        //Invoke();
    }
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(Camera.main.orthographicSize * 2 * Camera.main.aspect, Camera.main.orthographicSize * 2));
    }
}
