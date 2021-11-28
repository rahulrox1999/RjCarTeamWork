using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PUN2_CarSync : MonoBehaviourPun, IPunObservable
{
    public MonoBehaviour[] localScripts; //Scripts that should only be enabled for the local player (Ex. Car controller)
    public GameObject[] localObjects; //Objects that should only be active for the local player (Ex. Camera)
    public Transform[] wheels; //Car wheel transforms

    Rigidbody r;
    // Values that will be synced over network
    Vector3 latestPos;
    Quaternion latestRot;
    Vector3 latestVelocity;
    Vector3 latestAngularVelocity;
    Quaternion[] wheelRotations = new Quaternion[0];
    // Lag compensation
    float currentTime = 0;
    double currentPacketTime = 0;
    double lastPacketTime = 0;
    Vector3 positionAtLastPacket = Vector3.zero;
    Quaternion rotationAtLastPacket = Quaternion.identity;
    Vector3 velocityAtLastPacket = Vector3.zero;
    Vector3 angularVelocityAtLastPacket = Vector3.zero;

    // Use this for initialization
    void Awake()
    {
        r = GetComponent<Rigidbody>();
        r.isKinematic = !photonView.IsMine;
        for (int i = 0; i < localScripts.Length; i++)
        {
            localScripts[i].enabled = photonView.IsMine;
        }
        for (int i = 0; i < localObjects.Length; i++)
        {
            localObjects[i].SetActive(photonView.IsMine);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(r.velocity);
            stream.SendNext(r.angularVelocity);

            wheelRotations = new Quaternion[wheels.Length];
            for (int i = 0; i < wheels.Length; i++)
            {
                wheelRotations[i] = wheels[i].localRotation;
            }
            stream.SendNext(wheelRotations);
        }
        else
        {
            // Network player, receive data
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();
            latestVelocity = (Vector3)stream.ReceiveNext();
            latestAngularVelocity = (Vector3)stream.ReceiveNext();
            wheelRotations = (Quaternion[])stream.ReceiveNext();

            // Lag compensation
            currentTime = 0.0f;
            lastPacketTime = currentPacketTime;
            currentPacketTime = info.SentServerTime;
            positionAtLastPacket = transform.position;
            rotationAtLastPacket = transform.rotation;
            velocityAtLastPacket = r.velocity;
            angularVelocityAtLastPacket = r.angularVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            // Lag compensation
            double timeToReachGoal = currentPacketTime - lastPacketTime;
            currentTime += Time.deltaTime;

            // Update car position and velocity
            transform.position = Vector3.Lerp(positionAtLastPacket, latestPos, (float)(currentTime / timeToReachGoal));
            transform.rotation = Quaternion.Lerp(rotationAtLastPacket, latestRot, (float)(currentTime / timeToReachGoal));
            r.velocity = Vector3.Lerp(velocityAtLastPacket, latestVelocity, (float)(currentTime / timeToReachGoal));
            r.angularVelocity = Vector3.Lerp(angularVelocityAtLastPacket, latestAngularVelocity, (float)(currentTime / timeToReachGoal));

            //Apply wheel rotation
            if (wheelRotations.Length == wheels.Length)
            {
                for (int i = 0; i < wheelRotations.Length; i++)
                {
                    wheels[i].localRotation = Quaternion.Lerp(wheels[i].localRotation, wheelRotations[i], Time.deltaTime * 6.5f);
                }
            }
        }
    }
}