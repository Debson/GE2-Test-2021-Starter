﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public StateMachine owner;
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Think() { }
}

public class StateMachine : MonoBehaviour
{
    public State currentState;
    public State globalState;
    public State previousState;

    private IEnumerator coroutine;

    public int updatesPerSecond = 5;
    // Use this for initialization
    void Start() { }

    public void ChangeStateDelayed(State newState, float delay)
    {
        coroutine = ChangeStateCoRoutine(newState, delay);
        StartCoroutine(coroutine);
    }

    public void CancelDelayedStateChange()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    IEnumerator ChangeStateCoRoutine(State newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(newState);
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

    public void ChangeState(State newState)
    {
        previousState = currentState;
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.owner = this;
        Debug.Log(currentState.GetType());
        currentState.Enter();
    }

    void Update()
    {
        if (globalState != null)
        {
            globalState.Think();
        }
        if (currentState != null)
        {
            currentState.Think();
        }
    }
}
