
export { };

declare global {
  interface Array<T> {
    sortByKey(type: string, key: string, order?: 'asc' | 'desc'): T[];
  }
}

if (!Array.prototype.sortByKey) {
  Array.prototype.sortByKey = function <T>(type: string, key: string, order: 'asc' | 'desc' = 'asc'): T[] {
    const compareFn = (a: any, b: any): number => {
      let aValue;
      let bValue;

      // Handling null or undefined values
      if (a[key] == null || a[key] == undefined) {
        return order === 'asc' ? -1 : 1;
      }
      if (b[key] == null || b[key] == undefined) {
        return order === 'asc' ? 1 : -1;
      }

      if (type == 'number') {
        aValue = parseFloat(a[key]);
        bValue = parseFloat(b[key]);
      } else if (type === 'boolean') {
        //aValue = a[key] === 'true' ? true : false;
        //bValue = b[key] === 'true' ? true : false;
        aValue = new Number(a[key]);
        bValue = new Number(b[key]);
      }
      else if (type == 'date') {
        aValue = new Date(a[key]);
        bValue = new Date(b[key]);
      }
      else {
        aValue = a[key] ? a[key].toLowerCase() : null;
        bValue = b[key] ? b[key].toLowerCase() : null;
      } 
      

      if (order === 'asc') {
        if (aValue < bValue) return -1;
        if (aValue > bValue) return 1;
        return 0;
      } else {
        if (aValue > bValue) return -1;
        if (aValue < bValue) return 1;
        return 0;
      }
    };

    return this.slice().sort(compareFn);
  };
}
