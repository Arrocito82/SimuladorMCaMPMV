using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Direccion : MonoBehaviour
{
    private string direccion;
    private Text direccionText;
    [SerializeField] private GameObject direccionInputField;
    [SerializeField] private GameObject memoriaVirtualContentScrollView;
    MemoriaVirtual memoriaVirtual;
    //[SerializeField] private GameObject memoriaPrincipalScrollView;
    //[SerializeField] private GameObject memoriaCacheScrollView;
    //private List<DireccionMemoriaCache> direccionesMemoriaCache;

    //private List<DireccionMemoriaPrincipal> direccionesMemoriaPrincipal;

    public void Start()
    {
        direccionText = direccionInputField.GetComponent<Text>();
        memoriaVirtual=memoriaVirtualContentScrollView.GetComponent<MemoriaVirtual>();
        //direccionesMemoriaPrincipal = new List<DireccionMemoriaPrincipal>();
        
        //direccionesMemoriaCache = new List<DireccionMemoriaCache>();

    }

    //public void addDireccionMemoriaVirtual()
    //{
    //    memoriaVirtual.addDireccionMemoriaVirtual(direccionText.text);
        
    //}
    //public void deleteDireccionMemoriaVirtual()
    //{
    //    memoriaVirtual.deleteDireccionMemoriaVirtual(int.Parse(direccionText.text));

    //}
    //public void addDireccionMemoriaPrincipal()
    //{
    //    DireccionMemoriaPrincipal direccionMemoriaPrincipal = new DireccionMemoriaPrincipal(direccionText.text);
    //    // agregando la nueva dirección
    //    direccionesMemoriaPrincipal.Add(direccionMemoriaPrincipal);
    //    // limpiando input field dirección
    //    direccionText.text = "";
    //    // limpiando el scroll view
    //    memoriaPrincipalScrollView.GetComponent<Text>().text = "";

    //    //renderizando lista de direcciones
    //    foreach (DireccionMemoriaPrincipal currentDireccion in direccionesMemoriaPrincipal)
    //    {
    //        memoriaPrincipalScrollView.GetComponent<Text>().text += currentDireccion.getDireccion() + "\n";
    //    }
    //}

    //public void addDireccionMemoriaCache()
    //{
    //    DireccionMemoriaCache direccionMemoriaCache = new DireccionMemoriaCache(direccionInputField.GetComponent<Text>().text);
    //    // agregando la nueva dirección
    //    direccionesMemoriaCache.Add(direccionMemoriaCache);
    //    // limpiando input field dirección
    //    direccionInputField.GetComponent<Text>().text = "";
    //    // limpiando el scroll view
    //    memoriaCacheScrollView.GetComponent<Text>().text = "";

    //    //renderizando lista de direcciones
    //    foreach (DireccionMemoriaCache currentDireccion in direccionesMemoriaCache)
    //    {
    //        memoriaCacheScrollView.GetComponent<Text>().text += currentDireccion.getDireccion() + "\n";
    //    }
    //}

}


//public class DireccionMemoriaPrincipal : Object
//{
//    private string direccion;
//    public DireccionMemoriaPrincipal(string direccion)
//    {
//        this.direccion = direccion;
//    }
//    public DireccionMemoriaPrincipal()
//    {

//    }
//    public string getDireccion()
//    {
//        return this.direccion;
//    }
//    public void setDireccion(string direccion)
//    {
//        this.direccion = direccion;
//    }
//}
//public class DireccionMemoriaCache : Object
//{
//    private string direccion;
//    public DireccionMemoriaCache(string direccion)
//    {
//        this.direccion = direccion;
//    }
//    public string getDireccion()
//    {
//        return this.direccion;
//    }
//    public void setDireccion(string direccion)
//    {
//        this.direccion = direccion;
//    }
//}
