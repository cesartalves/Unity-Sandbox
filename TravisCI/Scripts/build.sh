#! /bin/sh
echo "---------------------------------------"
echo "Attempting to build for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -executeMethod CIController.BuildWindowsPlayer \
  -quit

echo "-----------Build logs-------------"
cat $(pwd)/unity.log 

tree $(pwd)

