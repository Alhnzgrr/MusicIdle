using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : IMovement
{
    Rigidbody _rigidbody;
    Transform _transform;

    public Movements(Rigidbody rigidbody, Transform transform)
    {
        _rigidbody = rigidbody;
        _transform = transform;
    }

    public void Move(float horizontal , float horizontalSpeed , float vertical , float verticalSpeed)
    {
        _rigidbody.velocity = new Vector3(horizontal * horizontalSpeed, _rigidbody.velocity.y, vertical * verticalSpeed);
    }

    public void Rotate(float horizontal , float vertical , float rotateSpeed)
    {
        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;

        _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
    }
}
