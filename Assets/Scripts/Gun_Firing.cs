using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun_Firing : MonoBehaviour
{

    PhotonView view;

    public float damage = 10f;
    public float Range = 100f;

    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    public GameObject GunTriggerPoint;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;
    public AudioSource Fire;
    public float impcatForceGun1;

    //MachineGun
    public GameObject GunLeftTriggerPoint;
    public GameObject GunRightTriggerPoint;

    public ParticleSystem MuzzleFlash2;
    public ParticleSystem MuzzleFlash3;
    public AudioSource Fire2;
    public float impcatForceGun2;

    public Rigidbody Car;
    public float BackForce = 200f;


    // Start is called before the first frame update
    void Start()
    {

        view = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                MuzzleFlash.Play();
                Fire.Play();
                Shoot();
            }

            if (Input.GetButton("Fire2") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                MuzzleFlash2.Play();
                MuzzleFlash3.Play();
                Fire2.Play();
                Shoot2();
            }

            void Shoot()
            {
               
                RaycastHit Hit;

                if (Physics.Raycast(GunTriggerPoint.transform.position, GunTriggerPoint.transform.forward, out Hit, Range))
                {
                    Debug.Log(Hit.transform.name);

                    TakeDamege target = Hit.transform.GetComponent<TakeDamege>();

                    if (target != null)
                    {
                        target.Takedamege(damage);
                    }

                    if (Hit.rigidbody != null)
                    {
                        Hit.rigidbody.AddForce(-Hit.normal * impcatForceGun1);
                    }

                    Car.AddForce(0f, 0f, -BackForce);

                    GameObject ImpactGameObject = PhotonNetwork.Instantiate(ImpactEffect.name, Hit.point, Quaternion.LookRotation(Hit.normal));

                    Destroy(ImpactGameObject, 1.5f);
                }

            }

            void Shoot2()
            {
                
                RaycastHit Hit;

                if (Physics.Raycast(GunLeftTriggerPoint.transform.position, GunLeftTriggerPoint.transform.forward, out Hit, Range))
                {
                    Debug.Log(Hit.transform.name);

                    TakeDamege target = Hit.transform.GetComponent<TakeDamege>();

                    if (target != null)
                    {
                        target.Takedamege(5f);
                    }

                    if (Hit.rigidbody != null)
                    {
                        Hit.rigidbody.AddForce(-Hit.normal * impcatForceGun2);
                    }

                    GameObject ImpactGameObject = PhotonNetwork.Instantiate(ImpactEffect.name, Hit.point, Quaternion.LookRotation(Hit.normal));

                    Destroy(ImpactGameObject, 1.5f);
                }

                if (Physics.Raycast(GunRightTriggerPoint.transform.position, GunRightTriggerPoint.transform.forward, out Hit, Range))
                {
                    Debug.Log(Hit.transform.name);

                    TakeDamege target = Hit.transform.GetComponent<TakeDamege>();

                    if (target != null)
                    {
                        target.Takedamege(5f);
                    }
                    if (Hit.rigidbody != null)
                    {
                        Hit.rigidbody.AddForce(-Hit.normal * impcatForceGun2);
                    }

                    GameObject ImpactGameObject = PhotonNetwork.Instantiate(ImpactEffect.name, Hit.point, Quaternion.LookRotation(Hit.normal));

                    Destroy(ImpactGameObject, 1.5f);
                }

            }
        }
       
    }
}
