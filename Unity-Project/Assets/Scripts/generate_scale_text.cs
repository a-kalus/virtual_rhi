using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using System;
using TMPro;

public class generate_scale_text : MonoBehaviour
{
    //public TextMeshPro[] Texts = new TextMeshPro[81];
    int startChildIndex = 6;
    int stopChildIndex = 87;
    // Start is called before the first frame update
    void Start()
    {
        var reader = new StreamReader(File.OpenRead(@"F:\masterarbeit 0806\list04.txt"));
        List<string> listA = new List<string>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(';');

            listA.Add(values[0]);
            var ind = startChildIndex;
            foreach (var coloumn1 in listA)
            {
                if (ind < stopChildIndex) { 
                transform.GetChild(ind).gameObject.GetComponent<TextMeshPro>().text = coloumn1;
                ind++;
            }
            }
    
        }

        Debug.Log(transform.GetChild(6).gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
