using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Direccion : MonoBehaviour
{
    private string direccion;
    [SerializeField] private GameObject direccionInputField;
    [SerializeField] private GameObject memoriaVirtualScrollView;

    public void addDireccionMemoria()
    {
        direccion = direccionInputField.GetComponent<Text>().text;
        // mostrar dirección en sroll view
        memoriaVirtualScrollView.GetComponent<Text>().text = direccion;
    }
 
}
