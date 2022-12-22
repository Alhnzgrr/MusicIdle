using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void Move(float horizontal, float horizontalSpeed, float vertical, float verticalSpeed);
    void Rotate(float horizontal, float vertical, float rotateSpeed);
}
