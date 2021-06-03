using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public enum States { Waiting, Playing, Ending };
public class StateManager : MonoBehaviourPun, IPunObservable
{

    public enum States { Waiting, Playing, Ending };
    public States state;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();

        if (stream.IsWriting)
        {
            stream.SendNext(state);
        }
        else
        {
            this.state = (States)stream.ReceiveNext();
        }

    }
}
