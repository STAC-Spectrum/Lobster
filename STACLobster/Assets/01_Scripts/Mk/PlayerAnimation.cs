using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void AnimationEnd()
    {
        Debug.Log($"dsafioijsdiofsfijodsofsojf");
        _player.isAnimation = false;
    }
}
