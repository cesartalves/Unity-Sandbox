using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CIController {
    
    public static void BuildWindowsPlayer()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        string input = "";
        for (int i = 0; i < args.Length; i++)
        {
            Debug.Log("ARG " + i + ": " + args[i]);
            if (args[i] == "-folderInput")
            {
                input = args[i + 1];
            }
        }

        File.WriteAllText(Environment.CurrentDirectory + "/current.txt", "it is heree");

        var scenes = EditorBuildSettings.scenes.Select(it => it.path).ToArray();
        
        BuildPipeline.BuildPlayer( 
          scenes,
          Environment.CurrentDirectory + "/myPlayer.exe", 
          BuildTarget.StandaloneWindows64,
          BuildOptions.None
          );

    }
}


