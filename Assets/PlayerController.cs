using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{

    public float speed;
    PhotonView view;
    Camera m_MainCamera;



    // Start is called before the first frame update
    void Start()
    {


        view = GetComponent<PhotonView>();
        m_MainCamera = this.GetComponentInChildren<Camera>();

        if (view.IsMine)
        {
            m_MainCamera.enabled = true;
            //GetComponent<Camera>().enable = true;
        }
        else
        {
            m_MainCamera.enabled = false;
            //GetComponent<Camera>().enable = true;
        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;

        }
        
    }
}
