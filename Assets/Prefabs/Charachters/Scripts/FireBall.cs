using System;
using Prefabs.Charachters.Scripts;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private GameObject fireBall;

    private GameObject fireballPosition;
    private Camera mainCamera;
    private Animator animator;
    private void Start()
    {
        mainCamera = GameObject.Find("GameManager").GetComponent<MainConfig>().mainCamera;
        animator = GameObject.Find("GameManager").GetComponent<MainConfig>().player.GetComponent<Animator>();
        fireballPosition = GameObject.Find("GameManager").GetComponent<MainConfig>().fireBallPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(fireBall, fireballPosition.transform);
            // Instantiate(fireBall, fireballPosition.transform.position, fireBall.transform.rotation);
        }
    }

    void FixedUpdate()
    {
    }

}
