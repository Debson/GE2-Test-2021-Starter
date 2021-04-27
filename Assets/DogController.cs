using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new DogFollowPlayerState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
