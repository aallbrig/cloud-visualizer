class Webxr {
  arEnabled = false;
  vrEnabled = false;
  DoesNavigatorXRExists() {
    return window.navigator.xr ? true: false;
  }

  async DetermineWebXRStatus() {
    var arEnabled = this.DetermineARStatus();
    var vrEnabled = this.DetermineVRStatus();
    this.arEnabled = await arEnabled;
    this.vrEnabled = await vrEnabled;
  }

  toString() {
    return `AR enabled: ${this.arEnabled}\nVR enabled: ${this.vrEnabled}`;
  }

  async DetermineARStatus() {
    if (!this.DoesNavigatorXRExists()) {
      return false;
    }
    return await window.navigator.xr.isSessionSupported('immersive-ar');
  }

  async DetermineVRStatus() {
    if (!this.DoesNavigatorXRExists()) {
      return false;
    }
    return await window.navigator.xr.isSessionSupported('immersive-vr');
  }
}
