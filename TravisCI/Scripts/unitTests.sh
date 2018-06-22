#! /bin/sh
project="Unity-Sandbox"

echo "------------------------------------------"
echo "Running Editor Tests for $project"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -projectPath $(pwd) \
  -batchmode \
  -runEditorTests \
  -editorTestsResultFile $(pwd)/result.xml \
  -nographics \
  -testPlatform StandaloneOSXIntel64 \
  -quit

echo "Finished Editor Tests"
cat $(pwd)/result.xml  
echo "--------------------------------------------"
