using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInput : MonoBehaviour
{
    private string direccion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string input ){
        direccion=input;
        // AgregarDireccion(direccion);
    }

    public void AgregarDireccion(string input){
        List<string> direcciones = new List<string>();
        direcciones.Add(direccion);
        foreach (string d in direcciones)
        {
            Debug.Log(d);
        }
    }
}
