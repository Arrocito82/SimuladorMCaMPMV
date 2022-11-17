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
    [SerializeField] public int maximoDireccionableMP;
    private void Awake()
    {
        direccionesMemoriaPrincipal = new List<Tuple<int, int, int, int, int, GameObject>>();
        //recuperando el primer elemento que servira de template
        direccionTemplate = this.transform.GetChild(0).gameObject;

        Random dato = new Random();

        // Llenando memoria principal aleatoriamente
        int contadorMarcos = 0x0, contadorDesplazamiento = 0x0, contadorEtiqueta = 0x0, contadorPalabra = 0x0; 

        for (int i=0x0; i< maximoDireccionableMP; i++)
        {
            int datoAleatorio = dato.Next(0, 256);
            GameObject direccionItem = Instantiate(direccionTemplate, this.transform);
            direccionItem.transform.GetChild(0).GetComponent<Text>().text = $"{contadorMarcos:X1}";// marco 3 bits
            direccionItem.transform.GetChild(1).GetComponent<Text>().text = $"{contadorDesplazamiento:X2}";// desplazamiento 5 bits
            direccionItem.transform.GetChild(2).GetComponent<Text>().text = $"{contadorEtiqueta:X2}";// etiqueta 5 bits
            direccionItem.transform.GetChild(3).GetComponent<Text>().text = $"{contadorPalabra:X1}";// palabra 3 bits
            direccionItem.transform.GetChild(4).GetComponent<Text>().text = $"{datoAleatorio:X2}";
            direccionesMemoriaPrincipal.Add(new Tuple<int, int, int, int, int, GameObject>(contadorMarcos,contadorDesplazamiento,contadorEtiqueta,contadorPalabra,datoAleatorio, direccionItem));

            // set contadores dirección memoria caché
            if (contadorPalabra >= 0x7)
            {
                contadorEtiqueta++;
                contadorPalabra = 0x0;
            }
            contadorPalabra++;

            // set contadores dirección memoria virtual
            if (contadorDesplazamiento >= 0x1f)
            {
                contadorDesplazamiento = 0x0;
                contadorMarcos++;
            }
            contadorDesplazamiento++;
        }
        Destroy(direccionTemplate);
        
    }
    
    public Tuple<Tuple<int,int,int,int>, Tuple<int, int, int, int>> Leer(int bloque)
    {
        Tuple<int, int, int, int> datos1 = new Tuple<int, int, int, int>(0x0, 0x0, 0x0, 0x0);
        Tuple<int, int, int, int> datos2 = new Tuple<int, int, int, int>(0x0, 0x0, 0x0, 0x0);
        foreach (Tuple<int, int, int, int, int, GameObject>bloqueActual in direccionesMemoriaPrincipal)
        {
            if (bloqueActual.Item3 == bloque)
            {
                int index = direccionesMemoriaPrincipal.IndexOf(bloqueActual);
                //Debug.Log($"Bloque encontrado {index}");
                datos1 = new Tuple<int, int, int, int>(
                    direccionesMemoriaPrincipal[index].Item5,
                    direccionesMemoriaPrincipal[index+1].Item5,
                    direccionesMemoriaPrincipal[index+2].Item5,
                    direccionesMemoriaPrincipal[index+3].Item5
                    );
                datos2 = new Tuple<int, int, int, int>(
                    direccionesMemoriaPrincipal[index+4].Item5,
                    direccionesMemoriaPrincipal[index+5].Item5,
                    direccionesMemoriaPrincipal[index+6].Item5,
                    direccionesMemoriaPrincipal[index+7].Item5
                    );
                Debug.Log(new Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>(datos1, datos2));
                break;
            }
        }
        
        return new Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>(datos1, datos2);
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
