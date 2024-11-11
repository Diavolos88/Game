using System;
using Prefabs.Charachters.Scripts;
using UnityEngine;

public class Telekines : MonoBehaviour
{
    public float Length;
    [SerializeField] private float impulsForce = 50000;
    [SerializeField] public float TelekinesSpeed = 50;
    

    private GameObject telekinesPosition;
    private Camera mainCamera;
    private Animator animator;
    private LineDrawer lineDrawer;
    private bool isTelekines;
    private bool isFrought;
    private GameObject hit;
    private void Start()
    {
        telekinesPosition = GameObject.Find("GameManager").GetComponent<MainConfig>().telekinesPosition;
        mainCamera = GameObject.Find("GameManager").GetComponent<MainConfig>().mainCamera;
        animator = GameObject.Find("GameManager").GetComponent<MainConfig>().player.GetComponent<Animator>();
        lineDrawer = new LineDrawer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTelekines)
        {
            animator.SetBool("isTelekinesAttack", false);
            animator.SetBool("isTelekines", false);
            hit = null;
            isTelekines = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) || isTelekines)
        {
            if(!isTelekines) hit = GetHitObject();
            if (hit != null)
            {
                if (!isTelekines)
                {
                    animator.SetBool("isTelekinesAttack", false);
                    animator.SetBool("isTelekines", true);
                    isTelekines = true;
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && isTelekines)
        {
            isTelekines = false;
            isFrought = true;
        }
    }

    void FixedUpdate()
    {
        if(isTelekines)
        {
            hit.GetComponent<Touchable>().MoveToHero(telekinesPosition.transform, TelekinesSpeed);
        }
        else if(isFrought)
        {
            animator.SetBool("isTelekinesAttack", true);
            animator.SetBool("isTelekines", false);
            isFrought = false;
            Frought(hit.GetComponent<Touchable>(), impulsForce, mainCamera.transform);
            hit = null;
        }
    }

    public GameObject GetHitObject()
    {
        Vector3 lineOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Vector3 EndLine = mainCamera.transform.forward * Length;
        
        //lineDrawer.DrawLineInGameView(lineOrigin, EndLine, Color.black);
        
        Ray ray = new Ray(lineOrigin, EndLine);
        
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Length))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.GetComponent<Touchable>() != null)
            {
                return hitObject;
            }
        }

        return null;
    }

    public void Frought(Touchable hit, float force, Transform direction)
    {
        hit.addForceToObject(force, direction);
    }
}
