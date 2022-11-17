using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DireccionMemoriaCache : MonoBehaviour
{
    private string direccion;
    private int bloque;
    private int linea;

    public DireccionMemoriaCache( int bloque, int linea)
    {
        // this.direccion = direccion;
        this.bloque = bloque;
        this.linea = linea;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Métodos get y set
    public int getBloque()
    {
        // return this.direccion;
        return this.bloque;
    }
    public void setBloque(int bloque)
    {
        this.bloque = bloque;
    }
    public int getLinea()
    {
        // return this.direccion;
        return this.linea;
    }
    public void setLinea(int linea)
    {
        this.linea = linea;
    }
}

