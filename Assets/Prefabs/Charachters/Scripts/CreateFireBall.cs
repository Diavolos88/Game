using Unity.VisualScripting;
using UnityEngine;

public class CreateFireBall : MonoBehaviour
{

    [SerializeField] private float speed = 1f;
    [SerializeField] private float radius = 20f;
    [SerializeField] private float force = 500f;
    private Camera camera;
    private Rigidbody rigidbody;

    void Start()
    {
        camera = GameObject.Find("GameManager").GetComponent<MainConfig>().mainCamera;
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 offset = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        
        Vector3 pushDir = offset - transform.position;
        Vector3 addForce = - pushDir.normalized * speed;
        
        // rigidbody.AddForce(addForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.CompareTag("Player"))
        {
            Explode();
            Destroy(gameObject); 
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if(!other.gameObject.CompareTag("Player"))
    //     {
    //         Explode();
    //         Destroy(gameObject); 
    //     }
    // }

    void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, radius);

        for (int j = 0; j < overlappedColliders.Length; j++)
        {
            Rigidbody rigidbody = overlappedColliders[j].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}
