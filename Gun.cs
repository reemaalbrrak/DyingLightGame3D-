using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefap;
    public Transform FirePoint;
    public float BulletSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(BulletPrefap, FirePoint.position, Quaternion.identity, null);

            Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
            Vector3 direction = transform.right * 0 + transform.forward * BulletSpeed;
            bulletRigid.AddForce(direction );
            SoundManager.Instance.PlayShoot();

        }
    }
    

}