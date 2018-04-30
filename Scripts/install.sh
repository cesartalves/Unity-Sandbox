#! /bin/sh

# BASE_URL=https://netstorage.unity3d.com/unity
# HASH=88d00a7498cd
VERSION=2017.3.1f1

download() {
  file=$1
  url="https://download.unity3d.com/download_unity/fc1d3344e6ea/MacEditorInstaller/Unity-2017.3.1f1.pkg"

  echo "Downloading from $url: "
  curl -o `basename "$package"` "$url"
}

install() {
  package=$1
  download "$package"

  echo "Installing "`basename "$package"`
  sudo installer -dumplog -package `basename "$package"` -target /
}


# install "MacEditorInstaller/Unity-$VERSION.pkg"
# install "MacEditorTargetInstaller/UnitySetup-Windows-Support-for-Editor-$VERSION.pkg"
# install "MacEditorTargetInstaller/UnitySetup-Mac-Support-for-Editor-$VERSION.pkg"
install "MacEditorTargetInstaller/UnitySetup-Linux-Support-for-Editor-$VERSION.pkg"