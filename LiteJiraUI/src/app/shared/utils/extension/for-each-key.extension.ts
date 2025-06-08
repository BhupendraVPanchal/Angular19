
interface Object {
  forEachKey(callback: (key: string, value: any) => void): void;
}


Object.prototype.forEachKey = function (callback: (key: string, value: any) => void) {
  for (const key in this) {
    if (this.hasOwnProperty(key)) {
      callback(key, this[key]);
    }
  }
};
