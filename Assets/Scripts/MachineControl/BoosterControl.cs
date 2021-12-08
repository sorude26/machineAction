using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterControl : MonoBehaviour
{
    MachineController _machine = default;
    public void Set(MachineController controller)
    {
        _machine = controller;
        BoostEnd();
    }
    public void Boost()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StartBooster();
            }
        }
    }
    public void BoostF()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StartBoosterF();
            }
        }
    }
    public void BoostB()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StartBoosterB();
            }
        }
    }
    public void BoostL()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StartBoosterL();
            }
        }
    }
    public void BoostR()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StartBoosterR();
            }
        }
    }
    public void BoostEnd()
    {
        foreach (var parts in _machine.MachineParts.GetAllParts())
        {
            if (parts != null)
            {
                parts.StopBooster();
            }
        }
    }
}
