using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CIController {

    [MenuItem("Automation/BuildWindows64")]
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

        var dir = System.IO.Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Builds"));
        File.WriteAllText(dir.FullName + Path.DirectorySeparatorChar + "current.txt", "it is heree");

        var scenes = EditorBuildSettings.scenes.Select(it => it.path).ToArray();
        
        BuildPipeline.BuildPlayer( 
          scenes,
          dir.FullName + Path.DirectorySeparatorChar + "myPlayer.exe", 
          BuildTarget.StandaloneWindows64,
          BuildOptions.None
          );

    }
}


