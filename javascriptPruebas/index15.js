//Split array into chunks 
const perChunk = 2 // items per chunk    

const inputArray = ['a','b','c','d','e']

const chunkArrayInGroups1 = (arr,size)  =>  {
    const result = inputArray.reduce((resultArray, item, index) => { 
        const chunkIndex = Math.floor(index/perChunk)
      
        if(!resultArray[chunkIndex]) {
          resultArray[chunkIndex] = [] // start a new chunk
        }
      
        resultArray[chunkIndex].push(item)
      
        return resultArray
      }, [])

      return result;
}

const chunkArrayInGroups2 = (arr, size) => {
    let myArray = [];
    for(let i = 0; i < arr.length; i += size) {
      myArray.push(arr.slice(i, i + size));
      console.log(i);
    }
    return myArray;
  }

//console.log(chunkArrayInGroups1(inputArray,perChunk));
console.log(chunkArrayInGroups2(inputArray,perChunk));

//  sample of slice
const slicedArray = inputArray.slice(3,5)
console.log(slicedArray)

