using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput
{
    public bool LeftClick => Input.GetMouseButton(0);
    public float Horizontal => UIController.Instance.GetHorizontal();
    public float Vertical => UIController.Instance.GetVertical();
}
