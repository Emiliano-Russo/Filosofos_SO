using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inicio : MonoBehaviour
{

    public Text segundos_autocambio;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("segundos") == 0) PlayerPrefs.SetFloat("segundos", 5);
       
    }

    private void Update()
    {
        segundos_autocambio.text = PlayerPrefs.GetFloat("segundos").ToString() + " segundos";
    }


    public void setear_segundos(float segundos)
    {
        PlayerPrefs.SetFloat("segundos", segundos);
    }

    public void Iniciar_Simulacion()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
