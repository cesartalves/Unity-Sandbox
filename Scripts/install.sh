#! /bin/sh
# find links here http://unity.grimdork.net/2017.3.1p4

BASE_URL=https://download.unity3d.com/download_unity
HASH=7f25373c3e03
VERSION=2017.3.1p4

download() {
  file=$1
  url="$BASE_URL/$HASH/$package"

  echo "Downloading from $url: "
  curl -o `basename "$package"` "$url"
}

install() {
  package=$1
  download "$package"

  echo "Installing "`basename "$package"`
  sudo installer -dumplog -package `basename "$package"` -target /
}


install "MacEditorInstaller/Unity.pkg"
install "MacEditorTargetInstaller/UnitySetup-Windows-Support-for-Editor-$VERSION.pkg"
#install "MacEditorTargetInstaller/UnitySetup-Mac-Support-for-Editor-$VERSION.pkg"
#install "MacEditorTargetInstaller/UnitySetup-Linux-Support-for-Editor-$VERSION.pkg"

brew install tree

echo ""
echo "-------------Install completed---------------"
echo ""