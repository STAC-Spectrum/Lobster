using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{

    public void Initialize(Boss boss);
    public void Move(Vector3 vec);
    public void Stop();

}
