#! /bin/sh

project="Unity-Sandbox"
UNITY_EXECUTABLE="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
RESULTS_FILEPATH="$(pwd)/unitTestsResult.xml"

# echo "Attempting to build $project for Windows"
#   "$UNITY_EXECUTABLE" 
#   -batchmode 
#   -nographics 
#   -silent-crashes 
#   -logFile $(pwd)/unity.log 
#   -projectPath $(pwd) 
#   -buildWindowsPlayer "$(pwd)/Build/windows/$project.exe" 
#   -quit

# echo "Attempting to build $project for OS X"
#   "$UNITY_EXECUTABLE"  
#   -batchmode 
#   -nographics 
#   -silent-crashes 
#   -logFile $(pwd)/unity.log 
#   -projectPath $(pwd) 
#   -buildOSXUniversalPlayer "$(pwd)/Build/osx/$project.app" 
#   -quit

# echo "Attempting to build $project for Linux"
#   "$UNITY_EXECUTABLE"  
#   -batchmode 
#   -nographics 
#   -silent-crashes 
#   -logFile $(pwd)/unity.log 
#   -projectPath $(pwd) 
#   -buildLinuxUniversalPlayer "$(pwd)/Build/linux/$project.exe" 
#   -quit

echo "Running Editor Tests"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -projectPath $(pwd) \
  -batchmode \
  -runEditorTests \
  -editorTestsResultFile "$RESULTS_FILEPATH" \
  -logFile $(pwd)/unity.log \
  -nographics \
  -quit

echo "Logs from test at $(pwd)/unity.log"

echo "Results xml: $RESULTS_FILEPATH"
cat $(pwd)/unitTestsResult.xml

echo ""
echo "Attempting to build $project for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \ 
  -batchmode \ 
  -nographics \ 
  -logFile $(pwd)/unity.log \ 
  -projectPath $(pwd) \ 
  -buildWindowsPlayer "$(pwd)/Build/windows/$project.exe" \
  -quit

echo ""
echo 'Logs from build'
cat $(pwd)/unity.log

# echo 'Attempting to zip builds'
# zip -r $(pwd)/Build/linux.zip $(pwd)/Build/linux/
# zip -r $(pwd)/Build/mac.zip $(pwd)/Build/osx/
# zip -r $(pwd)/Build/windows.zip $(pwd)/Build/windows/