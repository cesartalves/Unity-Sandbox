#! /bin/sh

project="Unity-Sandbox"
UNITY_EXECUTABLE="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
RESULTS_FILEPATH="$(pwd)/result.xml"

echo "Running Editor Tests for $project"
"$UNITY_EXECUTABLE" \
  -projectPath $(pwd) \
  -batchmode \
  -runEditorTests \
  -nographics \

#echo "Unit test logs"
echo "Finished Editor Tests"
#cat $(pwd)/result.xml

tree $(pwd)

# echo "Attempting to build $project for Windows"
#   "$UNITY_EXECUTABLE" 
#   -batchmode 
#   -nographics 
#   -silent-crashes 
#   -logFile $(pwd)/unity.log 
#   -projectPath $(pwd) 
#   -buildWindowsPlayer "$(pwd)/Build/windows/$project.exe" 
#   -quit

echo "Attempting to build $project for OSX"
  "$UNITY_EXECUTABLE"  
  -batchmode \
  -nographics \
  -stackTraceLogType Full \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -executeMethod"CIController.BuildWindowsPlayer" \
  -quit

echo "Build logs"
cat $(pwd)/unity.log 

tree $(pwd)
# echo "Attempting to build $project for Linux"
#   "$UNITY_EXECUTABLE"  
#   -batchmode 
#   -nographics 
#   -silent-crashes 
#   -logFile $(pwd)/unity.log 
#   -projectPath $(pwd) 
#   -buildLinuxUniversalPlayer "$(pwd)/Build/linux/$project.exe" 
#   -quit

# echo 'Attempting to zip builds'
# zip -r $(pwd)/Build/linux.zip $(pwd)/Build/linux/
# zip -r $(pwd)/Build/mac.zip $(pwd)/Build/osx/
# zip -r $(pwd)/Build/windows.zip $(pwd)/Build/windows/