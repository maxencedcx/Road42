using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _speed = 10;
    [SerializeField] private int _limit = 6;

    private void Update()
    {
        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
            velocity.x = Mathf.Clamp(velocity.x - 1, -1, 1);
        if (Input.GetKey(KeyCode.RightArrow))
            velocity.x = Mathf.Clamp(velocity.x + 1, -1, 1);

        if (velocity != Vector3.zero)
            {
                velocity = LimitVelocity(velocity);
                gameObject.transform.Translate(velocity * Time.deltaTime * _speed, Space.World);
            }
    }

    private Vector3 LimitVelocity(Vector3 v)
    {
        if (Mathf.Abs(gameObject.transform.position.x + v.x) > _limit)
        {
            float diff = _limit - Mathf.Abs(gameObject.transform.position.x);
            v.x = (v.x < 0) ? (-diff) : (diff);
        }
        return v;
    }
}
