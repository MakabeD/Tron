using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// Factory simple para crear Event por clave. Registra creadores en un diccionario.
/// </summary>
public class EventFactory : IEventFactory
{
    private readonly Dictionary<string, Func<float, Event>> registry = new Dictionary<string, Func<float, Event>>(StringComparer.OrdinalIgnoreCase);

    public EventFactory() 
    {
        
        registry["TestEvent1"] = prob => new TestEvent1(prob);
        registry["TestEvent2"] = prob => new TestEvent1(prob);
        registry["TestEvent3"] = prob => new TestEvent1(prob);

       
    }

    public Event Create(string eventKey, float probability)
    {
        if (registry.TryGetValue(eventKey, out var creator))
        {
            return creator(probability);
        }
        throw new ArgumentException($"EventFactory: tipo de evento no registrado: {eventKey}");
    }

    
    public void Register(string eventKey, Func<float, Event> creator)
    {
        registry[eventKey] = creator;
    }
}
