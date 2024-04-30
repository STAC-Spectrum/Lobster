using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void AnimationEnd()
    {
        print("아니ㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣㅣ");
        //this.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        _player.isAnimation = false;
    }
}
