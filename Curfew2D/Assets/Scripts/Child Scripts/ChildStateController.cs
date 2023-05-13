using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildStateController : MonoBehaviour
{
    public enum State {Follow, InTrap, FollowEvil, Idle};
    public State currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Follow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
