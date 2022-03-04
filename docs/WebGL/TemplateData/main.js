var canvas = document.querySelector("#unity-canvas");

var config = {
    dataUrl: "Build/WebGL.data.gz",
    frameworkUrl: "Build/WebGL.framework.js.gz",
    codeUrl: "Build/WebGL.wasm.gz",
    streamingAssetsUrl: "StreamingAssets",
    companyName: "Andrew Allbright",
    productName: "Cloud Vizualizer Client",
    productVersion: "0.0.7",
    devicePixelRatio: 1,
}

createUnityInstance(canvas, config);
