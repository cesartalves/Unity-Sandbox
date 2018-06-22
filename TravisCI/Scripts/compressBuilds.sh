#! /bin/sh
echo "--------------------------------------"
echo 'Attempting to zip builds'
zip -r $(pwd)/Build/windows.zip $(pwd)/Builds/
# zip -r $(pwd)/Build/linux.zip $(pwd)/Build/linux/
# zip -r $(pwd)/Build/mac.zip $(pwd)/Build/osx/