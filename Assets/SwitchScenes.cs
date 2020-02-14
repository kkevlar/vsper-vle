using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "tutorial")
            {
                SceneManager.LoadScene("CH4");
            }
            else if (SceneManager.GetActiveScene().name == "CH4")
            {
                SceneManager.LoadScene("CH2O");
            }
            else if (SceneManager.GetActiveScene().name == "CH2O")
            {
                SceneManager.LoadScene("CHN");
            }
            else if (SceneManager.GetActiveScene().name == "CHN")
            {
                SceneManager.LoadScene("CO2");
            }
            else if (SceneManager.GetActiveScene().name == "CO2")
            {
                SceneManager.LoadScene("NH3");
            }
            else if (SceneManager.GetActiveScene().name == "NH3")
            {
                SceneManager.LoadScene("NOH");
            }
            else if (SceneManager.GetActiveScene().name == "NOH")
            {
                SceneManager.LoadScene("N2");
            }
            else if (SceneManager.GetActiveScene().name == "N2")
            {
                SceneManager.LoadScene("H2O");
            }
            else if (SceneManager.GetActiveScene().name == "H2O")
            {
                SceneManager.LoadScene("O2");
            }
            else if (SceneManager.GetActiveScene().name == "O2")
            {
                SceneManager.LoadScene("tutorial");
            }
        }
    }
}
