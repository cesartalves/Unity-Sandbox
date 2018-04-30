#! /bin/sh

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



