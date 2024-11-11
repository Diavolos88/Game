using UnityEngine;

public class Touchable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void MoveToHero(Transform moveTo, float speed)
    {
        float objectA = (transform.position.x + transform.position.z) / 2;
        float objectB = (moveTo.transform.position.x + moveTo.transform.position.z) / 2;
        float distance = objectA - objectB;

        // if(distance <= 1){
        //     gameObject.transform.SetParent(moveTo);
        // } else
        // {
            transform.position = Vector3.MoveTowards(transform.position, moveTo.transform.position, speed * Time.deltaTime);
        // }
    }

    public void addForceToObject(float force, Transform direction)
    {
        Vector3 offset = new Vector3(direction.position.x, direction.position.y, direction.position.z);
        
        Vector3 pushDir = offset - transform.position;
        Vector3 addForce = - pushDir.normalized * force;
        
        print(offset);
        
        _rigidbody.AddForce(addForce, ForceMode.Impulse);
    }
}
