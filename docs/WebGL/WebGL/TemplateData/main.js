var canvas = document.querySelector("#unity-canvas");

var config = {
    dataUrl: "Build/WebGL.data",
    frameworkUrl: "Build/WebGL.framework.js",
    codeUrl: "Build/WebGL.wasm",
    streamingAssetsUrl: "StreamingAssets",
    companyName: "Andrew Allbright",
    productName: "Cloud Vizualizer Client",
    productVersion: "0.0.15",
    devicePixelRatio: 1,
}

createUnityInstance(canvas, config);
