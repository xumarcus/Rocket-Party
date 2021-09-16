﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

public class AICombatAI : MonoBehaviour
{
    public float startFollowDistance;
    public float stopDistance;

    private AIControl control;
    private StateMachine stateMachine;

    private void Awake()
    {
        control = GetComponent<AIControl>();
        stateMachine = new StateMachine();
    }

    private void Start()
    {
        IState idleState = new IdleState(control);
        IState followState = new FollowState(control, stopDistance);
        IState fireRocketState = new FireRocketState(control);
        IState waitState = new WaitState(control, 0.5f, 1.2f);

        // idle
        stateMachine.AddTransition(idleState, followState, () => TargetDistance() < startFollowDistance);

        // follow
        stateMachine.AddTransition(followState, fireRocketState, CurrentStatedEnded);

        // fire rocket
        stateMachine.AddTransition(fireRocketState, waitState, CurrentStatedEnded);

        // wait
        stateMachine.AddTransition(waitState, idleState, CurrentStatedEnded);

        stateMachine.SetState(idleState);
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    private float TargetDistance()
    {
        return Vector2.Distance(control.GetTargetPos(), control.GetPosition());
    }

    private bool CurrentStatedEnded()
    {
        return stateMachine.CurrentState.StateEnded();
    }
}