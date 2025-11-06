using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    

public class databaseLogStructure
{
    public readonly string text;
    public readonly bool isMalicious;
    public readonly string feedback;

    public databaseLogStructure(string text, bool isMalicious, string feedback)
    {
        this.text = text;
        this.isMalicious = isMalicious;
        this.feedback = feedback;
    }


    public override string ToString()
    {
        return $"databaseLogStructure(text=\"{text}\", isMalicious={isMalicious})";
    }
}

public static class databaseLogFactory
{
    /// <summary>
    /// Crea una entrada simple.
    /// </summary>
    public static databaseLogStructure Create(string text, bool isMalicious, string feedback)
    {
        return new databaseLogStructure(text, isMalicious, feedback);
    }

   
}