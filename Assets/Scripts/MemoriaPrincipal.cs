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
    private List<Tuple<int,int,int,int>> gestionEmplazamientoLRU;//marco, uso, indice primer bloque, index posición primer bloque del marco
    private GameObject direccionTemplate;
    [SerializeField] public int maximoDireccionableMP;
    [SerializeField] private GameObject memoriaVirtual;
    private MemoriaVirtual memoriaVirtualControlador;
    public void Awake()
    {
        //inicializando la lista de direcciones
        direccionesMemoriaPrincipal = new List<Tuple<int, int, int, int, int, GameObject>>();

        // inicializando la memoria virtual
        memoriaVirtualControlador = memoriaVirtual.GetComponent<MemoriaVirtual>();
        
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
            direccionItem.transform.GetChild(4).GetComponent<Text>().text = $"{datoAleatorio:X2}"; // dato
            direccionesMemoriaPrincipal.Add(new Tuple<int, int, int, int, int, GameObject>(contadorMarcos,contadorDesplazamiento,contadorEtiqueta,contadorPalabra,datoAleatorio, direccionItem));

            contadorPalabra++;
            // set contadores dirección memoria caché
            if (contadorPalabra > 0x7)
            {
                contadorEtiqueta++;
                contadorPalabra = 0x0;
            }

            contadorDesplazamiento++;
            // set contadores dirección memoria virtual
            if (contadorDesplazamiento > 0x1f)
            {
                contadorDesplazamiento = 0x0;
                // asignar uso
                contadorMarcos++;
            }
        }
        Destroy(direccionTemplate);
        

    }
    public void Start()
    {
        //inicializando el indice de uso LRU para emplazamiento para 32 bloques
        gestionEmplazamientoLRU = new List<Tuple<int, int,int,int>>(); // marco, uso
        int indiceBloque = 0x0;
        int posicionInicialMarco = 0;
        for (int i = 0; i < 8; i++)
        {
            //marco no asignado, uso 0
            gestionEmplazamientoLRU.Add(new Tuple<int, int,int,int>(-1, 0, indiceBloque,posicionInicialMarco));
            indiceBloque +=0x8;
            posicionInicialMarco += 8;
            //Debug.Log(gestionEmplazamientoLRU[i]);
        }
        for (int i = 0; i < gestionEmplazamientoLRU.Count; i++)
        {
            //marco no asignado, uso 0
            Debug.Log(gestionEmplazamientoLRU[i]);
        }
        //Debug.Log(gestionEmplazamientoLRU.Count);
    }

    public Tuple<Tuple<int,int,int,int>, Tuple<int, int, int, int>> Leer(int bloque)
    {
        bool existe = false;
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
                //Debug.Log(new Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>(datos1, datos2));
                Debug.Log("Acierto Memoria Principal");
                existe = true;
                break;
            }
        }
        if (existe == false)
        {

            Debug.Log("Fallo Memoria Principal");
            this.TLB(bloque);// agregando marco a la memoria principal
            foreach (Tuple<int, int, int, int, int, GameObject> bloqueActual in direccionesMemoriaPrincipal)
            {
                if (bloqueActual.Item3 == bloque)
                {
                    int index = direccionesMemoriaPrincipal.IndexOf(bloqueActual);
                    //Debug.Log($"Bloque encontrado {index}");
                    datos1 = new Tuple<int, int, int, int>(
                        direccionesMemoriaPrincipal[index].Item5,
                        direccionesMemoriaPrincipal[index + 1].Item5,
                        direccionesMemoriaPrincipal[index + 2].Item5,
                        direccionesMemoriaPrincipal[index + 3].Item5
                        );
                    datos2 = new Tuple<int, int, int, int>(
                        direccionesMemoriaPrincipal[index + 4].Item5,
                        direccionesMemoriaPrincipal[index + 5].Item5,
                        direccionesMemoriaPrincipal[index + 6].Item5,
                        direccionesMemoriaPrincipal[index + 7].Item5
                        );
                    //Debug.Log(new Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>(datos1, datos2));
                    Debug.Log("Acierto Memoria Principal");
                    existe = true;
                    break;
                }
            }
        }
        
        return new Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>>(datos1, datos2);
    }

 
    /**
     * Este método tiene por proposito calcular en que indice inicia el bloque que buscan.
     * Si el bloque esta cargado
     * entonces actualizar el indicador de uso de todos los marcos y retornar el indice donde inicia el bloque
     * sino 
     * entonces hay que ir a buscar el marco completo (página) a la memoria virtual
     * calcular en que posición se colocará dependiendo del indice de uso
     * y luego actualizar el indicador de uso de todos los marcos y retornar el indice donde inicia el bloque
    */
    public void TLB(int bloque)
    {
        /** 
         calculando a que marco corresponde dicho bloque
         si el marco contiene 32 palabras
         y el bloque 8 palabras
        cada marco contiene 4 bloques
        */

        int marco = bloque / 4;
        int primerBloqueDelMarco = marco * 0x4 ;
        //Debug.Log(primerBloqueDelMarco);
        // buscando marco
        Tuple<bool,int> existe = this.existeMarco(marco); // existe, posicion actual
        if (existe.Item1)
        {
            // actualizar usos
            this.updateMarcos(marco, existe.Item2);
        }
        else
        {
            //recuperar marco
            // página, desplazamiento, dato
            List<Tuple<int,int,int>>marcoRecuperado=this.memoriaVirtualControlador.BusquedaMemoriaVirtual(marco);
            //for(int i = 0;i< marcoRecuperado.Count; i++)
            //{

            //Debug.Log(marcoRecuperado[i]);
            //}
            // a donde lo pongo?
            int posicionInicialMarco = this.menosUsado();
            this.addMarco(marco,primerBloqueDelMarco,posicionInicialMarco); // se agrego al LRU
            //Debug.Log(indexARemplazar);

            int bloqueIndice = primerBloqueDelMarco;
            int palabra = 0x0;
            //marco, desplazamiento, etiqueta, palabra, dato
            for (int posicion=posicionInicialMarco; posicion < posicionInicialMarco + 0x20; posicion++)
            {
                Debug.Log(posicion);
                if (palabra >= 0x8)
                {
                    palabra = 0x0;
                    bloqueIndice++;
                }
                direccionesMemoriaPrincipal[posicion] = new Tuple<int, int, int, int, int, GameObject>(
                marcoRecuperado[palabra].Item1, //marco
                marcoRecuperado[palabra].Item2,//desplazamiento
                bloqueIndice,//bloque
                palabra, //palabra
                marcoRecuperado[palabra].Item3, //dato
                direccionesMemoriaPrincipal[posicion].Item6
                );
                //actualizando el view
                GameObject direccionItem = direccionesMemoriaPrincipal[posicion].Item6;
                direccionItem.transform.GetChild(0).GetComponent<Text>().text = $"{marcoRecuperado[palabra].Item1:X1}";// marco 3 bits
                direccionItem.transform.GetChild(1).GetComponent<Text>().text = $"{marcoRecuperado[palabra].Item2:X2}";// desplazamiento 5 bits
                direccionItem.transform.GetChild(2).GetComponent<Text>().text = $"{bloqueIndice:X2}";// etiqueta 5 bits
                direccionItem.transform.GetChild(3).GetComponent<Text>().text = $"{palabra:X1}";// palabra 3 bits
                direccionItem.transform.GetChild(4).GetComponent<Text>().text = $"{marcoRecuperado[palabra].Item3:X2}"; // dato
                palabra++;

            }
        }
    }

    private void addMarco(int marco, int indicePrimerBloque, int posicionInicialMarco)
    {
        int uso = 1;
        //aumentando en 1 todos los marcos
        for (var i = 0; i < gestionEmplazamientoLRU.Count; i++)
        {
            Tuple<int, int, int,int> elemento = gestionEmplazamientoLRU[i];
            elemento = new Tuple<int, int, int,int>(elemento.Item1, elemento.Item2 + 1,elemento.Item3,elemento.Item4);
        }
        // agregando el marco existente a la posición inicial
        this.gestionEmplazamientoLRU.Insert(0, new Tuple<int, int,int,int>(marco, uso,indicePrimerBloque,posicionInicialMarco));
    }
    // pone el marco en la posición inicial con uso = 1, e incrementa los usos de los demás marcos
    private void updateMarcos(int marco, int posicionActual)
    {
        int uso = 1;
        // removiendo el marco
        int posicionPrimerBloque = gestionEmplazamientoLRU[posicionActual].Item3;
        int posicionInicialMarco= gestionEmplazamientoLRU[posicionActual].Item4;
        this.gestionEmplazamientoLRU.RemoveAt(posicionActual);
        //aumentando en 1 todos los marcos
        for (var i = 0; i < gestionEmplazamientoLRU.Count; i++)
        {
            Tuple<int, int, int, int> elemento = gestionEmplazamientoLRU[i];
            elemento = new Tuple<int, int,int,int>(elemento.Item1, elemento.Item2 + 1,elemento.Item3,elemento.Item4);
        }
        // agregando el marco existente a la posición inicial
        this.gestionEmplazamientoLRU.Insert(0, new Tuple<int, int, int, int>(marco, uso,posicionPrimerBloque,posicionInicialMarco));
    }

    //retorna si existe o no y si existe te dice la posición actual
    private Tuple<bool,int> existeMarco(int marco)
    {
        bool existe = false;
        int posicion = -1;
        for (var i = 0; i < gestionEmplazamientoLRU.Count; i++)
        {
            Tuple<int, int,int,int> marcoActual = gestionEmplazamientoLRU[i];
            if (marcoActual.Item1 == marco)
            {
                //marco encontrado
                existe = true;
                posicion = i;
                break;
            }
        }
        return new Tuple<bool,int>(existe, posicion);
    }
    public int menosUsado()
    {
        int  posicionInicialMarco= 0;
        // verificando el tamaño de la lista de usos, debe ser menor a 8
        // cual es el bloque más viejo?
        for (int i = 0; i < 8; i++)
        {
            Tuple<int, int, int,int> currentMarco = this.gestionEmplazamientoLRU[i];
            //Debug.Log($"Marco {currentMarco.Item1}");
            
                if (currentMarco.Item1 == -1)
                {
                    //marco no asignado, esta es la última posición
                    posicionInicialMarco = currentMarco.Item4;
                    break;
                }
                else if(i == 7)
                {
                    posicionInicialMarco = currentMarco.Item4;
                break;
                }

          
            
        }
        return posicionInicialMarco;
    }

}
