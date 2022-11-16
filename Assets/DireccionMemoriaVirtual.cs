using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DireccionMemoriaVirtual : MonoBehaviour
{
    private string direccion;
    private int pagina;
    private int desplazamiento;
    public DireccionMemoriaVirtual(string direccion)
    {
        this.direccion = direccion;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public string getDireccion()
    {
        return this.direccion;
    }
    public void setDireccion(string direccion)
    {
        this.direccion = direccion;
    }
}


