using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateController : MonoBehaviour
{
    Rigidbody rb;

    Vector2 input;

    //mnoznik przyspieszenia statku
    public float enginePower = 10;

    //mnoznik sterowania
    public float gyroPower = 2;

    private CameraScript cs;

    //miejsce na prefab pocisku
    public GameObject bulletPrefab;
    //okresl miejsca spawnowania pociskow
    public Transform gunLeft, gunRight;
    //predkosc poczatkowa pocisku
    public float bulletSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cs = Camera.main.transform.GetComponent<CameraScript>();
        input = Vector2.zero;
        gunLeft = transform.Find("GunLeft").transform;
        gunRight = transform.Find("GunRight").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //sterowanie w poziomie (a/d)
        float x = Input.GetAxis("Horizontal");
        //sterowanie w pionie (w/s)
        float y = Input.GetAxis("Vertical");

        input.x = x;
        input.y = y;

        //teleportuj statek jezeli wyjdzie z ekranu
        if(Mathf.Abs(transform.position.x) > cs.gameWidth / 2)
        {
            //wylecielismy z ekranu w poziomie - teleporutj w druga strone
            Vector3 newPosition = new Vector3(transform.position.x * (-0.99f),
                                               0,
                                               transform.position.z);
            transform.position = newPosition;
        }

        if (Mathf.Abs(transform.position.z) > cs.gameHeight / 2)
        {  //wylecielismy z ekranu w pionie - teleporutj w druga strone
            Vector3 newPosition = new Vector3(transform.position.x,
                                               0,
                                               transform.position.z * (-0.99f));
            transform.position = newPosition;
        }
        if (Input.GetKeyDown(KeyCode.Space))
            Fire();
    }
    void FixedUpdate()
    {
        rb.AddForce(transform.forward * input.y * enginePower, ForceMode.Acceleration);
        rb.AddTorque(transform.up * input.x * gyroPower, ForceMode.Acceleration);
    }
    
    void Fire()
    {
        //spawn pocisk
        GameObject leftBullet = Instantiate(bulletPrefab, gunLeft.position, 
                                                         Quaternion.identity);
        //zmien predkosc pocisku
        leftBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed,
                                                       ForceMode.VelocityChange);
        //zniszcz pocisk po 5 sek
        Destroy(leftBullet, 5);

        //spawn pocisk
        GameObject rightBullet = Instantiate(bulletPrefab, gunRight.position,
                                                         Quaternion.identity);
        //zmien predkosc pocisku
        rightBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed,
                                                       ForceMode.VelocityChange);
        //zniszcz pocisk po 5 sek
        Destroy(rightBullet, 5);
    }
}


