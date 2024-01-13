using System;
using UnityEngine;

public class CoinView : BaseView
{
    [SerializeField] private string playerTagName = "Player";
    [SerializeField] private string collecAnimState = "Collect";
    [SerializeField] private string idleAnimState = "Idle";
    private Animator animator;

    public event Action<CoinView> OnCollect;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject?.tag == playerTagName)
        {
            gameObject.SetActive(false);
            OnCollect.Invoke(this);
        }

    }

    private void OnEnable()
    {
        //animator.Play(idleAnimState);
    }
}
