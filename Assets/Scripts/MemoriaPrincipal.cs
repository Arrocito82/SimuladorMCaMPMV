using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class MemoriaPrincipal : MonoBehaviour
{
    //marco, desplazamiento, etiqueta, palabra, dato
    private List<Tuple<int, int, int, int, int, GameObject>> direccionesMemoriaPrincipal;
    private GameObject direccionTemplate;
    [SerializeField] private int maximoDireccionableMP;
    private void Awake()
    {
        direccionesMemoriaPrincipal = new List<Tuple<int, int, int, int, int, GameObject>>();
        //recuperando el primer elemento que servira de template
        direccionTemplate = this.transform.GetChild(0).gameObject;

        Random dato = new Random();

        for (int i=0x0; i< maximoDireccionableMP; i++)
        {
            GameObject direccionItem = Instantiate(direccionTemplate, this.transform);
            direccionItem.transform.GetChild(0).GetComponent<Text>().text = $"{i:X1}";// marco 3 bits
            direccionItem.transform.GetChild(1).GetComponent<Text>().text = $"{i:X2}";// desplazamiento 5 bits
            direccionItem.transform.GetChild(2).GetComponent<Text>().text = $"{i:X2}";// etiqueta 5 bits
            direccionItem.transform.GetChild(3).GetComponent<Text>().text = $"{i:X1}";// palabra 3 bits
            direccionItem.transform.GetChild(4).GetComponent<Text>().text = $"{dato.Next(0,256):X2}";
            direccionesMemoriaPrincipal.Add(new Tuple<int, int, int, int, int, GameObject>(i,i,i,i,i, direccionItem));
        }
        Destroy(direccionTemplate);
        
    }
    
    public Tuple<Tuple<int,int,int,int>, Tuple<int, int, int, int>> Leer(int bloque)
    {
        Tuple<int, int, int, int> datos = new Tuple<int, int, int, int>(0, 0, 0, 0);
        return new Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>(datos, datos);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
