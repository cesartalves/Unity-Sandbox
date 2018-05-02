using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CIController {
    
    public static void BuildWindowsPlayer()
    {
        var scenes = EditorBuildSettings.scenes.Select(it => it.path).ToArray();

        BuildPipeline.BuildPlayer( 
          scenes,
          Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/myPlayer.exe", 
          BuildTarget.StandaloneWindows64,
          BuildOptions.None
          );

    }
}


