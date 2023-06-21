using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FileReader reader = new FileReader();
        ParametersStructure parameters = reader.readParams();
        Dictionary<string, string> enemiesProp = parameters.enemiesProp;
        reader.debugPrint(enemiesProp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
