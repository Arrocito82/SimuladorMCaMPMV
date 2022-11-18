using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class MemoriaVirtual : MonoBehaviour
{
    private List<Tuple<int, int, GameObject, int>> direccionesMemoriaVirtual; // página, desplazamiento, item, dato
    private GameObject direccionTemplate;
    [SerializeField] private int maximoDireccionableMV;
    private void Awake()
    {
        direccionesMemoriaVirtual = new List<Tuple<int, int, GameObject, int>>();
        //recuperando el primer elemento que servira de template
        direccionTemplate = this.transform.GetChild(0).gameObject;
        /* 
         * INICIALIZANDO MEMORIA VIRTUAL
         * El desplazamiento cuenta las palabras dentro de cada página, en este caso hay 32. 
         * Mismo caso aplica para la página, dado que hay 32 páginas dentro de la memoria virtual.
         * Sin embargo, debe ser en hexadecimal de 5 bits. 
         * 32 decimal = 20 hexadecimal.
         * Con el fin de tener datos de prueba se llenara toda la memoria virtual de datos aleatoriamente
        */
        int contadorDesplazamiento = 0x0; 
        int contadorPagina = 0x0;
        Random dato = new Random();

        for (int i=0x0; i< maximoDireccionableMV; i++)
        {
            int datoAleatorio = dato.Next(0, 256);
            GameObject direccionItem = Instantiate(direccionTemplate, this.transform);
            direccionItem.transform.GetChild(0).GetComponent<Text>().text = $"{contadorPagina:X2}";
            direccionItem.transform.GetChild(1).GetComponent<Text>().text = $"{contadorDesplazamiento:X2}";
            direccionItem.transform.GetChild(2).GetComponent<Text>().text = $"{datoAleatorio:X2}";
            direccionesMemoriaVirtual.Add(new Tuple<int,int, GameObject,int>(contadorPagina, contadorDesplazamiento, direccionItem,datoAleatorio));

            // set contadores
            if (contadorDesplazamiento < 0x1f)
            {
                //recorriendo página
                contadorDesplazamiento++;
            }
            else
            {
                // reiniciando a la posición 0 de la siguiente página
                contadorDesplazamiento=0x0;
                contadorPagina++;
            }

        }
        Destroy(direccionTemplate);
        
    }
    public List<Tuple<int, int, int>> BusquedaMemoriaVirtual(int paginaIndex)// página, desplazamiento, dato
    {
        List<Tuple<int, int, int>>marcoObjetivo=new List<Tuple<int, int, int>>(); 
        for (int i=0; i < this.direccionesMemoriaVirtual.Count; i++)
        {
            Tuple<int, int, GameObject, int> direccionActual = this.direccionesMemoriaVirtual[i];
            if (direccionActual.Item1 == paginaIndex)
            {
                marcoObjetivo.Add(new Tuple<int, int, int>(direccionActual.Item1, direccionActual.Item2, direccionActual.Item4));
            }
        }
        return marcoObjetivo;
    }


}
