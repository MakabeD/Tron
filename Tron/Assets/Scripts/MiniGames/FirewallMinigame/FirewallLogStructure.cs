using System.Collections;
using System.Collections.Generic;

public class FirewallLogStructure
{
    
    public string text;

    
    public bool isPermitted;
    public string feedback;
    
    public FirewallLogStructure(string text, bool isPermitted, string feedback)
    {
        this.text = text;
        this.isPermitted = isPermitted;
        this.feedback = feedback;
    }
}

public interface IFirewallLogFactory
{
    FirewallLogStructure CrearLog(string text, bool isPermitted, string feedback);
}
public class FirewallLogFactory : IFirewallLogFactory
{
    public FirewallLogStructure CrearLog(string text, bool isPermitted, string feedback)
    {
        

        return new FirewallLogStructure(text, isPermitted, feedback);
    }
}
