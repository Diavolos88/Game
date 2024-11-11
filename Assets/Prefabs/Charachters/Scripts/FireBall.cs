using System;
using Prefabs.Charachters.Scripts;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private GameObject fireBall;

    private GameObject telekinesPosition;
    private Camera mainCamera;
    private Animator animator;
    private void Start()
    {
        mainCamera = GameObject.Find("GameManager").GetComponent<MainConfig>().mainCamera;
        animator = GameObject.Find("GameManager").GetComponent<MainConfig>().player.GetComponent<Animator>();
        telekinesPosition = GameObject.Find("GameManager").GetComponent<MainConfig>().telekinesPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(fireBall, telekinesPosition.transform.position, fireBall.transform.rotation);
        }
    }

    void FixedUpdate()
    {
    }

}
