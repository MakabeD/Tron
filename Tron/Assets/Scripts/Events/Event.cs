using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Event
{
    // Probabilidad de ocurrencia (encapsulada)
    public float Probability { get; protected set; } = 0f;

    public bool IsSpawned { get;  set; } = false;
    public bool EventEmergency { get;  set; } = false;

    // Comportamiento que las subclases deben implementar
    public abstract void ExecuteEvent();
    public abstract void StopEventExecution();

    // Métodos de utilidad para manejar la probabilidad
    public float GetProbability() => Probability;

    public void DecreaseProbability(float amount)
    {
        Probability -= amount;
        if (Probability < 0f) Probability = 0f;
    }

    public void IncreaseProbability(float amount)
    {
        Probability += amount;
    }
    public void SetEmergency(bool value) => EventEmergency = value;
}

